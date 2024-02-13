
# Copy index.html for GitHub pages
Copy-Item -Path 'wwwroot\index.html' -Destination 'wwwroot\404.html'

#Copy-Item -Path 'wwwroot\appsettings.GitHub.json' -Destination 'wwwroot\appsettings.json'

# Read the .csproj file and extract the version number
$xml = [xml](Get-Content "BlazorRadzenMls.csproj")
$version = $xml.Project.PropertyGroup.Version

# Write content to the file
$version | Out-File -FilePath "wwwroot\data\version.txt" -Encoding utf8
