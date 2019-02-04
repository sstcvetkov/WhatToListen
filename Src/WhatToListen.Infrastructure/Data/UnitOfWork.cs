using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using WhatToListen.Core.Abstractions;

namespace WhatToListen.Infrastructure.Data
{
	public class UnitOfWork<TContext> : IUnitOfWork
		where TContext : DbContext
	{
		private TContext Context;
		private Hashtable Repositories;

		public UnitOfWork(TContext context)
		{
			Context = context;
			Repositories = new Hashtable();
		}

		public IRepository<TEntity> Get<TEntity>() where TEntity : class
		{
			if (!Repositories.Contains(typeof(TEntity)))
			{
				Repositories.Add(typeof(TEntity), new Repository<TEntity, TContext>(Context));
			}
			return (IRepository<TEntity>)Repositories[typeof(TEntity)];
		}
		public void Complete()
		{
			Context.SaveChanges();
		}

		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					Context.Dispose();
				}
			}
			this.disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
