using Luisapangilinan.Midterm.Infrastructure.Domain;
using Luisapangilinan.Midterm.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Luisapangilinan.Midterm.Pages.Manage.Classes
{
    public class Index : PageModel
    {
        private DefaultDbContext _context;
        private ILogger<Index> _logger;

        [BindProperty]
        public ViewModel View { get; set; }

        public Index(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public void OnGet(int? pageIndex = 1, int? pageSize = 10, string? sortBy = "", SortOrder sortOrder = SortOrder.Ascending, string? keyword = "", Guid? courseID = null)
        {
            var skip = (int)((pageIndex - 1) * pageSize);

            var query = _context.Classes
                                .Include(a => a.Course)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.Code != null && a.Code.ToLower().Contains(keyword.ToLower())
                        || a.YearLevel != null && a.YearLevel.ToLower().Contains(keyword.ToLower())
                );
            }

            if (courseID != null)
            {
                query = query.Where(a => a.CourseId ==courseID );
            }

            var totalRows = query.Count();

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.ToLower() == "code" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Code);
                }
                else if (sortBy.ToLower() == "code" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Code);
                }
                else if (sortBy.ToLower() == ""&& sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.CourseId);
                }
                else if (sortBy.ToLower() == "" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.CourseId);
                }
            }

            var classes = query
                            .Skip(skip)
                            .Take((int)pageSize)
                            .ToList();

            if (courseID != null)
            {
                View.CourseID = courseID;
                View.Courses = classes.FirstOrDefault()?.Course?.Name;
            }

            View.Classes = new Paged<Class>()
            {
                Items = classes,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows,
                SortBy = sortBy,
                SortOrder = sortOrder,
                Keyword = keyword,
            };
        }

        public JsonResult? OnGetRolesLookup(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {

            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.Name != null && a.Name.ToLower().StartsWith(keyword.ToLower())
                );
            }

            return new JsonResult(query?.Select(a => new LookupDto.Result()
            {
                Id = a.Id.ToString(),
                Text = a.Name
            })
            .OrderBy(a => a.Text)
            .GetLookupPaged(pageIndex, pageSize));
        }

        public class ViewModel
        {
            public Paged<Class>? Classes { get; set; }
            public Guid? CourseID { get; set; }
            public string? Courses { get; set; }
        }


    }
}

