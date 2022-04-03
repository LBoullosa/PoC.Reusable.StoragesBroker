namespace PoC.Reusable.StoragesBroker.Brokers
{
	public interface IStorageBroker<T>

	{
		IQueryable<T> SelectAll() => throw new NotImplementedException();
		ValueTask<T> InsertAsync(T entity) => throw new NotImplementedException();
		ValueTask<T> UpdateAsync(T entity) => throw new NotImplementedException();
		ValueTask DeleteAsync(T entity) => throw new NotImplementedException();
	}
}
