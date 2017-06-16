using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockStreet.DLL.RepositoryClass
{
    
    public class BrokerSystem
    {
        static StockStExternalEntities1 context = new StockStExternalEntities1();

        public void Execute()
        {
                       

            List<ExternalBlock> blocks = new List<ExternalBlock>();
            blocks = context.ExternalBlocks.ToList();
            
            CompareByTime sortBlock = new CompareByTime();
            blocks.Sort(sortBlock); 

            Random r1 = new Random();

            for (int i = 0; i < blocks.Count; i++) // 10000 ->  1-5000 -> 2222
            {
                // fetching max execution limit for a block

                // BrokerSecurity bs = new BrokerSecurity();
                

                while(blocks[i].openQuantity > 0 && !(blocks[i].blockStatus.Equals("Expired")))
                {
                    string symbol = blocks[i].symbol;

                    var x = (from n in context.BrokerSecurities
                             where n.securitySymbol.Equals(symbol)
                             select n).FirstOrDefault();
                    //fetching market data for the symbol

                    int randomMaxOrderPerIteration = r1.Next(1, x.maxExeOrder);  //1-5000 -> 2222

                    if (randomMaxOrderPerIteration > blocks[i].openQuantity)
                        randomMaxOrderPerIteration = (int)blocks[i].openQuantity; //make sure that open quantity is not null

                    //while(true) //it's looping only once
                    
                        int randomOrderToExecute = r1.Next(1, randomMaxOrderPerIteration); // 1-2222 -> 1111

                    //entering new  data in trade execution

                    decimal tempTradePrice = GetTradePrice(blocks[i]);

                    //check for price match
                    int priceMatchStatus = CheckPriceMatch(tempTradePrice, blocks[i]);

                    if (priceMatchStatus == -1)
                    {
                        if (i + 1 < blocks.Count)
                        {
                            i++;
                            continue;
                        }
                            
                        else
                            break;
                        
                    }
                        

                    TradeExecution te = new TradeExecution();
                        te.symbol = blocks[i].symbol;
                        te.tradedQuantity = 0;
                        te.remainingOrderQuantity = randomMaxOrderPerIteration;

                       
                         
                        te.tradePrice = tempTradePrice;
                        te.blockId = blocks[i].blockId;
                        te.status = "Execution";
                        te.timestamp = DateTime.Now;

                        blocks[i].updatedTStamp = te.timestamp;
                        x.tradePrice = tempTradePrice;

                        context.TradeExecutions.Add(te);
                        context.SaveChanges();

                        
                        


                        AutoAllocate(te, randomOrderToExecute);   //call auto alocate function with randomOrderToExecute as parameter


                        int randomContinueExecution = r1.Next(0, 100); // random value to determine whether to continue 
                        if(randomContinueExecution > (100 - x.perFullyExe))
                        {
                            TradeExecution Te = context.TradeExecutions.Find(te.tradeId);
                            Te.tradedQuantity = randomMaxOrderPerIteration;
                            Te.remainingOrderQuantity = 0;

                            blocks[i].executedQuantity += randomMaxOrderPerIteration;
                            
                            Te.remainingOrderQuantity = 0;
                            Te.status = "Fully";
                           // context.SaveChanges();
                            //break;
                        }
                        else
                        {
                            TradeExecution Te = context.TradeExecutions.Find(te.tradeId);
                            Te.status = "Partially";
                            
                            blocks[i].executedQuantity += randomOrderToExecute;

                           // context.SaveChanges();
                            //break;
                        }
                       
                    

                    blocks[i].openQuantity -= randomMaxOrderPerIteration;

                    if(blocks[i].executedQuantity == blocks[i].totalQuantity)
                    {
                        blocks[i].blockStatus = "Fully";
                    }
                    else
                    {
                        blocks[i].blockStatus = "Partially";

                    }
                    context.SaveChanges();
                }

                blocks[i].openQuantity = blocks[i].totalQuantity - blocks[i].executedQuantity;
                context.SaveChanges();
            }

            //return 1; //return status 
         
        }

        public static decimal GetTradePrice(ExternalBlock b)
        {
            Random r = new Random();
            decimal finalTradePrice;

            //BrokerSecurity bs = new BrokerSecurity();
            var bs = (from n in context.BrokerSecurities
                     where n.securitySymbol.Equals(b.symbol)
                     select n).FirstOrDefault();

            var x = (from n in context.BrokerSecurities
                     where n.securitySymbol.Equals(b.symbol)
                     select n).FirstOrDefault();

            if (b.side.Equals("Buy"))
            {
                if (b.orderType.Equals("Market")) //market price buy side
                {
                    //market price
                    int randomSpread = r.Next(0, bs.maxSpread);
                    int randomSign = r.Next(0, 1);
                    // = r.NextDouble((bs.tradePrice * (1 - (randomSpread / 100) )), (bs.tradePrice * (randomSpread / 100 + 1)));
                    if (randomSign == 0)
                    {
                        finalTradePrice = bs.tradePrice * ((100 - randomSpread) / 100);
                    }
                    else
                    {
                        finalTradePrice = bs.tradePrice * (randomSpread +100) / 100 ;
                    }

                   
                }
                else //limit price buy side
                {
                    //limit price
                    int ran = r.Next(0, bs.maxSpread) ;
                    finalTradePrice = (bs.tradePrice *(100 - ran))/100;
                    
                }

            }
            else //sell side
            {
                if(b.orderType.Equals("Limit")) //limit price
                {
                    //sell side with limit price
                    finalTradePrice = (bs.tradePrice * (100 + r.Next(0, bs.maxSpread)))/100;
                }
                else
                {
                    //sell side with market price 
                    //no change to trade price required
                    finalTradePrice = bs.tradePrice;
                }

            }

            return finalTradePrice;
        }

        public static void AutoAllocate(TradeExecution te, int randomOrderToExecute)
        {
            TradeExecution tempTe = context.TradeExecutions.Find(te.tradeId);
            tempTe.tradedQuantity += randomOrderToExecute;
            tempTe.remainingOrderQuantity -= randomOrderToExecute;
            tempTe.status = "Partially";
            context.SaveChanges();
            
        }

        public static int CheckPriceMatch(decimal tempTradePrice, ExternalBlock b)
        {
            if (b.symbol.Equals("Buy") && b.orderType.Equals("Limit")) //buy at limit price
            {
                if (tempTradePrice < b.price)
                    return 1;
                else
                    return -1;
            }
            else if (b.symbol.Equals("Sell") && b.orderType.Equals("Limit")) // sell at limit price
            {
                if (tempTradePrice > b.price)
                    return 1;
                else
                    return -1;
            }
            else                                     // market price buy or sell will always go through
                return 1;
        }

        public decimal UpdateBlockPrice(Block b)
        {
            var x = from n in context.TradeExecutions
                      where n.blockId == b.blockId
                      select n;

            List<TradeExecution> te = new List<TradeExecution>();
            te = x.ToList();

            decimal finalBlockPrice = 0;
            int totalTradeQuantity = 0;

            for (int i = 0; i < te.Count; i++)
            {
                decimal price = te[i].tradePrice;
                int quan = te[i].tradedQuantity;
                finalBlockPrice = ((price * quan) + (finalBlockPrice * totalTradeQuantity))/(totalTradeQuantity + quan);
                totalTradeQuantity += quan;

            }

            return finalBlockPrice;
        }

        public int ThreadExecute()
        {
            

            TimeSpan time = new TimeSpan(20, 28, 00); // hours, minutes, seconds
            DateTime todayWithTime = DateTime.Today + time;

            int count = 0;

            while(count < 10)
            {
                ThreadStart ts = new ThreadStart(Execute);
                Thread childThread = new Thread(ts);
                childThread.Start();

                Thread.Sleep(2000);
                //childThread.Suspend(500);
                    
                childThread.Abort();
                count++;

                

                if (DateTime.Now > todayWithTime )
                {
                    EOD();
                }
            }

            

            return 1;
        }

        public  void EOD()
        {
            var x = from n in context.ExternalBlocks
                    where n.blockStatus.Equals("Partial")
                    select n;

            List<ExternalBlock> blocks = new List<ExternalBlock>();
            blocks = x.ToList();

            foreach (var item in blocks)
            {
                ExternalBlock bb = context.ExternalBlocks.Find(item.blockId);
                
                item.blockStatus = "Expired";
                
            }
            context.SaveChanges();
        }
    }
}
