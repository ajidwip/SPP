<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportStokvsBuffer.aspx.cs" Inherits="BarcodeTeknik.Transaction.ReportStokvsBuffer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/plugins/dataTables/dataTablesStyles") %>
    <%:Scripts.Render("~/plugins/dataTables")%>

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Report Stok vs Buffer Stok</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="~/" runat="server">Home</a>
                </li>
                <li>
                    <a>Report</a>
                </li>
                <li class="active">
                    <strong>Stok vs Buffer</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox-title">
                    <h5>Report Stok vs Buffer Stok</h5>
                </div>
                <div class="ibox-content">
                    <asp:ListView ID="ListView1" runat="server" DataSourceID="sqldsData">
                        <LayoutTemplate>
                            <table class="table table-bordered table-condensed table-hover dt">
                                <thead>
                                    <tr>
                                        <th>Site</th>
                                        <th>Part No</th>
                                        <th>Nama Barang</th>
                                        <th>Acc. Group</th>
                                        <th>Unit Meas</th>
                                        <th>No Rak</th>
                                        <th>Current Qty</th>
                                        <th>Safety Stok</th>
                                        <th>Selisih</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="ItemPlaceHolder" runat="server"></tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Eval("Contract") %>
                                </td>
                                <td>
                                    <%# Eval("PartNo") %>
                                </td>
                                <td>
                                    <%# Eval("part_desc") %>
                                </td>
                                <td>
                                    <%# Eval("accounting_group") %>
                                </td>
                                <td>
                                    <%# Eval("unit_meas") %>
                                </td>
                                <td>
                                    <%# Eval("Rak_No") %>
                                </td>
                                <td>
                                    <%# Eval("Quantity") %>
                                </td>
                                <td>
                                    <%# Eval("safety_stock") %>
                                </td>
                                <td>
                                    <%# Eval("selisih") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>

    <%--<form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h2>Report Stok vs Buffer Stok</h2>
    </form>--%>

    <asp:SqlDataSource ID="sqldsData" runat="server" 
        ConnectionString="<%$ ConnectionStrings:sqlcon %>" 
        SelectCommand="if OBJECT_ID('tempdb..#INVENTORY_PART_PLANNING') is not null drop table #INVENTORY_PART_PLANNING
select * into #INVENTORY_PART_PLANNING
from openquery (ILPPROD, 'selecT contract,part_no,safety_stock FROM IFSAPP.INVENTORY_PART_PLANNING 
where contract = ''TKBP''' );
 select  i.[Contract], i.PartNo, i.[description] part_desc, i.accounting_group, i.unit_meas,i.Rak_No, i.Quantity, b.safety_stock,  isnull(i.Quantity,0)-isnull(b.safety_stock,0) selisih  
 from T_InventoryPart i 
	inner join #INVENTORY_PART_PLANNING b on i.PartNo=b.Part_NO and i.[Contract]=b.[Contract]
where isnull(i.Quantity,0)-isnull(b.safety_stock,0) &lt; 0 ">        
    </asp:SqlDataSource>

    <script>
         //Initiate DataTable

        var dt = $('.dt').DataTable({
            //ajax: "ajax/rte-11-serpihan-select?start_date=" + $('#start_date').val() + '&end_date=' + $('#end_date').val(),
            //columns: [
            //    {
            //        "className": 'fa fa-search-plus',
            //        "orderable": false,
            //        "data": null,
            //        "defaultContent": ''
            //    },
            //    { data: "ProcessDate" },
            //    { data: "PackingDate" },
            //    { data: "jumlah_ok_pcs" },
            //    { data: "reject_gembung_besar_pcs" },
            //    { data: "reject_gembung_sedikit_pcs" },
            //    { data: "reject_bocor_pcs" },
            //    { data: "WinLoginName" },
            //    { data: "remarks" },
            //    {
            //        data: null,
            //        render: function (data, type, row) {
            //            return '<span class="label label-warning btn-edit"><i class="fa fa-pencil-square-o"></i>&nbsp;Edit</span>'
            //        }
            //    }
            //],
            "columnDefs": [
                //{ "visible": false, "targets": [0, 5, 6] },
                //{ "targets": [1, 2], "type": "date" },
                //{ "className": "text-center", "targets": [0, 9] },
                { "className": "text-right", "targets": [6,7,8] }
            ],
            //language: {
            //    url: "../localization/Indonesian.json"
            //},
            //order: [[1, 'desc']],
            pageLength: 10,
            pagingType: "full_numbers",
            responsive: true,
            //stateSave: true,
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                { extend: 'copy' },
                { extend: 'csv' },
                { extend: 'excel', title: 'report_stok_vs_buffer' },
                { extend: 'pdf', title: 'report_stok_vs_buffer' },
                {
                    extend: 'print',
                    customize: function (win) {
                        $(win.document.body).addClass('white-bg');
                        $(win.document.body).css('font-size', '10px');

                        $(win.document.body).find('table')
                            .addClass('compact')
                            .css('font-size', 'inherit');
                    }
                }
            ]
        });
        //End of Initiate DataTable
    </script>

</asp:Content>
