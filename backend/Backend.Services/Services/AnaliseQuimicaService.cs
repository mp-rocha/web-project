using Backend.Domain.Entities;
using Backend.Domain.Interfaces.IRepository;
using Backend.Domain.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services.Services
{
    public class AnaliseQuimicaService : IAnaliseQuimicaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnaliseQuimicaService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public Response<AnaliseQuimica> Create(AnaliseQuimica obj)
        {
            obj.DateCreated = DateTime.Now;
            obj.DateUpdate = DateTime.Now;

            var dados = _unitOfWork.AnaliseQuimicaRepository.Create(obj);
            _unitOfWork.Commit();

            return Response<AnaliseQuimica>.GetResult(200, "OK", dados);
        }

        public async Task<Response<AnaliseQuimica>> GetByIdAsync(Guid id)
        {
            var dados = await _unitOfWork.AnaliseQuimicaRepository.GetByIdAsync(id);
            return Response<AnaliseQuimica>.GetResult(200, "OK", dados);
        }

        public async Task<Response<IEnumerable<AnaliseQuimica>>> ListAllAsync()
        {
            var dados = await _unitOfWork.AnaliseQuimicaRepository.ListAllAsync();
            return Response<IEnumerable<AnaliseQuimica>>.GetResult(200, "OK", dados);
        }

        public async Task<Response<AnaliseQuimica>> Remove(Guid id)
        {
            var dados = await _unitOfWork.AnaliseQuimicaRepository.GetByIdAsync(id);
            if (dados != null)
            {
                _unitOfWork.AnaliseQuimicaRepository.Remove(dados);
                _unitOfWork.Commit();
            }
            return Response<AnaliseQuimica>.GetResult(200, "OK", dados);
        }

        public async Task<Response<AnaliseQuimica>> UpdateAsync(AnaliseQuimica obj)
        {
            var dados = await _unitOfWork.AnaliseQuimicaRepository.GetByIdAsync(obj.Id);

            dados.DateUpdate = DateTime.Now;
            dados.Areia = obj.Areia;
            dados.Argila = obj.Argila;
            dados.Boro = obj.Boro;
            dados.Calcio = obj.Calcio;
            dados.Carbono = obj.Carbono;
            dados.Cobre = obj.Cobre;
            dados.CTC = obj.CTC;
            dados.Enxofre = obj.Enxofre;
            dados.Ferro = obj.Ferro;
            dados.Fosforo = obj.Fosforo;
            dados.Latitude = obj.Latitude;
            dados.Longitude = obj.Longitude;
            dados.Magnesio = obj.Magnesio;
            dados.Manganes = obj.Manganes;
            dados.MO = obj.MO;
            dados.pH = obj.pH;
            dados.pHTampao = obj.pHTampao;
            dados.Potassio = obj.Potassio;
            dados.RelacaoCA = obj.RelacaoCA;
            dados.RelacaoMg = obj.RelacaoMg;
            dados.SatBases = obj.SatBases;
            dados.Silte = obj.Silte;
            dados.Zinco = obj.Zinco;

            _unitOfWork.AnaliseQuimicaRepository.UpdateAsync(dados);
            _unitOfWork.Commit();

            return Response<AnaliseQuimica>.GetResult(200, "OK", dados);
        }

        public Response<List<decimal>> RecCalagem(decimal v2, decimal PRNT, Guid currentUserID)
        {
            List<decimal> resultado = new List<decimal>
            {
                
            };

            var amostras = _unitOfWork.AnaliseQuimicaRepository.GetAnaliseByUserId(currentUserID);

            foreach (var amostra in amostras.Result)
            {
                var NC = (amostra.CTC * (v2 - amostra.SatBases)) / (10 * PRNT);
                resultado.Add(NC);
            }

            return Response<List<decimal>>.GetResult(200, "OK", resultado);
        }

        public Response<AnaliseQuimica> CreateByUser(AnaliseQuimica analise)
        {
            analise.DateCreated = DateTime.Now;
            analise.DateUpdate = DateTime.Now;
            analise.Id = Guid.NewGuid();
            var userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            analise.UserId = userId;

            _unitOfWork.AnaliseQuimicaRepository.CreateByUser(analise);
            _unitOfWork.Commit();

            return Response<AnaliseQuimica>.GetResult(200, "OK", null);

        }

        public async Task<Response<IEnumerable<AnaliseQuimica>>> GetAnaliseByUserId(Guid UserId)
        {
            var dados = await _unitOfWork.AnaliseQuimicaRepository.GetAnaliseByUserId(UserId);
            return Response<IEnumerable<AnaliseQuimica>>.GetResult(200, "OK", dados);
        }

        public Response<IEnumerable<AnaliseQuimica>> CreateListByUser(List<AnaliseQuimica> analise, Guid id)
        {
            foreach(AnaliseQuimica obj in analise)
            {
                obj.DateCreated = DateTime.Now;
                obj.DateUpdate = DateTime.Now;
                obj.Id = Guid.NewGuid();
                obj.UserId = id;
            }

            _unitOfWork.AnaliseQuimicaRepository.CreateListByUser(analise);
            _unitOfWork.Commit();

            return Response<IEnumerable<AnaliseQuimica>>.GetResult(200, "OK", null);
        }
    }
}
