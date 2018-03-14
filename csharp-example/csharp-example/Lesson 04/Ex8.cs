using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex8 : TestBaseOld
	{
		[SetUp]
		public void Start()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void StickersExist()
		{
			driver.Url = "http://localhost:8080/litecart";

			var ducks = driver.FindElements(By.CssSelector(".product.column.shadow.hover-light"));
			foreach (IWebElement duck in ducks)
			{
				Assert.IsTrue( duck.FindElements(By.CssSelector(".sticker")).Count == 1 );
				//Debug.WriteLine(duck.FindElement(By.CssSelector(".sticker")).Text);
			}
		}
	}
}
