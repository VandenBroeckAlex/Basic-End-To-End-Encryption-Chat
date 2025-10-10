namespace stamp_back.Dto
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string TimeStamp { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }


        public class SendMessageDto
        {

            public required string Body { get; set; }
            public required Guid UserId { get; set; }
            public required Guid ChatId { get; set; }

        }
    }

}
