namespace G5.Denuncias.BE.Domain.Utils;

public class PaginatedResponse <T>
{
    public IEnumerable<T> Data { get; set; }

    public PaginationInfo Pagination { get; set; }

    public class PaginationInfo
    {
        public int TotalRecords { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
