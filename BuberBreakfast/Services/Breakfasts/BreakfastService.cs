using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Persistence;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService(DbContext dbContext) : IBreakfastService
{
    private readonly DbContext _dbContext = dbContext;

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _dbContext.Add(breakfast);
        _dbContext.SaveChanges();
        return Result.Created;    
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if (_dbContext.Breakfasts.Find(id) is { } breakfast)
        {
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpdatedBreakfastCallback> UpdateBreakfast(Breakfast breakfast)
    {
        var isNewlyCreated = _dbContext.Breakfasts.Find(breakfast.Id) is not null;
        if (isNewlyCreated)
        {
            _dbContext.Add(breakfast);
        }
        else
        {
            _dbContext.Update(breakfast);
        }
        _dbContext.SaveChanges();
        return new UpdatedBreakfastCallback(isNewlyCreated);
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        var breakfast = _dbContext.Breakfasts.Find(id);
        if (breakfast == null)
        {
            return Errors.Breakfast.NotFound;
        }
        _dbContext.Remove(breakfast);
        _dbContext.SaveChanges();
        return Result.Deleted;
    }
}