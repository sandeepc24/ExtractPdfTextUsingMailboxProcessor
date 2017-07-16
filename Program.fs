// Learn more about F# at http://fsharp.org

open System
open Suave  
open Suave.Filters  
open Suave.Operators  
open Suave.Successful
open PdfUtils

let app =  
    choose
        [ GET >=> choose
            [ 
                path "/" >=> OK "Index"
                pathScan "/api/text/%s" getFileText
            ]
          POST >=> choose
            [ 
                path "/api/text" >=> extractText
            ] 
        ]

let initApp() =
    createAppDirs()
    processExistingPdfFiles()


[<EntryPoint>]
let main argv =
    initApp()

    let newConfig = 
        { defaultConfig with
            bindings = [ HttpBinding.createSimple HTTP "127.0.0.1" 8085 ]
        }
    startWebServer newConfig app

    0