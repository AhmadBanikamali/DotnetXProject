namespace Application.Common;

public record Response(object? Data = null, string? Message = "", bool IsSuccess = true);