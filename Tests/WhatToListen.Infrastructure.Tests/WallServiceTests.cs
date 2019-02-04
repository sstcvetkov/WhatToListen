using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using VkNet;
using VkNet.Enums.Filters;
using WhatToListen.Core.Services;
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
		}

		public VkApi VkApi { get; }
		public VkContext Context { get; }
		public UnitOfWork<VkContext> Repositories { get; }

		[Fact]
		public void LoadPostsTest()
		{
			var svc = new WallService(VkApi, Repositories, "grimeofficial");
			svc.LoadPosts(page: 1218);
		}
	}
}
