<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="FormRptQtyAkhir.aspx.cs" Inherits="BarcodeTeknik.Transaction.FormRptQtyAkhir" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <br />
    <asp:Label ID="Label1" runat="server" Text="Laporan Pemakaian Sparepart per WO" Font-Bold="True" 
        Font-Size="XX-Large"></asp:Label>
      <asp:UpdatePanel ID="up" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <asp:Table ID="Table1" runat="server">
              <asp:TableRow ID="TableRow2" runat="server">
               <asp:TableCell runat="server">
                         Bulan:
                      <asp:DropDownList ID="ddlbulan" runat="server" AutoPostBack="true" >
                      </asp:DropDownList>
               </asp:TableCell>
                  <asp:TableCell runat="server">
                        Tahun:
                      <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" >
                      </asp:DropDownList>
                  </asp:TableCell>
                   <asp:TableCell runat="server">
                        Site:
                      <asp:DropDownList ID="ddlsite" runat="server" AutoPostBack="true" >
                      </asp:DropDownList>
                  </asp:TableCell>
                  </asp:TableRow>
     
        </asp:Table>
              <asp:Button ID="btnview" OnClick="btnview_Click" CssClass="btn btn-w-m btn-primary" runat="server" Text="view" />
      </ContentTemplate>
    </asp:UpdatePanel>
    <br /><br /><br />
   <br /><br />
 
   <%-- <rsweb:reportviewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
        Width="650px">
            <LocalReport ReportPath="Transaction\ReportQtyAkhir.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:reportviewer>--%>
        <br />
        
   <%--     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:sqlcon %>" 
            
            
            SelectCommand="[SP_WO_CLOSE] @bulan,@tahun">

            <SelectParameters>
                 <asp:ControlParameter ControlID="ddlbulan" DefaultValue="&quot;&quot;" 
                    Name="bulan" PropertyName="Text" />
                <asp:ControlParameter ControlID="ddlTahun" DefaultValue="&quot;&quot;" 
                    Name="tahun" PropertyName="Text" />
            </SelectParameters>

        </asp:SqlDataSource>--%>
        
    <br />
    </form>
</asp:Content>