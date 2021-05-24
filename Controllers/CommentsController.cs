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
    public class CommentsController : ControllerBase
    {
        private readonly CommentsService _service;
    private readonly AccountsService _serviceAcct;

    public CommentsController(CommentsService service, AccountsService serviceAcct)
    {
      _service = service;
      _serviceAcct = serviceAcct;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Comment>> Create([FromBody] Comment newComment)
    {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      Account fullAccount = _serviceAcct.GetOrCreateAccount(userInfo);
      newComment.CreatorId = userInfo.Id;
      Comment comment = _service.Create(newComment);
      comment.Creator = fullAccount;
      return Ok(comment);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Comment>> DeleteComment(int id)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          _service.DeleteComment(id, userInfo.Id);
          return Ok("Successfully Deleted");
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Comment>> Update(int id,[FromBody] Comment update)
    {
      try
      {
          Account account = await HttpContext.GetUserInfoAsync<Account>();
          update.Id = id;
          Comment updated = _service.Update(account.Id, update);
          return updated;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }














  }
}