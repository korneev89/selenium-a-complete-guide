using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	internal class ProductPage : Page
	{
		[FindsBy(How = How.CssSelector, Using = "button[name=add_cart_product]")]
		internal IWebElement AddToCartButton;

		[FindsBy(How = How.CssSelector, Using = "select[name*=options]")]
		internal IWebElement SizeSelector;

		[FindsBy(How = How.CssSelector, Using = "span.quantity[style]")]
		internal IWebElement ProductsCount;

		public ProductPage(IWebDriver driver) : base(driver)
		{
			PageFactory.InitElements(driver, this);
		}

		internal void SelectSizeByIndex(int index)
		{
			var sizeSelect = new SelectElement(SizeSelector);
			sizeSelect.SelectByIndex(index);
		}

		internal void Checkout()
		{
			driver.FindElements(By.CssSelector("a.link"))[0].Click();
		}
	}
}
