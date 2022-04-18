using Microsoft.Extensions.Configuration;

namespace PoC.Reusable.TheStandard.Brokers
{
    public partial interface IStorageBroker : IDisposableBroker
    {
		public IConfiguration Configuration { get; }
	}
}
