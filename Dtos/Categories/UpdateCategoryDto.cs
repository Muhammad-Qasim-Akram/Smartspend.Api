using System.ComponentModel.DataAnnotations;

namespace Smartspend.Api.Dtos.Categories;

public record UpdateCategoryDto
(
    [Required][StringLength(50)] string Name

);
