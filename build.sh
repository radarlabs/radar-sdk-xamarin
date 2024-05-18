#!/bin/bash

dotnet restore

dotnet build -c Release Library/RadarIO/RadarIO.csproj
#msbuild $proj /p:Configuration=Release Library/RadarIO.Xamarin.iOS/RadarIO.Xamarin.iOS.csproj
#msbuild $proj /p:Configuration=Release Library/RadarIO.Xamarin.Android/RadarIO.Xamarin.Android.csproj
dotnet build -c Release Library/RadarIO.Maui/RadarIO.Maui.csproj
