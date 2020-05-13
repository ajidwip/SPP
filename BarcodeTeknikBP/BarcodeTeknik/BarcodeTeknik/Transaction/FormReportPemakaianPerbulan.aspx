<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Site.Master" CodeBehind="FormReportPemakaianPerbulan.aspx.cs" Inherits="BarcodeTeknik.Transaction.FormReportPemakaianPerbulan" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <br />
    <asp:Label ID="Label1" runat="server" Text="Laporan Pemakaian Sparepart per Bulan" Font-Bold="True" 
        Font-Size="XX-Large"></asp:Label>
      <asp:UpdatePanel ID="up" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <asp:Table ID="Table1" runat="server" Height="98px" Width="225px">
              <asp:TableRow ID="TableRow2" runat="server">
               <asp:TableCell runat="server">
                         Bulan:
                      <asp:DropDownList ID="ddlbulan" runat="server" AutoPostBack="true" >
                      </asp:DropDownList>
               </asp:TableCell>
                   <asp:TableCell runat="server">
                        sampai dengan 
                      <asp:DropDownList ID="ddlbulan2" runat="server" AutoPostBack="true" >
                      </asp:DropDownList>
               </asp:TableCell>
             
                  </asp:TableRow>
             <asp:TableRow ID="TableRow1" runat="server">
                      <asp:TableCell runat="server">
                          <br />
                        Tahun:
                      <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" >
                      </asp:DropDownList>
                  </asp:TableCell>
        </asp:TableRow>
               <asp:TableRow ID="TableRow3" runat="server">
                      <asp:TableCell runat="server">
                              <br />
                     Site:<asp:DropDownList ID="cmbsite"  runat="server"></asp:DropDownList>
                  </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
              <asp:Button ID="btnview" OnClick="btnview_Click" CssClass="btn btn-w-m btn-primary" runat="server" Text="view" />
      </ContentTemplate>
    </asp:UpdatePanel>
    <br /><br /><br />
   <br /><br />
 
        
    <br />
    </form>
</asp:Content>