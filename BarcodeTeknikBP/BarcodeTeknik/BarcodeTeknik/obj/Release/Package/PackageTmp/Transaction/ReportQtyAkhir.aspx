<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportQtyAkhir.aspx.cs" Inherits="BarcodeTeknik.Transaction.ReportQtyAkhir" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
   <link href=<%= Page.ResolveClientUrl("~/Content/bootstrap.min.css") %> rel="stylesheet" />
    <link href=<%= Page.ResolveClientUrl("~/fonts/font-awesome/css/font-awesome.min.css") %> rel="stylesheet" /> 
<%--    <link href=<%= Page.ResolveClientUrl("~/Resource/Js/bootstrap-datepicker/css/bootstrap-datepicker3.min.css") %> rel="stylesheet" />--%>
  
<%--    <link href=<%= Page.ResolveClientUrl("~/Resource/Js/jquery-ui/jquery-ui.min.css") %> rel="stylesheet" />--%>
 <%--   <link href=<%= Page.ResolveClientUrl("~/Resource/Js/sweetalert-master/sweetalert.css") %> rel="stylesheet" />--%>
  <link href=<%= Page.ResolveClientUrl("~/Content/dataTables.fixedHeader.css") %> rel="stylesheet" />
<%--   <link href=<%= Page.ResolveClientUrl("~/Resource/Js/sweetalert-master/facebook.css") %> rel="stylesheet" />--%>
  <link href=<%= Page.ResolveClientUrl("~/Content/selectize.bootstrap3.css") %> rel="stylesheet" />
    <link href=<%=Page.ResolveClientUrl("~/Content/plugins/dataTables/datatables.min.css")%> rel="stylesheet" />
    <link href=<%= Page.ResolveClientUrl("~/Content/custom.css") %> rel="stylesheet" />
      <style>
            .table.table-main.table-invoice tbody td.deleteborder {
                border-top:none !important;
                border-right: 1px solid #000 !important;
                  border-left: 1px solid #000 !important;
                  font-family:'Calibri';font-size: 13px;
                  border-bottom:none !important;
            }
              .table.table-main.table-invoice tbody td {
                border-top:none !important;
                border-right: 1px solid #000 !important;
                  border-left: 1px solid #000 !important;
                  font-family:'Calibri';font-size: 13px;
                    border-bottom:1px solid #000 !important;
            }
               
            .table-invoice tbody tr.summary {
                border: 1px solid #000 !important;
                   font-family:'Calibri';font-size: 13px;
            }
            
                .table-invoice tbody tr.summary td.summary-blank {
                    border-bottom: 1px solid transparent !important;
                    border-left-color: transparent !important;
                     
                     
                }
                .table-invoice tbody tr.summary td {
                    border: 1px solid #000 !important;
                    font-family:'Calibri';font-size: 13px;
                    
                }
                .table-invoice thead {
                    border-top: 1px solid!important;
                   
                }
                
                
                 .label-inverse {
            border-radius: 0;
            font-size: 130%;
            background: transparent;
            color: #555;
            border: .2em solid #555;
            font-weight: normal;
        }

            .label-inverse.label-On_Progress {
                color: #337AB7;
                border-color: #337AB7;
            }

            .label-inverse.label-Rejected {
                color: #D9534F;
                border-color: #D9534F;
            }

            .label-inverse.label-Completed {
                color: #5CB85C;
                border-color: #5CB85C;
            }

        .text-On_Progress {
            color: #337AB7 !important;
        }

        .text-Completed {
            color: #3D9970 !important;
        }

        .text-Rejected {
            color: #D9534F !important;
        }

        .invoice-info {
            margin-right: -10px;
            margin-left: -10px;
        }

            .invoice-info div {
                border-right: 1px solid #ddd;
                border-top: 1px solid #ddd;
                padding: 5px;
                text-align: center;
            }

                .invoice-info div:last-child {
                    border-right: none;
                }

            .invoice-info label {
                display: block;
            }

        .nav.nav-tabs {
            border-top: 1px solid #C1C1C1;
            border-bottom: 1px solid #C1C1C1;
            background: transparent;
        }

            .nav.nav-tabs > li {
                margin: 0;
            }

                .nav.nav-tabs > li > a {
                    border: none;
                    border: 5px solid transparent;
                    border-width: 5px 0;
                    border-radius: 0;
                    color: #414549;
                    margin: 0;
                }

                    .nav.nav-tabs > li > a:hover,
                    .nav.nav-tabs > li > a:focus,
                    .nav.nav-tabs > li.active > a {
                        background: #EBEBEB;
                        border-width: 5px 0;
                        border-color: #EBEBEB;
                        outline: none;
                    }

                .nav.nav-tabs > li.active > a {
                    border-bottom-color: #FF9500;
                }

                    .nav.nav-tabs > li.active > a:hover {
                        border-bottom-color: #FF9500;
                    }

                .nav.nav-tabs > li > a > .detail-count {
                    background: #ccc;
                    border-radius: 4px;
                    font-size: 12px;
                    padding: 2px 5px;
                    text-align: center;
                }

                .nav.nav-tabs > li.active > a > .detail-count {
                    background: #FF9500;
                    color: #fff;
                }

        .tab-pane {
            padding: 10px;
        }

        .flexbox {
            display: flex;
            flex-direction: row;
            flex-wrap: wrap;
        }

        .flex-item {
            max-width: 50%;
           
            min-width: 20%;
            min-height: 125px;
            position: relative;
           font-family:'Calibri';font-size: 13px;
        }
       
        .container {
          display: grid;
          grid-template-columns: 1fr 1fr 1fr; 
        }

        @media print {
            .box {
                border: none !important;
                margin-bottom: 5px;
            }

            .invoice-info,
            .invoice-info div {
                border: none !important;
                text-align:left;
            }

                .invoice-info label {
                    display: inline;
                    text-align: left;
                }

                    .invoice-info label span::after {
                        content: ":";
                    }


            .info-header h3 {
                margin: 5px;
            }

                 }
           </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin:0px 100px 0 100px;">
          <div class="row" style="margin-right:-60px;margin-left:-70px">
             <div class="container">
                    <div>
                          <h3 style="text-align:center;font-family:Calibri">Laporan Pemakaian Sparepart per WO</h3>   
                     </div>

                     <%-- <div style="text-align:right">
                         <asp:Image ID="image1" runat="server" Width="50px" height="50px"  />
                            
                       </div>--%>
               </div>
               
           <%-- <h4 style="text-align:left"> <%= oBKK.Companyname1  %></h4>--%>
            <div class="no-print">
                 <button runat="server" id="Button1" type="button" class="btn btn-black btn-md" onclick="window.print();">
                    <i class=" no-print">Print Hasil Pemakaian Barang</i>
                </button>
                <asp:Button ID="Button2" runat="server" class="btn btn-black btn-md" Text="Excel" OnClick="Button2_Click" />
             </div>
                   <br />  <br />
                <table  class="table-main table-invoice table table-condensed">
                    <thead >
                        <tr>
                            <th class="col-md-1 col-sm-1" style="text-align: center;">WO</th>
                            <th class="col-md-1 col-sm-2" style="text-align: center;">Part No</th>
                            <th class="col-md-1 col-sm-2" style="text-align: center;">Qty MR</th>
							  <th class="col-md-1 col-sm-2" style="text-align: center;">Tanggal</th>
                            <th class="col-md-1 col-sm-2" style="text-align: center;">Dibuat  Oleh</th>
                           
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView ID="lvBKKDetail" runat="server">
                            <LayoutTemplate>
                                <tr runat="server" id="ItemPlaceholder">
                                </tr>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td <%# ((string)Eval("Border")=="") ? "class=deleteborder":"" %>><%# ((Int64)Eval("No") == 1) ? (string)Eval("Wo_No")+"-"+(string)Eval("Description")+"<br/> Note:"+(string)Eval("CauseDescription"):"" %>
                                    </td>
                                    <td  style="text-align: center;"><%# Eval("PartNo") %></td>
                                    <td><%# Eval("QtyToMR") %></td>
									 <td style="text-align: center;"><%# Eval("StartDate") %></td>
                                    <td style="text-align: center;"><%# Eval("Dibuatoleh") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    <%--    <tr class="summary">                         
                            <td colspan="4" class="summary-blank">
                             
                                &nbsp;
                            </td>
                            <td>Jumlah
                            </td>
                            <td>
                                <asp:Literal ID="ltJumlah" runat="server"></asp:Literal>
                            </td>--%>
                       <%-- </tr>--%>
                    </tbody>
                 </table>
           
           <%--  <asp:ListView ID="lvAuthorization" runat="server">
                <LayoutTemplate>
                    <div class="flexbox">
                        <div runat="server" id="ItemPlaceholder"></div>
                        <div class="flex-item only-print">
                            <div class="text-center" style="font-weight: bold; margin-bottom: 10px;">
                                Diterima oleh,
                            </div>
                            <div class='text-center' style="margin-bottom: 15px;">
                                &nbsp;
                            </div>
                            <div class='text-center' style="position: absolute; bottom: 0; left: 0; right: 0;">
                            </div>
                        </div>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="flex-item">
                        <div class="text-center" style="font-weight: bold; margin-bottom: 10px;">
                            <%# Eval("FieldText") %>,
                        </div>
                        <asp:HiddenField runat="server" ID="hfApprTemplateLineID" Visible="false" Value='<%# Eval("ApprTemplateLineID") %>' />
                        <asp:HiddenField runat="server" ID="hfIsCurrApproval" Visible="false" Value='<%# Eval("IsCurrApproval") %>' />
                        <asp:ListView ID="lvAction" runat="server">
                            <LayoutTemplate>
                                <div class="text-center no-print" style="padding-top: .5em;">
                                    <button runat="server" id="ItemPlaceholder">
                                    </button>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <button type="button" onclick='showModal("<%# StringExtender.ToProper(Eval("ApprovalType")) %>",this)'
                                    class='btn btn-sm <%# IIf(Eval("ApprovalType") = "APPROVE", "btn-orange btn-glass", IIf(Eval("ApprovalType") = "REVOKE", "btn-red btn-glass", IIf(Eval("ApprovalType") = "REJECT", "btn-red-inverse", IIf(Eval("ApprovalType") = "CANCEL", "btn-red btn-glass", "")))) %>'>
                                    <i class='fa <%# IIf(Eval("ApprovalType") = "APPROVE", "fa-check", IIf(Eval("ApprovalType") = "REVOKE", "fa-undo", IIf(Eval("ApprovalType") = "REJECT", "fa-times", IIf(Eval("ApprovalType") = "CANCEL", "fa-undo", "")))) %>'></i>
                                    <%# StringExtender.ToProper(Eval("ApprovalType")) %>
                                </button>
                            </ItemTemplate>
                        </asp:ListView>
                                       
                        <div runat="server" visible='<%# IIf(Eval("ApprovalType") = "REVOKE" Or Eval("ApprovalType") = "REJECT", False, True) %>' class='text-center <%# IIf(Eval("IsCurrApproval"), "hide", "") %>' style="position: absolute; bottom: 0; left: 0; right: 0;">
                            <span class='text-muted <%# IIf(Eval("Name").ToString.Length <= 0, "hide", "") %>' style="display: block;">
                                <%# Eval("KeyID") %>
                            </span>
                            <%# Eval("Name") %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>--%>
       
                  </div>         
           
        </div> 
    </form>
</body>
</html>
