using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data;

public class AppDbContext(DbContextOptions options):DbContext(options)
{
  public DbSet<AppUser> Users { get; set; }

    public DbSet<Member> Members { get; set; }
    public DbSet<Photo> Photos { get; set; }

    public DbSet<MemberLike> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MemberLike>()
        .HasKey(k => new {k.SourseMemberId,k.TargetMemberId});


        modelBuilder.Entity<MemberLike>()
        .HasOne(s => s.SourseMember)
        .WithMany(l => l.LikedMembers)
        .HasForeignKey(S => S.SourseMemberId)
        .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<MemberLike>()
        .HasOne(t => t.TargetMember)
        .WithMany(l => l.LikedByMembers)
        .HasForeignKey(T => T.TargetMemberId)
        .OnDelete(DeleteBehavior.NoAction);

        var dateTimeCoverter=new ValueConverter<DateTime,DateTime>(
          v =>v.ToUniversalTime(),
          v => DateTime.SpecifyKind(v,DateTimeKind.Utc)
        );

        foreach(var entityType in modelBuilder.Model.GetEntityTypes())
    {
      foreach(var property in entityType.GetProperties())
      {
        if (property.ClrType == typeof(DateTime))
        {
          property.SetValueConverter(dateTimeCoverter);
        }
      }
    }
    }
}
