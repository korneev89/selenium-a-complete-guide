using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex10 : TestBase
	{
		[SetUp]
		public void Start()
		{
			//driver = new ChromeDriver();
			driver = new FirefoxDriver();
			//driver = new InternetExplorerDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void CheckProductPageInfo()
		{
			driver.Url = "http://localhost:8080/litecart/en/";

			var regPriceC = driver.FindElement(By.CssSelector("#box-campaigns .regular-price"));
			var campPriceC = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price"));

			var campaignBlockInfo = new ProductInfo(
				driver.FindElement(By.CssSelector("#box-campaigns .name")).GetAttribute("textContent"),
				regPriceC.GetAttribute("textContent"),
				campPriceC.GetAttribute("textContent")
				);
			// checks for page with campaigh block
			CheckFontSizeAndColor(regPriceC, campPriceC);

			driver.FindElement(By.CssSelector("#box-campaigns a.link")).Click();

			var regPriceP = driver.FindElement(By.CssSelector("#box-product .regular-price"));
			var campPriceP = driver.FindElement(By.CssSelector("#box-product .campaign-price"));

			var productPageInfo = new ProductInfo(
				driver.FindElement(By.CssSelector("#box-product .title")).GetAttribute("textContent"),
				regPriceP.GetAttribute("textContent"),
				campPriceP.GetAttribute("textContent")
				);

			// checks for page with campaigh block
			CheckFontSizeAndColor(regPriceP, campPriceP);

			Assert.AreNotEqual(campaignBlockInfo, productPageInfo);
			Debug.WriteLine(campaignBlockInfo);
			Debug.WriteLine(productPageInfo);
		}

		private static void CheckFontSizeAndColor(IWebElement regPrice, IWebElement campPrice)
		{
			var regPriceColor = ColorHelper.ParseColor(regPrice.GetCssValue("color"));
			ColorIsGrey(regPriceColor);
			Assert.AreEqual(regPrice.GetCssValue("text-decoration-line"), "line-through");

			var campPriceColor = ColorHelper.ParseColor(campPrice.GetCssValue("color"));
			ColorIsRed(campPriceColor);
			Assert.AreEqual(campPrice.GetCssValue("font-weight"), "700");

			var regPriceSize = regPrice.GetCssValue("font-size");
			var rSize = (int)float.Parse(regPriceSize.Substring(0, regPriceSize.Length - 2), CultureInfo.InvariantCulture);

			var campPriceSize = campPrice.GetCssValue("font-size");
			decimal cSize = (int)float.Parse(campPriceSize.Substring(0, campPriceSize.Length - 2), CultureInfo.InvariantCulture);

			Assert.Greater(cSize, rSize);
		}

		private static void ColorIsGrey(Color color)
		{
			Assert.AreEqual(color.B, color.G);
			Assert.AreEqual(color.B, color.R);
		}

		private static void ColorIsRed(Color color)
		{
			Assert.AreEqual(color.G, 0);
			Assert.AreEqual(color.B, 0);
		}

		public class ProductInfo
		{
			public ProductInfo() { }
			public ProductInfo(string name, string regularPrice, string campaignPrice)
			{
				Name = name;
				RegularPrice = regularPrice;
				CampaignPrice = campaignPrice;
			}
			public string Name { get; set; }
			public string RegularPrice { get; set; }
			public string CampaignPrice { get; set; }

			public override string ToString()
			{
				return String.Join(" - ", Name, RegularPrice, CampaignPrice);
			}
		}
	}
}
