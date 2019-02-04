using System;
using System.Collections.Generic;
using System.Text;

namespace WhatToListen.Core.Entities.Vk.Attachments
{
	public class Audio : EntityVk
	{
		public string Artist { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public int? GenreId { get; set; }

		public override void FromVkObject(object vkObject)
		{
			var obj = (VkNet.Model.Attachments.Audio)vkObject;
			Id = obj.Id.Value;
			Artist = obj.Artist;
			GenreId = obj.Genre.HasValue ? (int)obj.Genre : (int?)null;
			Title = obj.Title;
			Url = obj.Url?.AbsoluteUri;
		}
	}
}
