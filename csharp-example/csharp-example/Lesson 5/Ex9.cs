using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex9 : TestBase
	{
		[SetUp]
		public void Start()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void CheckAllCountriesOrder()
		{
			LoginAsAdmin();
			driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
			var countriesCount = driver.FindElements(By.CssSelector("tbody > tr.row")).Count;
			string currentCounrty;
			string prevCountry;

			// initial value i=1 because we don't wont compare 1st country with 0th
			for (int i = 1; i < countriesCount; i++)
			{
				prevCountry = driver
					.FindElements(By.CssSelector("tbody > tr.row a:not([title=Edit])"))[i - 1]
					.GetAttribute("textContent");

				currentCounrty = driver
					.FindElements(By.CssSelector("tbody > tr.row a:not([title=Edit])"))[i]
					.GetAttribute("textContent");

				int compareResult = string.Compare(currentCounrty, prevCountry);
				Assert.Greater(compareResult, 0);
				Debug.WriteLine(String.Concat(prevCountry," < ",currentCounrty));
			}
		}

		[Test]
		public void CheckZonesOrder()
		{
			LoginAsAdmin();
			driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
			var countriesCount = driver.FindElements(By.CssSelector("tbody > tr.row")).Count;
			string zonesCount;
			string prevZone;
			string currentZone;

			for (int i = 0; i < countriesCount; i++)
			{
				zonesCount = driver
					.FindElements(By.CssSelector("tbody > tr.row > td:nth-child(6)"))[i]
					.GetAttribute("textContent");

				int z = Int32.Parse(zonesCount);
				if (z > 0)
				{
					Debug.WriteLine("Check zones order...");
					driver.FindElements(By.CssSelector("tbody > tr.row a:not([title=Edit])"))[i].Click();

					for (int k = 0; k < z-1; k++)
					{
						prevZone = driver
							.FindElements(By.CssSelector("tbody > tr:not(.header) > td:nth-child(3):not(#content)"))[k]
							.GetAttribute("textContent");

						currentZone = driver
							.FindElements(By.CssSelector("tbody > tr:not(.header) > td:nth-child(3):not(#content)"))[k+1]
							.GetAttribute("textContent");

						int compareResult = string.Compare(currentZone, prevZone);
						Assert.Greater(compareResult, 0);
						Debug.WriteLine(String.Concat(prevZone, " < ", currentZone));
					}

					driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
				}
			}
		}

		[Test]
		public void CheckGeoZonesOrder()
		{
			LoginAsAdmin();
			string geoZonesURL = "http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones";
			driver.Url = geoZonesURL;

			var countriesCount = driver.FindElements(By.CssSelector("tbody > tr.row")).Count;
			int zonesCount;
			string prevZone;
			string currentZone;

			for (int i = 0; i < countriesCount; i++)
			{
				driver.FindElements(By.CssSelector("tbody > tr.row > td:nth-child(3) > a"))[i].Click();

				// throw the last row
				zonesCount = driver.FindElements(By.CssSelector("table#table-zones > tbody > tr:not(.header)")).Count - 1;

				Debug.WriteLine("Check zones order...");

				for (int k = 0; k < zonesCount-1; k++)
				{
					prevZone = driver
						.FindElements(By.CssSelector("table#table-zones > tbody > tr:not(.header) > td:nth-child(3) > select > option[selected]"))[k]
						.GetAttribute("textContent");

					currentZone = driver
						.FindElements(By.CssSelector("table#table-zones > tbody > tr:not(.header) > td:nth-child(3) > select > option[selected]"))[k + 1]
						.GetAttribute("textContent");

					int compareResult = string.Compare(currentZone, prevZone);
					Assert.Greater(compareResult, 0);
					Debug.WriteLine(String.Concat(prevZone, " < ", currentZone));
				}

				driver.Url = geoZonesURL;
			}
		}
	}
}
