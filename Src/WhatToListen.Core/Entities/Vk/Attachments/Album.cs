using System;
using System.Collections.Generic;
using System.Linq;

namespace WhatToListen.Core.Entities.Vk.Attachments
{
	public class Album : EntityVk
	{
		public long? VkAlbumId { get; set; }
		public string Text { get; set; }
		public string Title { get; set; }
		public DateTime? Date { get; set; }
		public ICollection<PhotoSize> Sizes { get; set; }

		public override void FromVkObject(object vkObject)
		{
			var obj = (VkNet.Model.Attachments.Album)vkObject;
			Id = obj.Id.Value;
			VkAlbumId = obj.Thumb?.AlbumId;
			Title = obj.Title;
			Text = obj.Thumb?.Text;
			Date = obj.CreateTime;
			Sizes = obj.Thumb?.Sizes?.Select(p => new PhotoSize
			{
				Url = p.Url?.AbsoluteUri,
				Type = p.Type?.ToString(),
				Height = (int)p.Height,
				Width = (int)p.Width,
			}).ToArray();
		}
	}
}
