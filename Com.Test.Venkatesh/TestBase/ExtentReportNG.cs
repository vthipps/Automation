using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Com.Test.Venkatesh.TestBase
{
    public class ExtentReportNG
    {
        static ExtentReports extent;

        public static ExtentReports SetUpExtentReport()
        {
            return null;
        }

        public static void CloseReport()
        {
            extent.Flush();
        }
    }
}
