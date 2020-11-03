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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnaliseQuimicaController : ControllerBase
    {
        public readonly IAnaliseQuimicaService _analiseService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AnaliseQuimicaController(IAnaliseQuimicaService analiseService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _analiseService = analiseService;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet("RecCalcario")]
        public Response<List<decimal>> GetResultado(decimal v2, decimal PRNT)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = new Guid(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var resultado = _analiseService.RecCalagem(v2, PRNT, currentUserID);
            return resultado;
        }

        [HttpGet("AnaliseByUser")]
        public async Task<Response<IEnumerable<AnaliseQuimica>>> GetAnaliseByUserId()
        {
            //ClaimsPrincipal currentUser = this.User;
            //var currentUserID = new Guid(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userId = new Guid(_userManager.GetUserId(HttpContext.User));
            return await _analiseService.GetAnaliseByUserId(userId);
        }

        [HttpGet]
        public async Task<Response<IEnumerable<AnaliseQuimica>>> GetAllAnaliseListAsync()
        {
           return await _analiseService.ListAllAsync();
        }

        [HttpGet("id")]
        public async Task<Response<AnaliseQuimica>> GetAnaliseByIdAsync(Guid id)
        {
            return await _analiseService.GetByIdAsync(id);
        }

        [HttpPost]
        public Response<AnaliseQuimica> SalvarAnaliseBanco([FromForm] AnaliseQuimica analise)
        {
            //ClaimsPrincipal currentUser = this.User;
            //var currentUserID = new Guid(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            return _analiseService.CreateByUser(analise);
        }

        [HttpPut]
        public async Task<Response<AnaliseQuimica>> PutAnalise([FromForm]AnaliseQuimica analiseQuimica)
        {
            return await _analiseService.UpdateAsync(analiseQuimica);
        }

        [HttpDelete("{id}")]
        public async Task<Response<AnaliseQuimica>> DeleteAnalise([FromForm] Guid id)
        {
            return await _analiseService.Remove(id);
        }
    }
}
