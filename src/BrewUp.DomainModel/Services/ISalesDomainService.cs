using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;

namespace BrewUp.DomainModel.Services;

public interface ISalesDomainService
{
	Task CreateSalesOrderAsync(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, OrderDate orderDate, CustomerId customerId,
		CustomerName customerName, IEnumerable<SalesOrderRowJson> rows, CancellationToken cancellationToken);
}