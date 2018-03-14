using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class FirefoxTests : TestBaseOld
	{
		[SetUp]
		public void Start()
		{
			FirefoxOptions options = new FirefoxOptions
			{
				BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe" // Standard
			};
			driver = new FirefoxDriver(options);
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void FirefoxLogin()
		{
			driver.Url = "http://localhost:8080/litecart/admin/login.php";
			driver.FindElement(By.Name("username")).SendKeys("admin");
			driver.FindElement(By.Name("password")).SendKeys("admin");
			driver.FindElement(By.Name("login")).Click();
		}
	}
}