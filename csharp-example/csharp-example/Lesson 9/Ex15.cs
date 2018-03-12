using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex15 : TestBaseOld
	{
		[SetUp]
		public void Start()
		{
			var remoteAddress = "http://RUFRW-DKOR.cneu.cnwk:4444/wd/hub";
			DesiredCapabilities capability = new DesiredCapabilities();
			capability.SetCapability("browserName", "chrome");
			//capability.SetCapability("version", "64");
			//capability.SetCapability("platform", "VISTA");

			driver = new RemoteWebDriver(new Uri(remoteAddress), capability, TimeSpan.FromSeconds(60));
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void RemoteWebDriver()
		{
			driver.Url = "http://www.google.com/";
			driver.FindElement(By.Name("q")).SendKeys("webdriver");
			driver.FindElement(By.Name("q")).Submit();
			Thread.Sleep(10000);
			wait.Until(ExpectedConditions.TitleIs("webdriver - Google Search"));
		}
	}
}
