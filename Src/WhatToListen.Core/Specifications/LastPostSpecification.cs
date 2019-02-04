using WhatToListen.Core.Entities.Vk;

namespace WhatToListen.Core.Specifications
{
	public class LastPostSpecification : BaseSpecification<Post>
	{
		public LastPostSpecification()
			: base(x => x.Date != null)
		{
			ApplyOrderByDescending(x => x.Date);
			ApplyPaging(0, 1);
		}
	}
}
