using System;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class LikesRepository(AppDbContext context) : ILikesRepository
{
    public void AddLike(MemberLike like)
    {
        context.Likes.Add(like);
    }

    public void DeleteLike(MemberLike like)
    {
        context.Likes.Remove(like);
    }

    public async Task<IReadOnlyList<string>> GetCurrentMemberLikeIds(string memberId)
    {
       return await context.Likes.Where(l => l.SourseMemberId == memberId)
       .Select(l => l.TargetMemberId).ToListAsync();
    }

    public async Task<MemberLike?> GetMemberLike(string sourceMemberId, string targetMemberId)
    {
        return await context.Likes.FindAsync(sourceMemberId, targetMemberId);
    }
    

    public async Task<PaginatedResult<Member>> GetMemberLikes(LikesParams likesParams)
    {
        var query = context.Likes.AsQueryable();
        IQueryable<Member> result;

        switch (likesParams.Predicate)
        {
            case "liked":
                result = query
                    .Where(like => like.SourseMemberId == likesParams.MemberId)
                    .Select(like => like.TargetMember);
                break;
            case "likedBy":
                result = query
                    .Where(like => like.TargetMemberId == likesParams.MemberId)
                    .Select(like => like.SourseMember);
                break;
            default: // mutual
                var likeIds = await GetCurrentMemberLikeIds(likesParams.MemberId);

                result = query
                    .Where(x => x.TargetMemberId == likesParams.MemberId
                        && likeIds.Contains(x.SourseMemberId))
                    .Select(x => x.SourseMember);
                break;
        }
        
        return await PaginationHelper.CreateAsync(result,
            likesParams.PageNumber, likesParams.PageSize);
    }
    public async Task<bool> SaveAllAsync()
    {
       return await context.SaveChangesAsync() > 0;
    }
}
