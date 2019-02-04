using System.Collections.Generic;
using System.Linq;

namespace WhatToListen.Core.Entities.Vk.Attachments
{
	public class Link : EntityVk
	{
		public string Uri { get; set; }
		public string Title { get; set; }
		public string Caption { get; set; }
		public string Description { get; set; }
		public ICollection<PhotoSize> Sizes { get; set; }

		public override void FromVkObject(object vkObject)
		{
			var obj = (VkNet.Model.Attachments.Link)vkObject;
			Id = obj.Id.HasValue ? obj.Id.Value : 0;
			Uri = obj.Uri?.AbsoluteUri;
			Title = obj.Title;
			Caption = obj.Caption;
			Description = obj.Description;
			Sizes = obj.Photo?.Sizes?.Select(p => new PhotoSize
			{
				Url = p.Url?.AbsoluteUri,
				Type = p.Type?.ToString(),
				Height = (int)p.Height,
				Width = (int)p.Width,
			}).ToArray();
		}
	}
}
