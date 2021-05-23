using System;
using System.Collections.Generic;
using blogger_cs.Models;
using blogger_cs.Repositories;

namespace blogger_cs.Services
{
    public class CommentsService
    {
        private readonly CommentsRepository _repo;

    public CommentsService(CommentsRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<Comment> GetCommentsByCreatorId(string id)
    {
      return _repo.GetCommentsByCreatorId(id);
    }

    internal IEnumerable<Comment> GetCommentsByBlogId(int id)
    {
      return _repo.GetCommentsByBlogId(id);
    }

    internal Comment Create(Comment newComment)
    {
      return _repo.Create(newComment);
    }

    internal void DeleteComment(int id, string creatorId)
    {
      Comment comment = GetCommentById(id);
      if(comment.CreatorId != creatorId)
      {
        throw new Exception("You cannot delete another user's comment");
      }
      if(!_repo.DeleteComment(id))
      {
        throw new Exception("Something's Gone Wrong");
      }
    }

    private Comment GetCommentById(int id)
    {
      return _repo.GetCommentById(id);
    }
  }
}