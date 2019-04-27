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
        public List<DegreeAuditOutOfMajorReqs> outOfMajorRequirements;

        public DegreeAuditData(List<DegreeAuditMajorReqs> majorReqs, List<DegreeAuditElectives> majorElectives, List<DegreeAuditOutOfMajorReqs> outOfMajorRequirements)
        {
            this.majorReqs = majorReqs;
            this.majorElectives = majorElectives;
            this.outOfMajorRequirements = outOfMajorRequirements;
        }
    }
}