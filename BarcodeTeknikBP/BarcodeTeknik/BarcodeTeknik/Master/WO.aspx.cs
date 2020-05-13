using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BarcodeTeknik.GetterSetter;

namespace BarcodeTeknik.Master
{
    public partial class WO : System.Web.UI.Page
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        static CRUD crud = new CRUD();
        static string returna = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetData(string filter)
        {

            List<wowithoutpartgetset> Detail = new List<wowithoutpartgetset>();

            string SelectString = "select * from T_MsWo where withoutpart is null and Status<>'C' and WO_NO<>'' and WO_NO not in(select distinct WO_NO from T_WO_Transaction) and WO_NO like '%'+'"+ filter + "'+'%'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Detail = reader.Cast<IDataRecord>()
                         .Select(r => new wowithoutpartgetset
                         {
                             WoNo = (string)r["WO_No"],
                             Deksripsi = (string)r["Description"],
                             Contract = (string)r["Contact"]
                            
                         }).ToList();
                }
            }


            con.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string a = js.Serialize(Detail);
            return a;
        }
        [WebMethod]
        public static string crud1(string WoNo, string Deskripsi, string Contract, string tipe)
        {
          
            if (tipe == "1")
            {
               
            }
            else if (tipe == "2")
            {
               
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update T_MsWO set Withoutpart=1 where WO_NO='"+ WoNo + "' and Contact='"+Contract+"'", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    
                    returna = WoNo;
                }
                catch (Exception ex)
                {
                    returna = "err$" + ex.Message.ToString();
                    crud.con.Close();
                }

            }

            return returna;
        }
    }
}