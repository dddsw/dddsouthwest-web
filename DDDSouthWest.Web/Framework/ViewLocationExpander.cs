using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace DDDSouthWest.Web.Framework
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var routeData = context.ActionContext.ActionDescriptor.RouteValues;
            Console.WriteLine("here2");
            context.Values.Add("controller", routeData.FirstOrDefault(x => x.Key == "controller").Value);
            context.Values.Add("action", routeData.FirstOrDefault(x => x.Key == "action").Value);
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            Console.WriteLine("here1");
            var controllerActionDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null &&
                controllerActionDescriptor.ControllerTypeInfo.FullName.Contains("Features"))
                return new List<string>
                {
                    GetFeatureLocation(controllerActionDescriptor.ControllerTypeInfo.FullName)
                };

            return viewLocations;
        }

        private static string GetFeatureLocation(string fullControllerName)
        {
            Console.WriteLine("here3");
            var words = fullControllerName.Split('.');
            var path = "";
            var isInFeature = false;
            foreach (var word in words.Take(words.Length - 1))
            {
                if (word.Equals("Features", StringComparison.CurrentCultureIgnoreCase))
                    isInFeature = true;
                if (isInFeature)
                    path = Path.Combine(path, word);
            }

            var res = Path.Combine(path, "Views", "{0}.cshtml");

            return res;
        }
    }
}