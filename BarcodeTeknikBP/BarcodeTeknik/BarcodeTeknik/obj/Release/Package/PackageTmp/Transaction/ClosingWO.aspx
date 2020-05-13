<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ClosingWO.aspx.cs" Inherits="BarcodeTeknik.Transaction.ClosingWO" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/plugins/dataTables/dataTablesStyles") %>
    <%:Scripts.Render("~/plugins/dataTables")%>
   
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Close Wo</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li>
                    <a>Tables</a>
                </li>
                <li class="active">
                    <strong>Close Wo</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>
    <%--angular app--%>
    <div ng-app="myApp">
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">

                            <%--input form controller--%>
                            <div ng-controller="MyControllerForm1" id="FormInput" style="display: none">
                                <%--input form--%>
                                <table>
                                    <tr>

                                        <td>&nbsp;WO
                                        </td>
                                        <td>
                                            <input type="text" id="WO" style="margin-top: 5px" disabled class="form-control" />
                                        </td>
                                        <td>&nbsp;&nbsp;Jenis Pekerjaan
                                              
                                                   
                                        </td>

                                        <td>
                                            <div class="form-inline">
                                              <select id="JenisPekerjaan" class="form-control"></select>
                                                 <%--   <input type="submit" value="..." class="btn btn-sm btn-primary" onclick="showpopupForm('JenisPekerjaan', 'Description', '', '<%=ResolveUrl("~/Default.aspx/GetClass") %>')" />--%>
                                               
                                              <%--  <input type="text" id="JenisPekerjaan" class="form-control" style="margin-top: 5px" readonly/>--%>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>&nbsp;Deskripsi WO
                                        </td>
                                        <td>
                                            <input type="text" id="DeskripsiWO" style="margin-top: 5px" disabled class="form-control" />
                                        </td>
                                        <td>&nbsp;&nbsp;Penyebab
                                        </td>
                                        <td>
                                              <div class="form-inline">
                                                <select id="Penyebab" class="form-control"></select>
                                                 <%--   <input type="submit" value="..." class="btn btn-sm btn-primary" onclick="showpopupForm('Penyebab', 'Description', '', '<%=ResolveUrl("~/Default.aspx/GetCause") %>')" />
                                             
                                                <input type="text" id="Penyebab" class="form-control" style="margin-top: 5px" readonly/>--%>
                                             </div>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>&nbsp;Contract
                                        </td>
                                        <td>
                                            <input type="text" id="Contract" style="margin-top: 5px" disabled class="form-control" />
                                        </td>
                                        <td>&nbsp;&nbsp;Object Rusak
                                              
                                        </td>
                                        <td>
                                            <div class="form-inline">
                                               <select id="ObjectRusak" class="form-control"></select>
                                                 <%--   <input type="submit" value="..." class="btn btn-sm btn-primary" onclick="showpopupForm('ObjectRusak', 'Description', '', '<%=ResolveUrl("~/Default.aspx/GetType") %>')" />
                                             
                                                <input type="text" id="ObjectRusak" class="form-control" style="margin-top: 5px" readonly/>--%>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>&nbsp;NIK
                                        </td>
                                        <td>
                                            <input oninput="maxLengthCheck(this)" type = "number" maxlength = "5" id="NIK"  style="margin-top: 5px" class="form-control" />
                                        </td>
                                        <td>&nbsp;&nbsp;Tindakan
                                                
                                        </td>
                                        <td>
                                             <div class="form-inline">
                                                 <select id="Tindakan" class="form-control"></select>
                                                    <%--<input type="submit" value="..." class="btn btn-sm btn-primary" onclick="showpopupForm('Tindakan', 'Description', '', '<%=ResolveUrl("~/Default.aspx/GetPerformedActionId") %>')" />
                                              
                                                <input type="text" id="Tindakan" class="form-control" style="margin-top: 5px" readonly/>--%>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>&nbsp;&nbsp;Deskripsi Penyebab
                                        </td>
                                        <td>
                                            <input type="text" id="DeskripsiPenyebab" style="margin-top: 5px" class="form-control" />
                                        </td>

                                    </tr>
                                    <tr>

                                        <td>&nbsp;&nbsp;Tindakan Perbaikan
                                        </td>
                                        <td>
                                            <input type="text" id="TindakanPerbaikan" maxlength="50" style="margin-top: 5px" class="form-control" />
                                            *Max 50 karakter
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td>
                                            <input type="text" id="SolusiDetail" style="margin-top: 5px" class="form-control" />
                                        </td>
                                        <td>
                                            <input type="text" id="Status" style="margin-top: 5px; display: none" class="form-control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="margin-top: 10px">
                                            <input type="submit" value="update" id="update" style="margin-top: 10px; display: none" class="btn btn-w-m btn-warning" ng-click="updateform1('Pastikan untuk mengembalikan lebih barang sebelum melakukan transaksi ini!')" />
                                            <input type="submit" value="save" id="Save" style="margin-top: 10px; display: none" class="btn btn-w-m btn-warning" ng-click="saveForm()" />
                                        </td>
                                        <td>
                                            <input type="submit" value="Cancel" id="Cancel" style="margin-top: 10px; margin-left: 10px; display: none" class="btn btn-w-m btn-warning" />
                                        </td>
                                    </tr>
                                </table>
                                <%--end input form--%>
                            </div>
                            <%--end input form controller--%>

                            <div class="ibox-tools">

                                <ul class="dropdown-menu dropdown-user">
                                    <li><a href="#">Config option 1</a>
                                    </li>
                                    <li><a href="#">Config option 2</a>
                                    </li>
                                </ul>

                            </div>
                        </div>
                        <%--table controller--%>
                        <div class="ibox-content" ng-controller="MyController">
                            WO Open
                                <table>
                                    <tr>
                                        <td>
                                            <div class="pull-right mail-search">
                                                <div class="input-group">
                                                    <input id="txtsearch" class="form-control input-sm" placeholder="WO" type="text">
                                                    <div class="input-group-btn">
                                                        <button class="btn btn-sm btn-primary" type="submit" ng-click="search1();">Search </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <div id="loading" class="center-div" style="display:none">
                    <img src="<%=ResolveUrl("~/tmp/loading.gif") %>" style="width: 100px; height: 100px;" />
                </div>
                            <div class="table-responsive">
                                <%--show data  in table--%>
                                <table id="tablemenu" datatable="ng" class="table table-striped table-bordered table-hover dataTables-example">
                                    <thead>
                                        <tr>
                                            <th>WO</th>
                                            <th>Deskripsi WO</th>
                                            <th>Deskripsi Penyebab</th>
                                            <th>Tindakan Perbaikan</th>
                                            <th>Status</th>
                                            <th>Contract</th>
                                            <th>Jenis Pekerjaan</th>
                                            <th>Penyebab</th>
                                            <th>Object Rusak</th>
                                            <th>Tindakan</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="{{$index}}" ng-repeat="x in master"  my-post-repeat-directive class="gradeX">
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.WO}}</label>
                                                <input type="text" id="WO_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.WO" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.DeskripsiWO}}</label>
                                                <input type="text" id="DeskripsiWO_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.DeskripsiWO" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Penyebab}}</label>
                                                <input type="text" id="DeskripsiPenyebab_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Penyebab" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Solusi}}</label>
                                                <input type="text" id="TindakanPerbaikan_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Solusi" />
                                                <input type="text" id="SolusiDetail_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.SolusiDetail" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Status}}</label>
                                                <input type="text" id="Status_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Status" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Contract}}</label>
                                                <input type="text" id="Contract_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Contract" />
                                                <input type="text" id="NIK_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.NIK" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Class}}</label>
                                                <input type="text" id="JenisPekerjaan_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Class" />

                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Cause}}</label>
                                                <input type="text" id="Penyebab_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Cause" />

                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Type}}</label>
                                                <input type="text" id="ObjectRusak_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Type" />

                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.PerfomedAction}}</label>
                                                <input type="text" id="Tindakan_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.PerfomedAction" />

                                            </td>
                                            <td>
                                                <input ng-click="FormEdit1($index)" class="hidewhenclick_{{$index}} btn btn-success" style="display: inline" type="submit" value="Edit" />
                                                <input ng-click="Close1($index)" class="hidewhenclick_{{$index}} btn btn-danger" type="submit" value="Close" />

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <%--end show data in table--%>

                                <%--information total record in table --%>
                                <div id="tableinfo" class="dataTables_info1"></div>
                                <%-- end information total record in table --%>

                                <%--paging table --%>
                                <div>
                                    <ul class="pagination">
                                        <li class="paginate_button previous">
                                            <input class="btn btn-primary" type="submit" id="previous" value="Previous" ng-click="nextprev('prev');" /></li>
                                        <li>
                                            <input type="text" id="page" style="width: 30px;text-align:center;" /></li>
                                        <li>
                                            <input class="btn btn-primary" type="submit" id="next" value="Next" ng-click="nextprev('next');" /></li>
                                    </ul>

                                    <%-- end paging table --%>
                                </div>
                            </div>
                        </div>
                        <%--end table controller--%>

                        <div class="ibox-content" ng-controller="MyControllerDTL">
                            <div class="table-responsive">
                                WO Posted
                                 <table>
                                     <tr>
                                         <td>
                                             <div class="pull-right mail-search">
                                                 <div class="input-group">
                                                     <input id="txtsearchdtl" class="form-control input-sm" placeholder="WO" type="text">
                                                     <div class="input-group-btn">
                                                         <button class="btn btn-sm btn-primary" type="submit" ng-click="searchdtl1();">Search </button>
                                                     </div>
                                                 </div>
                                             </div>
                                         </td>
                                     </tr>
                                 </table>

                                <%--show data  in table--%>
                                <table id="tablemenu1" datatable="ng" class="table table-striped table-bordered table-hover dataTables-example">
                                    <thead>
                                        <tr>

                                            <th>WO</th>
                                            <th>Deskripsi WO</th>
                                         <th>Deskripsi Penyebab</th>
                                            <th>Tindakan Perbaikan</th>
                                            <th>Status</th>
                                            <th>Contract</th>
                                             <th>Jenis Pekerjaan</th>
                                            <th>Penyebab</th>
                                            <th>Object Rusak</th>
                                            <th>Tindakan</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="{{$index}}" ng-repeat="x in masterdtl"  my-post-repeat-directive class="gradeX">

                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.WO}}</label>
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.WO" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.DeskripsiWO}}</label>
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.DeskripsiWO" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Penyebab}}</label>
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.Penyebab" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Solusi}}</label>
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.Solusi" />
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.SolusiDetail" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Status}}</label>
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.Status" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Contract}}</label>
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.Contract" />
                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Class}}</label>
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.Class" />

                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Cause}}</label>
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.Cause" />

                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.Type}}</label>
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.Type" />

                                            </td>
                                            <td>
                                                <label class="hidewhenclick_{{$index}}">{{x.PerfomedAction}}</label>
                                                <input type="text" class="control_{{$index}}" style="display: none" ng-value="x.PerfomedAction" />

                                            </td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <%--end show data in table--%>

                                <%--information total record in table --%>
                                <div id="tableinfodtl" class="dataTables_info1"></div>
                                <%-- end information total record in table --%>

                                <%--paging table --%>
                                <div>
                                    <ul class="pagination">
                                        <li class="paginate_button previous">
                                            <input class="btn btn-primary" type="submit" id="previousdtl" value="Previous" ng-click="nextprevdtl('prev');" /></li>
                                        <li>
                                            <input type="text" id="pagedtl" style="width: 30px;text-align:center;" /></li>
                                        <li>
                                            <input class="btn btn-primary" type="submit" id="nextdtl" value="Next" ng-click="nextprevdtl('next');" /></li>
                                    </ul>
                                </div>
                                <%-- end paging table --%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--emd angular app--%>

    <script>
        //binding data 
        window.id1 = ['WO', 'DeskripsiWO', 'DeskripsiPenyebab', 'TindakanPerbaikan', 'SolusiDetail', 'Status', 'Contract', 'NIK', 'JenisPekerjaan', 'Penyebab', 'ObjectRusak', 'Tindakan'];
        window.url1 = "<%=ResolveUrl("~/Transaction/ClosingWO.aspx/GetData") %>";
        window.urldtl1 = "<%=ResolveUrl("~/Transaction/ClosingWO.aspx/GetDatadtl") %>";
        //CRUD
        window.url2 = "<%=ResolveUrl("~/Transaction/ClosingWO.aspx/crud1") %>";
        window.Key = 'WO';
        window.Key2 = 'Contract';
        //popupurl
        window.urlpopup = [""];

        //form CRUD
        $(document).on('click', '#NewForm', function () {
            $("#FormInput").toggle();
            $("#Save").show();
            $("#Cancel").show();
            $("#update").hide();
        });
        $(document).on('click', '#Cancel', function () {
            $("#FormInput").hide();
            $("#Save").hide();
            $("#Cancel").hide();
            $("#update").hide();
            document.getElementById('NewForm').style.display = '';
            for (var i = 0; i < window.id1.length; i++) {
                if (document.getElementById(window.id1[i]) != null) {
                    document.getElementById(window.id1[i]).value = '';
                }

            }

        });
		function maxLengthCheck(object)
		  {
			if (object.value.length > object.maxLength)
			  object.value = object.value.slice(0, object.maxLength)
		  }
        $(document).ready(function () {
            $('#loading').show();
            $.ajax(
                {
                    type: "POST",
                    url: "<%=ResolveUrl("~/Default.aspx/GetClass") %>",
                    datatype: "json",
                    contentType: "application/json; charset=utf-8",
                    data: {},
                    success: function (data) {

                        var items = "";
                       
                        $.each(JSON.parse(data.d), function (i, item) {
                            items += "<option value='" + item.kode + "'>"+(item.kode) +  "-"  + (item.desc)  +"</option>";
                        });
                        $("#JenisPekerjaan").html(items);
                    }, error: function (data) {
                        alert('error');
                    }
                });

            $.ajax(
                {
                    type: "POST",
                    url: "<%=ResolveUrl("~/Default.aspx/GetType1") %>",
                    datatype: "json",
                    contentType: "application/json; charset=utf-8",
                    data: {},
                    success: function (data) {

                        var items = "";

                        $.each(JSON.parse(data.d), function (i, item) {
                            items += "<option value='" + item.kode + "'>"+(item.kode) + "-" + (item.desc)  +"</option>";
                        });
                        $("#ObjectRusak").html(items);
                    }, error: function (data) {
                        alert('error');
                    }
                });

            $.ajax(
                {
                    type: "POST",
                    url: "<%=ResolveUrl("~/Default.aspx/GetCause") %>",
                    datatype: "json",
                    contentType: "application/json; charset=utf-8",
                    data: {},
                    success: function (data) {

                        var items = "";

                        $.each(JSON.parse(data.d), function (i, item) {
                            items += "<option value='" + item.kode + "'>"+(item.kode) + "-" + (item.desc) +"</option>";
                        });
                        $("#Penyebab").html(items);
                    }, error: function (data) {
                        alert('error');
                    }
                });

            $.ajax(
                {
                    type: "POST",
                    url: "<%=ResolveUrl("~/Default.aspx/GetPerformedActionId") %>",
                    datatype: "json",
                    contentType: "application/json; charset=utf-8",
                    data: {},
                    success: function (data) {

                        var items = "";

                        $.each(JSON.parse(data.d), function (i, item) {
                            items += "<option value='" + item.kode + "'>"+(item.kode) + "-" + (item.desc) +"</option>";
                        });
                        $("#Tindakan").html(items);
                    }, error: function (data) {
                        alert('error');
                    }
                });
            $('#loading').hide();
        });
    </script>
</asp:Content>

