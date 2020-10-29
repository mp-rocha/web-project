using Backend.Domain.Interfaces.IRepository;
using Backend.Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext _context;

        public IProductRepository ProductRepository { get; }

        public IAnaliseQuimicaRepository AnaliseQuimicaRepository { get; }

        public UnitOfWork(AppDbContext contexto)
        {
            this._context = contexto;
            this.ProductRepository = new ProductRepository(_context);
            this.AnaliseQuimicaRepository = new AnaliseQuimicaRepository(_context);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
