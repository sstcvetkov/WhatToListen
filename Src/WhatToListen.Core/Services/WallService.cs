using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;
using WhatToListen.Common.Helpers;
using WhatToListen.Core.Abstractions;
using WhatToListen.Core.Entities;
using WhatToListen.Core.Entities.Vk;
using WhatToListen.Core.Entities.Vk.Attachments;
using WhatToListen.Core.Specifications;

namespace WhatToListen.Core.Services
{
	public class WallService
	{
		private class PostItem
		{
			public Post Post;
			public ICollection<User> Likers;
			public ICollection<User> Reposters;
			public ICollection<VkNet.Model.Attachments.Attachment> Attachments;
		}

		private readonly IVkApi Api;
		private readonly IUnitOfWork Repositories;
		private readonly IRepository<Post> Posts;
		private readonly IRepository<User> Users;

		private readonly IRepository<PostUser> PostUsers;
		private readonly IRepository<PostPost> PostPosts;

		private readonly IRepository<Attachment> Attachments;
		private readonly IRepository<Album> Albums;
		private readonly IRepository<Audio> Audios;
		private readonly IRepository<Document> Documents;
		private readonly IRepository<Link> Links;
		private readonly IRepository<Page> Pages;
		private readonly IRepository<Photo> Photos;
		private readonly IRepository<Poll> Polls;
		private readonly IRepository<Video> Videos;

		private readonly string DomainSubName;
		private readonly int PageSize;

		public WallService(IVkApi vkApi, IUnitOfWork repositories, string groupName)
		{
			Api = vkApi;
			Repositories = repositories;
			Posts = Repositories.Get<Post>();
			Users = Repositories.Get<User>();
			PostUsers = Repositories.Get<PostUser>();
			PostPosts = Repositories.Get<PostPost>();
			Attachments = Repositories.Get<Attachment>();
			Albums = Repositories.Get<Album>();
			Audios = Repositories.Get<Audio>();
			Documents = Repositories.Get<Document>();
			Links = Repositories.Get<Link>();
			Pages = Repositories.Get<Page>();
			Photos = Repositories.Get<Photo>();
			Polls = Repositories.Get<Poll>();
			Videos = Repositories.Get<Video>();
			DomainSubName = groupName;
			PageSize = 100;
		}

		public void LoadPosts(bool fromBegining = false, int page = 0)
		{
			var swAll = Stopwatch.StartNew();
			for (; page < int.MaxValue; page++)
			{
				Debug.Write($"Wall page #{page}:");
				var sw = Stopwatch.StartNew();

				var data = LoadPosts(page, 10, out int loadedPosts);
				var loadTime = sw.ElapsedMilliseconds;
				Debug.Write($"\tloaded {loadedPosts.Align(0, -5)} in {loadTime.Align(0, -5)}");
				var addedPosts = 0;
				var allPosts = 0;

				var addedUsers = 0;
				var allUsers = 0;

				var addedAttachments = 0;
				var allAttachments = 0;

				var attachments = new List<Attachment>();
				foreach (var item in data)
				{
					allPosts++;
					var instances = item.Attachments.AsEnumerable();
					if (Posts.Get(item.Post.Id) == null)
					{
						item.Post.Albums = CutAttachments<Album>(ref instances, attachments);
						item.Post.Audios = CutAttachments<Audio>(ref instances, attachments);
						item.Post.Documents = CutAttachments<Document>(ref instances, attachments);
						item.Post.Links = CutAttachments<Link>(ref instances, attachments);
						item.Post.Pages = CutAttachments<Page>(ref instances, attachments);
						item.Post.Photos = CutAttachments<Photo>(ref instances, attachments);
						item.Post.Polls = CutAttachments<Poll>(ref instances, attachments);
						item.Post.Videos = CutAttachments<Video>(ref instances, attachments);
						attachments.AddRange(instances.Select(x => new Attachment(x)));

						var attachmentsNew = 0;
						var usersNew = 0;
						item.Post.Albums = Track(Albums, item.Post.Albums, ref attachmentsNew);
						item.Post.Audios = Track(Audios, item.Post.Audios, ref attachmentsNew);
						item.Post.Documents = Track(Documents, item.Post.Documents, ref attachmentsNew);
						item.Post.Links = Track(Links, item.Post.Links, ref attachmentsNew);
						item.Post.Pages = Track(Pages, item.Post.Pages, ref attachmentsNew);
						item.Post.Photos = Track(Photos, item.Post.Photos, ref attachmentsNew);
						item.Post.Polls = Track(Polls, item.Post.Polls, ref attachmentsNew);
						item.Post.Videos = Track(Videos, item.Post.Videos, ref attachmentsNew);
						item.Likers = Track(Users, item.Likers, ref usersNew);
						item.Reposters = Track(Users, item.Reposters, ref usersNew);

						SetUsers(item.Post, item.Likers, item.Reposters);

						Posts.Add(item.Post);
						addedPosts++;
						addedUsers += usersNew;
						allUsers += item.Likers.Count + item.Reposters.Count;
						addedAttachments += attachmentsNew;
						allAttachments += item.Attachments.Count;
					}
				}
				var addTime = sw.ElapsedMilliseconds - loadTime;
				Debug.Write($"\tadded " +
					$"posts[{addedPosts.Align()}/{allPosts.Align()}] " +
					$"users[{addedUsers.Align()}/{allUsers.Align()}] " +
					$"attachments[{ addedAttachments.Align(0, -5)}/{allAttachments.Align()}] in { addTime.Align(0, -5)}");

				Repositories.Complete();
				foreach (var item in attachments)
					item.InstanceId = item.Instance?.Id;
				Attachments.Add(attachments);
				Repositories.Complete();

				var completeTime = sw.ElapsedMilliseconds - addTime;
				Debug.Write($"\tcompleted in {completeTime.Align(0, -5)}");
				Debug.WriteLine($"\ttotal in {sw.ElapsedMilliseconds.Align(0, -5)}");

				if (loadedPosts == 0)
					break;
				if (!fromBegining && addedPosts == 0)
					break;

			}

			var processTime = swAll.ElapsedMilliseconds;
			Debug.Write($"Posts added in {processTime.Align(0, -5)},");
			Repositories.Complete();
			Debug.WriteLine($"\t completed in {(swAll.ElapsedMilliseconds - processTime).Align(0, -5)}");
		}

		public void UpdateUsers(int page = 0, int size = 100, int pageCount = 10)
		{
			var sw = Stopwatch.StartNew();

			for (var i = 0; i < pageCount; i++)
			{
				Debug.Write($"Wall page #{i}:");
				var postStartTime = sw.ElapsedMilliseconds;
				var usersChangesCount = 0;
				var likersChangesCount = 0;
				var reportersChangesCount = 0;
				var newUsers = 0;
				foreach (var post in Posts.List(new PostWithUsersSpecificationPaged(page, size)))
				{
					var users = LoadPostUsers(post);
					// You can't get reposters id now, so no need to use filter.
					//var likers = LoadPostUsers(post, filter: LikesFilter.Likes);
					//var reporters = LoadPostUsers(post, filter: LikesFilter.Copies);

					usersChangesCount +=
						Math.Abs(post.Users.Count - users.Count);
					//likersChangesCount += 
					//	Math.Abs(post.Users.Count(x => x.Type == PostUserType.Liker) - likers.Count);
					//reportersChangesCount += 
					//	Math.Abs(post.Users.Count(x => x.Type == PostUserType.Reposter) - reporters.Count);

					users = Track(Users, users, ref newUsers);
					SetUsers(post, users, new List<User>(0));
					post.Liks = post.Users.Count(
						x => x.Type == PostUserType.Liker || x.Type == PostUserType.Bouth);
				}

				var time = sw.ElapsedMilliseconds - postStartTime;
				Debug.Write($"\tchanged " +
					$"newUsers:{newUsers.Align(0, -5)}, " +
					$"likers:{likersChangesCount.Align(0, -5)}, " +
					$"reposters:{reportersChangesCount.Align(0, -5)} in {time.Align(0, -5)}");
				Repositories.Complete();
				Debug.WriteLine($"\t completed in {(sw.ElapsedMilliseconds - time).Align(0, -5)}");

				if (newUsers == 0 && usersChangesCount == 0)
					break;
			}
		}

		public void UpdatePostPostsCache(Post post)
		{
			var minMainPostLikes = 10;
			var minTargetLikes = 5;
			Debug.Write($"Creating cache for postId[{post.Id.Align(0, -5)}], likes[{post.Liks.Align(0, -5)}]:");
			var sw = new Stopwatch(); sw.Start();
			long timeRemoved = 0;
			// Remove existed cache
			var existed = PostPosts.List(new PostPostsSpecification(post.Id));
			if (existed.Any())
			{
				PostPosts.Remove(existed);
				timeRemoved = sw.ElapsedMilliseconds;
				Debug.Write($"\ttimeRemoved: {timeRemoved.Align(0, -5)},");
			}
			// Create cache
			var data = PostUsers.ListAll()
				.Where(x => post.Users.Any(u => u.UserId == x.UserId))
				.GroupBy(x => x.PostId)
				.Where(x => x.Key != post.Id)
				.Select(x => new PostPost
				{
					ParentPost = post,
					ChildPost = Posts.Get(x.Key),
					UsersCount = x.Select(z => z.UserId)
						.Intersect(post.Users.Select(u => u.UserId)).Count()
				})
				.Where(x => x.UsersCount >= minTargetLikes)
				.OrderByDescending(x => x.UsersCount);

			var arrayData = data.ToList();
			if (arrayData.Count > 0)
				PostPosts.Add(arrayData);
			var timeCreated = sw.ElapsedMilliseconds - timeRemoved;

			Debug.WriteLine($"\tadded:{arrayData.Count.Align(0, -5)} in {timeCreated.Align(0, -5)}");
		}

		private static void SetUsers(Post post, ICollection<User> likers, ICollection<User> reporters)
		{
			var existed = post.Users;
			post.Users = new List<PostUser>();

			foreach (var liker in likers)
			{
				var found = existed.FirstOrDefault(x => x.UserId == liker.Id);
				if (found == null)
					found = new PostUser { PostId = post.Id, UserId = liker.Id };

				found.Type = PostUserType.Liker;
				post.Users.Add(found);
			}

			foreach (var reposter in reporters)
			{
				var found = existed.FirstOrDefault(x => x.UserId == reposter.Id);
				if (found != null)
					found.Type = PostUserType.Bouth;
				else
					found = new PostUser { PostId = post.Id, UserId = reposter.Id, Type = PostUserType.Reposter };

				post.Users.Add(found);
			}
		}

		private List<T> Track<T>(IRepository<T> repo, ICollection<T> collection, ref int countNew)
			where T : Entity
		{
			var result = new List<T>();
			foreach (var item in collection)
			{
				var existed = repo.Get(item.Id);
				if (existed == null)
				{
					if (!result.Contains(existed))
					{
						result.Add(repo.Add(item));
						countNew++;
					}
					else
					{

					}
				}
				else
				{
					if (!result.Contains(existed))
					{
						result.Add(existed);
					}
					else
					{

					}
				}
			}

			return result;
		}

		private static ICollection<TAttachment> CutAttachments<TAttachment>(
			ref IEnumerable<VkNet.Model.Attachments.Attachment> data, List<Attachment> attachments)
			where TAttachment : EntityVk, new()
		{
			var result = data.Where(x => x.Type.Name == typeof(TAttachment).Name)
				.Select(y =>
				{
					var obj = new TAttachment();
					obj.FromVkObject(y.Instance);
					var attach = new Attachment(y);
					attach.Instance = obj;
					attachments.Add(attach);
					return obj;
				}).ToList();
			data = data.Where(x => x.Type.Name != typeof(TAttachment).Name);
			return result;
		}

		private List<User> LoadPostUsers(Post post, LikesFilter filter = null)
		{
			var reposters = new List<User>();
			for (var liksPage = 0; liksPage < int.MaxValue; liksPage++)
			{
				var gotUsers = Api.Likes.GetList(new LikesGetListParams
				{
					Type = LikeObjectType.Post,
					OwnerId = post.OwnerId,
					ItemId = post.Id,
					Offset = (uint)(liksPage * 100),
					Filter = filter
				})?.Select(x => new User() { Id = x }).ToList() ?? new List<User>();
				reposters.AddRange(gotUsers);
				if (gotUsers.Count == 0) break;
			}
			return reposters;
		}

		private IEnumerable<PostItem> LoadPosts(int page, int pageSize, out int count)
		{
			var loaded = Api.Wall.Get(new WallGetParams
			{
				Domain = DomainSubName,
				Count = (ulong)pageSize,
				Offset = (ulong)(page * pageSize),
			});
			count = loaded?.WallPosts?.Count ?? -1;
			var data = loaded?.WallPosts?.Select(postObj =>
			{
				var post = new Post();
				post.FromVkObject(postObj);
				var likers = LoadPostUsers(post);
				var reposters = LoadPostUsers(post, LikesFilter.Copies);

				return new PostItem
				{
					Post = post,
					Likers = likers,
					Reposters = reposters,
					Attachments = postObj.Attachments
				};

			}).ToList() ?? new List<PostItem>();

			return data;
		}
	}
}
