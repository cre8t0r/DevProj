using NUnit.Framework;

using DevelopmentProject.BusinessLogic;
using DevelopmentProject.Models;

namespace DevelopmentProject.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test1()
		{
			Customer customer = new Customer();
			customer.Age = 30;
			customer.SumInsured = 1000000;
			decimal totalValue = CalculationLogic.CalculateTotalValue(customer: customer, ratingFactor: 1.1m);

			Assert.AreEqual(30.555555555555555555555555556m, totalValue);
			//Assert.Pass();
		}
	}
}