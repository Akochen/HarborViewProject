using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.HelperClasses
{
    public class NullException : Exception
    {
        public NullException(string message)
         : base(message)
        {
        }
    }
}



