<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="WO.aspx.cs" Inherits="BarcodeTeknik.Master.WO" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
      <%:Styles.Render("~/Content/plugins/dataTables/dataTablesStyles") %>
    <%:Scripts.Render("~/plugins/dataTables")%>

       <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>WO No Part</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li>
                    <a>Tables</a>
                </li>
                <li class="active">
                    <strong>WO No Part</strong>
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
                                                    <input id="txtsearch" class="form-control input-sm" placeholder="Wo No" type="text">
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
								<br/>
                         <table>
                                        <tr>
                                            <td>Site</td>
                                            <td style="padding-left: 3px">
                                                <select id="ddlfilter" ng-model="myddl" ng-options="x.filter as x.filter for x in masterdll" ng-change="filteradd();">
                                                </select>
                                            </td>
                                        </tr>
                                      
                                    </table>
									<br/>
                                    <%--show data  in table--%>
                                    <table id="tablemenu" datatable="ng" class="table table-striped table-bordered table-hover dataTables-example">
                                        <thead>
                                            <tr>
                                                <th>Wo No</th>
                                                <th>Deskripsi</th>
                                                <th>Contract</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="{{$index}}" ng-repeat="x in master" my-post-repeat-directive class="gradeX">
                                      
                                                <td>
                                                    
                                                    <label class="hidewhenclick_{{$index}}">{{x.WoNo}}</label>
                                                    <input type="text" id="WoNo_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.WoNo" />
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.Deksripsi}}</label>
                                                    <input type="text" id="Deskripsi_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Deksripsi" />
                                                </td>
                                                <td>
                                                    <label class="hidewhenclick_{{$index}}">{{x.Contract}}</label>
                                                    <input type="text" id="Contract_{{$index}}" class="control_{{$index}}" style="display: none" ng-value="x.Contract" />
                                                </td>
                                                <td>

                                                    <input ng-click="delete1($index)" class="hidewhenclick_{{$index}} btn btn-danger" style="display: inline"                   type="submit" value="Tanpa Part" />
                                            
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
                                                <input type="text" id="page" style="width: 30px;text-align:center;" /></li>
                                            <li>
                                                <input class="btn btn-primary" type="submit" id="next" value="Next" ng-click="nextprev('next');" /></li>
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
        window.id1 = ['WoNo', 'Deskripsi', 'Contract']; //field yang di tampilkan pada table, urutan sesuai dengan <th> pada datatable, huruf besar kecil sama
        window.url1 = "<%=ResolveUrl("~/Master/WO.aspx/GetData") %>"; // url binding data ke table
       
        //CRUD
        window.url2 = "<%=ResolveUrl("~/Master/WO.aspx/crud1") %>"; // url untuk insert update delete
        window.Key = 'WoNo'; //unique key pada table
 
		window.urlddl = "<%=ResolveUrl("~/Default.aspx/GetSiteddl") %>";
        window.filter1 = 'Contract';
           $(document).ready(function () {
           
        });
    </script>
</asp:Content>
