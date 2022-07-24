namespace AppApiDapper.Services
{
    public class PagingList<T> : List<T>
    {
        public PagingList(List<T> items, int count, int pageIndex, int PageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(count / (double)PageSize);
            AddRange(items);
        }

        public int PageIndex { get; set; }
        public int TotalPage { get; set; }

        public static PagingList<T> Create(IQueryable<T> source, int pageIndex, int PageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
            return new PagingList<T>(items, count, pageIndex, PageSize);
        }
    }
}
