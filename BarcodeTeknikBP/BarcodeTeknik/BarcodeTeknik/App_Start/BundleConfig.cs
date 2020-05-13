using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace BarcodeTeknik
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Content/animate.css",
                     "~/Content/style.css",
                      "~/Content/jquery.loading.css",
                       "~/Content/plugins/sweetalert/sweetalert.css",
                        "~/Content/plugins/slick/slick.css",
                       "~/Content/plugins/slick/slick-theme.css",
                       "~/Content/plugins/toastr/toastr.min.css"
                    ));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js",
                         "~/Scripts/jquery-1.10.2.js",
                           "~/Scripts/jquery.loading.js",
                           "~/Scripts/angular.js",
                           "~/Scripts/angular.min.js",
                           "~/Scripts/plugins/blueimp/jquery.blueimp-gallery.min.js",
                              "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js",
                                 "~/Scripts/plugins/pace/pace.min.js",
                                  "~/Scripts/plugins/metisMenu/metisMenu.min.js",
                                "~/Scripts/plugins/slick/slick.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jspdf").Include(
                      "~/Scripts/jspdf.js",
                      "~/Scripts/jspdf.min.js",
                      "~/Scripts/jspdf.debug.js",
                      "~/Scripts/jspdf.PLUGINTEMPLATE.js",
                      "~/Scripts/jspdf.png_support.js",
                      "~/Scripts/jspdf.addimage.js",
                      "~/Scripts/jspdf.autoprint.js"));

            // jQueryUI CSS
            bundles.Add(new StyleBundle("~/Scripts/plugins/jquery-ui/jqueryuiStyles").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.css"));

            // jQueryUI 
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js",
                      "~/Scripts/plugins/pace/pace.min.js",
                      "~/Scripts/app/inspinia.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Scripts/app/skin.config.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));


            // iCheck css styles
            bundles.Add(new StyleBundle("~/Content/plugins/iCheck/iCheckStyles").Include(
                      "~/Content/plugins/iCheck/custom.css"));

            bundles.Add(new ScriptBundle("~/fonts/font-awesome/css/code128").Include(
                      "~/fonts/font-awesome/css/code128.css"));
            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Scripts/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/datatables.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/plugins/dataTables/datatables.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/angulardatatables").Include(
                    "~/Scripts/angular-datatables.js",
                    "~/Scripts/angular-datatables.util.js",
                    "~/Scripts/angular-datatables.options.js",
                    "~/Scripts/angular-datatables.renderer.js",
                    "~/Scripts/angular-datatables.directive.js",
                      "~/Scripts/angular-datatables.factory.js",
                       "~/Scripts/angular-datatables.instances.js",
                       "~/Scripts/myApp.js",
                        "~/Scripts/MyController.js"

                    ));

            // validate 
            bundles.Add(new ScriptBundle("~/plugins/validate").Include(
                      "~/Scripts/plugins/validate/jquery.validate.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Scripts/plugins/datapicker/bootstrap-datepicker.js"));
            //inlineEdit
            bundles.Add(new ScriptBundle("~/plugins/inline").Include(
            "~/Scripts/InlineEdit.js", "~/Scripts/plugins/sweetalert/sweetalert.min.js"
            ));


            // jsTree
            bundles.Add(new ScriptBundle("~/plugins/jsTree").Include(
                      "~/Scripts/plugins/jsTree/jstree.min.js"));

            // jsTree styles
            bundles.Add(new StyleBundle("~/Content/plugins/jsTree").Include(
                      "~/Content/plugins/jsTree/style.css"));


            // Awesome bootstrap checkbox
            bundles.Add(new StyleBundle("~/plugins/awesomeCheckboxStyles").Include(
                      "~/Content/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css"));


            // Sweet alert Styless
            bundles.Add(new StyleBundle("~/plugins/sweetAlertStyles").Include(
                      "~/Content/plugins/sweetalert/sweetalert.css"));

            // Sweet alert
            bundles.Add(new ScriptBundle("~/plugins/sweetAlert").Include(
                      "~/Scripts/plugins/sweetalert/sweetalert.min.js"));


            bundles.Add(new StyleBundle("~/plugins/custom").Include(
                      "~/Content/custompopup.css"));

        }
    }
}