﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class DkorLogin
	{
		private IWebDriver driver;
		private WebDriverWait wait;

		[SetUp]
		public void Start()
		{
			FirefoxOptions options = new FirefoxOptions
			{
				BrowserExecutableLocation = @"C:\Program Files\Nightly\Firefox Nightly\firefox.exe" // Nightly
				//BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe" // Standard
			};
			driver = new FirefoxDriver(options);
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void NightlyLogin()
		{
			driver.Url = "http://localhost:8080/litecart/admin/login.php";
			driver.FindElement(By.Name("username")).SendKeys("admin");
			driver.FindElement(By.Name("password")).SendKeys("admin");
			driver.FindElement(By.Name("login")).Click();
		}

		[TearDown]
		public void Stop()
		{
			driver.Quit();
			driver = null;
		}
	}
}