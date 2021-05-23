using System;
using System.Collections.Generic;
using blogger_cs.Models;
using blogger_cs.Services;
using Microsoft.AspNetCore.Mvc;

namespace blogger_cs.Controllers
{

[ApiController]
[Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly AccountsService _service;
        private readonly BlogsService _serviceBlog;
        private readonly CommentsService _serviceComm;

    public ProfileController(AccountsService service, BlogsService serviceBlog, CommentsService serviceComm)
    {
      _service = service;
      _serviceBlog = serviceBlog;
      _serviceComm = serviceComm;
    }

[HttpGet("{id}")]
public ActionResult<Account> GetProfileById(string id)
{
  try
  {
      Account profile = _service.GetProfileById(id);
      return Ok(profile);
  }
  catch (Exception e)
  {
      return BadRequest(e.Message);
  }
}

[HttpGet("{id}/blogs")]
public ActionResult<IEnumerable<Blog>> GetBlogsByProfileId(string id)
{
  try
  {
      IEnumerable<Blog> blogs = _serviceBlog.GetBlogsByCreatorId(id);
      return Ok(blogs);
  }
  catch (Exception e)
  {
      return BadRequest(e.Message);
  }
}

[HttpGet("{id}/comments")]
public ActionResult<IEnumerable<Comment>> GetCommentsByProfileId(string id)
{
  try
  {
      IEnumerable<Comment> comments = _serviceComm.GetCommentsByCreatorId(id);
      return Ok(comments);
  }
  catch (Exception e)
  {
      return BadRequest(e.Message);
  }
}






  }
}