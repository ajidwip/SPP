<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="KatalogPart.aspx.cs" Inherits="BarcodeTeknik.Master.KatalogPart" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/plugins/dataTables/dataTablesStyles") %>
    <%:Scripts.Render("~/plugins/dataTables")%>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Katalog</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li>
                    <a>Tables</a>
                </li>
                <li class="active">
                    <strong>Katalog</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>
    <div ng-app="myApp">
        <div class="wrapper wrapper-content animated fadeInRight" ng-controller="MyController">
                 <div id="group">
                                  <table>
                                      <tr>
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
           <%-- <table>
                <tr>
                    <td width="80%"></td>
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
            </table>--%>
            <div id="angulardiv">

                <div class="row">
                        <div class="col-md-3" ng-repeat="x in master" my-post-repeat-directive>
                        <div class="ibox">
                               <div id="loading" class="center-div" style="display:none">
                    <img src="<%=ResolveUrl("~/tmp/loading.gif") %>" style="width: 100px; height: 100px;" />
                </div>
                            <div class="ibox-content product-box">

                                <div class="product-imitation">
                                     <img ng-src="<%=ResolveUrl("~") %>{{x.image}}" width="150px" height="150px" />
                                </div>
                                <div class="product-desc">

                                    <small class="text-muted">Part</small>
                                    <a ng-href="<%=ResolveUrl("~/Transaction/katalogdetail/?") %>img1=<%=ResolveUrl("~") %>{{x.image}}&img2=<%=ResolveUrl("~") %>{{x.image2}}&img3=<%=ResolveUrl("~") %>{{x.image3}}" class="product-name">{{x.PartNo}}</a>
                                </div>
                            </div>
                        </div>
                    </div>
                 
                    </div>
            </div>
			  <div id="tableinfo" class="dataTables_info1"></div>
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
    <script>
        window.id1 = ['PartNo', 'Description', 'image'];
        window.ukuran = ['80', '80', '80'];
        window.url1 = "<%=ResolveUrl("~/Master/KatalogPart.aspx/GetData") %>";
        window.url2 = "<%=ResolveUrl("~/Master/KatalogPart.aspx/crud1") %>";
        window.urlpopup = [""];
        window.filter = 'PartNo';

        $(document).ready(function () {
            $('#loading').show();
            SearchText();
        });

        function SearchText() {
        $("#txtsearch").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "<%=ResolveUrl("~/Default.aspx/GetKatalogPartNoAutComplete") %>",
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
