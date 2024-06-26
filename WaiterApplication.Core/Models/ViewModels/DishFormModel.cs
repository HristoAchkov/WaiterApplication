﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using WaiterApplication.Core.Contracts;
using static WaiterApplication.Infrastructure.Constants.DataConstants;

namespace WaiterApplication.Core.Models.ViewModel
{
    public class DishFormModel : IDishModel
    {
        [Required]
        [MinLength(MinNameLength)]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Description]
        [MinLength(MinDescriptionLength)]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(MaxUrlLength)]
        public string Image { get; set; } = string.Empty;
        [Required]
        [Range(0.00, 1000.00, ErrorMessage = PriceRange)]
        public decimal Price { get; set; }
        public string? Ingredients { get; set; }
    }
}
