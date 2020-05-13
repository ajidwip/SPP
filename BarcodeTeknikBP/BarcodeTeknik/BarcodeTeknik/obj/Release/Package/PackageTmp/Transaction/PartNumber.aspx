<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PartNumber.aspx.cs" Inherits="BarcodeTeknik.Transaction.PartNo" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <%--  <button id="toggleSpinners" class="btn btn-primary">Enable/disable spinners on below panels</button>--%>
    <form runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Kamus Part Number</h2>
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
                         <%--   <input id="generate" class="btn btn-w-m btn-danger" type="submit" value="Barcode" onclick="generate();" />
                              <input id="sync" class="btn btn-w-m btn-warning" type="submit" value="Refresh Qty" onclick="refreshqty();" />
                            <div id="loading1"></div>
                            <div id="loading" class="center-div" style="display:none">
                                <img src="<%=ResolveUrl("~/tmp/loading.gif") %>" style="width: 100px; height: 100px;" />
                            </div>--%>
                          
                            <div class="ibox-tools">
                               
                                <ul class="dropdown-menu dropdown-user">
                                    <li><a href="#">Config option 1</a>
                                    </li>
                                    <li><a href="#">Config option 2</a>
                                    </li>
                                </ul>
                                
                            </div>
                        </div>
                <%--             <div class="ibox" id="ibox2">                    
                        <div class="ibox-content">
                            <div class="sk-spinner sk-spinner-wave">
                                <div class="sk-rect1"></div>
                                <div class="sk-rect2"></div>
                                <div class="sk-rect3"></div>
                                <div class="sk-rect4"></div>
                                <div class="sk-rect5"></div>
                            </div>

                        </div>
                    </div>--%>
                     <div class="ibox" id="ibox2">    
                         <div class="ibox-content">
                              <div class="sk-spinner sk-spinner-wave">
                                <div class="sk-rect1"></div>
                                <div class="sk-rect2"></div>
                                <div class="sk-rect3"></div>
                                <div class="sk-rect4"></div>
                                <div class="sk-rect5"></div>
                            </div>
                             <div id="group">
                               <asp:Label ID="lbldept" runat="server" ClientIDMode="Static" CssClass="hidden"></asp:Label>
                                    <table>
                                        <tr>
                                            <td>                                           
                                                <div class="pull-right mail-search">
                                                    <div class="input-group">
                                                        <input id="txtsearch" runat="server" class="form-control input-sm" placeholder="Part No/Deskripsi" type="text">
                                                          <div class="input-group-btn">
                                                           <%-- <button id="btnSearch" runat="server" class="btn btn-sm btn-primary" type="submit" >Search </button>--%>
                                                              <asp:Button id="btnSearch" runat="server" class="btn btn-sm btn-primary" Text="Search" ValidationGroup="search" OnClick="btnSearch_Click"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>    <button id="btnshow" runat="server" type="button" class="btn btn-w-m btn-primary" onclick="showAddEditModal('add',this)" visible="false">
                                                    <i class="fa fa-plus"></i>
                                                    Create
                                                </button>

                                            </td>
                                        </tr>
                                    </table>
                                    <%--<br />--%>
                             </div>
                      
                                 <div class="">
                                 <table class="table-main table table-condensed datatables">
                                     <thead>
                                           <tr>
                                             <th>Part No</th>
                                             <th>Part Description</th>
                                             <th>Site</th>
                                             <th>Site Description</th>
                                             <th>Status</th>
                                          </tr>
                                     </thead>
                                     <tbody>
                                         <asp:ListView ID="lvviewforecast" runat="server">                                               
                                                <LayoutTemplate>
                                                      <tr runat="server" id="ItemPlaceholder">
                                                      </tr>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                         <td><%# Eval("PartNo") %></td>
                                                         <td><%# Eval("description") %></td>
                                                         <td><%# Eval("site") %></td>
                                                         <td><%# Eval("[Site Des]") %></td>                                                      
                                                         <td><%# Eval("Status") %></td>
                                                    </tr>
                                                           
                                               </ItemTemplate>
                                           </asp:ListView>
                                     </tbody>
                                     <tfoot>
                                         <tr>
                                             <th colspan="2">
                                               
                                          <%--      <asp:Button ID="btnview" CssClass="btn btn-w-m btn-primary" runat="server" Text="view" />--%>
                                             <%--    <asp:LinkButton ID="LbCreate" runat="server" class="btn btn-sm btn-black" ClientIDMode="Static" >Create</asp:LinkButton>--%>

                                             </th>
                                             <th></th>
                                             <th></th>
                                             <th></th>
                                         </tr>
                                     </tfoot>
                                    
                                 </table>

                            </div>
                        </div>
                      </div>




                    </div>
                </div>
            </div>

        </div>

     <div class="modal fade" tabindex="-1" role="dialog" id="mdlAddEdit" aria-hidden="true">
        <div class="modal-dialog modal-glass" role="document">
            <div class="modal-content">
                <%--<div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">
                            <i class="fa fa-close">close</i>
                        </span>
                    </button>
                    <h3 class="modal-title">
                        <span id="#mdlMode"></span> Part Number
                    </h3>
                </div>--%>
                <div class="modal-header">
                     <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                         <span aria-hidden="true">&times;</span>
                         <span class="sr-only">Close</span>

                     </button>
                       <h4 class="modal-title">Part Number</h4>
                                        
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfID" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hfMode" runat="server" ClientIDMode="Static" />
                    <div class="form-horizontal">
                        <div class="form-group form-group-sm">
                            <div class="col-md-6" id="aa" runat="server">
                                &nbsp;
                                <a href='<%: ResolveClientUrl("~/Transaction/PartTeknik.aspx") %>'  id="lbteknik" class="btn btn-warning dim pull-right"><i class="fa fa-plus"></i>Part Teknik</a>
                             </div>
                            <div class="col-md-6">
                                   <a href='<%: ResolveClientUrl("~/Transaction/PartUmum.aspx") %>' id="lbumum" class="btn btn-success dim"><i class="fa fa-plus"></i>Part Umum</a>
                            </div>
                        </div>
               

                    </div>
                </div>
                <%--<div class="modal-footer modal-footer-btn">      

                    <asp:Button id="btnAddEdit" runat="server" class="btn btn-orange btn-glass modal-btn-confirm" Text="Save"/> &raquo;
                </div>--%>
            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div><!-- /.modal -->

         </ContentTemplate>               
        </asp:UpdatePanel>
    </form>


      <script src='<%=Page.ResolveClientUrl("~/Content/plugins/metisMenu/jquery.metisMenu.js") %>'></script>

<script  type="text/javascript">
 
    function showAddEditModal(mode, obj) {
        //alert($(obj).attr('data-id'));
        if (mode == 'edit') {


        } else {
            $('#hfID').val(0);
            var dept;          
            
            dept = document.getElementById("lbldept").innerHTML;
        
            if (dept == '1' || dept == '6' || dept == '11') {

            }else if (dept == '19' || dept == '5' || dept == '29') {
                document.getElementById("lbumum").style.visibility = "hidden";
                                        
            } else {
                document.getElementById("lbteknik").style.visibility = "hidden";
            }

           

        //else {
            //    document.getElementById("lbumum").style.visibility = "hidden";    
            //}
          
            
        }

        $('#mdlAddEdit #mdlMode').text(mode);
        $('#mdlAddEdit').modal('show');
    }

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
  

    //function spin() {
    //    $('#ibox1').children('.ibox-content').toggleClass('sk-loading');
    //    $('#ibox2').children('.ibox-content').toggleClass('sk-loading');
    //}

    //$('#toggleSpinners').on('click', function () {

    //    $('#ibox1').children('.ibox-content').toggleClass('sk-loading');
    //    $('#ibox2').children('.ibox-content').toggleClass('sk-loading');

    //})
 </script>
</asp:Content>
