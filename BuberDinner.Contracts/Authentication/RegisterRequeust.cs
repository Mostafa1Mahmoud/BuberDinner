namespace BuberDinnner.Contracts.Authntication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);
