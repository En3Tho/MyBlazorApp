while ($true)
{
    npx tailwindcss -i ./Styles/TailwindComponents.css -o ./wwwroot/css/TailwindComponents.css --watch
    Write-Host "Restarting garbage script"
}