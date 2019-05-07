using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class UpdateSectionHelper
    {
        public List<string> times;
        public List<string> professors;

        public UpdateSectionHelper(List<string> times, List<string> professors)
        {
            this.times = times;
            this.professors = professors;
        }
    }
}