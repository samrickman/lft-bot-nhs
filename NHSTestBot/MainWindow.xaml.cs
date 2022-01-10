using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Timers;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace NHSTestBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }


           
        private bool fakeOpenBrowser()
        {
            System.Threading.Thread.Sleep(500);
            return true;
        }
        private bool OpenBrowser()
        {
            IWebDriver driver = new ChromeDriver(Environment.CurrentDirectory);
            int sleepTime = 3000; // 3 seconds to see if URL changes (i.e. site down)
            int elementLoadTime = 3; // Max seconds for element to load 
            string url = "https://test-for-coronavirus.service.gov.uk/order-lateral-flow-kits/";
            string serviceDownUrl = "https://test-for-coronavirus.service.gov.uk/order-lateral-flow-kits/service-unavailable";
            //string password = passwordTextBox.Text;
            string password = passwordTextBox.Password;
            try
            {
                driver.Url = url;
                
                // Set wait time
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, elementLoadTime));

                // Accept cookies
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[1]/div[1]/div/div/div[2]/button"))).Click();

                // Check we are not redirected quickly
                System.Threading.Thread.Sleep(sleepTime);
                if (driver.Url == serviceDownUrl)
                {
                    driver.Quit();
                    return false;
                }
                
                var allInput = driver.FindElements(By.XPath("//input"));
                var buttons = driver.FindElements(By.XPath("//button"));

                // No symptoms
                foreach (var input in allInput)
                {
                    if (input.GetDomProperty("value") == ("false")) 
                    {
                        input.Click();
                    }
                }

                // Continue
                foreach(var button in buttons)
                {
                    if (button.Text.Contains("Continue")){ 
                        button.Click(); 
                    }
                }

                // Wait for next page to load
                System.Threading.Thread.Sleep(sleepTime);
                // Sign in
                buttons = driver.FindElements(By.XPath("//button"));
                foreach (var button in buttons)
                {
                    if (button.Text.Contains("Sign in"))
                    {
                        button.Click();
                    }
                }

                // Enter email address
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("user-email"))).SendKeys(email.Text + Keys.Enter);


                // Wait for next page to load and click continue
                System.Threading.Thread.Sleep(sleepTime);
                buttons = driver.FindElements(By.XPath("//button"));
                foreach (var button in buttons)
                {
                    if (button.GetDomAttribute("loginanalyticsbuttonclick") == "log-in-password")
                    {
                        button.Click();
                    }
                }

                // Enter password
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("password-input"))).SendKeys(password + Keys.Enter);

                // Wait for next page to load
                System.Threading.Thread.Sleep(sleepTime);

                // Do not work for NHS
                allInput = driver.FindElements(By.XPath("//input"));
                buttons = driver.FindElements(By.XPath("//button"));
                // No symptoms
                foreach (var input in allInput)
                {
                    if (input.GetDomProperty("value") == ("false"))
                    {
                        input.Click();
                    }
                }

                foreach (var button in buttons)
                {
                    if (button.Text.Contains("Continue"))
                    {
                        button.Click();
                    }
                }

                // Wait for next page
                System.Threading.Thread.Sleep(sleepTime);

                // Check answers - confirm
                buttons = driver.FindElements(By.XPath("//button"));
                foreach (var button in buttons)
                {
                    if (button.Text.Contains("Save and continue"))
                    {
                        button.Click();
                    }
                }

                // See if redirected to nothing available
                System.Threading.Thread.Sleep(sleepTime);

                if (driver.Url == serviceDownUrl)
                {
                    driver.Quit();
                    Console.WriteLine("false");
                    return false;
                }

                // See if we get last 24 hours message
//                var ps = driver.FindElements(By.XPath("//p"));
                if (driver.Url.Contains("problem"))
                {

                    driver.Quit();
                    Console.WriteLine("wait24");
                    return false;
                    
                }


                var allSpan = driver.FindElements(By.XPath("//span"));
                buttons = driver.FindElements(By.XPath("//button"));

                // Confirm order
                foreach (var span in allSpan)
                {
                    if (span.Text.Contains("confirm"))
                    {
                        span.Click();
                    }
                }

                // Continue
                foreach (var button in buttons)
                {
                    if (button.Text.Contains("Place order"))
                    {
                        button.Click();
                    }
                }

                Console.WriteLine("true");
                driver.Quit();
                return true;

            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Cannot resolve site. Check internet connection.",
                    "Cannot connect"
                );

                driver.Quit();
                return false;
            }


        }
        private string myValue;
        public string StatusText
        {
            get { return myValue; }
            set
            {
                myValue = value;
                RaisePropertyChanged("StatusText");
            }
        }

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private bool orderTests()
        {
            const int numCharsStartMessage = 50; // Keep the start messsage when flush the status box

            const int maxLines = 15;
            int numLines;
            if (String.IsNullOrEmpty(StatusText))
            {
                StatusText = "Starting attempts to order at: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            }
            var getTests = OpenBrowser();
            if (!getTests)
            {
                string newLine = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ": Site unavailable. Retrying in 60 seconds...";
                StatusText = StatusText + "\n" + newLine;

                
                numLines = StatusText.Split('\n').Length;

                string startMessage = StatusText.Substring(0, numCharsStartMessage);
                
                if(numLines > maxLines)
                {
                    const int numLinesToDelete = 10;
                    string truncatedLines = String.Join("\n", StatusText.Split("\n".ToCharArray()).Skip(numLinesToDelete).ToArray());
                    StatusText = startMessage + "\n<truncated>\n" + truncatedLines;
                    
                }
                


            }
            return getTests;
        }
        private async void orderButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(passwordTextBox.Password))
            {
                MessageBox.Show("Please enter your password", "Error");
                return;
            }  
            int sleepTime = 60; // change to 60 seconds
            
            bool testsOrdered = false;
            while(!testsOrdered)
            {
                testsOrdered = orderTests();
                await Task.Delay(TimeSpan.FromSeconds(sleepTime));
            }
            StatusText = StatusText + "\n" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + ":  Tests ordered successfully!";
        }
    }
}
