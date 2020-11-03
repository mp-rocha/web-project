using Backend.Domain.Entities;
using Backend.Domain.Interfaces.IRepository;
using Backend.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.Repository
{
    public class AnaliseQuimicaRepository : BaseRepository<AnaliseQuimica>, IAnaliseQuimicaRepository
    {
        public AnaliseQuimicaRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public void CreateByUser(AnaliseQuimica entity)
        {
            _context.Set<AnaliseQuimica>().Add(entity);
        }

        public void CreateListByUser(List<AnaliseQuimica> entity)
        {
            _context.Set<AnaliseQuimica>().AddRange(entity);
        }

        public async Task<IEnumerable<AnaliseQuimica>> GetAnaliseByUserId(Guid UserId)
        {
            return await _context.Set<AnaliseQuimica>().Where(x => x.UserId == UserId).ToListAsync();
        }
    }
}
