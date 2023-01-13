using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application;

public sealed class ApplicationUser : IdentityUser<int>, IUser
{
}