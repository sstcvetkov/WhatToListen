namespace WhatToListen.Core.Entities.Vk
{
	public class PostPost : Entity
	{
		public Post ParentPost { get; set; }
		public Post ChildPost { get; set; }
		public long UsersCount { get; set; }
	}
}
