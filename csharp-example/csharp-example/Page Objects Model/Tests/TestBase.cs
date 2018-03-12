using NUnit.Framework;

namespace csharp_example
{
	public class TestBase
	{
		public Application app;

		[SetUp]
		public void Start()
		{
			app = new Application();
		}

		[TearDown]
		public void Stop()
		{
			app.Quit();
			app = null;
		}
	}
}