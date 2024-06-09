using BrewUp.DomainModel.Entities.Sales;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.DomainModel.Services;

public sealed class SalesDomainService([FromKeyedServices("sales")] IRepository repository) : ISalesDomainService
{
	public async Task CreateSalesOrderAsync(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, IEnumerable<SalesOrderRowJson> rows, CancellationToken cancellationToken)
	{
		var aggregate = SalesOrder.CreateSalesOrder(salesOrderId, salesOrderNumber, orderDate, customerId, customerName, rows);

		await repository.InsertAsync(aggregate.MapToReadModel(), cancellationToken);
	}
}