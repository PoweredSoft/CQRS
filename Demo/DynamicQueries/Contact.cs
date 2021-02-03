using System;

namespace Demo.DynamicQueries
{
    public class Contact
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
    }

    public class SearchContactParams
    {
        public string SearchDisplayName { get; set; }
    }
}
