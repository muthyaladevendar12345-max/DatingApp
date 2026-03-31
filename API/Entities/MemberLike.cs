using System;

namespace API.Entities;

public class MemberLike
{
public required string SourseMemberId { get; set; }
public Member SourseMember { get; set; }=null!;
public required string TargetMemberId  { get; set; }
public Member TargetMember { get; set; }=null!;
}
