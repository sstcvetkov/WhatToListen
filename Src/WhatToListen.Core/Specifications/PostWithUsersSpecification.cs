using WhatToListen.Core.Entities.Vk;

namespace WhatToListen.Core.Specifications
{
	public class PostWithUsersSpecification : BaseSpecification<Post>
	{
		public PostWithUsersSpecification()
		{
			AddInclude(x => x.Users);
		}

		public PostWithUsersSpecification(long minLikes)
			: base(x => x.Liks >= minLikes)
		{
			AddInclude(x => x.Users);
		}
	}
}
