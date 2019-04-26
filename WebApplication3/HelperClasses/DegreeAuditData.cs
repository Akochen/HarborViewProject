using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class DegreeAuditData
    {
        public List<DegreeAuditMajorReqs> majorReqs;

        public DegreeAuditData(List<DegreeAuditMajorReqs> majorReqs)
        {
            this.majorReqs = majorReqs;
        }
    }
}