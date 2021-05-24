using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using blogger_cs.Models;
using Dapper;

namespace blogger_cs.Repositories
{
    public class CommentsRepository
    {
        private readonly IDbConnection _db;

    public CommentsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Comment> GetCommentsByCreatorId(string id)
    {
      string sql = @"
      SELECT 
      c.*,
      a.*
      FROM comments c 
      JOIN accounts a ON c.creatorId = a.id
      WHERE creatorId = @id";
      return _db.Query<Comment, Account, Comment>(sql,(comment, account)=>
      {
        comment.Creator = account;
        return comment;
      }, new {id}, splitOn:"id");
    }

    internal IEnumerable<Comment> GetCommentsByBlogId(int id)
    {
      string sql = @"
      SELECT 
      c.*,
      a.*
      FROM comments c 
      JOIN accounts a ON c.creatorId = a.id
      WHERE blog = @id";
      return _db.Query<Comment, Account, Comment>(sql, (comment, account)=> 
      {
        comment.Creator = account;
        return comment;
      }, new {id}, splitOn: "id");
    }

    internal Comment Create(Comment newComment)
    {
      string sql = @"
      INSERT INTO comments
      (creatorId, body, blog)
      VALUES
      (@CreatorId, @Body, @Blog);
      SELECT LAST_INSERT_ID()";
      newComment.Id = _db.ExecuteScalar<int>(sql, newComment);
      return newComment;
    }

    internal Comment GetCommentById(int id)
    {
            string sql = @"
      SELECT 
      c.*,
      a.*
      FROM comments c 
      JOIN accounts a ON c.creatorId = a.id
      WHERE id = @id";
      return _db.Query<Comment, Account, Comment>(sql, (comment, account)=> 
      {
        comment.Creator = account;
        return comment;
      }, new {id}, splitOn: "id").FirstOrDefault();
    }

    internal bool Update(Comment original)
    {
      string sql = @"
      UPDATE comments
      SET
      body=@Body,
      blog=@Blog
      WHERE id=@Id";
      int affectedRows = _db.Execute(sql, original);
      return affectedRows == 1;
    }

    internal bool DeleteComment(int id)
    {
      string sql = "DELETE FROM comments WHERE id = @id LIMIT 1";
      int affectedRows = _db.Execute(sql, new {id});
      return affectedRows == 1;
    }
  }
}