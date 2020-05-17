using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using ApplicationLog.Models;

namespace ApplicationLog.Data
{
    public static class DataAccess
    {
        private static NpgsqlConnection con = new NpgsqlConnection(Staticinfos.conString);
        private static string sql = string.Empty;

        public static int saveData(tbllog data)
        {
            int result = 0;
            try
            {
                sql = Staticinfos.qruryInsert;

                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("logdetails", data.logdetails);
                    cmd.Parameters.AddWithValue("logdate", DateTime.Now);
                    IDataReader dr = cmd.ExecuteReader();
                    result = dr.RecordsAffected;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return result;
        }

        public static List<tbllog> getLogData()
        {

            List<tbllog> result = null;
            try
            {
                sql = Staticinfos.qrurySelect;

                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    result = Utilities.DataReaderMapToList<tbllog>(cmd.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }

            return result;
        }
    }
}