using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> Breakfasts = new();

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        Breakfasts.Add(breakfast.Id, breakfast);
        return Result.Created;    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (Breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpdatedBreakfastCallback> UpdateBreakfast(Breakfast breakfast)
    {
        var isNewlyCreated = !Breakfasts.ContainsKey(breakfast.Id);
        Breakfasts[breakfast.Id] = breakfast;
        return new UpdatedBreakfastCallback(isNewlyCreated);
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        Breakfasts.Remove(id);
        return Result.Deleted;
    }
}