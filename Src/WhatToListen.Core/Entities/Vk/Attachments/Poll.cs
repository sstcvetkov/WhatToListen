using System;

namespace WhatToListen.Core.Entities.Vk.Attachments
{
	public class Poll : EntityVk
	{
		public DateTime? Created { get; set; }
		public string Question { get; set; }
		public int? Votes { get; set; }

		public override void FromVkObject(object vkObject)
		{
			var obj = (VkNet.Model.Attachments.Poll)vkObject;
			Id = obj.Id.Value;
			Created = obj.Created;
			Question = obj.Question;
			Votes = obj.Votes;
		}
	}
}
