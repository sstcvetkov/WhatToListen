using System.Collections.Generic;

namespace WhatToListen.Core.Entities.Vk
{
	public class User : EntityVk
	{
		public ICollection<PostUser> Posts { get; set; }

		public User()
		{
			Posts = new List<PostUser>();
		}

		public override void FromVkObject(object vkObject)
		{
			var obj = (VkNet.Model.User)vkObject;
			Id = obj.Id;
		}
	}
}
