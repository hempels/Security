﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Authentication.OAuth
{
    public class JsonSubKeyClaimMapper : JsonKeyClaimMapper
    {
        public JsonSubKeyClaimMapper(string claimType, string valueType, string jsonKey, string subKey)
            : base(claimType, valueType, jsonKey)
        {
            SubKey = subKey;
        }

        public string SubKey { get; }

        public override void Map(JObject data, ClaimsIdentity identity, string issuer)
        {
            var value = GetValue(data, JsonKey, SubKey);
            if (!string.IsNullOrEmpty(value))
            {
                identity.AddClaim(new Claim(ClaimType, value, ValueType, issuer));
            }
        }

        // Get the given subProperty from a property.
        private static string GetValue(JObject user, string propertyName, string subProperty)
        {
            if (user.TryGetValue(propertyName, out var value))
            {
                var subObject = JObject.Parse(value.ToString());
                if (subObject != null && subObject.TryGetValue(subProperty, out value))
                {
                    return value.ToString();
                }
            }
            return null;
        }
    }
}
