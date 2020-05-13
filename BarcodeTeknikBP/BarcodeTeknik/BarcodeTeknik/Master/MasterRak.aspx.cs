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
using BarcodeTeknik;

namespace BarcodeTeknik.Master
{
    public partial class MasterRak : System.Web.UI.Page
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
            string a = "";

            List<rakgetset> Detail = new List<rakgetset>();

            string SelectString = "[SP_ViewMasterRak]'" + filter + "'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Detail = reader.Cast<IDataRecord>()
                         .Select(r => new rakgetset
                         {
                             RakNo = (string)r["Rak_No"],
                             Site=(string)r["Site"],
                             RakNoIFS=(string)r["Rak_NoIFS"]
                         }).ToList();
                }
            }

        
            con.Close();
           
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            a = js.Serialize(Detail);


            return a;
        }

        [WebMethod]
        public static string crud1(string RakNo,string Site,string RakNoIFS, string tipe)
        {
            if (tipe == "1")
            {
                try
                {
                    crud.columna = new string[] { "" + RakNo + "" };
                    crud.ExecuteSP("[SP_InsertRak]", crud.columna);

                    string a = "";

                    rakgetset DataObj = new rakgetset();
                    DataObj.RakNo = RakNo;
                   
                   

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    a = js.Serialize(DataObj);

                    con.Close();
                    returna = a;
                }
                catch (Exception ex)
                {
                    returna = "err$" + ex.Message.ToString();
                    crud.con.Close();
                }
            }
            else if (tipe == "2")
            {
               
            }
            else
            {
                try
                {
                    crud.columna = new string[] { "" + RakNo + "" };
                    crud.ExecuteSP("[SP_deleteRak]", crud.columna);
                    returna = RakNo;
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