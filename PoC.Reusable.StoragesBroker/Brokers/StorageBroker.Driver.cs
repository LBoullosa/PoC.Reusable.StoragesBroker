using Microsoft.EntityFrameworkCore;
using PoC.Reusable.StoragesBroker.Models;

namespace PoC.Reusable.StoragesBroker.Brokers
{
    public partial class StorageBroker : IStorageBroker<Driver>
    {
        DbSet<Driver> Drivers { get; set; }

        public IQueryable<Driver> SelectAll() => this.Drivers;
    }
}
