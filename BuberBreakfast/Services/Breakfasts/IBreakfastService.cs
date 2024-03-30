using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;

namespace BuberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    void CreateBreakfast(Breakfast Breakfast);
    Breakfast GetBreakfast(Guid id);

    /*
    CreateBreakfastResponse UpdateBreakfast(Guid id, UpdateBreakfastRequest request);
    CreateBreakfastResponse DeleteBreakfast(Guid id);
    */
}