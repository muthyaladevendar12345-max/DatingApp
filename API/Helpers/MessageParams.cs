using System;

namespace API.Helpers;

public class MessageParams:PagingParams
{
public string? memberId { get; set; }
public string? Container { get; set; }="Inbox";

}
