<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="FinishWO.aspx.cs" Inherits="BarcodeTeknik.Transaction.FinishWO" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/plugins/dataTables/dataTablesStyles") %>
    <%:Scripts.Render("~/plugins/dataTables")%>
   
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Finish Wo</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li>
                    <a>Tables</a>
                </li>
                <li class="active">
                    <strong>Finish Wo</strong>
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
                        <div class="ibox-content" ng-controller="MyController">
                           
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
                                              
                                                <input ng-click="delete1($index)" class="hidewhenclick_{{$index}} btn btn-danger" type="submit" value="Finish" />

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

                   
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--emd angular app--%>

    <script>
        //binding data 
        window.id1 = ['WO', 'DeskripsiWO', 'DeskripsiPenyebab', 'TindakanPerbaikan', 'SolusiDetail', 'Status', 'Contract', 'NIK', 'JenisPekerjaan', 'Penyebab', 'ObjectRusak', 'Tindakan'];
        window.url1 = "<%=ResolveUrl("~/Transaction/FinishWO.aspx/GetData") %>";
      
        //CRUD
        window.url2 = "<%=ResolveUrl("~/Transaction/FinishWO.aspx/crud1") %>";
        window.Key = 'WO';
        window.Key2 = 'Contract';

    </script>
</asp:Content>

