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
    public partial class ClosingWO : System.Web.UI.Page
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        static CRUD crud = new CRUD();
        static string returna = "";
        public string user = "",departemen="";
      static string filter1="";
        string[] usertmp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["filter"] != null)
                    {
                        filter1 = Request.QueryString["filter"];
                        if (Session["filter"] == null)
                        {
                            Session["filter"] = filter1;

                        }
                    }
                    user = Page.User.Identity.Name;
                    usertmp = user.Split('\\');
                    user = usertmp[1].ToString();
                    string SelectString = "select Departemen from T_MsUser where UserId='" + user + "'";
                    SqlCommand cmd = new SqlCommand(SelectString, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    departemen = dt.Rows[0][0].ToString();
                }
                catch(Exception ex)
                {
                    Response.Redirect("~/Default.aspx");
                }
               
            }
            
        }
        [WebMethod]
        public static string GetData(string filter)
        {
         
         
            List<WOGetSet> Detail = new List<WOGetSet>();

            string SelectString = "[SP_ViewWO]'" + filter + "'";

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
                             Contract= (string)r["Contract"],
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
        public static string GetDatadtl(string filter)
        {
        

            List<WOGetSet> Detail = new List<WOGetSet>();

            string SelectString = "[SP_ViewWO2]'" + filter + "'";

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
        public static string crud1(string WO, string DeskripsiWO, string DeskripsiPenyebab, string TindakanPerbaikan, string SolusiDetail,string Status,string Contract,string NIK ,string JenisPekerjaan, string Penyebab, string ObjectRusak, string Tindakan, string tipe)
        {
            if (tipe == "1")
            {
               
            }
            else if (tipe == "2")
            {
                crud.columna = new string[] { "" + WO + "", "" + DeskripsiPenyebab + "",""+ TindakanPerbaikan + "",""+ SolusiDetail + "",""+Contract+"",""+ NIK + "", "" + JenisPekerjaan + "", "" + Penyebab + "", "" + ObjectRusak + "" ,"" + Tindakan + "" };
                crud.ExecuteSP("[SP_UpdatetWO2]", crud.columna);

                string a = "";

                WOGetSet DataObj = new WOGetSet();
                DataObj.WO = WO;
                DataObj.DeskripsiWO = DeskripsiWO;
                DataObj.Penyebab = DeskripsiPenyebab;
                DataObj.Solusi = TindakanPerbaikan;
                DataObj.SolusiDetail = SolusiDetail;
                DataObj.Status = Status;
                DataObj.Contract = Contract;
                DataObj.NIK = NIK;
                DataObj.Class = JenisPekerjaan;
                DataObj.Cause = Penyebab;
                DataObj.Type = ObjectRusak;
                DataObj.PerfomedAction = Tindakan;
                JavaScriptSerializer js = new JavaScriptSerializer();
                a = js.Serialize(DataObj);

                con.Close();
                returna = a;
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
                        crud.ExecuteSP("[SP_CloseWO]", crud.columna);



                        WOGetSet DataObj = new WOGetSet();
                        DataObj.WO = WO;
                        DataObj.Contract = Contract;
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        a = js.Serialize(DataObj);
                        returna = a;
                    }
                    else
                    {
                        WOGetSet DataObj = new WOGetSet();
                        DataObj.WO = "";
                        DataObj.Contract = "";
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        a = js.Serialize(DataObj);
                        returna = a;
                    }
                }
                else
                {
                    WOGetSet DataObj = new WOGetSet();
                    DataObj.WO = "";
                    DataObj.Contract = "";
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    a = js.Serialize(DataObj);
                    returna = a;
                }
            }

            return returna;
        }
    }
}