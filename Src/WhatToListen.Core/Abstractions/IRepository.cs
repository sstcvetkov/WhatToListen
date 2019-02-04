using System.Collections.Generic;

namespace WhatToListen.Core.Abstractions
{
	public interface IRepository<T> where T : class
	{
		T Get(long id);
		T Get(ISpecification<T> spec);
		IEnumerable<T> ListAll();
		IEnumerable<T> List(ISpecification<T> spec);

		T Add(T entity);
		void Add(IEnumerable<T> entities);
		void Remove(T entity);
		void Remove(IEnumerable<T> entities);

		int Count(ISpecification<T> spec);
	}
}
