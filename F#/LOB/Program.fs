// Learn more about F# at http://fsharp.net


(* order types *)
type orderType = 
    | BUY_MRKT = 0
    | BUY_LMT = 1
    | SELL_MRKT = 2
    | SELL_LMT = 3
    
(* order *)
type order( orderType : orderType, 
            timeRecievedGMT : System.DateTime, 
            tickerSymbol : string, 
            price : double,
            quantity : double
            ) =
    member public this.orderId = System.Guid.NewGuid() //stamp the order with a new GUID
    member public this.orderType = orderType
    member public this.timeRecievedGMT = timeRecievedGMT
    member public this.tickerSymbol = tickerSymbol
    member public this.quantity = quantity
    member public this.price = price

(* the entire LOB is a set of [0..n] limit order books for each symbol in the market *)
type lmtOrderBook( tickerSymbol : string, 
                    last : decimal, 
                    bid : decimal, 
                    ask : decimal,
                    askOrders : order [],
                    bidOrders : order []) =
    member public this.tickerSymbol = tickerSymbol
    member public this.last = last
    member public this.bid = bid
    member public this.ask = ask
    
    (*  these lists represent orders currently in the book
     *  the choice to use LinkedLists is based on O(n) inserts & removals
     *  random access won't be as necessary
     *)
    member public this.askOrders = askOrders
    member public this.bidOrders : bidOrders
 //   member public this.bidOrders = order LinkedList
    
    // add orders to this book, FIFO
  //  static member this.addOrder(order : order) = 
//        let AddLast : askOrders -> LinkedListNode<order>
    

   // let rec remove i l =
     //   match i, l with
      //  | 0, x::xs -> xs
     //   | i, x::xs -> x::remove (i - 1) xs
     //   | i, [] -> failwith "index out of range"



    // cancel orders in this book
  //  static member this.cancelOrder(order : order) =
        
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




