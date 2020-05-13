<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MasterRak.aspx.cs" Inherits="BarcodeTeknik.Master.MasterRak" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/plugins/dataTables/dataTablesStyles") %>
    <%:Scripts.Render("~/plugins/dataTables")%>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Rak</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li>
                    <a>Tables</a>
                </li>
                <li class="active">
                    <strong>Rak</strong>
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
                              <div id="loading1"></div>
                            <div id="loading" class="center-div" style="display:none">
                    <img src="<%=ResolveUrl("~/tmp/loading.gif") %>" style="width: 100px; height: 100px;" />
                </div>
                           
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
                                                    <input id="txtsearch" class="form-control input-sm" placeholder="Rak No" type="text">
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
                                            <%--<td>PartNo</td>
                                            <td style="padding-left: 3px">
                                                <select id="ddlfilter2" ng-model="myddl2" ng-options="x.filter as x.filter for x in masterdll2" ng-change="filteradd();">
                                                </select>
                                            </td>--%>
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
                                                <th style="display:none">Rak No</th>
                                                 <th>Site</th>
                                                 <th>Rak No IFS</th>
                                               <%-- <th></th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="{{$index}}" ng-repeat="x in master" my-post-repeat-directive class="gradeX">
											   <td>
                                                    <input id="checkBox_{{$index}}" ng-click="getrow($index)" type="checkbox"  ng-checked="getcheckedfalse($index)">
                                                </td>
                                                <td style="display:none">
                                                    <label class="hidewhenclick_{{$index}}">{{x.RakNo}}</label>
                                                    <input type="text" id="RakNo_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.RakNo" />
                                                </td>
                                                 <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.Site}}</label>
                                                    <input type="text" id="Site_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Site" />
                                                </td>
                                                 <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.RakNoIFS}}</label>
                                                    <input type="text" id="RakNoIFS_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.RakNoIFS" />
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
     <%--emd angular app--%>

    <script>
        //binding data ke table
        window.id1 = ['RakNo', 'Site','RakNoIFS'];//field yang di tampilkan pada table, urutan sesuai dengan <th> pada datatable, huruf besar kecil sama
        window.ukuran = ['80','80','80'];
        window.url1 = "<%=ResolveUrl("~/Master/MasterRak.aspx/GetData") %>";

        //CRUD
        window.url2 = "<%=ResolveUrl("~/Master/MasterRak.aspx/crud1") %>";
        window.Key = 'RakNo';

        //filter table dengan dropdown
        window.urlddl = "<%=ResolveUrl("~/Default.aspx/GetSiteddl") %>";
        window.filter1 = 'Site';
        //gunakan code dibawah ini jika membutuhkan dua buah filter menggunakan dropdown
        //window.urlddl2 = "<%=ResolveUrl("~/Default.aspx/GetRakNoddl") %>";
        //window.filter2 = 'RakNo';

        //barcode generator
        window.barcode1 = 'RakNo';
        window.barcode2 = 'RakNo';
        $(document).ready(function () {
            $('#loading').show();
           
        });
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

                    var data = "{partNo:'" + window.row1[i] + "',site:'" + window.row2[i] + "',count:'" + i + "'}";
                    generatebarcode('<%=ResolveUrl("~/Transaction/barcode.aspx/GetkdBarang") %>', data, 0);
                } else {

                    var data = "{partNo:'" + window.row1[i] + "',site:'" + window.row2[i] + "',count:'" + i + "'}";
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
                openbarcode('<%=ResolveUrl("~/Transaction/barcode.aspx/generatebarcodesitepdf") %>', '', 1);
            }, timer);
        }

       //form crud
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
