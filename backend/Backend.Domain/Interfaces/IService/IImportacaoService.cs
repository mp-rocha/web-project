using Backend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces.IService
{
    public interface IImportacaoService
    {
        Task<Response<List<AnaliseQuimica>>> Import(IFormFile arquivoExcel, Guid currentUserID);
    }
}
