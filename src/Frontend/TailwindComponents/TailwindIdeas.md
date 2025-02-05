## Ideas:

### Source generator:
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

Maybe a better idea: like an interceptor which will return a const string reference or something like that
focus("x", "y", "z") which will get intercepted and instead "focus:x focus:y focus:z" string will be returned
this way something like md(focus("x", "y")) can be intercepted too and get converted to md:focus:x md:focus:y
this is basically a macro-like thing which seems quite cool for tailwind
can also convert multiple things like md(focus(...), hover(...)) ...
important(...)
```