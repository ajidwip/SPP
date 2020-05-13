<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Part.aspx.cs" Inherits="BarcodeTeknik.Transaction.Part" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/plugins/dataTables/dataTablesStyles") %>
    <%:Scripts.Render("~/plugins/dataTables")%>
    
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Inventory Part</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li>
                    <a>Tables</a>
                </li>
                <li class="active">
                    <strong>Part</strong>
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
                            <input id="generate" class="btn btn-w-m btn-danger" type="submit" value="Barcode" onclick="generate();" />
                              <input id="sync" class="btn btn-w-m btn-warning" style="display:none" type="submit" value="Refresh Qty" onclick="refreshqty();" />
                            <div id="loading1"></div>
                            <div id="loading" class="center-div" style="display:none">
                    <img src="<%=ResolveUrl("~/tmp/loading.gif") %>" style="width: 100px; height: 100px;" />
                </div>
                          <%--  <div ng-controller="MyControllerForm1" id="FormInput" style="display: none">
                                <table>
                                    <tr>
                                        <td>&nbsp;Part Description
                                        </td>
                                        <td>
                                            <input type="text" id="PartDescription" class="form-control" style="margin-top:5px" />
                                        </td>
                                        <td>&nbsp;Site
                                        </td>
                                        <td>
                                            <input type="submit" value="..." onclick="showpopupForm('Site', 'undefined', '', '<%=ResolveUrl("~/Default.aspx/GetSite") %>    ')" />
                                            <input type="text" id="Site"  class="form-control"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Part No
                                        </td>
                                        <td>
                                            <input type="text" id="PartNo" class="form-control" style="margin-top:5px"/>
                                        </td>
                                         <td>Rak No
                                        </td>
                                        <td>
                                            <input type="text" id="RakNo" class="form-control"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Accounting Group
                                        </td>
                                        <td>
                                            <input type="submit" value="..." onclick="showpopupForm('AccountingGroup', 'AccountingGroupDesc','', '<%=ResolveUrl("~/Default.aspx/GetAT") %>    ')" />
                                            <input type="text" id="AccountingGroup" class="form-control" style="margin-top:5px"/>
                                            <input style="display: none" type="text" id="AccountingGroupDesc" />
                                        </td>

                                    </tr>

                                    <tr>
                                        <td>&nbsp;Unit
                                        </td>
                                        <td>
                                            <input type="text" id="Unit"  class="form-control" style="margin-top:5px"/>
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
                          <%--table controller--%>
                        <div class="ibox-content" ng-controller="MyController">
                            <div id="group">
                                <table>
                                    <tr>
                                        <td>
                                           
                                            <div class="pull-right mail-search">
                                                <div class="input-group">
                                                    <input id="txtsearch" class="form-control input-sm" placeholder="Part No/Deskripsi" type="text">
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
                                                <select id="ddlfilter" ng-model="myddl" ng-options="x.filter as x.filter for x in masterdll" ng-change="filteradd();">
                                                </select>
                                            </td>
                                        </tr>
										 <tr>
                                        <td>
												<br/>
                                            <input id="checkBoxall" onclick="checkall();" type="checkbox" ng-model="all">Check All
                                        </td>
                                        </tr>
                                    </table>

                                     <%--show data  in table--%>
                                    <table id="tablemenu" datatable="ng" class="table table-striped table-bordered table-hover dataTables-example">
                                        <thead>
                                            <tr>
												<th></th>
                                                <th>Part No</th>
                                                <th title="NamaTenant">Site</th>
                                                <th class="date">Accounting Group</th>
                                                <th title="TipeSewa">Unit</th>
                                                <th>Part Desctiption</th>
                                                <th>Rak No</th>
                                                 <th>Barcode</th>
                                                   <th>Qty</th>
                                              <%--  <th></th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="{{$index}}" ng-repeat="x in master" my-post-repeat-directive class="gradeX">
												<td>
                                                    <input id="checkBox_{{$index}}" ng-click="getrow($index)" type="checkbox" ng-checked="getcheckedfalse($index)">
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.PartNo}}</label>
                                                    <input type="text" id="PartNo_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.PartNo" />
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.Site}}</label>
                                                    <input type="text" id="Site_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Site" />
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.AccountingGroup}}</label>
                                                    <input type="text" id="AccountingGroup_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.AccountingGroup" />
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
                                                    <label class="hidewhenclick_{{$index}}">{{x.Barcode}}</label>
                                                    <input type="text" id="Barcode_{{$index}}" onkeypress="validate(event)" class="control_{{$index}}" style="display: none; width: 30px" ng-value="x.Barcode" />
                                                </td>
                                                  <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.qty}}</label>
                                                    <input type="text" id="Qty_{{$index}}" onkeypress="validate(event)" class="control_{{$index}}" style="display: none; width: 30px" ng-value="x.qty" />
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
                                                <input type="text" id="page" style="width: 30px" /></li>
                                            <li>
                                                <input class="btn btn-primary" type="submit" id="next" value="next" ng-click="nextprev('next');" /></li>
                                        </ul>
                                    </div>
                                     <%-- end paging table --%>
                                </div>
                            </div>
                        </div>
                          <%--end table controller--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <%--end angular appr--%>
    <script>

        //binding data ke table
        window.id1 = ['PartNo', 'Site', 'AccountingGroup', 'Unit', 'PartDescription','RakNo','Barcode'];
        window.ukuran = ['80', '80', '80', '80', '80', '80', '80'];
        window.url1 = "<%=ResolveUrl("~/Transaction/Part.aspx/GetData") %>";

        //CRUD
        window.url2 = "<%=ResolveUrl("~/Transaction/Part.aspx/crud1") %>";
        window.Key = 'Barcode';

        //Popup searh master data 
        window.urlpopup = ["<%=ResolveUrl("~/Default.aspx/GetPartNo") %>", "<%=ResolveUrl("~/Default.aspx/GetAT") %>"];

        //filter table dengan dropdown
        window.urlddl = "<%=ResolveUrl("~/Default.aspx/GetSiteddl") %>";
        window.filter1 = 'Site';

        //barcode generator
        window.barcode1 = 'Barcode';
        window.barcode2 = 'Site';

        $(document).ready(function () {
            $('#loading').show();
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
                window.row2 = new Array();
            }
        }
        function generate() {
            for (var i = 0; i < window.row1.length; i++) {
                row = window.row1[i];

                if (i != window.row1.length - 1) {

                    var data = "{partNo:'" + window.row1[i] + "',site:'"+window.row2[0]+"',count:'"+i+"'}";
                    generatebarcode('<%=ResolveUrl("~/Transaction/barcode.aspx/GetkdBarang") %>', data, 0);
                } else {

                    var data = "{partNo:'" + window.row1[i] + "',site:'" + window.row2[0] +"',count:'" + i + "'}";
                    generatebarcode('<%=ResolveUrl("~/Transaction/barcode.aspx/GetkdBarang") %>', data, 1);
                }
            }
			var timer;

            if (window.row1.length <= 10) {
                timer = 5000;
            }
            else {
                timer = 15000;
            }
            window.row1 = new Array();

            var myinterval = setInterval(function () {
                $('#loading1').loading('toggle');
            }, 1000);

            setTimeout(function () {
                clearInterval(myinterval);
                openbarcode('<%=ResolveUrl("~/Transaction/barcode.aspx/generatebarcodepdf") %>', '', 1);
            }, timer);
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
        function refreshqty() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "<%=ResolveUrl("~/Transaction/Part.aspx/syncqty") %>",
                data: {},
                dataType: "json",
                beforeSend: function () {
                    $('#loading').show();
                },
                        success: function (data) {
                            location.reload();
                        },
                        error: function (result) {
                            alert("error");
                        }, complete: function () {
                            $('#loading').hide();
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
