#!/bin/bash

dotnet restore

dotnet build -c Release Library/RadarIO/RadarIO.csproj
dotnet build -c Release Library/RadarIO.Maui/RadarIO.Maui.csproj
