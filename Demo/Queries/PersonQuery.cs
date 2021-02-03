using System.Collections.Generic;

namespace Demo.Queries
{
    public class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonQuery
    {
        public string Search { get; set; }
    }
}
