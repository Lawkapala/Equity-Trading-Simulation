using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockStreet.DLL.RepositoryClass
{
    public class CompareByTime : IComparer<ExternalBlock>
    {
               public int Compare(ExternalBlock x, ExternalBlock y)
        {
            return DateTime.Compare((DateTime)x.createTStamp, (DateTime)y.createTStamp);

        }
    }
}
