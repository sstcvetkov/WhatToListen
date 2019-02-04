using System;

namespace WhatToListen.Core.Entities.Vk.Attachments
{
	public class Video : EntityVk
	{
		public DateTime? Date { get; set; }
		public string Title { get; set; }
		public string Platform { get; set; }

		public override void FromVkObject(object vkObject)
		{
			var obj = (VkNet.Model.Attachments.Video)vkObject;
			Id = obj.Id.Value;
			Date = obj.Date;
			Platform = obj.Platform;
			Title = obj.Title;
		}
	}
}
