using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockStreet.DLL.EntityClass
{
    public class TokenBasedAuth
    {
        public string Encode(string userName)
        {
            string token = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(userName));
            return token;
        }


        public string Decode(string token)
        {
            var base64EncodedUserName = System.Convert.FromBase64String(token);
            string userName = System.Text.Encoding.UTF8.GetString(base64EncodedUserName);
            return userName;
        }
    }
}
