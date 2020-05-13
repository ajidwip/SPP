using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BarcodeTeknikMobile
{
   
    public class PemakaianBarangGetterSetter
    {
        public string PartNo = "";
        public string Qty = "";
        public string flag = "";
        public string firstscan = "";
        public string Contract = "";
        public string statuskirim = "";
        public string RakNO = "";
        public PemakaianBarangGetterSetter()
        {

        }
        public PemakaianBarangGetterSetter(string mPartNo, string pQty,string pFlag,string pfirstscan,string pContract,string pstatuskirim,string pRakNO)
        {
            this.PartNo = mPartNo;
            this.Qty = pQty;
            this.flag = pFlag;
            this.firstscan = pfirstscan;
            this.Contract = pContract;
            this.statuskirim = pstatuskirim;
            this.RakNO = pRakNO;

        }

        public string getPartNo() { return PartNo; }
        public void setPartNo(string PartNo) { this.PartNo = PartNo; }

        public string getQty() { return Qty; }
        public void setQty(string Qty) { this.Qty = Qty; }

        public string getflag() { return flag; }
        public void setflag(string flag) { this.flag = flag; }

        public string getfirstscan() { return firstscan; }
        public void setfirstscan(string firstscan) { this.firstscan = firstscan; }

        public string getContract() { return Contract; }
        public void setContract(string Contract) { this.Contract = Contract; }
        public string getstatuskirim() { return statuskirim; }
        public void setstatuskirim(string statuskirim) { this.statuskirim = statuskirim; }

        public string getRakNo() { return RakNO; }
        public void setRakNO(string RakNO) { this.RakNO = RakNO; }
    }

}