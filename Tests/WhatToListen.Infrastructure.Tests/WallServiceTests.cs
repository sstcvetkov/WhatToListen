using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VkNet;
using VkNet.Enums.Filters;
using WhatToListen.Core.Abstractions;
using WhatToListen.Core.Entities.Vk;
using WhatToListen.Core.Services;
using WhatToListen.Core.Specifications;
using WhatToListen.Infrastructure.Data;
using Xunit;

namespace WhatToListen.Infrastructure.Tests
{
	public class WallServiceTests
	{
		public WallServiceTests()
		{
			VkApi = new VkApi();
			VkApi.Authorize(new VkNet.Model.ApiAuthParams
			{
				Login = "sstcvetkov@gmail.com",
				Password = "H3ll0S0a1M2p3L4e",
				ApplicationId = 6793397,
				Settings = Settings.All,
				//TwoFactorAuthorization = () =>
				//{
				//	Console.WriteLine("Enter Code:");
				//	return Console.ReadLine();
				//}
			});

			var optionsBuilder = new DbContextOptionsBuilder<VkContext>();
			optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=whattolisten;Username=postgres;Password=1");
			Context = new VkContext(optionsBuilder.Options);
			Repositories = new UnitOfWork<VkContext>(Context);
			Service = new WallService(VkApi, Repositories, "grimeofficial");
			Posts = Repositories.Get<Post>();
			PostUsers = Repositories.Get<PostUser>();
			PostPosts = Repositories.Get<PostPost>();
		}

		public VkApi VkApi { get; }
		public VkContext Context { get; }
		public UnitOfWork<VkContext> Repositories { get; }
		private WallService Service;
		private IRepository<Post> Posts;
		private IRepository<PostUser> PostUsers;
		private IRepository<PostPost> PostPosts;

		//[Fact]
		public void LoadPostsTest()
		{
			Service.LoadPosts();
		}

		//[Fact]
		public void UpdatePostPostsCacheTest()
		{
			foreach (var post in Posts
				.List(new PostFullSpecification())
				.Where(x => x.Liks >= WallService.MinMainPostLikes)
				.Take(100))
			{
				Service.UpdatePostPostsCache(post);
				Repositories.Complete();
			}
		}

		[Fact]
		public void PostPostsCachePerfomanceTest()
		{
			var post = Posts.Get(new PostFullSpecification(90926));
			var sw = new Stopwatch(); sw.Start();

			// Creating cache
			Service.UpdatePostPostsCache(post);
			Repositories.Complete();
			var timeLive = sw.ElapsedMilliseconds;

			// Red cache
			post = Posts.Get(new PostFullSpecification(90926));
			var timeCached = sw.ElapsedMilliseconds- timeLive;

			Assert.True(post.Posts.Count > 0);
			Assert.True(timeCached < timeLive);
			Assert.True(timeCached < 500);

			// Remove cache
			PostPosts.Remove(post.Posts);
			post.Posts = new List<PostPost>();
			Repositories.Complete();
		}
	}
}
