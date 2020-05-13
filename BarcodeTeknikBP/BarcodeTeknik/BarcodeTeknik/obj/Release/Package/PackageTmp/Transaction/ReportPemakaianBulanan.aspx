<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReportPemakaianBulanan.aspx.cs" Inherits="BarcodeTeknik.Transaction.ReportPemakaianBulanan" %>

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
        <div style="margin: 0px 100px 0 100px;">
            <div class="row" style="margin-right: -60px; margin-left: -70px">
                <div class="container">
                    <div>
                        <h3 style="text-align: center; font-family: Calibri">Laporan Pemakaian Sparepart per Bulan</h3>
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
                <br />
                <br />

                <div id="loading" class="center-div">
                    <img src="<%=ResolveUrl("~/tmp/loading.gif") %>" style="width: 100px; height: 100px;" />
                </div>

            </div>

        </div>


    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax(
                {
                    type: "POST",
                    url: "<%=ResolveUrl("~/Transaction/ReportPemakaianBulanan.aspx/BinData") %>",
                     datatype: "json",
                     contentType: "application/json; charset=utf-8",
                     data: "{'bulan':'<%Response.Write(bulan);%>','bulan2':'<%Response.Write(bulan2);%>','tahun':'<%Response.Write(tahun);%>','site':'<%Response.Write(site.ToString());%>'}",
                    beforeSend: function () {
                        $('#loading').show();
                    },
                    success: function (data) {

                        var items = JSON.parse(data.d);
                        var html = '<table class="table-main table-invoice table table-condensed"><thead>';
                        html += '<tr>';
                        var flag = 0;
                        $.each(items[0], function (index, value) {
                            html += '<th class="col-md-1 col-sm-1" style="text-align: center;">' + index + '</th>';
                        });
                        html += '</tr></thead>';
                        $.each(items, function (index, value) {
                            html += '<tr>';
                            $.each(value, function (index2, value2) {
                                html += '<td style="text-align:center;">' + value2 + '</td>';
                            });
                            html += '<tr>';
                        });
                        html += '</table>';
                        $('.row').append(html);



                    }, error: function (data) {
                        alert('error');
                    }, complete: function () {
                        $('#loading').hide();
                    }
                });
        });
    </script>
</body>
</html>
