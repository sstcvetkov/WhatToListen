using WhatToListen.Core.Entities.Vk;

namespace WhatToListen.Core.Specifications
{
	public class PostFullSpecification : BaseSpecification<Post>
	{
		public PostFullSpecification(long id) : base(x => x.Id == id)
		{
			AddInclude(x => x.Users);
			AddInclude(x => x.Posts);

		}
		public PostFullSpecification()
		{
			AddInclude(x => x.Users);
			AddInclude(x => x.Posts);
		}
	}
}
