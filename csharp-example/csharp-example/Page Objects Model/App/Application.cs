using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	public class Application
	{
		private IWebDriver driver;
		private WebDriverWait wait;

		private AdminPanelLoginPage adminPanelLoginPage;
		private StoreMainPage storeMainPage;
		private ProductPage productPage;
		private CartPage cartPage;

		public Application()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			adminPanelLoginPage = new AdminPanelLoginPage(driver);
			storeMainPage = new StoreMainPage(driver);
		}

		internal void LoginIfNotLoggedIn()
		{
			if (adminPanelLoginPage.Open().IsOnThisPage())
			{
				adminPanelLoginPage.EnterUsername("admin").EnterPassword("admin").SubmitLogin();
			}
		}

		internal void AddProductsToCart(int count)
		{
			storeMainPage.Open();
			var productQuantity = 0;

			while (productQuantity < count)
			{
				storeMainPage.Open();
				storeMainPage.MostPopularProduct.Click();

				wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("button[name=add_cart_product]")));
				productPage = new ProductPage(driver);

				if (IsElementPresent(driver, By.CssSelector(("select[name*=options]"))))
				{
					productPage.SelectSizeByIndex(1);
				}

				productPage.AddToCartButton.Click();

				wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("span.quantity[style]")));

				productQuantity = GetProductQuantityOnProductPage();
			}
		}

		private int GetProductQuantityOnProductPage()
		{
			return Int32.Parse(productPage.ProductsCount.GetAttribute("textContent"));
		}
		private int GetProductQuantityOnCartPage()
		{
			return driver.FindElements(By.CssSelector("td.item")).Count;
		}

		internal void CheckoutCart()
		{
			productPage.Checkout();
		}

		internal void DeleteProductsFromCart()
		{
			cartPage = new CartPage(driver);
			var productQuantity = GetProductQuantityOnCartPage();

			while (productQuantity > 0)
			{
				var productItem = driver.FindElement(By.CssSelector("td.item"));
				wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("button[name=remove_cart_item]")));
				cartPage.RemoveCartItemButton.Click();

				wait.Until(ExpectedConditions.StalenessOf(productItem));

				productQuantity = GetProductQuantityOnCartPage();
			}
		}

		internal Boolean IsElementPresent(IWebDriver driver, By locator)
		{
			try
			{
				wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
				return driver.FindElements(locator).Count > 0;
			}
			finally
			{
				wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			}
		}

		public void Quit()
		{
			driver.Quit();
		}
	}
}