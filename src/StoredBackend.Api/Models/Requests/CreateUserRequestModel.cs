using System;
using System.ComponentModel.DataAnnotations;

namespace StoreBackend.Api.Models.Requests;

public class CreateUserRequestModel
{
    [Required]
    public Guid? UserResourceId { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }
}
