using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
	[TestFixture]
	public class Ex7 : TestBase
	{
		[SetUp]
		public void Start()
		{
			driver = new ChromeDriver();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void CheckAllMenuItems()
		{
			LoginAsAdmin();
			var menuItemsCount = driver.FindElements(By.CssSelector("#box-apps-menu > li")).Count;

			for (int i = 0; i < menuItemsCount; i++)
			{
				driver.FindElements(By.CssSelector("#box-apps-menu > li"))[i].Click();
				wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#content h1")));
				//Debug.WriteLine(driver.FindElement(By.CssSelector("#content h1")).Text);
				
				var subMenuItemsCount = driver.FindElements(By.CssSelector("#box-apps-menu > li.selected li")).Count;

				// initial value of variable k = 1 because skipping 1st submenu item
				for (int k = 1; k < subMenuItemsCount; k++)
				{
					driver.FindElements(By.CssSelector("#box-apps-menu > li.selected li"))[k].Click();
					wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#content h1")));
					//Debug.WriteLine(driver.FindElement(By.CssSelector("#content h1")).Text);
				}
			}
		}
	}
}
