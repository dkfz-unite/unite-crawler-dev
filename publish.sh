#!/bin/bash
os=$1
params="-c Release -r $os -o ./publish/$os -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -p:DebugType=None --self-contained"
#params="-c Release -r $os -o ./publish/$os -p:PublishTrimmed=true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -p:DebugType=None --self-contained"

rm -rf ./publish/$os

dotnet publish Unite.Crawler $params
dotnet publish Unite.Reader.Meta $params

mv ./publish/$os/Unite.Crawler ./publish/$os/crawler
mv ./publish/$os/Unite.Reader.Meta ./publish/$os/meta

chmod a+x ./publish/$os/*

cd ./publish/$os
gzip *
cd ../../