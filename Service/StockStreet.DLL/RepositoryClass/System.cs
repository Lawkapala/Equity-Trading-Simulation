using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockStreet.DLL.RepositoryClass
{
    public class System
    {
        StockStExternalEntities1 context = new StockStExternalEntities1();

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

                var x = (from n in context.BrokerSecurities
                         where n.securitySymbol.Equals(blocks[i].symbol)
                         select n).FirstOrDefault();
                //fetching market data for the symbol

                while(blocks[i].openQuantity > 0)
                {
                    int randomMaxOrderPerIteration = r1.Next(1, x.maxExeOrder);  //1-5000 -> 2222

                    if (randomMaxOrderPerIteration > blocks[i].openQuantity)
                        randomMaxOrderPerIteration = (int)blocks[i].openQuantity; //make sure that open quantity is not null

                    while(true) //it's looping only once
                    {
                        int randomOrderToExecute = r1.Next(1, randomMaxOrderPerIteration); // 1-2222 -> 1111

                        //entering data in trade execution

                        TradeExecution te = new TradeExecution();
                        te.symbol = blocks[i].symbol;
                        te.tradedQuantity = 0;
                        te.remainingOrderQuantity = randomMaxOrderPerIteration;
                        te.tradePrice = GetTradePrice(blocks[i]);
                        te.blockId = blocks[i].blockId;
                        te.status = "Execution";
                        te.timestamp = DateTime.Now;
                        //if (blocks[i].createTStamp == null)
                        //{
                        //    te.timestamp = DateTime.Now;
                        //}
                        //else
                        //{
                        //    te.timestamp = (DateTime)blocks[i].createTStamp;
                        //}

                        context.TradeExecutions.Add(te);

                        AutoAllocate(te, randomOrderToExecute);   //call auto alocate function with randomOrderToExecute as parameter

                        blocks[i].executedQuantity = randomOrderToExecute;

                        int randomContinueExecution = r1.Next(0, 100); // random value to determine whether to continue 
                        if(randomContinueExecution > (100 - x.perFullyExe))
                        {
                            te.tradedQuantity = randomMaxOrderPerIteration;
                            blocks[i].executedQuantity = randomMaxOrderPerIteration;
                            te.remainingOrderQuantity = 0;
                            te.status = "Fully Executed";
                            context.SaveChanges();
                            break;
                        }
                        else
                        {
                            te.status = "Partially Executed";
                            context.SaveChanges();
                            break;
                        }
                       

                    }

                    blocks[i].openQuantity -= randomMaxOrderPerIteration;
                   

                }

            }

         
        }

        public decimal GetTradePrice(ExternalBlock b)
        {
            Random r = new Random();
            decimal finalTradePrice;

            BrokerSecurity bs = new BrokerSecurity();
            var x = (from n in context.BrokerSecurities
                     where n.securitySymbol.Equals(b.symbol)
                     select n).FirstOrDefault();


            if (b.side == "Buy")
            {
                if (b.price == -1) //market price buy side
                {
                    //market price
                    int randomSpread = r.Next(0, x.maxSpread);
                    int randomSign = r.Next(0, 1);
                    // = r.NextDouble((bs.tradePrice * (1 - (randomSpread / 100) )), (bs.tradePrice * (randomSpread / 100 + 1)));
                    if (randomSign == 0)
                    {
                        finalTradePrice = bs.tradePrice * (1 - randomSpread / 100);
                    }
                    else
                    {
                        finalTradePrice = bs.tradePrice * (randomSpread / 100 + 1);
                    }

                   
                }
                else //limit price buy side
                {
                    //limit price
                    finalTradePrice = bs.tradePrice *(1 - (r.Next(0, x.maxSpread)/100));
                }

            }
            else //sell side
            {
                if(b.price == -1) //market price
                {
                    //sell side with limit price
                    finalTradePrice = bs.tradePrice * (1 + (r.Next(0, x.maxSpread) / 100));
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

        public void AutoAllocate(TradeExecution te, int randomOrderToExecute)
        {

            te.tradedQuantity += randomOrderToExecute;
            te.remainingOrderQuantity -= randomOrderToExecute;
            te.status = "Partially Executed";
            context.SaveChanges();
            
        }
    }
}
