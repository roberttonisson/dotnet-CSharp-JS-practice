﻿
using DAL.App.DTO;
using ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaInCartRepository  : IBaseRepository<PizzaInCart>, IPizzaInCartRepositoryCustom
    {
        
    }
}