using System;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class LikesController(ILikesRepository likesRepository) : BaseApiController
{
    [HttpPost("{targetMemberId}")]
    public async Task<ActionResult> ToggleLike(string targetMemberId)
    {
        var sourceMemberId = User.GetMemberId();

        var like = await likesRepository.GetMemberLike(sourceMemberId, targetMemberId);

        if (like != null)
        {
            likesRepository.DeleteLike(like);
            if (await likesRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to unlike user");
        }

        like = new MemberLike
        {
            SourseMemberId = sourceMemberId,
            TargetMemberId = targetMemberId
        };

        likesRepository.AddLike(like);

        if (await likesRepository.SaveAllAsync()) return Ok();

        return BadRequest("Failed to like user");
    }

     [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<string>>> GetCurrentMemberLikeIds()
    {
        return Ok(await likesRepository.GetCurrentMemberLikeIds(User.GetMemberId()));
    }

      [HttpGet]
    public async Task<ActionResult<PaginatedResult<Member>>> GetMemberLikes(
     [FromQuery] LikesParams likesParams)
    {
        likesParams.MemberId = User.GetMemberId();
        var members = await likesRepository.GetMemberLikes(likesParams);
        return Ok(members);
    }
}
