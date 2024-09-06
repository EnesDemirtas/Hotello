﻿using Microsoft.AspNetCore.Identity;

namespace Hotello.API.Data;

public class ApiUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
