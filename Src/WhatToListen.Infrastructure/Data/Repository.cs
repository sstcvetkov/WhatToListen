using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WhatToListen.Core.Abstractions;

namespace WhatToListen.Infrastructure.Data
{
	public class Repository<TEntity, TContext> : IRepository<TEntity>
		where TEntity : class
		where TContext : DbContext
	{
		protected readonly DbContext _dbContext;

		public Repository(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public virtual TEntity Get(long entityId)
		{
			return _dbContext.Set<TEntity>().Find(entityId);
		}

		public TEntity Get(ISpecification<TEntity> spec)
		{
			return List(spec).FirstOrDefault();
		}

		public IEnumerable<TEntity> ListAll()
		{
			return _dbContext.Set<TEntity>().AsEnumerable();
		}

		public IEnumerable<TEntity> List(ISpecification<TEntity> spec)
		{
			return ApplySpecification(spec).AsEnumerable();
		}

		public int Count(ISpecification<TEntity> spec)
		{
			return ApplySpecification(spec).Count();
		}

		public TEntity Add(TEntity entity)
		{
			_dbContext.Set<TEntity>().Add(entity);
			return entity;
		}

		public void Add(IEnumerable<TEntity> entities)
		{
			_dbContext.Set<TEntity>().AddRange(entities);
		}

		public void Delete(TEntity entity)
		{
			_dbContext.Set<TEntity>().Remove(entity);
		}

		public void Remove(TEntity entity)
		{
			_dbContext.Set<TEntity>().Remove(entity);
		}

		public void Remove(IEnumerable<TEntity> entities)
		{
			_dbContext.Set<TEntity>().RemoveRange(entities);
		}

		private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
		{
			return SpecificationEvaluator<TEntity>.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), spec);
		}
	}
}
