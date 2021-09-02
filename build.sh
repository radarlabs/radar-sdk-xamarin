nuget restore
find Samples -type f -name "*.csproj" | for proj in $(cat) ; do 
msbuild $proj /p:Configuration=Release
done
find Samples -type f -name "*.csproj" | for proj in $(cat) ; do 
msbuild $proj /p:Configuration=Debug
done
