using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhatToListen.Core.Entities.Vk;
using WhatToListen.Infrastructure.Data;

namespace WhatToListen.WebUI.Pages
{
    public class PostIndexModel : PageModel
    {
        private readonly WhatToListen.Infrastructure.Data.VkContext _context;

        public PostIndexModel(WhatToListen.Infrastructure.Data.VkContext context)
        {
            _context = context;
        }

		public PaginatedList<Post> Post { get; set; }
		public string LikesSort { get; set; }
		public string DateSort { get; set; }
		public string SameFilter { get; set; }
		public string CurrentFilter { get; set; }
		public string CurrentSort { get; set; }

		public async Task OnGetAsync(
			string sortOrder, string currentFilter, string sameFilter, string searchString,
			long id, int? pageIndex)
        {
			CurrentSort = sortOrder;
			LikesSort = string.IsNullOrEmpty(sortOrder) ? "likes_desc" : "";
			DateSort = sortOrder == "Date" ? "date_desc" : "Date";
			if (searchString != null)
			{
				pageIndex = 1;
			}
			else
			{
				searchString = currentFilter;
			}
			CurrentFilter = searchString;

			var posts = from s in _context.Posts
						select s;
			if (!string.IsNullOrEmpty(searchString))
			{
				posts = posts.Where(s => s.Text.Contains(searchString));
			}

			if (!string.IsNullOrEmpty(sameFilter))
			{
				var post = _context.Posts.Include(x => x.Users)
					.First(x => x.Id == long.Parse(sameFilter));
				ViewData["SamePostId"] = sameFilter;
				ViewData["SamePostOwnerId"] = post.OwnerId;
				ViewData["SamePostText"] = post.Text;

				const int MinMainPostLikes = 10;
				const int MinTargetLikes = 5;
				var samePosts = _context.PostUsers
					.Where(x => post.Users.Any(u => u.UserId == x.UserId))
					.GroupBy(x => x.PostId)
					.Select(x => new PostPost
					{
						ParentPost = post,
						ChildPost = _context.Posts.Find(x.Key),
						UsersCount = x.Select(z => z.UserId)
							.Intersect(post.Users.Select(u => u.UserId)).Count()
					})
					.Where(x => x.UsersCount >= MinTargetLikes)
					.OrderByDescending(x => x.UsersCount)
					.Join(_context.Posts, pp => pp.Id, p => p.Id, (pp, p) => p);

				posts = samePosts.Join(_context.Posts, pp => pp.Id, p => p.Id, (pp, p) => p);
			}

			switch (sortOrder)
			{
				case "likes_desc":
					posts = posts.OrderByDescending(s => s.Liks);
					break;
				case "Date":
					posts = posts.OrderBy(s => s.Date);
					break;
				case "date_desc":
					posts = posts.OrderByDescending(s => s.Date);
					break;
				default:
					posts = posts.OrderBy(s => s.Date);
					break;
			}

			var pageSize = 10;
			Post = await PaginatedList<Post>.CreateAsync(
				posts.AsNoTracking(), pageIndex ?? 1, pageSize);
		}
    }
}
