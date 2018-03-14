using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex13 : TestBaseOld
	{
		[SetUp]
		public void Start()
		{
			driver = new ChromeDriver();

			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void WorkingWithCart()
		{
			var productQuantity = 0;
			//add products to cart
			while (productQuantity < 3)
			{
				driver.Url = "http://localhost:8080/litecart/en/";
				driver.FindElement(By.CssSelector("#box-most-popular > div > ul > li")).Click();

				wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("button[name=add_cart_product]")));

				if (IsElementPresent(driver, By.CssSelector(("select[name*=options]"))))
				{
					var sizeSelect = new SelectElement(driver.FindElement(By.CssSelector("select[name*=options]")));
					sizeSelect.SelectByIndex(1);
				}

				driver.FindElement(By.CssSelector("button[name=add_cart_product]")).Click();

				wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("span.quantity[style]")));

				productQuantity = Int32.Parse(driver.FindElement(By.CssSelector("span.quantity[style]")).GetAttribute("textContent"));
			}

			// checkout
			driver.FindElements(By.CssSelector("a.link"))[0].Click();

			// delete products from cart
			while (productQuantity > 0)
			{
				var productItem = driver.FindElement(By.CssSelector("td.item"));

				wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[name=remove_cart_item]")));
				driver.FindElement(By.CssSelector("button[name=remove_cart_item]")).Click();

				wait.Until(ExpectedConditions.StalenessOf(productItem));

				productQuantity = driver.FindElements(By.CssSelector("td.item")).Count;
			}
		}
	}
}
