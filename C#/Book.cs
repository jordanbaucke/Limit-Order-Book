/*  
 *  Open Source Trader Project     
 *  
 *  Limit order book in C#
 *
 *  Maintains a set of bids and asks for traders in a single symbol (Security).
 *  
 *  Orders of opertaions is very important. As such all operation are blocking,
 *  and each operation must have completed to the books satisfaction before
 *  another operation may take place. This will ensure orderly operations, and 
 *  no unexpected prioritization. 
 *  
 *  The book is programmed to perform each of the following operations in the
 *  order set forth below. To analyze post-compiled behavior and run-time analysis 
 *  uncomment sections marked #VERBOS
 *  
 *  11/24/2011 - Added basic data-structures, and threading context to allow 
 *               for multithread contextual analysis
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LOB
{
    /// <summary>
    /// The book class only implements operations necessary to
    /// an individual symbol:
    /// 
    /// 1. Tracking: Last crossed price, and the current bid and ask.
    /// 2. Inserting, updating, or deleting orders.
    /// 
    /// Data types are intentionally kept as small as possible as this structure
    /// is ultimately intended to live in high-level cache to increase the speed
    /// of operations.
    /// 
    /// </summary>
    class Book
    {
        decimal Last { get; set; } //Last Sale
        decimal Bid { get; set; } //Highest bid
        decimal Ask { get; set; } //Lowest ask 
        
        HashSet<Dictionary<decimal, Int32>> ToBuy {get; set;}  //current orders in the book <bid,tradersId>
        HashSet<Dictionary<decimal, Int32>> ToSell { get; set; }

        /// <summary>
        /// Mutex blocking operation
        /// 
        /// Asks will not be added if they cross the current bid. Inserting a new ask order will lock the 
        /// bid until it is matched or inserted. 
        /// </summary>
        /// <param name="price"></param>
        /// <param name="seller"></param>
        /// <returns></returns>
        public Boolean insertAsk(decimal price,Int32 sellerId, Object threadContext)
        {
            Monitor.Enter(this.Bid); //lock the bid and bid orders
            Monitor.Enter(this.ToBuy);

            try
            {
                #region Verbos

                #endregion
                if (price == this.Bid)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                #region Verbos

                #endregion
                return false;
            }
            finally
            {
                Monitor.Exit(this.ToBuy); //unlock the bid array
            }
        }

        /// <summary>
        /// Mutex blocking operation
        /// 
        /// Bids will not be inserted if they cross the current ask. Inserting a new bid will lock
        /// the ask until it is matched or inserted. 
        /// 
        /// <param name="price"></param>
        /// <param name="bidderId"></param>
        /// <returns></returns>
        public Boolean insertBid(decimal price, Int32 bidderId, Object threadContext)
        {
            Monitor.Enter(this.ToSell); //lock the ask array
            try
            {
                if (price == this.Ask)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                Monitor.Exit(this.ToSell); //unlock the ask array
            }
        }

        public Boolean update()
        {
            return true;
        }

        public Boolean deleteBid()
        {
            return true;
        }

        public Boolean deleteAsk()
        {
            return true;
        }

        /// <summary>
        /// The default constructor creates or restores a new active LOB for the 
        /// symbol in question. Hashsets' are the arguements because that is the form
        /// they'll take once the book is active, but could arguably be any other
        /// data structure as creating or restoring a LOB is not a time sensative
        /// task.
        /// </summary>
        /// <param name="Last"></param>
        /// <param name="gtcBuy"></param>
        /// <param name="gtcSell"></param>
        public Book(Int16 Last, HashSet<Order> gtcBuy, HashSet<Order> gtcSell)
        {

        }
    }
}
