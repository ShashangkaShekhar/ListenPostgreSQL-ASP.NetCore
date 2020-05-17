using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace ApplicationLogConsole
{
    class Program
    {
        static tbllog logModel = null;
        static string baseUrl = "http://localhost:52985/";
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    logModel = Applog.getlogData();
                    if (logModel != null)
                    {
                        using (var client = new HttpClient())
                        {
                            string contentType = "application/json";
                            string PostUrl = baseUrl + "api/log/setLogData";
                            var content = new StringContent(JsonConvert.SerializeObject(logModel), Encoding.UTF8, contentType);
                            using (HttpResponseMessage response = client.PostAsync(PostUrl, content).Result)
                            {
                                if (response.IsSuccessStatusCode)
                                    Console.WriteLine("{0}", logModel.logdetails);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("{0}", "Error");
                }

                Thread.Sleep(1000);
            }
        }
    }

    public class tbllog
    {
        public int id { get; set; }
        public string logdetails { get; set; }
        public DateTime logdate { get; set; }
    }

    static class Applog
    {
        public static tbllog getlogData()
        {
            tbllog objdata = new tbllog()
            {
                logdetails = "Operation-Code~" + Utilities.RandomNumber(1, 1000),
                logdate = DateTime.Now
            };

            return objdata;
        }
    }

    static class Utilities
    {
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
