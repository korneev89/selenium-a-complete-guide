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
			//log in as admin if necessary
			//app.LoginIfNotLoggedIn();

			app.AddProductsToCart(3);

			app.CheckoutCart();

			app.DeleteProductsFromCart();
		}
	}
}
