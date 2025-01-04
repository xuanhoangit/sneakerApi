using Microsoft.AspNetCore.Mvc;
using InventoryService.Core.Interfaces;

public abstract class BaseController : ControllerBase
{   
    private readonly IUnitOfWork _uow;
    public BaseController(IUnitOfWork uow)
    {
        _uow = uow;
    }
}