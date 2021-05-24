using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using blogger_cs.Models;
using blogger_cs.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blogger_cs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly BlogsService _service;
        private readonly CommentsService _serviceComm;
    private readonly AccountsService _serviceAcct;

    public BlogsController(BlogsService service, CommentsService serviceComm, AccountsService serviceAcct)
    {
      _service = service;
      _serviceComm = serviceComm;
      _serviceAcct = serviceAcct;
    }

  [HttpGet]
  public ActionResult<IEnumerable<Blog>> GetAllBlogs()
  {
    try
    {
        IEnumerable<Blog> blogs = _service.GetAllBlogs();
        return Ok(blogs);
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public ActionResult<Blog> GetBlogById(int id)
  {
    try
    {
        Blog blog = _service.GetBlogById(id);
        return Ok(blog);
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/comments")]
  public ActionResult<IEnumerable<Comment>> GetCommentsByBlogId(int id)
  {
    try
    {
        IEnumerable<Comment> comments = _serviceComm.GetCommentsByBlogId(id);
        return Ok(comments);
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Blog>> Create([FromBody] Blog newBlog)
  {
    try
    {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        Account fullAccount = _serviceAcct.GetOrCreateAccount(account);
        newBlog.CreatorId = account.Id;

        Blog blog = _service.Create(newBlog);
        blog.Creator = fullAccount;
      return Ok(blog);
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<Blog>> DeleteBlog(int id)
  {
    try
    {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _service.DeleteBlog(id, userInfo.Id);
        return Ok("Successfully Deleted");
    }
    catch (System.Exception e)
    {
        return BadRequest(e.Message);
    }
  }

  // TODO put
[HttpPut("{id}")]
[Authorize]
public async Task<ActionResult<Blog>> Update(int id, [FromBody] Blog update)
{
  try
  {
      Account account = await HttpContext.GetUserInfoAsync<Account>();
      update.Id = id;
      Blog updated = _service.Update(update, account.Id);
      return Ok(updated);
  }
  catch (Exception e)
  {
      return BadRequest(e.Message);
  }
}





  }
}