using BrewUp.DomainModel.Services;
using BrewUp.ReadModel;
using BrewUp.ReadModel.Sales.Services;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Availability = BrewUp.Shared.Entities.Availability;

namespace BrewUp.Rest.Services;

public static class SalesService 
{
	public static async Task<string> HandleCreateSalesOrder(ISalesDomainService salesDomainService,
		IQueries<Availability> availabilityServices,
		SalesOrderJson body,
		CancellationToken cancellationToken)
	{
		List<SalesOrderRowJson> beersAvailable = new(); 
		foreach (var row in body.Rows)
		{
			var availabilities = await availabilityServices.GetByFilterAsync(b => b.BeerId.Equals(row.BeerId),
				1, 10, cancellationToken);
			if (availabilities.Results.Any())
				beersAvailable.Add(row);
		}

		body = body with {Rows = beersAvailable};
		await salesDomainService.CreateSalesOrderAsync(new SalesOrderId(new Guid(body.SalesOrderId)),
			new SalesOrderNumber(body.SalesOrderNumber), new OrderDate(body.OrderDate),
			new CustomerId(body.CustomerId), new CustomerName(body.CustomerName),
			body.Rows, cancellationToken);

		return body.SalesOrderId;
	}
		
	public static async Task<Results<Ok<PagedResult<SalesOrderJson>>, NotFound>> HandleGetOrders(ISalesQueryService salesQueryService, CancellationToken cancellationToken)
	{
		var orders = await salesQueryService.GetSalesOrdersAsync(0, 30, cancellationToken);
		return TypedResults.Ok(orders);
	}
}