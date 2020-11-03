using Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces.IService
{
    public interface IAnaliseQuimicaService : IBaseService<AnaliseQuimica>
    {
        Response<List<decimal>> RecCalagem(decimal v2, decimal PRNT, Guid currentUserID);
        Response<AnaliseQuimica> CreateByUser(AnaliseQuimica analise);
        Response<IEnumerable<AnaliseQuimica>> CreateListByUser(List<AnaliseQuimica> analise, Guid id);
        Task<Response<IEnumerable<AnaliseQuimica>>> GetAnaliseByUserId(Guid UserId);
    }
}
