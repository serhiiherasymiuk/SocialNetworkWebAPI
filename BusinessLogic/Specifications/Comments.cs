﻿using Ardalis.Specification;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public static class Comments
    {
        public class ById : Specification<Comment>
        {
            public ById(int id)
            {
                Query
                    .Where(c => c.Id == id)
                    .Include(c => c.CommentLikes);
            }
        }
        public class ByUserId : Specification<Comment>
        {
            public ByUserId(string userId)
            {
                Query
                    .Where(c => c.UserId == userId)
                    .Include(c => c.CommentLikes);
            }
        }
        public class ByPostId : Specification<Comment>
        {
            public ByPostId(int postId)
            {
                Query
                    .Where(c => c.PostId == postId)
                    .Include(c => c.CommentLikes);
            }
        }
    }
}
