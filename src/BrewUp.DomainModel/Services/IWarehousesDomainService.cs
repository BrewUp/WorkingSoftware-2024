using BrewUp.Shared.CustomTypes;

namespace BrewUp.DomainModel.Services;

public interface IWarehousesDomainService
{
	Task UpdateAvailabilityDueToProductionOrderAsync(BeerId beerId, BeerName beerName, Quantity quantity,
		CancellationToken cancellationToken);
}