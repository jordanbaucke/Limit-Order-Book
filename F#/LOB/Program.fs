// Learn more about F# at http://fsharp.net

// order types
type orderType = 
    | BUY_MRKT = 0
    | BUY_LMT = 1
    | SELL_MRKT = 2
    | SELL_LMT = 3
    
// order
type order( orderType : orderType, 
            timeRecievedGMT : System.DateTime, 
            tickerSymbol : string, 
            quantity : double
            ) =
    member this.orderType = orderType
    member this.timeRecievedGMT = timeRecievedGMT
    member this.tickerSymbol = tickerSymbol
    member this.quantity = quantity

// the entire LOB is a set of [0..n] limit order books for each symbol in the market
type lmtOrderBook( tickerSymbol : string, 
                    last : float, 
                    bid : float, 
                    ask : float ) =
    member this.tickerSymbol = tickerSymbol
    member this.last = last
    member this.bid = bid
    member this.ask = ask

(* Async workflows sample (parallel CPU and I/O tasks) *)
 
(* A very naive prime number detector *)
let isPrime (n:int) =
   let bound = int (System.Math.Sqrt(float n))
   seq {2 .. bound} |> Seq.exists (fun x -> n % x = 0) |> not
 
(* We are using async workflows *)
let primeAsync n =
    async { return (n, isPrime n) }
 
(* Return primes between m and n using multiple threads *)  
let primes m n =
    seq {m .. n}
        |> Seq.map primeAsync
        |> Async.Parallel
        |> Async.RunSynchronously
        |> Array.filter snd
        |> Array.map fst
 
(* Run a test *)
primes 1000000 1015000
    |> Array.iter (printfn "%d")




