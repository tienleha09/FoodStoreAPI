namespace FoodStoreAPI.Utilities
{
    public class RequestParameters
    {
        const int maxPageSize = 25;
        private int _pageSize = 20;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize =(value > maxPageSize) ? maxPageSize: value; }
        }
        public int PageNumber { get; set; } = 1;
    }

    
}
