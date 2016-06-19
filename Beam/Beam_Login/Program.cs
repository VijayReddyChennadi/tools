using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Threading;
using System.Configuration;

namespace Beam_Login
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Method1
            //var request = (HttpWebRequest)WebRequest.Create("http://portal.actcorp.in/web/hyd/home/-/act/login");

            //var postData = "userName=vijayreddychennadi1%40gmail.com";
            //postData += "& userIP=10.113.176.255";
            //postData += "& pword:dibmesblbmb";
            //var data = Encoding.ASCII.GetBytes(postData);

            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;

            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}

            //var response = (HttpWebResponse)request.GetResponse();

            //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            #endregion

            #region Method2

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["userIP"] = ConfigurationManager.AppSettings["ip"];
                values["uname"] = ConfigurationManager.AppSettings["username"];
                if (args.Length > 0)
                {
                    values["pword"] = args[0].Trim();
                }
                else
                {
                    values["pword"] = ConfigurationManager.AppSettings["pwd"];
                }
                try
                {
                    var response = client.UploadValues("http://portal.actcorp.in/web/hyd/home/-/act/login", values);
                    var responseString = Encoding.Default.GetString(response);
                    Console.WriteLine("Success Login. \nThis window will close in 10 seconds, Press any key to close this immediately...");
                }
                catch (Exception ex)
                {
                    Console.Write("Failed. " + ex.Message);
                }
                Timer t = new Timer(timerC, null, 10000, Timeout.Infinite);
                Console.ReadKey();
            }
            #endregion
        }

        public static void timerC(object state)
        {
            Environment.Exit(0);
        }
    }
}
