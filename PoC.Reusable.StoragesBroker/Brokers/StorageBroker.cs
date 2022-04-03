using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PoC.Reusable.StoragesBroker.Brokers
{
    public partial class StorageBroker : DbContext
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}