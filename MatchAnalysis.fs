open System.Diagnostics
open System

//Some variables of varying use
let mutable k = 0

type enumMaybe =
    | AValue
    | BValue
    | CValue
    | DValue

type notEnum =
    | AV of int
    | BV of int
    | CV of int
    | DV of int

let aEnum = [| AValue; BValue; CValue; DValue; AValue; BValue; CValue; DValue; AValue; BValue; CValue; DValue; |]

let aNotEnum = [| AV 1; BV 2; CV 3; DV 4; AV 5; BV 6; CV 7; DV 8; AV 9; BV 10; CV 11; DV 12|]

let la = [|'a';'b';'c';'d';'e';'f';'g';'h';'j';'k';'l'|]

let la2 = array2D [ ['1';'2';'3';'a';'b';'c';'2';'3';'4';'5';'6';'7';'8';'9'];
                    ['1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'];
                    ['1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'];
                    ['1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'];
                    ['1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'];
                    ['1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'];
                    ['1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'];
                    ['1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'];
                    ['1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'];
                    ['1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'];
                    ['1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9']]

let la2a = [| [|'1';'2';'3';'a';'b';'c';'2';'3';'4';'5';'6';'7';'8';'9'|];
              [|'1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'|];
              [|'1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'|];
              [|'1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'|];
              [|'1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'|];
              [|'1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'|];
              [|'1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'|];
              [|'1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'|];
              [|'1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'|];
              [|'1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'|];
              [|'1';'3';'4';'e';'f';'g';'2';'3';'4';'5';'6';'7';'8';'9'|]|]

let ll = ['a';'b';'c';'d';'e';'f';'g';'h';'j';'k';'l']


//Generate a list of random numbers of length count
let genRandomNumbers count =
    let rnd = System.Random()
    List.init count (fun _ -> rnd.Next(11))

let genRandArray count =
    let rnd = System.Random()
    Array.init count (fun _ -> rnd.Next(11))

//Standard deviation calculations
let stdDevList list =
    let avg = List.average list
    sqrt (List.fold (fun acc elem -> acc + (float elem - avg) ** 2.0 ) 0.0 list / float list.Length) 

let testRunner test name count =
    let eval = List.init count test
    do printfn "%.1f,%.1f,%.1f,%.1f %s" (List.average eval) (List.min eval) (List.max eval) (stdDevList eval) name

let testArray f (i:int) : double =
    GC.Collect()
    let timer = Stopwatch()
    let nums = genRandArray 100000000
    do timer.Start()
    do f nums
    do timer.Stop()
    let retval = timer.Elapsed.Milliseconds
    do timer.Reset()
    double(retval)

let test f (i:int) : double =
    GC.Collect()
    let timer = Stopwatch()
    let nums = genRandomNumbers 1000000
    do timer.Start()
    do f nums
    do timer.Stop()
    let retval = timer.Elapsed.Milliseconds
    do timer.Reset()
    double(retval)

let testWithBuild f b (i:int)  : double =
    GC.Collect()
    let timer = Stopwatch()
    let nums = List.map b (genRandomNumbers 1000000)
    do timer.Start()
    do f nums
    do timer.Stop()
    let retval = timer.Elapsed.Milliseconds
    do timer.Reset()
    double(retval)

//A map function that should be inlineable
let rec customMap f l acc =
    match l with
    | [] -> acc  
    | hd::tl -> customMap f tl ((f hd)::acc)
    
//A map function that should be inlineable
let inline custMapInline f l =
    let rec customMapacc f l acc =
        match l with
        | [] -> acc  
        | hd::tl -> customMap f tl ((f hd)::acc)
    customMapacc f l []

let rec notailMap f l =
    match l with
    | [] -> []
    | hd::tl -> (f hd)::(notailMap f tl)

let matchString s =
    match s with
    | 'a' -> 1
    | 'b' -> 2
    | 'c' -> 3
    | 'd' -> 4
    | _ -> 5

let matchEnum e =
    match e with
    | AValue -> 1
    | BValue -> 2
    | CValue -> 3
    | DValue -> 4

let matchNotEnum ne =
    match ne with
    | AV x -> x
    | BV x -> x
    | CV x -> x
    | DV x -> x


GC.Collect()


let mapCustomArrayTest = test (fun nums  -> customMap (fun x -> la.[x]) nums [] |> ignore)
let mapCustomInline= test (fun nums  -> custMapInline (fun x -> la.[x]) nums |> ignore)
let mapArrayTest = test (fun nums  -> List.map (fun x -> la.[x]) nums |> ignore)

let mapCustomListTest = test (fun nums  -> customMap (fun x -> ll.[x]) nums [] |> ignore)
let mapCustomInlineList= test (fun nums  -> custMapInline (fun x -> ll.[x]) nums |> ignore)

let mapCustomArrayTest2 = test (fun nums  -> customMap (fun x -> la2.[x,x]) nums [] |> ignore)
let mapCustomInline2 = test (fun nums  -> custMapInline (fun x -> la2.[x,x]) nums |> ignore)
let mapArrayTest2 = test (fun nums  -> List.map (fun x -> la2.[x,x]) nums |> ignore)

let mapArrayArrayTest2 = testArray (fun nums  -> Array.map (fun x -> la.[x]) nums |> ignore)


let mapCustomArrayTest2a = test (fun nums  -> customMap (fun x -> la2a.[x].[x]) nums [] |> ignore)
let mapCustomInline2a = test (fun nums  -> custMapInline (fun x -> la2a.[x].[x]) nums |> ignore)
let mapArrayTest2a = test (fun nums  -> List.map (fun x -> la2a.[x].[x]) nums |> ignore)

let forArrayTest = test (fun nums -> 
                            let mutable j = []
                            for i in nums do 
                                do j <- i::j
                            ())

let testString = testWithBuild (fun nums -> List.map matchString nums |> ignore) (fun i -> la.[i])
let testEnum = testWithBuild (fun nums -> List.map matchEnum nums |> ignore) (fun i -> aEnum.[i])
let testNotEnum = testWithBuild (fun nums -> List.map matchNotEnum nums |> ignore) (fun i -> aNotEnum.[i])
let testStringCust = testWithBuild (fun nums -> custMapInline matchString nums |> ignore) (fun i -> la.[i])

do testRunner testString "stringmatch_listmap" 10
do testRunner testString "stringmatch_custmap" 10
do testRunner testNotEnum "notenum_map" 10
do testRunner testEnum "enum_map" 10

do testRunner mapCustomArrayTest "custommap_array" 10
do testRunner mapCustomInlineList "inline_list" 10
do testRunner mapArrayTest "map_array" 10
do testRunner mapArrayArrayTest2 "mapArray_array" 10
(*
do testRunner mapCustomArrayTest2 "custommap_array2" 30
do testRunner mapArrayTest2 "map_array2" 30
do testRunner mapCustomInline2 "inline_list2" 30

do testRunner mapCustomArrayTest2a "custommap_array2a" 30
do testRunner mapArrayTest2a "map_array2a" 30
do testRunner mapCustomInline2a "inline_list2a" 30

do testRunner forArrayTest "for_array" 30
do testRunner mapCustomInline "inline_array" 30
do testRunner mapCustomListTest "customlist" 30
*)
