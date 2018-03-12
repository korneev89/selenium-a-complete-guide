using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex18 : TestBaseOld
	{
		[SetUp]
		public void Start()
		{
			proxy = new Proxy
			{
				Kind = ProxyKind.Manual,
				HttpProxy = "localhost:8888"
			};

			ChromeOptions options = new ChromeOptions
			{
				Proxy = proxy
			};

			driver = new ChromeDriver(options);
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void UsingFiddler()
		{
			LoginAsAdmin();
			var logTypes = driver.Manage().Logs.AvailableLogTypes;
			driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1";
			var itemsCount = driver.FindElements(By.CssSelector("img[style*='margin-left: 32px; width: 16px; height: 16px; vertical-align: bottom;'] + a")).Count;

			for (var i = 0; i < itemsCount; i++)
			{
				driver.FindElements(By.
					CssSelector("img[style*='margin-left: 32px; width: 16px; height: 16px; vertical-align: bottom;'] + a"))[i].
						Click();

				foreach (var logType in logTypes)
				{
					var logsCount = driver.Manage().Logs.GetLog(logType).Count;
					var infoMessage = String.Concat("log type: ", logType, ", logs count: ", logsCount);
					if (logType != "performance")
					{
						Assert.AreEqual(logsCount, 0);
					}
					Debug.WriteLine(infoMessage);
				}

				driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1";
			}

		}
	}
}
