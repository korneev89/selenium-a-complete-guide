using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex14 : TestBase
	{
		[SetUp]
		public void Start()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void WorkingWithWindows()
		{
			LoginAsAdmin();
			driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
			driver.FindElement(By.CssSelector("#content > div > a")).Click();

			var externalLinksCount = driver.FindElements(By.CssSelector("form > table > tbody a[target=_blank]")).Count;
			for (var i = 0; i < externalLinksCount; i++)
			{
				// current window
				var originalWindow = driver.CurrentWindowHandle;

				// all opened windows
				var existingWindows = driver.WindowHandles;

				//click to external link
				driver.FindElements(By.CssSelector("form > table > tbody a[target=_blank]"))[i].Click();

				//wait until open opened
				var newWindow = wait.Until(ThereIsWindowOtherThan(existingWindows));

				//switch to new window
				driver.SwitchTo().Window(newWindow);

				// for clarity
				Thread.Sleep(500);

				//close new window
				driver.Close();

				//switch to main window
				driver.SwitchTo().Window(originalWindow);
			}
		}
	}
}
