using System;

namespace WhatToListen.Core.Abstractions
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<T> Get<T>() where T : class;
		void Complete();
	}
}
