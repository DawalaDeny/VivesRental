using System.ComponentModel.DataAnnotations;

namespace VivesRental.Services.Model.Requests;

public class CustomerRequest
{
    [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
    [Required] public string FirstName { get; set; } = null!;
    [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
    [Required] public string LastName { get; set; } = null!;

    [Required][EmailAddress] public string Email { get; set; } = null!;
    [Range(0, long.MaxValue, ErrorMessage = "Please enter a realistic phone number.")]
    [Required] public string PhoneNumber { get; set; } = null!;
}