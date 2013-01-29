using System;
using Adell.ItCan.Interop.Entities;
using System.Collections.Generic;
namespace Adell.ItCan.Interop.Services
{
    public interface IHostListService
    {
        //TODO: move to async-keyword in C# 5
        IObservable<IEnumerable<desktop>> GetHostListAsync();
    }
}