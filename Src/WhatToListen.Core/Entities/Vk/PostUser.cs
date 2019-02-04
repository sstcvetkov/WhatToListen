namespace WhatToListen.Core.Entities.Vk
{
	public enum PostUserType
	{
		Liker = 1,
		Reposter,
		Bouth
	}

	public class PostUser
	{
		public long PostId { get; set; }
		public Post Post { get; set; }
		public long UserId { get; set; }
		public User User { get; set; }
		public PostUserType Type { get; set; }

		public override bool Equals(object obj)
		{
			var user = obj as PostUser;
			return user != null &&
				   PostId == user.PostId &&
				   UserId == user.UserId;
		}

		public override int GetHashCode()
		{
			var hashCode = 1576474585;
			hashCode = hashCode * -1521134295 + PostId.GetHashCode();
			hashCode = hashCode * -1521134295 + UserId.GetHashCode();
			return hashCode;
		}
	}
}
