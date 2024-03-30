using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, Breakfast> Breakfasts = new();

    public void CreateBreakfast(Breakfast breakfast)
    {
        Breakfasts.Add(breakfast.Id, breakfast);
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (Breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }
        return Errors.Breakfast.NotFound;
    }
}    