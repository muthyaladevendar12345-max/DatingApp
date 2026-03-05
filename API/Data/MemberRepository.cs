using System;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MemberRepository(AppDbContext context) : IMemberRepository
{
    public async Task<Member?> GetMemberByIDAsync(string id)
    {
        return await context.Members.FindAsync(id);
    }

    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    {
       return await context.Members.ToListAsync();
    }

    public async Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(string memberId)
    {
        return await context.Members.Where(m => m.Id == memberId)
            .SelectMany(m => m.Photos)
            .ToListAsync();
    }

    public Task<bool> SaveAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;    
    }

   

    public void Update(Member member)
    {
        context.Entry(member).State = EntityState.Modified;
    }
}
