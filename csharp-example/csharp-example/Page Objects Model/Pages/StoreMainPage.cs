using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
	internal class StoreMainPage : Page
	{
		public StoreMainPage(IWebDriver driver) : base(driver)
		{
			PageFactory.InitElements(driver, this);
		}

		internal void Open()
		{
			driver.Url = "http://localhost:8080/litecart/en/";
		}

		[FindsBy(How = How.CssSelector, Using = "#box-most-popular > div > ul > li")]
		internal IWebElement MostPopularProduct;

		[FindsBy(How = How.CssSelector, Using = "span.quantity[style]")]
		internal IWebElement ProductsCount;
	}
}