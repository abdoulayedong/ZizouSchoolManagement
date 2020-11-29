namespace SchoolManagement.UI.Library.Models
{
    public interface ILoggedInUser
    {
        string Email { get; set; }
        string FirstName { get; set; }
        string FullName { get; }
        int Id { get; set; }
        string LastName { get; set; }
        string Token { get; set; }

        void Clear();
    }
}