using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DevelopmentProject.Models;

namespace DevelopmentProject.BusinessLogic
{
	public class CalculationLogic
	{
		/// <summary>
		/// Calculate the total value based on formula
		/// Formula >>> Total Value = (Sum Insured * Occupation Rating Factor) / (100 * 12 * Age)
		/// </summary>
		/// <param name="customer"></param>
		/// <param name="ratingFactor"></param>
		/// <returns></returns>
		public static decimal CalculateTotalValue(Customer customer, decimal ratingFactor)
		{
			// Formula >>> Total Value = (Sum Insured * Occupation Rating Factor) / (100 * 12 * Age)
			Decimal totalValue = (customer.SumInsured * ratingFactor) / (100 * 12 * customer.Age);

			return totalValue;
		}
	}
}
