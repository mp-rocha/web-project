using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImportacaoController : ControllerBase
    {
        public readonly IImportacaoService _importacaoService;

        public ImportacaoController(IImportacaoService importacaoService)
        {
            _importacaoService = importacaoService;
        }

        [HttpPost]
        public async Task<Response<List<AnaliseQuimica>>> ImportarDadosExcel(IFormFile Excel)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = new Guid(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await _importacaoService.Import(Excel, currentUserID);
        }
    }
}
