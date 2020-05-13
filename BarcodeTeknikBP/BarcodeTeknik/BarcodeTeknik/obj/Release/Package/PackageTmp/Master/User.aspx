<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="User.aspx.cs" Inherits="BarcodeTeknik.Master.User" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/plugins/dataTables/dataTablesStyles") %>
    <%:Scripts.Render("~/plugins/dataTables")%>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>User</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li>
                    <a>Tables</a>
                </li>
                <li class="active">
                    <strong>User</strong>
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
                            <input type="submit" class="btn btn-w-m btn-primary" id="NewForm" value="New" />
                          <input id="generate" class="btn btn-w-m btn-danger" type="submit" value="QR Code" onclick="generate();" />
                          <div id="loading1"></div>
                            <%--input form controller--%>
                            <div ng-controller="MyControllerForm1" id="FormInput" style="display: none">
                              <%--input form--%>
                                <table>
                                    <tr>
                                        <td>&nbsp;User ID
                                        </td>
                                        <td>
                                            <input type="text" id="UserId" style="margin-top: 5px" class="form-control disableform"/>
                                        </td>
                                        <td>&nbsp;User Name
                                        </td>
                                        <td>
                                            <input type="text" id="UserName" style="margin-top: 5px" class="form-control"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Email
                                        </td>
                                        <td>
                                            <input type="text" id="Email" style="margin-top: 5px" class="form-control"/>
                                        </td>

                                    </tr>
                                     <tr>
                                        <td>NIK
                                        </td>
                                        <td>
                                            <input type="text" id="NIK" style="margin-top: 5px" class="form-control disableform"/>
                                        </td>

                                    </tr>
                                     <tr>
                                        <td>Golongan User
                                        </td>
                                        <td>
                                         
                                            <input type="radio" name="GolonganUser" value="Teknik"> Teknik<br>
                                            <input type="radio" name="GolonganUser" value="Non Teknik"> Non Teknik<br>
                                        
                                        </td>

                                    </tr>
                                       <tr>
                                        <td>Active
                                        </td>
                                        <td>
                                            <input type="checkbox" name="Active" ng-click="setchecked($event.currentTarget)"> 
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
                        <div class="ibox-content"  ng-controller="MyController">
                                 <div id="loading" class="center-div" style="display:none">
                    <img src="<%=ResolveUrl("~/tmp/loading.gif") %>" style="width: 100px; height: 100px;" />
                </div>
                                <table>
                                    <tr>
                                        <td>
                                            <div class="pull-right mail-search">
                                                <div class="input-group">
                                                    <input id="txtsearch" class="form-control input-sm" placeholder="User Id" type="text">
                                                      <div class="input-group-btn">
                                                        <button class="btn btn-sm btn-primary" type="submit" ng-click="search1();">Search </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            <div class="table-responsive">
                                <div id="angulardiv">
                                <table>
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
                                                <th>User Id</th>
                                                <th>User Name</th>
                                                <th>Email</th>
                                                <th>NIK</th>
                                                <th>Golongan User</th>
                                                 <th>Active</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="{{$index}}" ng-repeat="x in master" my-post-repeat-directive class="gradeX">
                                            <td>
                                                    <input id="checkBox_{{$index}}" ng-click="getrow($index)" type="checkbox" ng-checked="getcheckedfalse($index)">
                                                </td>
                                                <td>
                                                    
                                                    <label class="hidewhenclick_{{$index}}">{{x.UserId}}</label>
                                                    <input type="text" id="UserId_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.UserId" />
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.UserName}}</label>
                                                    <input type="text" id="UserName_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.UserName" />
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.email}}</label>
                                                    <input type="text" id="Email_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.email" />
                                                </td>
                                                 <td>
                                                      <label class="hidewhenclick_{{$index}}">{{x.NIK}}</label>
                                                    <input type="text" id="NIK_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.NIK" />
                                                  
                                                </td>
                                                  <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.GolonganUser}}</label>
                                                    <input type="text" name="GolonganUser_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.GolonganUser" />
                                                </td>
                                                 <td>
                                                    <input type="checkbox" name="Active_{{$index}}" disabled class="hidewhenclick_{{$index}}" ng-value="x.Active" ng-model="x.Active"/>
                                                     <input type="checkbox" name="Active_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Active" ng-model="x.Active"/>
                                                </td>
                                                <td>
                                                    <input ng-click="FormEdit1($index)" class="hidewhenclick_{{$index}} btn btn-success" style="display: inline" type="submit" value="Edit" />
                                                    <input ng-click="delete1($index)" class="hidewhenclick_{{$index}} btn btn-danger" style="display: inline" type="submit" value="delete" />
                                            
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                     <%--end show data in table--%>

                                     <%--information total record in table --%>
									  <div id="tableinfo" class="dataTables_info1">

									  </div>
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
        window.id1 = ['UserId', 'UserName', 'Email', 'NIK','GolonganUser','Active']; //field yang di tampilkan pada table, urutan sesuai dengan <th> pada datatable, huruf besar kecil sama
        window.url1 = "<%=ResolveUrl("~/Master/User.aspx/GetData") %>"; // url binding data ke table
       
        //CRUD
        window.url2 = "<%=ResolveUrl("~/Master/User.aspx/crud1") %>"; // url untuk insert update delete
        window.Key = 'NIK'; //unique key pada table
        $(document).ready(function () {
            $('#loading').show();

        });
         //form crud
        $(document).on('click', '#NewForm', function () {
            $("#FormInput").toggle();
            $("#Save").show();
            $("#Cancel").show();
            $("#update").hide();
            $(".disableform").removeAttr('disabled');
            for (var i = 0; i < window.id1.length; i++) {
                if (document.getElementById(window.id1[i]) != null) {
                    document.getElementById(window.id1[i]).value = '';
                }

            }
        });
        $(document).on('click', '#Cancel', function () {
            $("#FormInput").hide();
            $("#Save").hide();
            $("#Cancel").hide();
            $("#update").hide();
            $(".disableform").removeAttr('disabled');
            document.getElementById('NewForm').style.display = '';
            for (var i = 0; i < window.id1.length; i++) {
                if (document.getElementById(window.id1[i]) != null) {
                    document.getElementById(window.id1[i]).value = '';
                }
                
            }
      
        });
       

        //barcode generator
        window.barcode1 = 'NIK';
        window.barcode2 = 'NIK';

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

                    var data = "{partNo:'" + window.row1[i] + "',site:'"+window.row2[i]+"',count:'"+i+"'}";
                    generatebarcode('<%=ResolveUrl("~/Transaction/barcode.aspx/GetkdBarang") %>', data, 0);
                } else {

                    var data = "{partNo:'" + window.row1[i] + "',site:'" + window.row2[i] +"',count:'" + i + "'}";
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
                openbarcode('<%=ResolveUrl("~/Transaction/barcode.aspx/generateqr") %>', '', 1);
            }, timer);
        }
    </script>
</asp:Content>
