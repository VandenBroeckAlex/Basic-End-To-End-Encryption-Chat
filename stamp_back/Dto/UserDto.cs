﻿namespace stamp_back.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public class UserRegisterDto
        {
            public string UserName { get; set; }
            public string Email { get; set; }

            public string Password { get; set; }
        }
    }
}
