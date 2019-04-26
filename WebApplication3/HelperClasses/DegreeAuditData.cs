using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class DegreeAuditData
    {
        public List<DegreeAuditMajorReqs> majorReqs;
        public List<DegreeAuditElectives> majorElectives;

        public DegreeAuditData(List<DegreeAuditMajorReqs> majorReqs, List<DegreeAuditElectives> majorElectives)
        {
            this.majorReqs = majorReqs;
            this.majorElectives = majorElectives;
        }
    }
}