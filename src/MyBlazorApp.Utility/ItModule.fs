module MyBlazorApp.Utility.It

open System

let inline Data a = (^a : (member Data : ^b) a)
let inline Dispose (a : #IDisposable) = a.Dispose()
let inline Id a = (^a : (member Id : ^b) a)
let inline Item a = (^a : (member Item : ^b) a)
let inline Items a = (^a : (member Items : ^b) a)
let inline Name a = (^a : (member Name : ^b) a)
let inline Post b a = (^a : (member Post : ^b -> unit) (a, b))
let inline User a = (^a : (member User : ^b) a)
let inline Value a = (^a : (member Value : ^b) a)
let inline Values a = (^a : (member Values : ^b) a)
