using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using blogger_cs.Models;
using blogger_cs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;

namespace blogger_cs.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly AccountsService _service;
        private readonly BlogsService _serviceBlog;
        private readonly CommentsService _serviceComm;

    public AccountController(AccountsService service, BlogsService serviceBlog, CommentsService serviceComm)
    {
      _service = service;
      _serviceBlog = serviceBlog;
      _serviceComm = serviceComm;
    }

[HttpGet]
public async Task<ActionResult<Account>> Get()
{
  try
  {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      Account currentUser = _service.GetOrCreateAccount(userInfo);
      return Ok(currentUser);
  }
  catch (Exception e)
  {
      return BadRequest(e.Message);
  }
}

[HttpGet("blogs")]
public async Task<ActionResult<IEnumerable<Blog>>> GetMyBlogs()
{
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      IEnumerable<Blog> blogs = _serviceBlog.GetBlogsByCreatorId(userInfo.Id);
      return Ok(blogs);
}

[HttpGet("comments")]
public async Task<ActionResult<IEnumerable<Comment>>> GetMyComments()
{
  try
  {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      IEnumerable<Comment> comments = _serviceComm.GetCommentsByCreatorId(userInfo.Id);
      return Ok(comments);
  }
  catch (Exception e)
  {
      return BadRequest(e.Message);
  }
}


// TODO HttpPut




  }


  
}