using System.ComponentModel.DataAnnotations;

namespace VivesRental.Services.Model.Requests;

public class ProductRequest
{
    [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
    [Required] public string Name { get; set; } = null!;
    [StringLength(50,ErrorMessage = "Name length can't be more than 50.")]
    public string? Description { get; set; }
    [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
    public string? Manufacturer { get; set; }
    [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
    public string? Publisher { get; set; }
    [Range(0, 100, ErrorMessage = "Please enter number.")]
    public int RentalExpiresAfterDays { get; set; }
}