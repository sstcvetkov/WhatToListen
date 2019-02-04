namespace WhatToListen.Core.Entities.Vk.Attachments
{
	public class Page : EntityVk
	{
		public long? Views { get; set; }
		public string Title { get; set; }
		public string ViewUrl { get; set; }

		public override void FromVkObject(object vkObject)
		{
			var obj = (VkNet.Model.Attachments.Page)vkObject;
			Id = obj.Id.Value;
			Views = obj.Views;
			ViewUrl = obj.Title;
			Title = obj.ViewUrl;
		}
	}
}
