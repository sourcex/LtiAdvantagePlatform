﻿using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdvantagePlatform.Pages.Shared
{
    public class MainNavPages
    {
        public static string Index => "/Pages/Index";
        public static string Clients => "/Pages/Clients";
        public static string Deployments => "/Pages/Deployments";
        public static string Tools => "/Pages/Tools";
        public static string About => "/Pages/About";
        public static string Contact => "/Pages/Contact";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
        public static string ClientsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Clients);
        public static string DeploymentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Deployments);
        public static string ToolsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Tools);
        public static string AboutNavClass(ViewContext viewContext) => PageNavClass(viewContext, About);
        public static string ContactNavClass(ViewContext viewContext) => PageNavClass(viewContext, Contact);

        public static string PageNavClass(ViewContext viewContext, string path)
        {
            var activePath = viewContext.View.Path;
            return activePath.StartsWith(path, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}