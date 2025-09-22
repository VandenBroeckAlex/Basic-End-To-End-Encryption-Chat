namespace stamp_back.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //public string icon_url { get; set; }

        //foreignkey
        public ICollection<UserChat> UserChats { get; set; }
    }
}
