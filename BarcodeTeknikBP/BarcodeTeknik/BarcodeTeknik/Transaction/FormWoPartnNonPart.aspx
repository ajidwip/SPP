<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="FormWoPartnNonPart.aspx.cs" Inherits="BarcodeTeknik.Transaction.FormWoPartnNonPart" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <br />
    <asp:Label ID="Label1" runat="server" Text="Wo Part & Non Part" Font-Bold="True" 
        Font-Size="XX-Large"></asp:Label>
      <asp:UpdatePanel ID="up" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <asp:Table ID="Table1" runat="server" Height="98px" Width="225px">
              <asp:TableRow ID="TableRow1" runat="server">
                   <asp:TableCell runat="server">
                         Tanggal Registrasi:
                   <asp:TextBox ID="txtTanggalreg" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                    TargetControlID="txtTanggalreg" 
                                                                    Format="dd-MMM-yyyy" />
               </asp:TableCell>
                   <asp:TableCell runat="server">
                        sampai dengan 
                         <asp:TextBox ID="txtTanggalreg2" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                    TargetControlID="txtTanggalreg2" 
                                                                    Format="dd-MMM-yyyy" />
               </asp:TableCell>
                   </asp:TableRow>
           
               <asp:TableRow ID="TableRow3" runat="server">
                      <asp:TableCell runat="server">
                              <br />
                     Site:<asp:DropDownList ID="cmbsite" class="form-control"  runat="server"></asp:DropDownList>
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
