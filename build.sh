#!/usr/bin/env bash

koreBuildZip="https://github.com/aspnet/KoreBuild/archive/1.0.0.zip"
if [ ! -z $KOREBUILD_ZIP ]; then
    koreBuildZip=$KOREBUILD_ZIP
fi

# Call "sync" between "chmod" and execution to prevent "text file busy" error in Docker (aufs)
chmod +x "$DIR/run.sh"; sync
"$DIR/run.sh" default-build "$@"
