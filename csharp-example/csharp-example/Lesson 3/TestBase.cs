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
	}
}