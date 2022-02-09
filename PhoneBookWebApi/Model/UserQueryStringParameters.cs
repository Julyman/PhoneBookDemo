namespace Dme.PhoneBook.Models
{
    public class UserQueryStringParameters : QueryStringParameters
    {
        public UserQueryStringParameters()
        {
            OrderBy = "FirstName";
        }

        // order by elements
        public string FirstName { get; set; }
    }
}
