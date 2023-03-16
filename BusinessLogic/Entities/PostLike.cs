﻿namespace Core.Entities
{
    public class PostLike
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
