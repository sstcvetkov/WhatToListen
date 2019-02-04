namespace WhatToListen.Core.Entities.Vk.Attachments
{
	public class Document : EntityVk
	{
		public string Title { get; set; }
		public long? Size { get; set; }
		public string Extentions { get; set; }
		public string Uri { get; set; }

		public override void FromVkObject(object vkObject)
		{
			var obj = (VkNet.Model.Attachments.Document)vkObject;
			Id = obj.Id.Value;
			Size = obj.Size;
			Extentions = obj.Ext;
			Uri = obj.Uri;
			Title = obj.Title;
		}
	}
}
