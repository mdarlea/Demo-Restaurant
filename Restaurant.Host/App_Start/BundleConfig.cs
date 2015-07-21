using System;
using System.Text;
using System.Web.Optimization;

namespace Restaurant.Host
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapui").Include(
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/angular-sanitizer.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/angular-local-storage.min.js",
                "~/Scripts/loading-bar.js"));

            bundles.Add(new ScriptBundle("~/bundles/swaksoft").Include(
                "~/Scripts/sw-common-{version}.js",
                "~/Scripts/sw-ui-bootstrap/sw-ui-bootstrap-tpls-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/Site.css",
                 "~/Content/navigation.css"));

            var configuration = new StringBuilder("~/app/providers/configuration");

            #if (DEBUG)
            configuration.Append(".Debug");
            #endif

            configuration.Append(".js");

            // our controller, services, directives and main app js files
            bundles.Add(new ScriptBundle("~/bundles/app")
                    .Include(
                           "~/app/app.js",
                           configuration.ToString())
                    .IncludeDirectory("~/app/controllers", "*.js", true)
                    .IncludeDirectory("~/app/directives", "*.js", true)
                    .IncludeDirectory("~/app/services", "*.js", true));

            //BundleTable.EnableOptimizations = false;
        }
    }
}
