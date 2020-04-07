using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(DbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<Cart>> Include(Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.Include(t => t.AppUser).ToListAsync();
            }
            var s = await RepoDbSet.Where(x => x.UserId == userId)
                .Include(t => t.AppUser)
                .ToListAsync();
            return s;
        }
        
        public async Task<IEnumerable<Cart>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync(); // base is not actually needed, using it for clarity
            }

            return await RepoDbSet.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<Cart> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.UserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.UserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }
       
        #region DTO methods
        public async Task<IEnumerable<CartDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.UserId == userId);
            }
            return await query
                .Select(o => new CartDTO()
                {
                    Id = o.Id,
                    UserId = o.UserId
                })
                .ToListAsync();
        }

        public async Task<CartDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.UserId == userId);
            }

            var cartDTO = await query.Select(o => new CartDTO()
            {
                Id = o.Id,
                UserId = o.UserId
            }).FirstOrDefaultAsync();

            return cartDTO;
        }
        #endregion
        
    }
}