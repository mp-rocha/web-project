using Backend.Domain.Entities;
using Backend.Domain.Interfaces.IRepository;
using Backend.Domain.Interfaces.IService;
using Microsoft.AspNetCore.Identity;
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

        public AnaliseQuimicaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AnaliseQuimica Create(AnaliseQuimica obj)
        {
            obj.DateCreated = DateTime.Now;
            obj.DateUpdate = DateTime.Now;

            var analise = _unitOfWork.AnaliseQuimicaRepository.Create(obj);
            _unitOfWork.Commit();

            return analise;
        }

        public async Task<AnaliseQuimica> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.AnaliseQuimicaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<AnaliseQuimica>> ListAllAsync()
        {
            return await _unitOfWork.AnaliseQuimicaRepository.ListAllAsync();
        }

        public async Task<AnaliseQuimica> Remove(Guid id)
        {
            var entity = await _unitOfWork.AnaliseQuimicaRepository.GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.AnaliseQuimicaRepository.Remove(entity);
                _unitOfWork.Commit();
            }
            return entity;
        }

        public async Task<AnaliseQuimica> UpdateAsync(AnaliseQuimica obj)
        {
            var entity = await _unitOfWork.AnaliseQuimicaRepository.GetByIdAsync(obj.Id);

            entity.DateUpdate = DateTime.Now;
            entity.Areia = obj.Areia;
            entity.Argila = obj.Argila;
            entity.Boro = obj.Boro;
            entity.Calcio = obj.Calcio;
            entity.Carbono = obj.Carbono;
            entity.Cobre = obj.Cobre;
            entity.CTC = obj.CTC;
            entity.Enxofre = obj.Enxofre;
            entity.Ferro = obj.Ferro;
            entity.Fosforo = obj.Fosforo;
            entity.Latitude = obj.Latitude;
            entity.Longitude = obj.Longitude;
            entity.Magnesio = obj.Magnesio;
            entity.Manganes = obj.Manganes;
            entity.MO = obj.MO;
            entity.pH = obj.pH;
            entity.pHTampao = obj.pHTampao;
            entity.Potassio = obj.Potassio;
            entity.RelacaoCA = obj.RelacaoCA;
            entity.RelacaoMg = obj.RelacaoMg;
            entity.SatBases = obj.SatBases;
            entity.Silte = obj.Silte;
            entity.Zinco = obj.Zinco;

            _unitOfWork.AnaliseQuimicaRepository.UpdateAsync(entity);
            _unitOfWork.Commit();
            return entity;
        }

        public List<decimal> RecCalagem(decimal v2, decimal PRNT, Guid currentUserID)
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

            return resultado;
        }

        public void CreateByUser(AnaliseQuimica analise, Guid id)
        {
            analise.DateCreated = DateTime.Now;
            analise.DateUpdate = DateTime.Now;
            analise.Id = Guid.NewGuid();

            analise.UserId = id;

            _unitOfWork.AnaliseQuimicaRepository.CreateByUser(analise);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<AnaliseQuimica>> GetAnaliseByUserId(Guid UserId)
        {
            return await _unitOfWork.AnaliseQuimicaRepository.GetAnaliseByUserId(UserId);
        }
    }
}
