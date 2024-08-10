using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces;

public interface IJwtTokenGenerator{
    string GenerateToken(User user);
}