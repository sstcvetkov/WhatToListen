using WhatToListen.Core.Entities.Vk;

namespace WhatToListen.Core.Specifications
{
	public class PostWithUsersSpecificationPaged : BaseSpecification<Post>
	{
		public PostWithUsersSpecificationPaged(int page, int size)
		{
			ApplyPaging(page, size);
			AddInclude(x => x.Users);

			ApplyOrderByDescending(x => x.Date);
		}
	}
}
