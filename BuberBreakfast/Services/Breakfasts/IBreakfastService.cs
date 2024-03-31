using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    ErrorOr<Created> CreateBreakfast(Breakfast breakfast);
    ErrorOr<Breakfast> GetBreakfast(Guid id);

    ErrorOr<UpdatedBreakfastCallback> UpdateBreakfast(Breakfast breakfast);

    ErrorOr<Deleted> DeleteBreakfast(Guid id);
    /*
    CreateBreakfastResponse UpdateBreakfast(Guid id, UpdateBreakfastRequest request);
    CreateBreakfastResponse DeleteBreakfast(Guid id);
    */
}