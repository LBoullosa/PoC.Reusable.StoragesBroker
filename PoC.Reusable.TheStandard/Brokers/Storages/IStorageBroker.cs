using Microsoft.Extensions.Configuration;

namespace PoC.Reusable.TheStandard.Brokers.Storages
{
	public partial interface IStorageBroker : IDisposableBroker
	{
		public IConfiguration Configuration { get; }
	}
}
