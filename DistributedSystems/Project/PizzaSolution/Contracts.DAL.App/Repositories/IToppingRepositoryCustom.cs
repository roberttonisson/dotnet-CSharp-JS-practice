using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IToppingRepositoryCustom: IToppingRepositoryCustom<Topping>
    {
    }

    public interface IToppingRepositoryCustom<TTopping>
    {
    }
    
}