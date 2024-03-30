using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    void CreateBreakfast(Breakfast Breakfast);
    ErrorOr<Breakfast> GetBreakfast(Guid id);

    /*
    CreateBreakfastResponse UpdateBreakfast(Guid id, UpdateBreakfastRequest request);
    CreateBreakfastResponse DeleteBreakfast(Guid id);
    */
}