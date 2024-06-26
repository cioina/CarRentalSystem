﻿namespace CarRentalSystem.Web.Services;

using System;
using System.Security.Claims;
using Application.Contracts;
using Microsoft.AspNetCore.Http;

public class CurrentUserService : ICurrentUser
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User!;
        if (user == null)
        {
            throw new InvalidOperationException("This request does not have an authenticated user.");
        }

        var claim = user!.FindFirstValue(ClaimTypes.NameIdentifier);
        this.UserId = claim!;
    }

    public string UserId { get; init; } = default!;
}