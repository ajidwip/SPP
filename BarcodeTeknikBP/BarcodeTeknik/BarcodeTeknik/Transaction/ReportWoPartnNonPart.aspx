<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportWoPartnNonPart.aspx.cs" Inherits="BarcodeTeknik.Transaction.ReportWoPartnNonPart" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src='<%: ResolveClientUrl("~/Scripts/jquery-3.1.1.min.js") %>'></script>
    <script src='<%: ResolveClientUrl("~/Scripts/jquery-1.10.2.js") %>'></script>

    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../fonts/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../Content/dataTables.fixedHeader.css" rel="stylesheet" />
    <link href="../Content/selectize.bootstrap3.css" rel="stylesheet" />
    <link href="../Content/plugins/dataTables/datatables.min.css" rel="stylesheet" />
    <link href="../Content/custom.css" rel="stylesheet" />

    <style>
        .table.table-main.table-invoice tbody td.deleteborder {
            border-top: none !important;
            border-right: 1px solid #000 !important;
            border-left: 1px solid #000 !important;
            font-family: 'Calibri';
            font-size: 13px;
            border-bottom: none !important;
        }

        .table.table-main.table-invoice tbody td {
            border-top: none !important;
            border-right: 1px solid #000 !important;
            border-left: 1px solid #000 !important;
            font-family: 'Calibri';
            font-size: 13px;
            border-bottom: 1px solid #000 !important;
        }

        .table-invoice tbody tr.summary {
            border: 1px solid #000 !important;
            font-family: 'Calibri';
            font-size: 13px;
        }

            .table-invoice tbody tr.summary td.summary-blank {
                border-bottom: 1px solid transparent !important;
                border-left-color: transparent !important;
            }

            .table-invoice tbody tr.summary td {
                border: 1px solid #000 !important;
                font-family: 'Calibri';
                font-size: 13px;
            }

        .table-invoice thead {
            border-top: 1px solid !important;
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
            font-family: 'Calibri';
            font-size: 13px;
        }

        .container {
            display: grid;
            grid-template-columns: 1fr 1fr 1fr;
        }

        .center-div {
            position: absolute;
            margin: auto;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            width: 100px;
            height: 100px;
        }

        @media print {
            .box {
                border: none !important;
                margin-bottom: 5px;
            }

            .invoice-info,
            .invoice-info div {
                border: none !important;
                text-align: left;
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
                          <h3 style="text-align:center;font-family:Calibri">Wo Part dan Non Part</h3>   
                     </div>

               </div>
               
           
            <div class="no-print">
                 <button runat="server" id="Button1" type="button" class="btn btn-black btn-md" onclick="window.print();">
                    <i class=" no-print">Print</i>
                </button>
                <asp:Button ID="Button2" runat="server" class="btn btn-black btn-md" Text="Excel" OnClick="Button2_Click" />

				<br/>
				
             
             </div>
                   <br /> 
              <asp:Label ID="lbltanggal" runat="server"></asp:Label>
              <br />
                <table  class="table-main table-invoice table table-condensed">
                    <thead >
                        <tr>
						    <th class="col-md-1 col-sm-2" style="text-align: center;">Site</th>
							<th class="col-md-1 col-sm-2" style="text-align: center;">Type Wo</th>
						 	<th class="col-md-1 col-sm-2" style="text-align: center;">Wo No</th>
                            <th class="col-md-1 col-sm-1" style="text-align: center;">NIK</th>
                            <th class="col-md-1 col-sm-2" style="text-align: center;">Registration Date</th>
							<th class="col-md-1 col-sm-2" style="text-align: center;">Deskripsi Wo</th>
                            <th class="col-md-1 col-sm-2" style="text-align: center;">Deskripsi Penyebab</th>
                             <th class="col-md-1 col-sm-2" style="text-align: center;">Tindakan Perbaikan</th>
                              <th class="col-md-1 col-sm-2" style="text-align: center;">Jenis Pekerjaan</th>
                               <th class="col-md-1 col-sm-2" style="text-align: center;">Penyebab</th>
                              <th class="col-md-1 col-sm-2" style="text-align: center;">Object Rusak</th>
                            <th class="col-md-1 col-sm-2" style="text-align: center;">Tindakan</th>
                             <th class="col-md-1 col-sm-2" style="text-align: center;">Status</th>
                             <th class="col-md-1 col-sm-2" style="text-align: center;">Close Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView ID="lvBKKDetail" runat="server">
                            <LayoutTemplate>
                                <tr runat="server" id="ItemPlaceholder">
                                </tr>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="summary">
								 <td><%# Eval("Site") %></td>
								    <td><%# Eval("Type_Wo") %></td>
								 	<td><%# Eval("WO_NO") %></td>
                                    <td><%# Eval("NIK") %></td>
                                      <td><%# Eval("Reg_date") %></td>
                                    <td><%# Eval("Deskripsi_WO") %></td>
									<td><%# Eval("Deskripsi_Penyebab") %></td>
                                    <td><%# Eval("Tindakan_Perbaikan") %></td>
								 <td><%# Eval("JenisPekerjaan") %></td>
                                     <td><%# Eval("Penyebab") %></td>
                                      <td><%# Eval("ObjectRusak") %></td>
                                     <td><%# Eval("Tindakan") %></td>
                                      <td><%# Eval("status") %></td>
                                     <td><%# Eval("ClosingDate") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                  
                    </tbody>
                 </table>

       
                  </div>         
           
        </div> 
    </form>
    </body>
</html>