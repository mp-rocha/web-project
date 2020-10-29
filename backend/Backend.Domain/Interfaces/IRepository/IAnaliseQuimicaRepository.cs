using Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces.IRepository
{
    public interface IAnaliseQuimicaRepository : IBaseRepository<AnaliseQuimica>
    {
        void CreateByUser(AnaliseQuimica entity);
        Task<IEnumerable<AnaliseQuimica>> GetAnaliseByUserId(Guid UserId);
    }
}
