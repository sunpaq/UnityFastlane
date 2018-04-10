#!/bin/bash

echo "we will build iOS use this bash script"

xcodebuild -scheme Unity-iPhone archive -archivePath Unity-iPhone.xcarchive CODE_SIGNING_REQUIRED=NO CODE_SIGN_IDENTITY=''
