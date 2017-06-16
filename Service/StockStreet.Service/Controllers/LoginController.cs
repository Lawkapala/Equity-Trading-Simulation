using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockStreet.DLL;
using StockStreet.DLL.RepositoryClass;
using System.Web.Http.Cors;
using StockStreet.DLL.EntityClass;
using System.Runtime.InteropServices;

namespace StockStreet.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {

        TokenBasedAuth t = new TokenBasedAuth();

        // GET: api/Login/5
        [HttpPut]
        [Route("api/SignIn/{userName}")]
        public LoginValidate SignIn(string userName,[FromBody]UserDetail usr )// userName, string pwd, [Optional] string accType)
        {
            Login db = new Login();
            LoginValidate lv = new LoginValidate();
            UserDetail u = db.SignIn(userName);
            if (u == null)
            {
                lv.loginStatus = -1; //User Does not Exist
                lv.accType = "";
                lv.token = "";
            }

            else
            {
                if (u.password == usr.password)
                {
                    if (u.role == "PM" && u.pmLoginStatus == false)
                    {
                        lv.loginStatus = 1; // login Successful
                        lv.accType = u.role;
                        lv.token = t.Encode(u.userName);
                        db.ChangeStatus(u.userName,"PM");
                    }
                    else if (u.role == "ET" && u.etLoginStatus == false)
                    {
                        lv.loginStatus = 1; // login Successful
                        lv.accType = u.role;
                        lv.token = t.Encode(u.userName);
                        db.ChangeStatus(u.userName, "ET");
                    }
                    else if (u.role == "BR")//broker ka check later
                    {
                        lv.loginStatus = 1; // login Successful
                        lv.accType = u.role;
                        lv.token = t.Encode(u.userName);
                    }
                    else if (u.role == "PMT" && (u.pmLoginStatus == false || u.etLoginStatus == false))
                    {
                        lv.loginStatus = 1; // login Successful
                        lv.accType = u.role;
                        db.ChangeStatus(u.userName, usr.role);
                        lv.token = t.Encode(u.userName);
                    }
                    else
                    {
                        lv.loginStatus = 2;//both trader and pm is already logged in at same time
                        lv.accType = u.role;
                        lv.token = "";
                    }
                }
                else
                {
                    lv.loginStatus = 0; // wrong Password
                    lv.accType = u.role;
                    lv.token = "";
                }


            }
            return lv;
        }


        [HttpPut]
        [Route("api/SignOut/{token}")]
        public void SignOut(string token, [FromBody]UserDetail usr )//string userName,string accType)
        {
            Login db = new Login();
            string userName = t.Decode(token);
            db.SignOut(userName, usr.role);
        }


        [HttpPut]
        [Route("api/ChangePassword/{token}")]
        public void ChangePassword(string token, [FromBody]UserDetail usr)//string userName,string pwd)
        {
            Login db = new Login();
            string userName = t.Decode(token);
            db.ChangePassword(userName, usr.password);
            db.SignOut(userName, usr.role);
        }


        [HttpPut]
        [Route("api/ForgotPassword/{userName}")]
        public LoginValidate ForgotPassword(string userName, [FromBody]UserDetail usr)//string userName,string answer)
        {
            Login db = new Login();
            LoginValidate lv = new LoginValidate();
            UserDetail u = db.SignIn(userName);
            if (u == null)
            {
                lv.loginStatus = -1; //User Does not Exist
                lv.accType = "";
                lv.token = "";
            }
            else
            {
                string ans = db.ForgotPassword(userName);
                if (usr.securityAnswer == ans) { 
                    lv.loginStatus = 1; // Correct Answer
                    lv.accType = u.role;
                    db.ChangeStatus(u.userName, usr.role);
                    lv.token = t.Encode(u.userName);
                }
                else{
                    lv.loginStatus = 0; // wrong Answer
                    lv.accType = u.role;
                    lv.token = "";
                }
            }

            return lv;

        }


        [HttpGet]
        [Route("api/GetDispName/{token}")]
        public Disp GetDisplayName(string token)
        {
            Login db = new Login();
            string userName = t.Decode(token);
            UserDetail u = db.SignIn(userName);
            Disp d = new Disp();
            d.DispName = u.displayName;
            d.accType = u.role;
            return d;
        }

        [HttpPost]
        [Route("api/SignUp")]
        public LoginValidate SignUp([FromBody]UserDetail usr)
        {
            Login db = new Login();
            LoginValidate lv = new LoginValidate();
            UserDetail u = db.SignIn(usr.userName);
            if (u != null)
            {
                lv.loginStatus = -1; //User already Exist
                lv.accType = "";
                lv.token = "";
            }
            else
            {
                db.SignUp(usr);
                lv.loginStatus = 1; //Signup Successful
                lv.accType = "";
                lv.token = "";
            }
            return lv;
        }

        [HttpPost]
        [Route("api/FeedBack")]
        public void FeedBack([FromBody]Feedback f)
        {
            Login db = new Login();
            db.FeedBack(f);
        }
            
        //[HttpPut]
        //[Route("api/ChangeStatus")]
        //public void ChangeStatus(string userName,string accType)
        //{
        //    Login db = new Login();
        //    db.ChangeStatus(userName, accType);
        //}

        /*
        
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
        */
    }
}
