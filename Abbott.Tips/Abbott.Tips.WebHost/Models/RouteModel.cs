using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abbott.Tips.WebHost.Models
{
    public class RouteModel
    {
        public string Path { get; set; }

        public string name { get; set; }

        public string redirect { get; set; }

        public string component { get; set; }

        public bool leaf { get; set; }

        public RouteMeta meta { get; set; }

        public List<RouteModel> children { get; set; }
    }

    public class RouteMeta
    {
        public string title { get; set; }

        public bool hidden { get; set; }

        public bool requiredLogin { get; set; }
    }
}
