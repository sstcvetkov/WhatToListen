using WhatToListen.Core.Entities.Vk;

namespace WhatToListen.Core.Specifications
{
	public class PostPostsSpecification : BaseSpecification<PostPost>
	{
		public PostPostsSpecification(long postId) : base(x => x.ParentPost.Id == postId)
		{
			AddInclude(x => x.ParentPost);
			AddInclude(x => x.ChildPost);
		}

		public PostPostsSpecification()
		{
			AddInclude(x => x.ParentPost);
			AddInclude(x => x.ChildPost);
		}
	}
}
