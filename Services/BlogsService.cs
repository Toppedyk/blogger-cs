using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using blogger_cs.Models;
using blogger_cs.Repositories;
using Microsoft.AspNetCore.Http;

namespace blogger_cs.Services
{
    public class BlogsService
    {
        private readonly BlogsRepository _repo;

    public BlogsService(BlogsRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<Blog> GetBlogsByCreatorId(string id)
    {
      return _repo.GetBlogsByCreatorId(id);
    }

    internal IEnumerable<Blog> GetAllBlogs()
    {
      return _repo.GetAllBlogs();
    }

    internal Blog GetBlogById(int id)
    {
      Blog blog = _repo.GetBlogById(id);
      if(blog == null)
      {
        throw new Exception("Invalid ID");
      }
      return blog;
    }

    internal Blog Create(Blog newBlog)
    {
      return _repo.Create(newBlog);
    }

    internal void DeleteBlog(int id, string creatorId)
    {
      Blog blog = _repo.GetBlogById(id);
      if(blog.CreatorId != creatorId)
      {
        throw new Exception("You cannot delete another user's blog");
      }
      if(!_repo.DeleteBlog(id))
      {
        throw new Exception("Something's Gone Wrong!");
      }
    }

    internal Blog Update(Blog update, string id)
    {
      Blog original = GetBlogById(update.Id);
      if(original.CreatorId != id){
        throw new Exception("You cannot edit another users blog");
      }
      original.Title = update.Title.Length > 0 ? update.Title : original.Title;
      original.Body = update.Body.Length > 0 ? update.Body : original.Body;
      original.ImgUrl = update.ImgUrl.Length > 0 ? update.ImgUrl : original.ImgUrl;
      original.Published = update.Published != original.Published ? update.Published : original.Published;
      if(_repo.Update(original))
      {
        return original;
      }
      throw new Exception("Something went Wrong");
    }
  }
}