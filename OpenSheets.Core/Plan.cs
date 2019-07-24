using System;
using System.Collections.Generic;

namespace OpenSheets.Core
{
    public class Plan
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Interval { get; set; }
        public int Price { get; set; }
        public Dictionary<PlanValue, long> Parameters { get; set; }
        public HashSet<PlanFlag> Flags { get; set; }
        public PlanKind Kind { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}