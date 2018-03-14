using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
	internal class CartPage : Page
	{
		public CartPage(IWebDriver driver) : base(driver)
		{
			PageFactory.InitElements(driver, this);
		}

		internal void Open()
		{
			driver.Url = "http://localhost:8080/litecart/en/checkout";
		}

		[FindsBy(How = How.CssSelector, Using = "button[name=remove_cart_item]")]
		internal IWebElement RemoveCartItemButton;
	}
}