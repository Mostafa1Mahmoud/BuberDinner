namespace BuberDinnner.Contracts.Authntication;

public record LoginRequest(
    string Email,
    string Password
);