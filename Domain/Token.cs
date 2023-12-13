namespace Domain;

public class Token
{
    public Guid Id { get; set; }
    public DateTime ExpireAt { get; set; }
    public bool IsValid { get; set; }
    public string RefreshToken { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}