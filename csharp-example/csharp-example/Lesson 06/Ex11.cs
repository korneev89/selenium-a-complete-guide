using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex11 : TestBaseOld
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
				Address1 = "test_address1",
				Address2 = "test_address2",
				City = "test_city",
				Company = "test_company",
				Email = "test" + d.ToString() + "@ya.ru",
				Firstname = "Ivan_test",
				Lastname = "Ivanov_test",
				Password = "paSSw0rd!",
				Phone = "+79876543210",
				Postcode = "12345",
				TaxId = "testTaxId"
			};

			var countrySelect = new SelectElement(driver.FindElement(By.CssSelector("select[name=country_code]")));
			countrySelect.SelectByText("United States");

			driver.FindElement(By.CssSelector("input[name=tax_id]")).SendKeys(userInfo.TaxId);
			driver.FindElement(By.CssSelector("input[name=company]")).SendKeys(userInfo.Company);
			driver.FindElement(By.CssSelector("input[name=firstname]")).SendKeys(userInfo.Firstname);
			driver.FindElement(By.CssSelector("input[name=lastname]")).SendKeys(userInfo.Lastname);
			driver.FindElement(By.CssSelector("input[name=address1]")).SendKeys(userInfo.Address1);
			driver.FindElement(By.CssSelector("input[name=address2]")).SendKeys(userInfo.Address2);
			driver.FindElement(By.CssSelector("input[name=postcode]")).SendKeys(userInfo.Postcode);
			driver.FindElement(By.CssSelector("input[name=city]")).SendKeys(userInfo.City);

			driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(userInfo.Email);
			driver.FindElement(By.CssSelector("input[name=phone]")).SendKeys(userInfo.Phone);
			driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(userInfo.Password);
			driver.FindElement(By.CssSelector("input[name=confirmed_password]")).SendKeys(userInfo.Password);

			//Thread.Sleep(500);
			var zoneSelect = new SelectElement(driver.FindElement(By.CssSelector("select[name=zone_code]")));
			zoneSelect.SelectByIndex(0);

			driver.FindElement(By.CssSelector("button[name=create_account]")).Click();

			driver.FindElement(By.CssSelector("#box-account > div > ul > li:nth-child(4) > a")).Click();

			driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(userInfo.Email);
			driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(userInfo.Password);
			driver.FindElement(By.CssSelector("button[name=login]")).Click();

			driver.FindElement(By.CssSelector("#box-account > div > ul > li:nth-child(4) > a")).Click();
		}

		public class UserInfo
		{
			public UserInfo() { }
			public string TaxId { get; set; }
			public string Company { get; set; }
			public string Firstname { get; set; }
			public string Lastname { get; set; }
			public string Address1 { get; set; }
			public string Address2 { get; set; }
			public string Postcode { get; set; }
			public string City { get; set; }
			public string Email { get; set; }
			public string Phone { get; set; }
			public string Password { get; set; }
		}
	}
}
