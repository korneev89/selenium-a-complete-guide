using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class TestBase
	{
		public IWebDriver driver;
		public WebDriverWait wait;

		[TearDown]
		public void Stop()
		{
			driver.Quit();
			driver = null;
		}
	
		public void LoginAsAdmin()
		{
			driver.Url = "http://localhost:8080/litecart/admin/login.php";
			driver.FindElement(By.Name("username")).SendKeys("admin");
			driver.FindElement(By.Name("password")).SendKeys("admin");
			driver.FindElement(By.Name("login")).Click();
		}
	}
}