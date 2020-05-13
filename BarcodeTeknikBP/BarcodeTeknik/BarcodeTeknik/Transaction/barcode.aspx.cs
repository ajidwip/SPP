using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using System.IO;
using System.Web.Services;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using BarcodeTeknik;
using System.Drawing.Imaging;


namespace BarcodeTeknik.Transaction
{
    public partial class barcode : System.Web.UI.Page
    {
        public string tmp = "";
        public static string[] kdbarang1 = new string[1000];
        int b = 0;
        int j = 0;
        static CRUD crud = new CRUD();
      
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                
            }
            // System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/tmp/toendra.JPG"));
        }

        [WebMethod]
        public static void GetkdBarang(string partNo,string site, string count)
        {

            // kdbarang1[int.Parse(count)] = kdbarang;
            crud.parama = new string[] { "partNo","Site" };
            crud.columna = new string[] { "" + partNo + "",""+ site + ""};
            crud.insert("tabletmpbarcode", crud.parama, crud.columna);

        }
        [WebMethod]
        public static string generatebarcodepdf()
        {
            LocalReport report = new LocalReport();
            report.ReportPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Transaction/BarcodePartNo.rdlc");


            DataTable dsOrders = GetSub("[SP_GenerateBarcode]");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dsOrders);
            report.DataSources.Add(datasource);


            Byte[] mybytes = report.Render("PDF");
            using (FileStream fs = File.Create(@"C:\inetpub\wwwroot\BarcodeTeknikBP\tmp\BArcodeBarang.pdf"))
            {
                fs.Write(mybytes, 0, mybytes.Length);
            }
            //using (FileStream fs = File.Create(@"D:\ProjectTes\BarcodeTeknik\BarcodeTeknik\tmp\BArcodeBarang.pdf"))
            //{
            //    fs.Write(mybytes, 0, mybytes.Length);
            //}
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Tabletmpbarcode", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();

            return System.Web.VirtualPathUtility.ToAbsolute("~/tmp/BArcodeBarang.pdf");

        }
        private static byte[] GenerateQrCode(string qrmsg)
        {
            string code = qrmsg;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData  qrCod1e = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCod1e);
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //imgBarCode.Height = 150;
            //imgBarCode.Width = 150;
            System.Drawing.Image img = qrCode.GetGraphic(20);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            img = new Bitmap(ms);
            ImageConverter converter = new ImageConverter();
            byte[] byteImage = (byte[])converter.ConvertTo(img, typeof(byte[]));
            return byteImage;
       
            
        }
        private static DataTable GetData(string query)
        {
            DataTable dt1 = new DataTable();
            string conString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;

                    sda.Fill(dt1);
                 
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                       dt1.Rows[i][0] = Convert.ToBase64String( GenerateQrCode((dt1.Rows[i][0].ToString())));
                    }
                }
            }
            return dt1;
        }
        [WebMethod]
        public static string generateqr()
        {
          
                LocalReport report = new LocalReport();
                report.ReportPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Transaction/qrcode.rdlc");


                DataTable dsOrders = GetData("[SP_GenerateQR]");
                ReportDataSource datasource = new ReportDataSource("DataSet1", dsOrders);
                report.DataSources.Add(datasource);


                Byte[] mybytes = report.Render("PDF");
                using (FileStream fs = File.Create(@"C:\inetpub\wwwroot\BarcodeTeknikBP\tmp\QR.pdf"))
                {
                    fs.Write(mybytes, 0, mybytes.Length);
                }
               
                //using (FileStream fs = File.Create(@"D:\ProjectTes\backup project\SPP\BarcodeTeknik\BarcodeTeknik\BarcodeTeknik\tmp\QR.pdf"))
                //{
                //    fs.Write(mybytes, 0, mybytes.Length);
                //}
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from Tabletmpbarcode", con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();

                return System.Web.VirtualPathUtility.ToAbsolute("~/tmp/QR.pdf");
           
        }
        [WebMethod]
        public static string generatebarcodesitepdf()
        {
            LocalReport report = new LocalReport();
            report.ReportPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Transaction/BarcodeSite.rdlc");


            DataTable dsOrders = GetSub("[SP_GenerateBarcodeSite]");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dsOrders);
            report.DataSources.Add(datasource);


            Byte[] mybytes = report.Render("PDF");
            using (FileStream fs = File.Create(@"C:\inetpub\wwwroot\BarcodeTeknikBP\tmp\BArcodeSIte.pdf"))
            {
                fs.Write(mybytes, 0, mybytes.Length);
            }
            //using (FileStream fs = File.Create(@"D:\ProjectTes\BarcodeTeknik\BarcodeTeknik\tmp\BArcodeBarang.pdf"))
            //{
            //    fs.Write(mybytes, 0, mybytes.Length);
            //}
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Tabletmpbarcode", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();

            return System.Web.VirtualPathUtility.ToAbsolute("~/tmp/BArcodeSIte.pdf");

        }
        private static DataTable GetSub(string query)
        {
            DataTable dt1 = new DataTable();
            string conString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;

                    sda.Fill(dt1);

                }
            }
            return dt1;
        }
    }
}