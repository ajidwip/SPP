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
    public partial class User : System.Web.UI.Page
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
            
            List<UserGetSet> Detail = new List<UserGetSet>();

            string SelectString = "[SP_ViewMasterUser]'" + filter+"'";

            con.Open();
            SqlCommand cmd = new SqlCommand(SelectString, con);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Detail = reader.Cast<IDataRecord>()
                         .Select(r => new UserGetSet
                         {
                             UserId = (string)r["UserId"],
                             UserName = (string)r["UserName"],
                             email = (string)r["Email"],
                             NIK = (string)r["NIK"],
                             GolonganUser = (string)r["Departemen"],
                             Active = (bool)r["IsActive"]
                         }).ToList();
                }
            }

           
            con.Close();
           
            JavaScriptSerializer js = new JavaScriptSerializer();
            string a = js.Serialize(Detail);
            return a;
        }

        [WebMethod]
        public static string crud1(string UserId, string UserName, string Email,string NIK,string GolonganUser,string Active, string tipe)
        {
            string Active1 = Active;
            if(Active1=="true")
            {
                Active1 = "1";
            }
            else
            {
                Active1 = "0";
            }
            if (tipe == "1")
            {
                try
                {
                    crud.columna = new string[] { "" + UserId + "", "" + UserName + "", "" + Email + "",""+NIK+"",""+ GolonganUser + "",""+ Active1 + "" };
                    crud.ExecuteSP("[SP_InsertUser]", crud.columna);

                    string a = "";

                    UserGetSet DataObj = new UserGetSet();
                    DataObj.UserId = UserId;
                    DataObj.UserName = UserName;
                    DataObj.email = Email;
                    DataObj.NIK = NIK;
                    DataObj.GolonganUser = GolonganUser;
                    DataObj.Active = bool.Parse(Active);
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
                crud.columna = new string[] { "" + UserId + "", "" + UserName + "", "" + Email + "",""+ GolonganUser + "",""+ NIK + "", "" + Active1 + "" };
                crud.ExecuteSP("[SP_UpdatetUser]", crud.columna);

                string a = "";

                UserGetSet DataObj = new UserGetSet();
                DataObj.UserId = UserId;
                DataObj.UserName = UserName;
                DataObj.email = Email;
                DataObj.NIK = NIK;
                DataObj.GolonganUser = GolonganUser;
                DataObj.Active = bool.Parse(Active);

                JavaScriptSerializer js = new JavaScriptSerializer();
                a = js.Serialize(DataObj);

                con.Close();
                returna = a;
            }
            else
            {
                try
                {
                    crud.columna = new string[] { "" + UserId + "",""+NIK+"" };
                    crud.ExecuteSP("[SP_deleteUser]", crud.columna);
                    returna = NIK;
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