namespace HotelListing.API.Core.Middleware;

public class QueryParameters
{
    private int pageSize = 15; // Default page size
    
    /// Record number where we'll start to recover records from
    public int StartIndex { get; set; }

    /// Number of records per page (default 15)
    public int PageSize
    {
        get { return pageSize; }
        set {  pageSize = value; }
    }
}
