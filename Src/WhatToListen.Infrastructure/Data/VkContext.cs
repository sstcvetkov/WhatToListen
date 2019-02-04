using Microsoft.EntityFrameworkCore;
using WhatToListen.Core.Entities.Vk;
using WhatToListen.Core.Entities.Vk.Attachments;

namespace WhatToListen.Infrastructure.Data
{
	public class VkContext : DbContext
	{
		public DbSet<Post> Posts { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<PostUser> PostUsers { get; set; }
		public DbSet<PostPost> PostPosts { get; set; }
		public DbSet<Attachment> Attachments { get; set; }
		public DbSet<Album> Albums { get; set; }
		public DbSet<Audio> Audios { get; set; }
		public DbSet<Document> Documents { get; set; }
		public DbSet<Link> Links { get; set; }
		public DbSet<Page> Pages { get; set; }
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Poll> Polls { get; set; }
		public DbSet<Video> Videos { get; set; }

		public VkContext(DbContextOptions<VkContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.HasDefaultSchema("vk");

			builder.Entity<PostUser>()
				.HasKey(t => new { t.PostId, t.UserId });
			builder.Entity<PostUser>()
				.HasOne(sc => sc.Post)
				.WithMany(s => s.Users)
				.HasForeignKey(sc => sc.PostId);
			builder.Entity<PostUser>()
				.HasOne(sc => sc.User)
				.WithMany(c => c.Posts)
				.HasForeignKey(sc => sc.UserId);
		}
	}
}
