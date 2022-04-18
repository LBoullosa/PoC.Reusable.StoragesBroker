using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PoC.Reusable.TheStandard.Brokers.Extensions
{
	public static class StorageBrokerExtensions
	{
		private static TBroker CreateNewBroker<TBroker>(TBroker actualBroker)
		where TBroker : DbContext, IStorageBroker, new()
		{
			TBroker broker = Activator.CreateInstance(
				typeof(TBroker)
				, new Type[] { typeof(IConfiguration) }
				, new object[] { actualBroker.Configuration }) as TBroker;

			return broker;
		}

		private static DbSet<TEntity> GetCurrentDbSet<TBroker, TEntity>(
				TBroker broker
				, Expression<Func<TBroker, DbSet<TEntity>>> dbSetExpression)
			where TBroker : DbContext, IStorageBroker
			where TEntity : class
		{
			return dbSetExpression
					.Compile()
					.Invoke(broker);
		}

		public static IQueryable<TEntity> SelectAll<TBroker, TEntity>(
				this TBroker broker
				, Expression<Func<TBroker, DbSet<TEntity>>> dbSetExpression)
			where TBroker : DbContext, IStorageBroker
			where TEntity : class
		{
			return GetCurrentDbSet(broker, dbSetExpression);
		}

		public static IQueryable<TEntity> Select<TBroker, TEntity>(
				this TBroker broker
				, DbSet<TEntity> dbSet
				, Expression<Func<TEntity, bool>> filters = null)
			where TBroker : DbContext, IStorageBroker
			where TEntity : class
		{
			if (filters == null)
				filters = x => true;
			return dbSet.Where(filters);
		}

		public static IQueryable<TEntity> Select<TBroker, TEntity>(
				this TBroker broker
				, Expression<Func<TBroker, DbSet<TEntity>>> dbSetExpression
				, Expression<Func<TEntity, bool>> filters = null)
			where TBroker : DbContext, IStorageBroker
			where TEntity : class
		{
			return broker.Select(dbSetExpression, filters);
		}

		public static async ValueTask<TEntity> InsertAsync<TBroker, TEntity>(
				this TBroker broker
				, TEntity entity
				, DbSet<TEntity> dbSet)
			where TBroker : DbContext, IStorageBroker
			where TEntity : class
		{
			EntityEntry<TEntity> entityEntry
				= await dbSet.AddAsync(entity: entity);

			await broker.SaveChangesAsync();

			entityEntry.State = EntityState.Detached;

			return entityEntry.Entity;
		}

		public static async ValueTask<TEntity> InsertAsync<TBroker, TEntity>(
				this TBroker broker
				, TEntity entity
				, Expression<Func<TBroker, DbSet<TEntity>>> dbSetExpression)
			where TBroker : DbContext, IStorageBroker
			where TEntity : class
		{
			return await broker.InsertAsync(entity, GetCurrentDbSet(broker, dbSetExpression));
		}

		public static async ValueTask<TEntity> UpdateAsync<TBroker, TEntity>(
				this TBroker broker
				, TEntity entity
				, DbSet<TEntity> dbSet)
			where TBroker : DbContext, IStorageBroker
			where TEntity : class
		{
			EntityEntry<TEntity> entityEntry = dbSet.Update(entity: entity);

			await broker.SaveChangesAsync();

			entityEntry.State = EntityState.Detached;

			return entityEntry.Entity;
		}

		public static async ValueTask<TEntity> UpdateAsync<TBroker, TEntity>(
				this TBroker broker
				, TEntity entity
				, Expression<Func<TBroker, DbSet<TEntity>>> dbSetExpression)
			where TBroker : DbContext, IStorageBroker
			where TEntity : class
		{
			return await broker.UpdateAsync(entity, GetCurrentDbSet(broker, dbSetExpression));
		}

		public static async ValueTask<TEntity> DeleteAsync<TBroker, TEntity>(
				this TBroker broker
				, TEntity entity
				, Expression<Func<TBroker, DbSet<TEntity>>> dbSetExpression)
			where TBroker : DbContext, IStorageBroker
			where TEntity : class
		{
			EntityEntry<TEntity> entityEntry
				= GetCurrentDbSet(broker, dbSetExpression)
					.Remove(entity: entity);

			await broker.SaveChangesAsync();

			entityEntry.State = EntityState.Detached;

			return entityEntry.Entity;
		}
	}
}
