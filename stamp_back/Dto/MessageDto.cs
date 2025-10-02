namespace stamp_back.Dto
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string TimeStamp { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }

        public string UserName { get; set; } // Optional
        public string ChatName { get; set; } // Optional
    }

}
