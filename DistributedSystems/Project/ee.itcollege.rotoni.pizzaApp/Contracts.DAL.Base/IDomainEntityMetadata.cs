using System;

namespace ee.itcollege.rotoni.pizzaApp.Contracts.DAL.Base
{
    public interface IDomainEntityMetadata
    {
        string? CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string? ChangedBy { get; set; }
        DateTime ChangedAt { get; set; }
    }
}