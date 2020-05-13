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

namespace BarcodeTeknik.Transaction
{
    public partial class FinishWO : System.Web.UI.Page
    {
        static string returna = "";
        static CRUD crud = new CRUD();
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetData(string filter)
        {


            List<WOGetSet> Detail = new List<WOGetSet>();

            string SelectString = "[SP_ViewWOFinish]'" + filter + "'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Detail = reader.Cast<IDataRecord>()
                         .Select(r => new WOGetSet
                         {
                             WO = (string)r["WO_No"],
                             DeskripsiWO = (string)r["Description"],
                             Penyebab = (string)r["CauseDescription"],
                             Solusi = (string)r["WorkDone"],
                             SolusiDetail = (string)r["WorkDoneDetail"],
                             Status = (string)r["Status"],
                             Contract = (string)r["Contract"],
                             NIK = (string)r["CreatedBy"],
                             Class = (string)r["Class"],
                             Cause = (string)r["Cause"],
                             Type = (string)r["Type"],
                             PerfomedAction = (string)r["PerfomedAction"]
                         }).ToList();
                }
            }


            con.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string a = js.Serialize(Detail);

            return a;
        }
        [WebMethod]
        public static string crud1(string WO, string DeskripsiWO, string DeskripsiPenyebab, string TindakanPerbaikan, string SolusiDetail, string Status, string Contract, string NIK, string JenisPekerjaan, string Penyebab, string ObjectRusak, string Tindakan, string tipe)
        {
            if (tipe == "1")
            {

            }
            else if (tipe == "2")
            {

            }
            else
            {
                string SelectString = "[SP_cekstatus]'" + WO + "','" + Contract + "'";

                con.Open();
                SqlCommand cmd = new SqlCommand(SelectString, con);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);


                con.Close();
                string a = "";
                if (dt.Rows[0][0].ToString() == "Released")
                {
                    string SelectString2 = "[SP_cekqtyLK]'" + WO + "','" + Contract + "'";

                    con.Open();
                    SqlCommand cmd2 = new SqlCommand(SelectString2, con);
                    DataTable dt2 = new DataTable();
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    da2.Fill(dt2);


                    con.Close();
                    if (dt2.Rows[0][0].ToString() == "0")
                    {
                        crud.columna = new string[] { "" + WO + "", "" + Contract + "" };
                        crud.ExecuteSP("[SP_FinishWO]", crud.columna);
                        returna = WO;
                    }
                    else
                    {
                        returna = "err : Harap cek Qty LK IFS";
                    }
                }
                else
                {
                    returna = "err : Harap cek status WO IFS (Harus Released)";
                }
            }
            return returna;
        }
    }
}