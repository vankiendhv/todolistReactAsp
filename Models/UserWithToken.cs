namespace todolistReactAsp.Models
{
    public class UserWithToken : User
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserWithToken(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
        }
    }
}