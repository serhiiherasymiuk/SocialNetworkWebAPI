﻿using Core.Interfaces;
using Core.Entities;
using Core.Specifications;
using Core.DTOs;
using AutoMapper;
using Core.Helpers;
using System.Net;
using Core.Resources;

namespace Core.Services
{
    public class PostsService : IPostsService
    {
        private readonly IRepository<Post> postsRepo;
        private readonly IMapper mapper;

        public PostsService(IRepository<Post> postsRepo, IMapper mapper)
        {
            this.postsRepo = postsRepo;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<PostDTO>> GetAll()
        {
            var posts = await postsRepo.GetAllBySpec(new Posts.OrderedByLikes());
            return mapper.Map<IEnumerable<PostDTO>>(posts);
        }
        public async Task<PostDTO?> GetById(int id)
        {
            Post post = await postsRepo.GetBySpec(new Posts.ById(id));
            if (post == null)
                throw new HttpException(ErrorMessages.PostByIdNotFound, HttpStatusCode.NotFound);
            return mapper.Map<PostDTO>(post);
        }
        public async Task<IEnumerable<PostDTO>> GetByUserId(string userId)
        {
            var posts = await postsRepo.GetAllBySpec(new Posts.ByUserId(userId));
            return mapper.Map<IEnumerable<PostDTO>>(posts);
        }
        public async Task Edit(PostDTO post)
        {
            await postsRepo.Update(mapper.Map<Post>(post));
            await postsRepo.Save();
        }

        public async Task Create(PostDTO post)
        {
            await postsRepo.Insert(mapper.Map<Post>(post));
            await postsRepo.Save();
        }

        public async Task Delete(int id)
        {
            if (await postsRepo.GetByID(id) == null)
                throw new HttpException(ErrorMessages.PostByIdNotFound, HttpStatusCode.NotFound);
            await postsRepo.Delete(id);
            await postsRepo.Save();
        }
    }
}