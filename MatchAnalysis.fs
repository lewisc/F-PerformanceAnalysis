open System.Diagnostics

let timer = Stopwatch()

let genRandomNumbers count =
    let rnd = System.Random()
    List.init count (fun _ -> rnd.Next(11))

let tenThousandNums = genRandomNumbers 1000000
let mutable k = 0
let mutable j = []
let la = [|'a';'b';'c';'d';'e';'f';'g';'h';'j';'k';'l'|]
let ll = ['a';'b';'c';'d';'e';'f';'g';'h';'j';'k';'l']

let loop () : double = 
    do timer.Start()
    for i in [1..100000] do
         k <- k+1
    do timer.Stop()
    let retval = timer.Elapsed.Milliseconds
    do timer.Reset()
    double(retval)

let mapArray () : double =
    do timer.Start()
    let outres = List.map (fun x -> la.[x]) tenThousandNums
    do timer.Stop()
    let retval = timer.Elapsed.Milliseconds
    do timer.Reset()
    double(retval)

let forArray () : double = 
    do timer.Start()
    for i in tenThousandNums do
        j <- la.[i]::j
    do timer.Stop()
    let retval = timer.Elapsed.Milliseconds
    do timer.Reset()
    double(retval)

let for10 = List.init 10 (fun _ -> loop ())
let map10 = List.init 10 (fun _ -> mapArray ())
let forA10 = List.init 10 (fun _ -> forArray ())

printfn "%f for loop" (List.average for10)
printfn "%f map array" (List.average map10)
printfn "%f for array"  (List.average forA10)
