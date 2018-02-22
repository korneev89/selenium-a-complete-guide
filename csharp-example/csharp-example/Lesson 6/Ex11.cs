using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex11 : TestBase
	{
		[SetUp]
		public void Start()
		{
			driver = new ChromeDriver();

			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void NewUserRegistration()
		{
			driver.Url = "http://localhost:8080/litecart/en/";

			driver.FindElement(By.CssSelector("div#box-account-login a")).Click();

			var d = DateTime.Now.ToString("yyyyMMddHHmmss");

			var userInfo = new UserInfo()
			{
				address1 = "test_address1",
				address2 = "test_address2",
				city = "test_city",
				company = "test_company",
				email = "test" + d.ToString() + "@ya.ru",
				firstname = "Ivan_test",
				lastname = "Ivanov_test",
				password = "paSSw0rd!",
				phone = "+79876543210",
				postcode = "12345",
				taxId = "testTaxId"
			};

			var countrySelect = new SelectElement(driver.FindElement(By.CssSelector("select[name=country_code]")));
			countrySelect.SelectByText("United States");

			driver.FindElement(By.CssSelector("input[name=tax_id]")).SendKeys(userInfo.taxId);
			driver.FindElement(By.CssSelector("input[name=company]")).SendKeys(userInfo.company);
			driver.FindElement(By.CssSelector("input[name=firstname]")).SendKeys(userInfo.firstname);
			driver.FindElement(By.CssSelector("input[name=lastname]")).SendKeys(userInfo.lastname);
			driver.FindElement(By.CssSelector("input[name=address1]")).SendKeys(userInfo.address1);
			driver.FindElement(By.CssSelector("input[name=address2]")).SendKeys(userInfo.address2);
			driver.FindElement(By.CssSelector("input[name=postcode]")).SendKeys(userInfo.postcode);
			driver.FindElement(By.CssSelector("input[name=city]")).SendKeys(userInfo.city);

			driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(userInfo.email);
			driver.FindElement(By.CssSelector("input[name=phone]")).SendKeys(userInfo.phone);
			driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(userInfo.password);
			driver.FindElement(By.CssSelector("input[name=confirmed_password]")).SendKeys(userInfo.password);

			//Thread.Sleep(500);
			var zoneSelect = new SelectElement(driver.FindElement(By.CssSelector("select[name=zone_code]")));
			zoneSelect.SelectByIndex(0);

			driver.FindElement(By.CssSelector("button[name=create_account]")).Click();

			driver.FindElement(By.CssSelector("#box-account > div > ul > li:nth-child(4) > a")).Click();

			driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(userInfo.email);
			driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(userInfo.password);
			driver.FindElement(By.CssSelector("button[name=login]")).Click();

			driver.FindElement(By.CssSelector("#box-account > div > ul > li:nth-child(4) > a")).Click();
		}

		public class UserInfo
		{
			public UserInfo() { }
			public string taxId { get; set; }
			public string company { get; set; }
			public string firstname { get; set; }
			public string lastname { get; set; }
			public string address1 { get; set; }
			public string address2 { get; set; }
			public string postcode { get; set; }
			public string city { get; set; }
			public string email { get; set; }
			public string phone { get; set; }
			public string password { get; set; }
		}
	}
}
