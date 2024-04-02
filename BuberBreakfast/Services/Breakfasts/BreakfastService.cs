using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Persistence;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService(mDbContext mDbContext) : IBreakfastService
{
    private readonly mDbContext _mDbContext = mDbContext;

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _mDbContext.Add(breakfast);
        _mDbContext.SaveChanges();
        return Result.Created;    
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (_mDbContext.Breakfasts.Find(id) is { } breakfast)
        {
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpdatedBreakfastCallback> UpdateBreakfast(Breakfast breakfast)
    {
        var isNewlyCreated = !_mDbContext.Breakfasts.Any((b => b.Id == breakfast.Id));
        if (isNewlyCreated)
        {
            _mDbContext.Add(breakfast);
        }
        else
        {
            _mDbContext.Update(breakfast);
        }
        _mDbContext.SaveChanges();
        return new UpdatedBreakfastCallback(isNewlyCreated);
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        var breakfast = _mDbContext.Breakfasts.Find(id);
        if (breakfast == null)
        {
            return Errors.Breakfast.NotFound;
        }
        _mDbContext.Remove(breakfast);
        _mDbContext.SaveChanges();
        return Result.Deleted;
    }
}