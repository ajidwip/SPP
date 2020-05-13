using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BarcodeTeknik
{
    public class CRUD
    {
       
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        public string[] parama;
        public string[] columna;
        public DataTable dt = new DataTable();
        public void ExecuteSP(string SPName, string[] column)
        {
            string param4 = "";
            string param5 = "";

            for (int i = 0; i < column.Length; i++)
            {
                param4 += "'" + column[i] + "'" + ",";
            }

            param5 = param4.Remove(param4.Length - 1);
            con.Open();
            SqlCommand cmd = new SqlCommand("exec "+SPName+" "+param5+"", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void insert(string TableName,string[] param,string[] column)
        {
            string param2 = "";
            string param3 = "";
            string param4 = "";
            string param5 = "";

            for (int i = 0; i < param.Length; i++)
            {
                param2 += param[i] + ",";
            }
            param3 = param2.Remove(param2.Length - 1);

            for (int i = 0; i < column.Length; i++)
            {
                param4 +="'"+ column[i] +"'"+",";
            }
            param5 = param4.Remove(param4.Length - 1);

           
         
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into " + TableName + " (" + param3 + ") values(" + param5 + ")", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
        
           
        }

        public void update(string TableName, string[] param, string[] column,string kondisi)
        {
            string param2 = "";
            string param3 = "";
      

            for (int i = 0; i < param.Length; i++)
            {
                param2 += param[i] + "='"+column[i]+"',";
            }
            param3 = param2.Remove(param2.Length - 1);
            
            con.Open();
            SqlCommand cmd = new SqlCommand("update " + TableName + " set " + param3 + " where "+kondisi+"", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();


        }
        public void delete(string TableName, string[] param, string[] column, string kondisi)
        {
          
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from " + TableName + " where " + kondisi + "", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();

        }

        public string  GetAkses(string User)
        {
            string akses = "";
            con.Open();
            SqlCommand cmd = new SqlCommand("select Desc_Access from T_MsUser inner join T_MsAksesUser on T_MsAksesUser.Id_Access=T_MsUser.Id_Access where UserId='" + User + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            akses = dt.Rows[0][0].ToString();
            con.Close();
            return akses;
        }
    }
}