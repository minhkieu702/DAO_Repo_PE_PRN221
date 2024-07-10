using DataAccessObject.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessObject.Models;

public partial class Eyeglass
{
    [Required]
    public int EyeglassesId { get; set; }
    [Required]
    [EyeglassesNameValidation]
    public string EyeglassesName { get; set; } = null!;
    [Required]
    public string? EyeglassesDescription { get; set; }
    [Required]
    public string? FrameColor { get; set; }
    [Required]
    public decimal? Price { get; set; }
    [Required]
    public int? Quantity { get; set; }
    [Required]
    public DateTime? CreatedDate { get; set; }
    [Required]
    public string? LensTypeId { get; set; }

    public virtual LensType? LensType { get; set; }
}
