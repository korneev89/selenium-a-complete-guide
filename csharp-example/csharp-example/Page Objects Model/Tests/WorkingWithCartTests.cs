using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace csharp_example
{
	[TestFixture]
	public class WorkingWithCartTests : TestBase
	{
		//[Test, TestCaseSource(typeof(DataProviders), "ValidCustomers")]
		[Test]
		public void AddAndRemoveProductsFromCart()
		{
			//log as admin
			//app.LoginIfNotLoggedIn();

			app.AddProductsToCart(3);

			app.CheckoutCart();

			app.DeleteProductsFromCart();
		}
	}
}
