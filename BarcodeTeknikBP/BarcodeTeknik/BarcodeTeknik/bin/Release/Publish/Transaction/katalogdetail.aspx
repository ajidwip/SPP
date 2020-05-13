<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="katalogdetail.aspx.cs" Inherits="BarcodeTeknik.Transaction.katalogdetail" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%:Styles.Render("~/Content/plugins/dataTables/dataTablesStyles") %>
    <%:Scripts.Render("~/plugins/dataTables")%>
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Katalog Detail</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li>
                    <a>Tables</a>
                </li>
                <li class="active">
                    <strong>Katalog Detail</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>
    <div>
        <div class="wrapper wrapper-content animated fadeInRight">

            <div id="angulardiv">

                <div class="row">
                    <div class="product-images">
                        <div>
                            <div class="image-imitation">
                               
                                    <img src="<%Response.Write(img1);%>"  class="center"/>
                                
                            </div>
                        </div>
                        <div>
                            <div class="image-imitation">
                               
                                    <img src="<%Response.Write(img2);%>" class="center"/>
                               
                            </div>
                        </div>
                        <div>
                            <div class="image-imitation">
                              
                                    <img src="<%Response.Write(img3);%>" class="center"/>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {


            $('.product-images').slick({
                dots: true
            });

        });

    </script>

    <style type="text/css">
        .center {
            display: block;
            margin-left: auto;
            margin-right: auto;
            width: 50%;
        }
    </style>

</asp:Content>

