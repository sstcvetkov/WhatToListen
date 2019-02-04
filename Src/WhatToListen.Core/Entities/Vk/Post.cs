using System;
using System.Collections.Generic;
using WhatToListen.Core.Entities.Vk.Attachments;

namespace WhatToListen.Core.Entities.Vk
{
	public class Post : EntityVk
	{
		public int Liks { get; set; }
		public int RepostsCount { get; set; }
		public int CommentsCount { get; set; }
		public int ViewsCount { get; set; }
		public string PostType { get; set; }
		public string Text { get; set; }
		public DateTime Date { get; set; }
		public long OwnerId { get; set; }

		public virtual ICollection<PostUser> Users { get; set; }
		public virtual ICollection<PostPost> Posts { get; set; }

		public ICollection<Album> Albums { get; set; }
		public ICollection<Audio> Audios { get; set; }
		public ICollection<Document> Documents { get; set; }
		public ICollection<Link> Links { get; set; }
		public ICollection<Page> Pages { get; set; }
		public ICollection<Photo> Photos { get; set; }
		public ICollection<Poll> Polls { get; set; }
		public ICollection<Video> Videos { get; set; }

		public Post()
		{
			Users = new List<PostUser>();
		}

		public override void FromVkObject(object vkObject)
		{
			var obj = (VkNet.Model.Post)vkObject;
			Id = obj.Id.Value;
			Text = obj.Text;
			Date = obj.Date.Value;
			RepostsCount = obj.Reposts?.Count ?? 0;
			CommentsCount = obj.Comments?.Count ?? 0;
			ViewsCount = obj.Views?.Count ?? 0;
			PostType = obj.PostType.ToString();
			OwnerId = obj.OwnerId.Value;
			Liks = obj.Likes.Count;
		}
	}
}
