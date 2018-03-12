using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class DkorExampleTest
	{
		private IWebDriver driver;
		private WebDriverWait wait;

		[SetUp]
		public void Start()
		{
			//ChromeOptions options = new ChromeOptions();
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			//Debug.WriteLine(options);
		}

		[Test]
		public void ExampleTest()
		{
			driver.Url = "http://www.google.com/";
			driver.FindElement(By.Name("q")).SendKeys("webdriver");
			driver.FindElement(By.Name("q")).Submit();
			wait.Until(ExpectedConditions.TitleIs("webdriver - Google Search"));
		}

		[TearDown]
		public void Stop()
		{
			driver.Quit();
			driver = null;
		}
	}
}