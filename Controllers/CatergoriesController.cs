using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SmartSpend.Api.Interfaces;

namespace Smartspend.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class CatergoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CatergoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    
}
