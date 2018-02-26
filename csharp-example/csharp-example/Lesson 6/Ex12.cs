using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex12 : TestBase
	{
		[SetUp]
		public void Start()
		{
			driver = new ChromeDriver();

			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void AddProduct()
		{
			LoginAsAdmin();

			// "Catalog" menu item
			driver.FindElements(By.CssSelector("#box-apps-menu > li"))[1].Click();

			// in fact the original number of catalogs and products
			var productsCount = driver.FindElements(By.CssSelector("table.dataTable tr.row")).Count;

			// "Add New Product" button
			driver.FindElements(By.CssSelector("a.button"))[1].Click();
			var imageFileInfo = 
				new FileInfo(Path.Combine(
					Path.GetDirectoryName(
						Assembly.GetExecutingAssembly().Location), 
						@"images\product_image.png"));

			var productInfo = new ProductInfo()
			{
				Code = "12345",
				Description = "Product description",
				HeadTitle = "Head Title",
				Keywords = "best new fresh",
				MetaDescription = "product test sale",
				Name = "Product Name",
				PriceEUR = "60",
				PriceUSD = "70",
				PurchasePrice = "99.99",
				Quantity = "100",
				ShortDescription = "Short description",
				ImagePath = imageFileInfo.FullName,
				DateValidFrom = DateTime.Now.ToString("dd.MM.yyyy"),
				DateValidTo = DateTime.Now.AddMonths(6).ToString("dd.MM.yyyy")
			};

			// status - enabled
			driver.FindElements(By.CssSelector("input[name=status]"))[0].Click();

			driver.FindElement(By.CssSelector("input[name*=name]")).SendKeys(productInfo.Name);
			driver.FindElement(By.CssSelector("input[name=code]")).SendKeys(productInfo.Code);
			driver.FindElements(By.CssSelector("input[name*=product_groups]"))[2].Click();

			driver.FindElement(By.CssSelector("input[name=quantity]")).Clear();
			driver.FindElement(By.CssSelector("input[name=quantity]")).SendKeys(productInfo.Quantity);

			driver.FindElement(By.CssSelector("input[type=file]")).SendKeys(productInfo.ImagePath);

			driver.FindElement(By.CssSelector("input[name=date_valid_from]")).SendKeys(productInfo.DateValidFrom);

			driver.FindElement(By.CssSelector("input[name=date_valid_to]")).SendKeys(productInfo.DateValidTo);

			// Information tab
			driver.FindElement(By.CssSelector("td#content > form > div > ul > li:nth-child(2)")).Click();
			// do not use for chrome
			//Thread.Sleep(500);

			var manufacturer = new SelectElement(driver.FindElement(By.CssSelector("select[name=manufacturer_id]")));
			manufacturer.SelectByIndex(1);

			driver.FindElement(By.CssSelector("input[name=keywords]")).SendKeys(productInfo.Keywords);

			driver.FindElement(By.CssSelector("input[name*=short_description]")).SendKeys(productInfo.ShortDescription);

			driver.FindElement(By.CssSelector("div.trumbowyg-editor")).SendKeys(productInfo.Description);

			driver.FindElement(By.CssSelector("input[name*=head_title]")).SendKeys(productInfo.HeadTitle);

			driver.FindElement(By.CssSelector("input[name*=meta_description]")).SendKeys(productInfo.MetaDescription);

			// Prices tab
			driver.FindElement(By.CssSelector("td#content > form > div > ul > li:nth-child(4)")).Click();
			// do not use for chrome
			//Thread.Sleep(500);

			driver.FindElement(By.CssSelector("input[name=purchase_price]")).Clear();
			driver.FindElement(By.CssSelector("input[name=purchase_price]")).SendKeys(productInfo.PurchasePrice);

			var purchasePrise = new SelectElement(driver.FindElement(By.CssSelector("select[name=purchase_price_currency_code]")));
			purchasePrise.SelectByText("US Dollars");

			driver.FindElements(By.CssSelector("input[name*=prices]"))[0].SendKeys(productInfo.PriceUSD);
			driver.FindElements(By.CssSelector("input[name*=prices]"))[2].SendKeys(productInfo.PriceEUR);

			driver.FindElement(By.CssSelector("button[name=save]")).Click();
			
			var newProductsCount = driver.FindElements(By.CssSelector("table.dataTable tr.row")).Count;

			// if the product has been added, the quantity will increase (adding to the root directory)
			Assert.Greater(newProductsCount, productsCount);
		}

		public class ProductInfo
		{
			public ProductInfo() { }
			public string Name { get; set; }
			public string Code { get; set; }
			public string Quantity { get; set; }
			public string ImagePath { get; set; }
			public string Keywords { get; set; }
			public string ShortDescription { get; set; }
			public string Description { get; set; }
			public string HeadTitle { get; set; }
			public string MetaDescription { get; set; }
			public string PurchasePrice { get; set; }
			public string PriceUSD { get; set; }
			public string PriceEUR { get; set; }
			public string DateValidFrom { get; set; }
			public string DateValidTo { get; set; }
		}
	}
}
