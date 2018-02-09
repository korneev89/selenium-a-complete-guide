using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class ESRFirefoxTests : TestBase
	{
		[SetUp]
		public void Start()
		{
			FirefoxOptions options = new FirefoxOptions
			{
				BrowserExecutableLocation = @"C:\Program Files\ESRMozilla Firefox\firefox.exe", // ESR
				UseLegacyImplementation = true
			};
			driver = new FirefoxDriver(options);
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void ESRLogin()
		{
			driver.Url = "http://localhost:8080/litecart/admin/login.php";
			driver.FindElement(By.Name("username")).SendKeys("admin");
			driver.FindElement(By.Name("password")).SendKeys("admin");
			driver.FindElement(By.Name("login")).Click();
		}
	}
}