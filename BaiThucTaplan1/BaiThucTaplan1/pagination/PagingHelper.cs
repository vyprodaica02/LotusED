namespace BaiThucTaplan1.pagination
{
    public class PagingHelper<T>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }

        public PagingHelper(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public List<T> GetPagedResult(List<T> data)
        {
            TotalCount = data.Count;

            return data
                .Skip(PageSize * (PageNumber - 1))
                .Take(PageSize)
                .ToList();
        }
    }

}
