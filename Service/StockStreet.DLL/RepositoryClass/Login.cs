using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockStreet.DLL.RepositoryClass
{
    public class Login
    {
        StockStInternalEntities ctx = new StockStInternalEntities();

        public UserDetail SignIn(string userName)
        {
            var u = (from n in ctx.UserDetails
                    where n.userName == userName
                    select n).FirstOrDefault();

            return u;
        }

        public void ChangeStatus(string userName,string accType)
        {
            var u = (from n in ctx.UserDetails
                     where n.userName == userName
                     select n).FirstOrDefault();
            switch (u.role)
            {
                case "PMT":
                    if(accType == "PM")
                    {
                        goto case "PM";
                    }
                    else if(accType == "ET")
                    {
                        goto case "ET";
                    }
                    else
                    {
                        goto default;
                    }
                case "PM":
                    u.pmLoginStatus = !u.pmLoginStatus;
                    break;
                case "ET":
                    u.etLoginStatus = !u.etLoginStatus;
                    break;
                default:
                    break;
            }
            ctx.SaveChanges();
        }

        public void SignOut(string userName,string accType)
        {
            ChangeStatus(userName, accType);

        }

        public void ChangePassword(string userName,string pwd)
        {
            var u = (from n in ctx.UserDetails
                     where n.userName == userName
                     select n).FirstOrDefault();

            u.password = pwd;
            ctx.SaveChanges();
        }

        public string ForgotPassword(string userName)
        {
            var u = (from n in ctx.UserDetails
                     where n.userName == userName
                     select n).FirstOrDefault();

            return u.securityAnswer;
        }

        public void SignUp( UserDetail usr)
        {
            ctx.UserDetails.Add(usr);
            ctx.SaveChanges();

        }

        public void FeedBack(Feedback f )
        {
            ctx.Feedbacks.Add(f);
            ctx.SaveChanges();

        }
    }
}
