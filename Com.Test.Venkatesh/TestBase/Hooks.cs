using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Com.Test.Venkatesh.TestBase
{
    [Binding]
    public class Hooks
    {
        BrowserFactory browserFactory = new BrowserFactory();
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private readonly ScenarioContext _scenarioContext;
        private static FeatureContext _featureContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [BeforeTestRun]
        public static  void BeforeTestRun()
        {
            //Initialize the Report
            var date = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss ");

            string binLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string reportPath = binLocation.Substring(0, binLocation.IndexOf("bin\\Debug"));

            var htmlReporter = new ExtentHtmlReporter( reportPath+ "Report\\ExtentReport_" + date+ ".html");
            htmlReporter.Configuration().ChartLocation = ChartLocation.Top;
            htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            htmlReporter.Configuration().DocumentTitle = "Automation Test Execution";
            htmlReporter.Configuration().ReportName = "Autmation Test Report";
            htmlReporter.Configuration().Theme = Theme.Standard;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();
        }

        [BeforeScenario]
        public void LaunchApplication()
        {
            string browser = ConfigurationManager.AppSettings["BrowserType"];

            string url = ConfigurationManager.AppSettings["url"];

            DriverFactory.GetInstance().SetDriver(browserFactory.CreateBrowserInstance(browser));

            DriverFactory.GetInstance().GetDriver().Manage().Window.Maximize();
            DriverFactory.GetInstance().GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            
            //Sceanrio name
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title); 
        }

        [AfterScenario]
        public void TearDown()
        {
            DriverFactory.GetInstance().CloseBrowser();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            //Feature Name
            featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            //ExtentFactory.GetInstance().SetExtent(featureName);

        }
        [AfterFeature]
        public static void AfterFeature()
        {
           
        }

        [AfterStep]
        public void ReportingSteps()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();


            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                var mediaEntity = CaptureScreenshotAndRetuenModel(_scenarioContext.ScenarioInfo.Title.Trim());

                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException, mediaEntity);
                   
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException, mediaEntity);
                    
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                }
            }
        }

        public MediaEntityModelProvider CaptureScreenshotAndRetuenModel(string name)
        {
            var screenshot = ((ITakesScreenshot)DriverFactory.GetInstance().GetDriver()).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build();
        }
    }
}
