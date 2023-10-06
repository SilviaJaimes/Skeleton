using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class VerifyDto
{
    [Required]
    public string Code { get; set; } = String.Empty;

    [Required]
    public string Usuario { get; set; }
}