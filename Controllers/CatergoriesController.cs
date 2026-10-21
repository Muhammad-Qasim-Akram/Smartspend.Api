using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Smartspend.Api.Dtos.Categories;
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

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAllCategory()
    {
        var userId = GetUserId();
        var result = await _categoryService.GetAllAsync(userId);
        return Ok(result); 
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var userId = GetUserId();
        var result = await _categoryService.GetByIdAsync(id,userId);
        if(result is null) return NotFound();
        return Ok(result); 
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto dto)
    {
        var userId = GetUserId();
        var result = await _categoryService.CreateAsync(userId,dto);
        if(result is null) return BadRequest("Invalid Category");
        return CreatedAtAction(nameof(GetById),new { id = result.Id},result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(int id,UpdateCategoryDto dto)
    {
        var userId = GetUserId();
        var result = await _categoryService.UpdateAsync(id,userId,dto);
        if(result is null) return NotFound();
        return Ok(result); 
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var userId = GetUserId();
        var result = await _categoryService.DeleteAsync(id,userId);
        if(!result) return NotFound();
        return NoContent();
    }
}   
