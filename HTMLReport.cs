using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using OpenQA.Selenium;

//using Selenium;

namespace TREDS.Virginia.Gov.QA.Utility.Helper
{
   
    public class HTMLReport
    {

         private System.IO.StreamWriter _reportWriter;
       //  System.IO.StreamWriter createReportSummary;
         public string TestCaseName; 
         private string _ReportTime;
         private string _ReportPath;
         private string TR = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td bgcolor='{6}'>{4}</td><td>{5}</td></tr>";

          public  HTMLReport(String ReportPath, string ReportTitle )
          {
              if (File.Exists(ReportPath))
                  File.Delete(ReportPath);

              _ReportTime = System.DateTime.Now.ToString("yyyy-dd-MM.hh.mm.ss");
              _ReportPath = ReportPath;
              WriteReportTitle(ReportTitle);
              WriteReportHeader();
                
          }

          private void WriteReportTitle(String TitleName)
          {
              using (_reportWriter = new System.IO.StreamWriter(_ReportPath, true))
              {
                  _reportWriter.WriteLine("<html><head><title>" + TitleName + " </title></head>");
                  _reportWriter.WriteLine("<body bgcolor='#FFFFFF'><p align='left' size=9></p><p size=30><center><h2   style=color:Brown;>Test Execution Details Report</h2></center></p><p align='right' size=12><b>Date:" + "[" + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToLongTimeString() + "] " + "</b></p>");
              }
          }


        private void WriteReportHeader()
        {
            using (_reportWriter = new System.IO.StreamWriter(_ReportPath, true))
            {
                _reportWriter.WriteLine("<h2 align='center'>Regresison Test</h2><table border='1' align='center'><tr><td>Test Cases Passed:</td><td></td><tr><td>Test Cases Failed:</td><td></td></tr><tr><td>Started on:</td><td>" + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToLongTimeString() + "</td></tr><tr><td>Total time:</td><td></td></tr></table>");
                _reportWriter.WriteLine("<table width='100%' border='1' class='invocation-passed'><tr><td width='15%' td bgcolor='#ADD8E6'><b>TEST CASE NAME</b></td><td width='18%' bgcolor='#ADD8E6'><b>TEST STEP NAME</b></td><td width='20%' bgcolor='#ADD8E6'><b>EXPECTED RESULT</b></td><td width='20%' bgcolor='#ADD8E6'><b>ACTUAL RESULT</b></td><td width='8%' bgcolor='#ADD8E6'><b>RESULTS</b></td><td width='10%' bgcolor='#ADD8E6'><b>COMMENTS</b></td></tr>");
            }
        }

        public void WriteResult(string TestStep, string ExpectedResult, string ActualResult, string Status, string Comments)
        {
            string ScreenShotName;
            using (_reportWriter = new System.IO.StreamWriter(_ReportPath, true))
            {
                if (Status.ToUpper() == "PASS")
                {
                    _reportWriter.WriteLine(string.Format(TR, TestCaseName, TestStep, ExpectedResult, ActualResult, Status, Comments, "green"));
                }
                else if (Status.ToUpper() == "FAIL")
                {
                    ScreenShotName = TestCaseName + "_" + TestStep + "_" + _ReportTime;
                    Comments = Comments + string.Format("Screen Shot Name: <a href='{0}.png'>{0}</a>", ScreenShotName) ;
                    //  _reportWriter.WriteLine("<tr><td>" + TestCaseName + "</td><td>" + TestStep + "</td><td>" + ExpectedResult + "</td><td>" + ActualResult + "</td><td bgcolor='red'>" + Status + "</td><td>" + Comments + "</td></tr>");
                    _reportWriter.WriteLine(string.Format(TR, TestCaseName, TestStep, ExpectedResult, ActualResult, Status, Comments, "red"));
                    BrowserContainer.ScreenShot(ScreenShotName);
                }
                
            }
        }

        


       
        /*
        public void createReportSummaryLayout()
        {

            createReportSummary.WriteLine("Test Report Summary");
            createReportSummary.WriteLine("");
            createReportSummary.WriteLine("Project: " + "Treds");
            createReportSummary.WriteLine("");
            createReportSummary.WriteLine("Version: " + "1.8");
            createReportSummary.WriteLine("");
            createReportSummary.WriteLine("Environment: " + "Test");
            createReportSummary.WriteLine("");
            createReportSummary.WriteLine("Url: " + "www.google.com");
            createReportSummary.WriteLine("");
            createReportSummary.WriteLine("Browser: " + "chrome");
            createReportSummary.WriteLine("");
            createReportSummary.WriteLine("Date: " + System.DateTime.Now.ToShortDateString());
            createReportSummary.WriteLine("");
            createReportSummary.WriteLine("Time: " + System.DateTime.Now.ToShortTimeString());
            createReportSummary.WriteLine("");
            createReportSummary.Close();
        }
         * */
        public void writereport(string reporttext)
        {
            using (_reportWriter = new System.IO.StreamWriter(_ReportPath, true))
            {
                //reportmsg.WriteLine("");
                _reportWriter.WriteLine(reporttext);
                //reportmsg.Close();
            }
        }

        //public void Close()
        //{
        //    _reportWriter.Close();
        //}
    }

}

