using ApplicationLog.Data;
using ApplicationLog.Models;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ApplicationLog.Extensions
{
    static class PostgreSQLBrokerExtension
    {
        public static async Task UsePostgreSQLBroker(this IApplicationBuilder builder)
        {
            var broker = new PostgreSQLBroker();
            broker.BrokerConfig();
        }
    }

    internal class PostgreSQLBroker
    {
        public async Task BrokerConfig()
        {
            await using var con = new NpgsqlConnection(Staticinfos.conString);
            await con.OpenAsync();
            con.Notification += LogNotificationHelper;
            await using (var cmd = new NpgsqlCommand())
            {
                cmd.CommandText = "LISTEN logchange;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
            }

            while (true)
            {
                // Waiting for Event
                con.Wait();
            }
        }

        private void LogNotificationHelper(object sender, NpgsqlNotificationEventArgs e)
        {
            //Deserialize Payload Data 
            var dataPayload = JsonConvert.DeserializeObject<tbllogInfo>(e.Payload);
            Console.WriteLine("{0}", dataPayload.table + " :: " + dataPayload.action + " :: " + dataPayload.data.logdetails);

            //Notify Client using SignalR
        }
    }

    public class tbllogInfo
    {
        public string table { get; set; }
        public string action { get; set; }
        public tbllog data { get; set; }
    }
}
