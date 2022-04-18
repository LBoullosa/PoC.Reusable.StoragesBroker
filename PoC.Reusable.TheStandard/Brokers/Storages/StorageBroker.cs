using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PoC.Reusable.TheStandard.Brokers
{
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public IConfiguration Configuration { get => configuration; }

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}