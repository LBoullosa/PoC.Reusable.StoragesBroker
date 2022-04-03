using Microsoft.EntityFrameworkCore;
using PoC.Reusable.StoragesBroker.Models;

namespace PoC.Reusable.StoragesBroker.Brokers
{
	public partial class StorageBroker : IStorageBroker<Car>
	{
		DbSet<Car> Cars { get; set; }

		public IQueryable<Car> SelectAll() => this.Cars;
	}
}
