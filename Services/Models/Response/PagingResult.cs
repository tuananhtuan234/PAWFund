using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
	public class PagingResult<T>
	{
		public int TotalItems { get; set; }
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
		public List<T> Items { get; set; }
		public PagingResult(int totalItems, int totalPages, int currentPage, int pageSize, string? search, List<T> items)
		{
			TotalItems = totalItems;
			TotalPages = totalPages;
			CurrentPage = currentPage;
			PageSize = pageSize;
			Search = search;
			Items = items;
		}
	}
}
