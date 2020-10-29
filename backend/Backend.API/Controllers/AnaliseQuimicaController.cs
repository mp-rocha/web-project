using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Domain.Entities;
using Backend.Domain.Interfaces.IService;
using Backend.Repository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnaliseQuimicaController : ControllerBase
    {
        public readonly IAnaliseQuimicaService _analiseService;

        public AnaliseQuimicaController(IAnaliseQuimicaService analiseService)
        {
            _analiseService = analiseService;
        }


        [HttpGet("RecCalcario")]
        public List<decimal> GetResultado(decimal v2, decimal PRNT)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = new Guid(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var resultado = _analiseService.RecCalagem(v2, PRNT, currentUserID);
            return resultado;
        }

        [HttpGet("AnaliseByUser")]
        public async Task<IEnumerable<AnaliseQuimica>> GetAnaliseByUserId()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = new Guid(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            return await _analiseService.GetAnaliseByUserId(currentUserID);
        }

        [HttpGet]
        public async Task<IEnumerable<AnaliseQuimica>> GetAllAnaliseListAsync()
        {
           var analises = await _analiseService.ListAllAsync();
           return analises;
        }

        [HttpGet("id")]
        public async Task<ActionResult<AnaliseQuimica>> GetAnaliseByIdAsync([FromForm] Guid id)
        {
            var analise = await _analiseService.GetByIdAsync(id);
            if (analise != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<AnaliseQuimica> SalvarAnaliseBanco([FromForm] AnaliseQuimica analise)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = new Guid(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            _analiseService.CreateByUser(analise, currentUserID);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutAnalise([FromForm]AnaliseQuimica analiseQuimica)
        {
            await _analiseService.UpdateAsync(analiseQuimica);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<AnaliseQuimica> DeleteAnalise([FromForm] Guid id)
        {
            _analiseService.Remove(id);
            return Ok();
        }
    }
}
