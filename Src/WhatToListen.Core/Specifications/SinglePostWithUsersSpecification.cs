using WhatToListen.Core.Entities.Vk;

namespace WhatToListen.Core.Specifications
{
	public class SinglePostWithUsersSpecification : BaseSpecification<Post>
	{
		public SinglePostWithUsersSpecification(long postId) : base(x => x.Id == postId)
		{
			AddInclude(x => x.Users);
		}
	}
}
