Command:
```
npx tailwindcss -i ./Styles/TailwindComponents.css -o ./wwwroot/css/TailwindComponents.css --watch
```
Docs:
```
https://tailwindcss.com/docs/installation
https://tailwindcss.com/docs/customizing-colors
https://play.tailwindcss.com/
https://github.com/tailwindlabs/tailwindcss/blob/master/stubs/defaultConfig.stub.js
```
Svgs:
```
http://www.zondicons.com/icons.html
https://heroicons.com/
https://heropatterns.com/
```
Components:
```
https://tailwindui.com/components   
https://tailwindui.com/components/preview
https://flowbite.com/docs/components/alerts/
https://tailwind-elements.com/docs/standard/forms/checkbox/
```
Tricks:
```
Applying a style to all children
<nav class="[&>*]:cursor-pointer">
  <a>...</a>
  <a>...</a>
</nav>
```
Ideas:

Source generator:
```
there can be a source generator that looks for references in razor files
and can process stuff like @focus@shadowLight for example to generate a file with a comment to enable tailwind jit.
hover:shadow-[inset_0_2px_0_hsla(0,_0%,_0%,_.25),_inset_0_-2px_0_hsla(0,_0%,_100%,_.25)]
Strings are always const and always reference TailwindUtilities class
Possible usages:
1. private const string Xxx = hover + shadowDeep;
2. private const string Xxx = $"{hover}{shadowDeep}";
3. @hover@shadowDeep
4. @component@nameof(MyComponent)px-4 // ?
small validation on const strings from html module
This then can be moved to nuget package and reused in other projects

[Tailwind]
// const string str = md:[...] ... ?

Include source generated documents via env var? ...\AppData\Local\Temp\SourceGeneratedDocuments

```