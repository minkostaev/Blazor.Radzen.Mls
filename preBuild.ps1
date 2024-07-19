# Copy index.html for GitHub pages is required
Copy-Item -Path 'wwwroot\index.html' -Destination 'wwwroot\404.html'

# Read the .csproj file and extract the version number
$xml = [xml](Get-Content "BlazorRadzenMls.csproj")
$version = $xml.Project.PropertyGroup.Version
# Write version to the file for versioning service
$version | Out-File -FilePath "wwwroot\data\version.txt" -Encoding utf8
