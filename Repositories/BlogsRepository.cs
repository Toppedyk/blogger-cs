using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using blogger_cs.Models;
using Dapper;

namespace blogger_cs.Repositories
{
    public class BlogsRepository
    {
        private readonly IDbConnection _db;

    public BlogsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Blog> GetAllBlogs()
    {
      string sql = @"
      SELECT
      b.*,
      a.*
      FROM blogs b
      JOIN accounts a ON b.creatorId = a.id
      ";
      return _db.Query<Blog, Account, Blog>(sql, (blog, account) =>
      {
        blog.Creator = account;
        return blog;
      }, splitOn: "id"); 
    }

    internal IEnumerable<Blog> GetBlogsByCreatorId(string id)
    {
      string sql = @"
      SELECT 
      b.*,
      a.*
      FROM blogs b
      JOIN accounts a ON b.creatorId = a.id
      WHERE creatorId = @id";
      return _db.Query<Blog, Account, Blog>(sql, (blog, account) => 
      {
        blog.Creator= account;
        return blog;
      }, new{id}, splitOn:"id");
    }

    internal Blog GetBlogById(int id)
    {
      string sql = @"
      SELECT
      b.*,
      a.*
      FROM blogs b
      JOIN accounts a ON b.creatorId = a.id
      WHERE id = @id";
      return _db.Query<Blog, Account, Blog>(sql, (blog, account)=> 
      {
        blog.Creator = account;
        return blog;
      },new {id}, splitOn : "id").FirstOrDefault();
    }

    internal Blog Create(Blog newBlog)
    {
      string sql = @"
      INSERT INTO blogs
      (creatorId, title, body, imgUrl, published)
      VALUES
      (@CreatorId, @Title, @Body, @ImgUrl, @Published);
      SELECT LAST_INSERT_ID()";
      newBlog.Id = _db.ExecuteScalar<int>(sql, newBlog);
      return newBlog;
    }

    internal bool DeleteBlog(int id)
    {
      string sql = "DELETE FROM blogs WHERE id = @id LIMIT 1";
      int affectedRows = _db.Execute(sql, new {id});
      return affectedRows == 1;
    }
  }
}