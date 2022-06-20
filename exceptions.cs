using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishingManagment
{
    public class PrintingFullException : Exception
    {
        protected double overLimit;
        public PrintingFullException(double added, double efficiency, double load, string msg)
            : base(msg)
        {
            this.overLimit = load + added - efficiency;
        }
    }
    
    public class PrintNotAssignedException : Exception
    {
        public Print print;
        public PrintNotAssignedException(Print print)
        {
            this.print = print;
        }
    }
}
