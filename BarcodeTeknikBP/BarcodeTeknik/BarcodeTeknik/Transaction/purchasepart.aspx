<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="purchasepart.aspx.cs" Inherits="BarcodeTeknik.Transaction.purchasepart" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/plugins/dataTables/dataTablesStyles") %>
    <%:Scripts.Render("~/plugins/dataTables")%>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Purchase Part</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li>
                    <a>Tables</a>
                </li>
                <li class="active">
                    <strong>Purchase Part</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>
    <div ng-app="myApp">
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                           <%-- <input type="submit" class="btn btn-w-m btn-primary" id="NewForm" value="New" />--%>
                              <input id="generate" class="btn btn-w-m btn-danger" type="submit" value="Barcode" onclick="generate();" />
                             <div id="loading1"></div>
                            <div ng-controller="MyControllerForm1" id="FormInput" style="display: none">
                                <table>
                                    <tr>
                                        <td>&nbsp;Part Description
                                        </td>
                                        <td>
                                            <input type="text" id="PartDescription"  style="margin-top: 5px" class="form-control"/>
                                        </td>
                                        <td>&nbsp;Site
                                        </td>
                                        <td>
                                             <input type="submit" value="..." onclick="showpopupForm('Site', 'undefined', '', '<%=ResolveUrl("~/Default.aspx/GetSite") %>    ')" />
                                            <input type="text" id="Site" class="form-control"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Part No
                                        </td>
                                        <td>
                                          
                                            <input type="text" id="PartNo"  style="margin-top: 5px;" class="form-control" />
                                        </td>
                                         <td>Rak No
                                        </td>
                                        <td>
                                            <input type="text" id="RakNo" class="form-control"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Purchase Group
                                        </td>
                                        <td>
                                            <input type="submit" value="..." onclick="showpopupForm('PurchaseGroup', 'PurchaseGroupDesc', '', '<%=ResolveUrl("~/Default.aspx/GetPurchaseGroup") %>')" />
                                            <input type="text" id="PurchaseGroup" class="form-control" style="margin-top:5px"/>
                                            <input style="display: none" type="text" id="PurchaseGroupDesc" />
                                        </td>

                                    </tr>

                                    <tr>
                                        <td>&nbsp;Unit
                                        </td>
                                        <td>
                                            <input type="text" id="Unit" style="margin-top: 5px" class="form-control"/>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td style="margin-top: 10px">
                                            <input type="submit" value="update" id="update" style="margin-top: 10px; display: none" class="btn btn-w-m btn-warning" ng-click="updateform1()" />
                                            <input type="submit" value="save" id="Save" style="margin-top: 10px; display: none" class="btn btn-w-m btn-warning" ng-click="saveForm()" />
                                        </td>
                                        <td>
                                            <input type="submit" value="Cancel" id="Cancel" style="margin-top: 10px; margin-left: 10px; display: none" class="btn btn-w-m btn-warning" />
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                                <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                                    <i class="fa fa-wrench"></i>
                                </a>
                                <ul class="dropdown-menu dropdown-user">
                                    <li><a href="#">Config option 1</a>
                                    </li>
                                    <li><a href="#">Config option 2</a>
                                    </li>
                                </ul>
                                <a class="close-link">
                                    <i class="fa fa-times"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content" ng-controller="MyController">
                            <div id="group">
                                  <table>
                                    <tr>
                                        <td width="80%">
                                            <input id="checkBoxall" onclick="checkall();" type="checkbox">Check All
                                        </td>
                                        <td>
                                            <div class="pull-right mail-search">
                                                <div class="input-group">
                                                    <input id="txtsearch" class="form-control input-sm" placeholder="Part No" type="text">
                                                      <div class="input-group-btn">
                                                        <button class="btn btn-sm btn-primary" type="submit" ng-click="search1();">Search </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            <br />
                            </div>
                            <div class="table-responsive">
                                <div id="angulardiv">
                                     <table>
                                        <tr>
                                            <td>Site</td>
                                            <td style="padding-left: 3px">
                                                <select id="ddlfilter" ng-model="myddl" ng-options="x.Site as x.Site for x in masterdll" ng-change="filteradd();">
                                                </select>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="tablemenu" datatable="ng" class="table table-striped table-bordered table-hover dataTables-example">
                                        <thead>
                                            <tr>

                                                <th>Part No</th>
                                                <th title="NamaTenant">Site</th>
                                                <th class="date">Purchase Group</th>
                                                <th title="TipeSewa">Unit</th>
                                                <th>Part Desctiption</th>
                                                <th>Rak No</th>
                                                <th></th>
                                               <%-- <th></th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="{{$index}}" ng-repeat="x in master|filter:activeId1" class="gradeX">

                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.PartNo}}</label>
                                                    <input type="text" id="PartNo_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.PartNo" />
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.Site}}</label>
                                                    <input type="text" id="Site_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Site" />
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.PurchaseGroup}}</label>
                                                    <input type="text" id="PurchaseGroup_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.PurchaseGroup" />
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.Unit}}</label>
                                                    <input type="text" id="Unit_{{$index}}" class="control_{{$index}} date" style="display: none; width: 80px" ng-value="x.Unit" />
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.PartDescription}}</label>
                                                    <input type="text" id="PartDescription_{{$index}}" onkeypress="validate(event)" class="control_{{$index}}" style="display: none; width: 30px" ng-value="x.PartDescription" />
                                                </td>
                                                   <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.RakNo}}</label>
                                                    <input type="text" id="RakNo_{{$index}}" onkeypress="validate(event)" class="control_{{$index}}" style="display: none; width: 30px" ng-value="x.RakNo" />
                                                </td>
                                                <td>
                                                    <input id="checkBox_{{$index}}" ng-click="getrow($index)" type="checkbox">
                                                </td>
                                            <%--    <td>
                                                    <input ng-click="FormEdit1($index)" class="hidewhenclick_{{$index}} btn btn-danger" style="display: block" type="submit" value="Edit" />
                                                    <input ng-click="delete1($index)" class="hidewhenclick_{{$index}} btn btn-danger" style="display: block" type="submit" value="delete" />
                                         
                                                </td>--%>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div>
                                        <ul class="pagination">
                                            <li class="paginate_button previous">
                                                <input class="btn btn-primary" type="submit" id="previous" value="Previous" ng-click="nextprev('prev');" /></li>
                                            <li>
                                                <input type="text" id="page" style="width: 30px" /></li>
                                            <li>
                                                <input class="btn btn-primary" type="submit" id="next" value="next" ng-click="nextprev('next');" /></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        window.id1 = ['PartNo', 'Site', 'PurchaseGroup', 'Unit', 'PartDescription','RakNo'];
        window.ukuran = ['80', '80', '80', '80', '80', '80', '80', '80'];
        window.url1 = "<%=ResolveUrl("~/Transaction/purchasepart.aspx/GetData") %>";
        window.url2 = "<%=ResolveUrl("~/Transaction/purchasepart.aspx/crud1") %>";
        window.urlpopup = ["<%=ResolveUrl("~/Default.aspx/GetPartNo") %>","<%=ResolveUrl("~/Default.aspx/GetPurchaseGroup") %>"];
        window.urlddl = "<%=ResolveUrl("~/Default.aspx/GetSiteddl") %>";
        window.filter1 = 'Site';
        window.barcode1 = 'PartNo';
        window.barcode2 = 'Site';
        $(document).ready(function () {
            SearchText();
        });


        $(document).on('click', '#NewForm', function () {
            $("#FormInput").toggle();
            $("#Save").show();
            $("#Cancel").show();
            $("#update").hide();
            SearchText();
        });
        $(document).on('click', '#Cancel', function () {
            $("#FormInput").hide();
            $("#Save").hide();
            $("#Cancel").hide();
            $("#update").hide();
            document.getElementById('NewForm').style.display = '';
            for (var i = 0; i < window.id1.length; i++) {

                document.getElementById(window.id1[i]).value = '';
            }

        });
        function enablepopup() {
            var partNo = document.getElementById('PartDescription').value;
            if (partNo != '') {
                document.getElementById('popupform1').disabled = false;
            }
        }
        function checkall() {
            var ischeck = document.getElementById('checkBoxall').checked;
            if (ischeck) {
                for (var i = 0; i < window.uniqueArray.length; i++) {
                    window.row1[i] = window.uniqueArray[i];

                }
                for (var i = 0; i < window.uniqueArray2.length; i++) {
                    window.row2[i] = window.uniqueArray2[i];

                }
            }
            else {
                window.row1 = new Array();
                window.row2= new Array();
            }
        }
         function generate() {
            for (var i = 0; i < window.row1.length; i++) {
                row = window.row1[i];

                if (i != window.row1.length - 1) {

                    var data = "{partNo:'" + window.row1[i] + "',site:'"+window.row2[0]+"',count:'"+i+"'}";
                    generatebarcode('<%=ResolveUrl("~/Transaction/barcode.aspx/GetkdBarang") %>', data, 0);
                } else {

                    var data = "{partNo:'" + window.row1[i] + "',site:'"+window.row2[0]+"',count:'" + i + "'}";
                    generatebarcode('<%=ResolveUrl("~/Transaction/barcode.aspx/GetkdBarang") %>', data, 1);
                }
            }
            window.row1 = new Array();

            var myinterval = setInterval(function () {
                $('#loading1').loading('toggle');
            }, 1000);

            setTimeout(function () {
                clearInterval(myinterval);
                openbarcode('<%=ResolveUrl("~/Transaction/barcode.aspx/generatebarcodepdf") %>', '', 1);
            }, 15000);
        }
         function SearchText() {
             $("#PartNo").autocomplete({
                 source: function (request, response) {
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         url: "<%=ResolveUrl("~/Default.aspx/GetPartNoAutComplete") %>",
                         data: "{'filter':'" + document.getElementById('PartNo').value + "'}",
                         dataType: "json",
                         success: function (data) {
                             response(data.d);
                         },
                         error: function (result) {
                             alert("No Match");
                         }
                     });
                 }
             });

             $("#PartDescription").autocomplete({
                 source: function (request, response) {
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         url: "<%=ResolveUrl("~/Default.aspx/GetDescPartNoAutComplete") %>",
                         data: "{'filter':'" + document.getElementById('PartDescription').value + "'}",
                         dataType: "json",
                         success: function (data) {
                             response(data.d);
                         },
                         error: function (result) {
                             alert("No Match");
                         }
                     });
                 }
             });

             $("#txtsearch").autocomplete({
                 source: function (request, response) {
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         url: "<%=ResolveUrl("~/Default.aspx/GetPartNoAutComplete") %>",
                         data: "{'filter':'" + document.getElementById('txtsearch').value + "'}",
                         dataType: "json",
                         success: function (data) {
                             response(data.d);
                         },
                         error: function (result) {
                             alert("No Match");
                         }

                     });
                 }
             });
         }  


    </script>
    
    <style>
       .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto;
            /* prevent horizontal scrollbar */
            overflow-x: hidden;
            /* add padding to account for vertical scrollbar */
            padding-right: 20px;
        } 
</style>
</asp:Content>
