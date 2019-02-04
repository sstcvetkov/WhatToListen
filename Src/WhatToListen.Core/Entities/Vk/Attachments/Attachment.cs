using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatToListen.Core.Entities.Vk.Attachments
{
	public class Attachment : Entity
	{
		[NotMapped]
		public EntityVk Instance { get; set; }

		public long? InstanceId { get; set; }

		public string Raw { get; set; }

		public string Type { get; set; }

		public Attachment()
		{
		}

		public Attachment(object vkObject)
		{
			var obj = (VkNet.Model.Attachments.Attachment)vkObject;
			Raw = JsonConvert.SerializeObject(obj);
			Type = obj.Type.Name;
		}
	}
}
