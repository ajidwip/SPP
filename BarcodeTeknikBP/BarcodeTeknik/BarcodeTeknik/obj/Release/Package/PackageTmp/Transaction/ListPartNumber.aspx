<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListPartNumber.aspx.cs" Inherits="BarcodeTeknik.Transaction.ListPartNumber" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <form runat="server" autocomplete="off">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>List Part Number</h2>
                <ol class="breadcrumb">
                    <li>
                        <a href="index.html">Home</a>
                    </li>
                    <li>
                        <a>Tables</a>
                    </li>
                    <li class="active">
                        <strong>Part Number</strong>
                    </li>
                </ol>
            </div>
            <div class="col-lg-2">
            </div>
        </div>
           <asp:UpdatePanel ID="up" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                                           
                            <div class="ibox-tools">
                               
                                <ul class="dropdown-menu dropdown-user">
                                    <li><a href="#">Config option 1</a>
                                    </li>
                                    <li><a href="#">Config option 2</a>
                                    </li>
                                </ul>
                                
                            </div>
                        </div>
                         <div class="ibox-content">
                             <div id="group">                             
                                    <table>
                                        <tr>
                                            <td>                                           
                                                <div class="pull-right mail-search">
                                                    <div class="input-group">
                                                        <table>
                                                            <tr>
                                                                <td> <asp:DropDownList ID="ddlstate" class="form-control" runat="server" DataTextField="state_code" DataValueField="doc_state_id" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"></asp:DropDownList></td>
                                                                <td> <input id="txtsearch" runat="server" class="form-control input-sm" placeholder="Part No/Deskripsi" type="text"> </td>
                                                                <td>   <div class="input-group-btn">
                                                           <%-- <button id="btnSearch" runat="server" class="btn btn-sm btn-primary" type="submit" >Search </button>--%>
                                                              <asp:Button id="btnSearch" runat="server" class="btn btn-sm btn-primary" Text="Search" ValidationGroup="search" OnClick="btnSearch_Click"/>
                                                                </div></td>
                                                            </tr>
                                                        </table>
                                                       
                                                       
                                                       
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                             </div>
                             <div class="">
                                 <table class="table-main table table-condensed datatables">
                                     <thead>
                                           <tr>
                                             <td></td>
                                             <th>Part No</th>
                                             <th>Part Description</th>
                                             <th>Site</th>
                                             <th>Site Description</th>
                                             <th>Status Barang</th>
                                             <th>Department</th>
                                             <th>Status Approve</th>
                                          </tr>
                                     </thead>
                                     <tbody>
                                         <asp:ListView ID="lvlistpart" runat="server">                                               
                                                <LayoutTemplate>
                                                      <tr runat="server" id="ItemPlaceholder">
                                                      </tr>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                         <td>
                                                             <div id="pnlteknikid" runat="server" visible='<%# Eval("VisTek") %>'>
                                                              <%--   <h3><a  href='<%# Page.ResolveClientUrl("~/Transaction/PartTeknik.aspx?partId="+ Eval("PartNo_Id") ) %>' title="View Detail" class="btn btn-xs btn-default">
                                                                 <i class="fa fa-arrow-circle-right" title="Lihat Detail"></i>    
                                                                 </a></h3>--%>
                                                                 <a href='<%# Page.ResolveClientUrl("~/Transaction/PartTeknik.aspx?partId="+ Eval("PartNo_Id") ) %>'><i class="fa fa-pencil"></i></a>
                                                           
                                                                 <asp:Label ID="lbid" runat="server" Text='<%# Eval("PartNo_Id") %>' CssClass="hidden"></asp:Label>
                                                             </div>
                                                              <div id="pnlumumid" runat="server" visible='<%# Eval("VisUmum") %>'>
                                                                 <a  href='<%# Page.ResolveClientUrl("~/Transaction/PartUmum.aspx?partId="+ Eval("PartNo_Id") ) %>' title="View Detail">
                                                                      <i class="fa fa-pencil"></i>
                                                                 </a>
                                                                     <asp:Label ID="lbid2" runat="server" Text='<%# Eval("PartNo_Id") %>' CssClass="hidden"></asp:Label>
                                                             </div>

                                                         </td>
                                                         <td>
                                                             <%# Eval("PartNo") %>
                                                           <%--  <div id="pnlteknik" runat="server" visible='<%# Eval("VisTek") %>'>
                                                                 <a  href='<%# Page.ResolveClientUrl("~/Transaction/PartTeknik.aspx?partId="+ Eval("PartNo_Id") ) %>' title="View Detail">
                                                                 <%# Eval("PartNo") %>
                                                             </div>
                                                              <div id="pnlumum" runat="server" visible='<%# Eval("VisUmum") %>'>
                                                                 <a  href='<%# Page.ResolveClientUrl("~/Transaction/PartUmum.aspx?partId="+ Eval("PartNo_Id") ) %>' title="View Detail">
                                                                 <%# Eval("PartNo") %>
                                                             </div>--%>
                                                         </td>
                                                         <td><%# Eval("description") %></td>
                                                         <td><%# Eval("Contract") %></td>
                                                         <td><%# Eval("[desc]") %></td>                                                      
                                                         <td><%# Eval("status_barang") %></td>
                                                         <td><%# Eval("Tipe") %></td>
                                                         <td><%# Eval("state_code") %></td>
                                                    </tr>
                                                           
                                               </ItemTemplate>
                                           </asp:ListView>
                                     </tbody>
                                 </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

       </ContentTemplate>           
       <Triggers>
           <asp:AsyncPostBackTrigger ControlID="ddlstate" EventName="SelectedIndexChanged" />
           
       </Triggers>    
     </asp:UpdatePanel>

 </form>
<script  type="text/javascript">
    function pageLoad() {
        $('.datatables').DataTable({
            pageLength: 15,
            responsive: true,
            bFilter: false,
            bInfo: true,
            searching: false,
            lengthChange: false,
            paging: true,
            ordering: true,
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [

            ],
            "order": [[0, "desc"]]

        });
    }
</script>
</asp:Content>
