using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace InvoiceList
{
    class Data
    {
        static string cs = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
        public static void executesql(string sql)
        {
            using (SqlConnection con =  new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static int ExecuteDataReader(string sql)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int InvNo = Convert.ToInt16(dr["InvNo"]);
                return InvNo;
            }
        }
    }
}
