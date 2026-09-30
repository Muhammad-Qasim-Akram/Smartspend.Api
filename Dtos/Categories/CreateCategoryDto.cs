using System.ComponentModel.DataAnnotations;

namespace Smartspend.Api.Dtos.Categories;

public record CreateCategoryDto
(
    [Required][StringLength(50)] string Name

);
