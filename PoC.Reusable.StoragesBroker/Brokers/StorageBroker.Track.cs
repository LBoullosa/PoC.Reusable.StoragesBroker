using Microsoft.EntityFrameworkCore;
using PoC.Reusable.StoragesBroker.Models;

namespace PoC.Reusable.StoragesBroker.Brokers
{
    public partial class StorageBroker : IStorageBroker<Track>
    {
        DbSet<Track> Tracks { get; set; }

        public IQueryable<Track> SelectAll() => this.Tracks;
    }
}
