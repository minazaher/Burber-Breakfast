using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
namespace BuberBreakfast.Controllers;

[ApiController]
[Route("breakfasts")]
public class BreakfastController(IBreakfastService breakfastService) : ApiController

{
    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }
        var breakfast = requestToBreakfastResult.Value;

        // TODO: Save to Database
        var createBreakfastResult = breakfastService.CreateBreakfast(breakfast);
        return createBreakfastResult.Match(
            created => CreatedAtGetBreakfast(breakfast),
            Problem);
    }
     
    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        var getBreakfastResult = breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            Problem);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateBreakfast(Guid id, UpdateBreakfastRequest request)
    {
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(id, request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;
        var updateBreakfastResult =breakfastService.UpdateBreakfast(breakfast);

        return updateBreakfastResult.Match(
            updated => updated.IsNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
            Problem);
   
}
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        var deleteBreakfastResult = breakfastService.DeleteBreakfast(id);
        
        return deleteBreakfastResult.Match(
            deleted => NoContent(),
            Problem );
    }

    public CreateBreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        var response = new CreateBreakfastResponse(breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet);
        return response;
    }
    
    private CreatedAtActionResult CreatedAtGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast));
    }
}