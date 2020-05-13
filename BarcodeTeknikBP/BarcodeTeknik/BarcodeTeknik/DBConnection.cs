using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BarcodeTeknik
{
    public class DBConnection
    {
        public static object ExecScalar(string pQuery, bool IsProc = false, string pConnString = null)
        {
            DataTable dtResult;
            dtResult = ExecSelect(pQuery, IsProc: IsProc, pConnString: pConnString);
            if (dtResult == null || dtResult.Rows.Count <= 0)
            {
                return string.Empty;
            }
            else
            {
                return dtResult.Rows[0][0].ToString();
            }
        }

        public static object ExecScalar(string pQuery, string pParam, string[] pValue, bool IsProc = false, string pConnString = null)
        {
            if (pParam.Trim().Length == 0 | pValue.ToString().Trim().Length == 0)
            {
                return string.Empty;
            }
            else
            {
                return ExecScalar(pQuery, new string[] { pParam.ToString() }, new string[] { pValue.ToString() }, IsProc, pConnString);
            }
        }


        public static object ExecScalar(string pQuery, string[] pParam, string[] pValue, bool IsProc = false, string pConnString = null)
        {
            if (pParam.Length == pValue.Length && pParam.Length > 0)
            {
                List<SqlParameter> l = new List<SqlParameter>();

                //List<string> names = new List<string> { };

                for (int i = 0; i <= pParam.Length - 1; i++)
                {
                    l.Add(new SqlParameter(pParam[i].ToString(), pValue[i].ToString()));
                }

                DataTable dtResult;
                dtResult = ExecSelect(pQuery, l, IsProc, pConnString);
                //dtResult = ExecSelect(pQuery, IsProc: IsProc, pConnString: pConnString);
                if (dtResult == null || dtResult.Rows.Count <= 0)
                {
                    return string.Empty;
                }
                else
                {
                    return dtResult.Rows[0][0].ToString();
                }

            }
            else
            {
                throw new Exception("Jumlah parameter harus sama dengan jumlah value");
            }
        }

        //SINGLE PARAMETER
        public static DataTable ExecSelect(string pQuery, string pParam, string pValue, bool IsProc = false, string pConnString = null)
        {
            List<SqlParameter> l = new List<SqlParameter>();
            l.Add(new SqlParameter(pParam.ToString(), pValue.ToString()));
            return ExecSelect(pQuery, l, IsProc, pConnString);
        }
        //MULTIPLE PARAMETER
        public static DataTable ExecSelect(string pQuery, string[] pParam, string[] pValue, bool IsProc = false, string pConnString = null)
        {
            if (pParam.Length == pValue.Length && pParam.Length > 0)
            {
                List<SqlParameter> l = new List<SqlParameter>();

                for (int i = 0; i <= pParam.Length - 1; i++)
                {
                    l.Add(new SqlParameter(pParam[i].ToString(), pValue[i].ToString()));
                }
                return ExecSelect(pQuery, l, IsProc, pConnString);
            }
            else
            {
                throw new Exception("Jumlah parameter harus sama dengan jumlah value");
            }
        }


        //SINGLE, MULTIPLE OR NO PARAMETER
        public static DataTable ExecSelect(string pQuery, List<SqlParameter> pParam = null, bool IsProc = false, string pConnString = null)
        {
            DataTable _dt = new DataTable();
            string _conn_string;

            if (pConnString == null || pConnString.Length <= 0)
            {
                _conn_string = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString.ToString();
            }
            else
            {
                _conn_string = pConnString;
            }

            using (SqlConnection conn = new SqlConnection(_conn_string))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //cmd.CommandType = IIf(IsProc, CommandType.StoredProcedure, CommandType.Text);
                    cmd.CommandType = IsProc ? CommandType.StoredProcedure : CommandType.Text;
                    //cmd.CommandType = CommandType.Text;
                    cmd.CommandText = pQuery;
                    cmd.CommandTimeout = 0;
                    if (pParam != null && pParam.Count > 0)
                    {
                        foreach (SqlParameter p in pParam)
                        {
                            cmd.Parameters.Add(p);
                        }

                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        da.Fill(_dt);
                    }

                }
            }
            return _dt;
        }

        //MULTIPLE PARAMETER
        public static SqlDataReader execReader(string pQuery, string[] pParam, string[] pValue, bool IsProc = false, string pConnString = null)
        {
            if (pParam.Length == pValue.Length && pParam.Length > 0)
            {
                List<SqlParameter> l = new List<SqlParameter>();

                for (int i = 0; i <= pParam.Length - 1; i++)
                {
                    l.Add(new SqlParameter(pParam[i].ToString(), pValue[i].ToString()));
                }
                return execReader(pQuery, l, IsProc, pConnString);
            }
            else
            {
                throw new Exception("Jumlah parameter harus sama dengan jumlah value");
            }
        }

        public static SqlDataReader execReader(string strSQL, List<SqlParameter> pParam = null, bool IsProc = false, string pConnString = null)
        {
            SqlConnection pscnMain = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString.ToString());
            pscnMain.Open();

            SqlCommand scmMain = new SqlCommand(strSQL, pscnMain);
            scmMain.CommandType = IsProc ? CommandType.StoredProcedure : CommandType.Text;
            scmMain.CommandText = strSQL;
            if (pParam != null && pParam.Count > 0)
            {
                foreach (SqlParameter p in pParam)
                {
                    scmMain.Parameters.Add(p);
                }
            }
            SqlDataReader sdr = scmMain.ExecuteReader();
            return sdr;

        }

        #region "UPDATE INSERT DELETE"
        public static int ExecUID(string pQuery, string pParam, string pValue, bool IsProc = false, string pConnString = null)
        {
            List<SqlParameter> l = new List<SqlParameter>();
            l.Add(new SqlParameter(pParam.ToString(), pValue.ToString()));
            return ExecUID(pQuery, l, IsProc, pConnString);
        }

        public static int ExecUID(string pQuery, string[] pParam, string[] pValue, bool IsProc = false, string pConnString = null)
        {
            if (pParam.Length == pValue.Length && pParam.Length > 0)
            {
                List<SqlParameter> l = new List<SqlParameter>();

                for (int i = 0; i <= pParam.Length - 1; i++)
                {
                    l.Add(new SqlParameter(pParam[i].ToString(), pValue[i].ToString()));
                }
                return ExecUID(pQuery, l, IsProc, pConnString);
            }
            else
            {
                throw new Exception("Jumlah parameter harus sama dengan jumlah value");
            }

        }

        public static int ExecUID(string pQuery, List<SqlParameter> pParam = null, bool IsProc = false, string pConnString = null)
        {
            int _affected_row = 0;
            string _conn_string = "";
            if (pConnString == null || pConnString.Length <= 0)
            {
                _conn_string = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString.ToString();
            }
            else
            {
                _conn_string = pConnString;
            }

            using (SqlConnection conn = new SqlConnection(_conn_string))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //cmd.CommandType = IIf(IsProc, CommandType.StoredProcedure, CommandType.Text);
                    cmd.CommandType = IsProc ? CommandType.StoredProcedure : CommandType.Text;
                    cmd.CommandText = pQuery;

                    if (pParam != null && pParam.Count > 0)
                    {
                        foreach (SqlParameter p in pParam)
                        {
                            cmd.Parameters.Add(p);
                        }

                    }

                    conn.Open();
                    _affected_row = cmd.ExecuteNonQuery();

                }
            }
            return _affected_row;
        }

        #endregion
    }
}