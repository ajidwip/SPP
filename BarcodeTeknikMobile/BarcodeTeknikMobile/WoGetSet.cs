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
    public class WoGetSet
    {
        public string WONO = "";
        public string Title = "";
        public string flag = "";
        public string desc = "";
        public WoGetSet()
        {

        }
        public WoGetSet(string mWoNo,string pTitle,string pflag,string pdesc)
        {
            this.WONO = mWoNo;
            this.Title = pTitle;
            this.flag = pflag;
            this.desc = pdesc;
        }

        public string getWO() { return WONO; }
        public void setWO(string WONO) { this.WONO = WONO; }

        public string getTitle() { return Title; }
        public void setTitle(string Title) { this.Title = Title; }

        public string getflag() { return flag; }
        public void setflag(string flag) { this.flag = flag; }

        public string getdesc() { return desc; }
        public void setdesc(string desc) { this.desc = desc; }
    }

    
}