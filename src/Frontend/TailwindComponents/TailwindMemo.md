## Commands:
```
npx tailwindcss -i ./Styles/TailwindComponents.css -o ./wwwroot/css/TailwindComponents.css --watch
```

## Docs:
```
https://tailwindcss.com/docs/installation
https://tailwindcss.com/docs/customizing-colors
https://play.tailwindcss.com/
https://github.com/tailwindlabs/tailwindcss/blob/master/stubs/defaultConfig.stub.js
```

## Svgs:
```
http://www.zondicons.com/icons.html
https://heroicons.com/
https://heropatterns.com/
```

## Components:
```
https://tailwindui.com/components
https://tailwindui.com/components/preview
https://flowbite.com/docs/components/alerts/
https://tailwind-elements.com/docs/standard/forms/checkbox/
```
## Tricks:

### Applying a style to all children
```
<nav class="[&>*]:cursor-pointer">
  <a>...</a>
  <a>...</a>
</nav>
```