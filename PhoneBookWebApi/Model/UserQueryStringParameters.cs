namespace Dme.PhoneBook.Model
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
