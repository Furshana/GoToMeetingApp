using GoToMeetingApp.Models;
using Microsoft.AspNetCore.Routing;

namespace GoToMeetingApp.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);

    }
}