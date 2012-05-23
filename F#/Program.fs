// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

// To use an explicit namespace, uncomment the following line. You will need
// to place all value and function definitions into a module.
// 
// namespace FSharp

/// This is a module to hold utility values and functions available in this file. 
module Utilities = 
     /// This is a sample value
     let sampleValue = 7 + 10

     /// This is a sample function
     let sampleFunction argument1 argument2 = 
         argument1 + argument2 

/// This is a sample class type. Delete it if it is not required.
type SampleClassType(argument1: int, argument2: int) = 

    /// Get the sum of the object arguments
    member x.Sum = argument1 + argument2

    /// Create an instance of the class type
    static member Create() = SampleClassType(3, 4)

open Utilities

// These are some sample values. Delete them if they are not required.
let sample1 = sampleFunction 4 12
let sample2 = SampleClassType(6, 11)
let sample3 = SampleClassType(7, 15)

System.Console.WriteLine("#1 = {0}, #2 = {0}, #3 = {0}", sample1, sample2.Sum, sample3.Sum)

// By default your program will evaluate the code above and then exit. If you would 
// like to use an explicit 'main' entry point, uncomment the code below
// 
// [<EntryPoint>]
// let main argv = 
//     // Do some work here then return an integer exit code
//     System.Console.WriteLine("done...")
//     0


