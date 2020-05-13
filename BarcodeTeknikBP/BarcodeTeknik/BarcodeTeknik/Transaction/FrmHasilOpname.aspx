<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="FrmHasilOpname.aspx.cs" Inherits="BarcodeTeknik.Transaction.FrmHasilOpname" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <br />
    <asp:Label ID="Label1" runat="server" Text="Hasil Opname" Font-Bold="True" 
        Font-Size="XX-Large"></asp:Label>
      <asp:UpdatePanel ID="up" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <asp:Table ID="Table1" runat="server">
              <asp:TableRow ID="TableRow2" runat="server">
               <asp:TableCell runat="server">
                         Tanggal Opname:<asp:TextBox ID="txtTanggal" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                    TargetControlID="txtTanggal" 
                                                                    Format="dd-MMM-yyyy" />
                           Site:<asp:DropDownList ID="cmbsite" class="form-control"  runat="server"></asp:DropDownList>
                         <br />
                   <asp:Button ID="btnview" OnClick="btnview_Click" CssClass="btn btn-w-m btn-primary" runat="server" Text="view" />
               </asp:TableCell>
              
                  </asp:TableRow>
     
        </asp:Table>
      </ContentTemplate>
    </asp:UpdatePanel>
    <br />
 <%--   <rsweb:reportviewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(Collection)"
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
        Width="650px">
            <LocalReport ReportPath="Transaction\hasilopname.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:reportviewer>--%>
        <br />
        
      <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:sqlcon %>" 
            
            
            SelectCommand="[SP_HasilOpname] @tanggal">

            <SelectParameters>
                 <asp:ControlParameter ControlID="txtTanggal" DefaultValue="&quot;&quot;" 
                    Name="tanggal" PropertyName="Text" />
             
            </SelectParameters>

        </asp:SqlDataSource>--%>
        
    <br />
    </form>
</asp:Content>