using System.Collections.Generic;
using System.Linq;

namespace WhatToListen.Core.Entities.Vk.Attachments
{
	public class Photo : EntityVk
	{
		public ICollection<PhotoSize> Sizes { get; set; }

		public override void FromVkObject(object vkObject)
		{
			var obj = (VkNet.Model.Attachments.Photo)vkObject;
			Id = obj.Id.Value;
			Sizes = obj.Sizes.Select(p => new PhotoSize
			{
				Url = p.Url?.AbsoluteUri,
				Type = p.Type?.ToString(),
				Height = (int)p.Height,
				Width = (int)p.Width,
			}).ToArray();
		}
	}
}
