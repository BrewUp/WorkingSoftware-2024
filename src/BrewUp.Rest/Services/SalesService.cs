using BrewUp.ReadModel.Sales.Services;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BrewUp.Rest.Services
{
	public static class SalesService 
	{	
		public static async Task<Results<Ok<PagedResult<SalesOrderJson>>, NotFound>> HandleGetOrders(ISalesQueryService salesQueryService, CancellationToken cancellationToken)
		{
			var orders = await salesQueryService.GetSalesOrdersAsync(0, 30, cancellationToken);
			return TypedResults.Ok(orders);
		}
	}
}
