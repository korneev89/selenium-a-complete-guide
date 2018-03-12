using System;
using System.IO;
using System.Reflection;
using AutomatedTester.BrowserMob;
using AutomatedTester.BrowserMob.HAR;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Web.Script.Serialization;

namespace csharp_example
{
	[TestFixture]
	public class BrowserMob : TestBase
	{
		[Test]
		public void UsingBrowserMob()
		{
			// Supply the path to the Browsermob Proxy batch file
			Server server = new Server(@"C:\browsermob-proxy-2.1.4\bin\browsermob-proxy.bat", 8090);
			server.Start();

			Client client = server.CreateProxy();
			client.NewHar("sample");

			var options = new ChromeOptions
			{
				Proxy = new Proxy { HttpProxy = client.SeleniumProxy }
			};

			driver = new ChromeDriver(options);

			LoginAsAdmin();
			driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1";
			var itemsCount = driver.FindElements(By.CssSelector("img[style*='margin-left: 32px; width: 16px; height: 16px; vertical-align: bottom;'] + a")).Count;

			for (var i = 0; i < itemsCount; i++)
			{
				driver.FindElements(By.
					CssSelector("img[style*='margin-left: 32px; width: 16px; height: 16px; vertical-align: bottom;'] + a"))[i].
						Click();

				driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1";
			}

			// Get the performance stats
			HarResult harData = client.GetHar();

			// write to HAR.txt
			//Log log = harData.Log;
			//Entry[] entries = log.Entries;

			//var logFileInfo = new FileInfo(Path.Combine(
			//							Path.GetDirectoryName(
			//								Assembly.GetExecutingAssembly().Location),
			//								"HAR.txt"));

			//var file = new StreamWriter(logFileInfo.FullName);

			//foreach (var entry in entries)
			//{
			//	Request request = entry.Request;
			//	Response response = entry.Response;
			//	var url = request.Url;
			//	var time = entry.Time;
			//	var status = response.Status;
			//	Console.WriteLine("Url: " + url + " - Time: " + time + " Response: " + status);

			//	file.WriteLine("Url: " + url + " - Time: " + time + " Response: " + status);
			//}

			//file.Close();

			// write to sample.har
			var json = new JavaScriptSerializer().Serialize(harData);

			Log logJSON = harData.Log;
			Entry[] entriesJSON = logJSON.Entries;

			var logFileInfoJSON = new FileInfo(Path.Combine(
							Path.GetDirectoryName(
								Assembly.GetExecutingAssembly().Location),
								"sample.har"));

			var fileJSON = new StreamWriter(logFileInfoJSON.FullName);

			fileJSON.WriteLine(json.ToLower());
			fileJSON.Close();


			client.Close();
			server.Stop();
		}
	}
}
