namespace WhatToListen.Core.Specifications
{
	public class PagingSpecification<TEntity> : BaseSpecification<TEntity>
	{
		public PagingSpecification(int page, int size)
		{
			ApplyPaging(page, size);
		}
	}
}
