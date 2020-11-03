using Backend.Domain.Entities;
using Backend.Domain.Interfaces.IRepository;
using Backend.Domain.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services.Services
{
    public class ImportacaoService : IImportacaoService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public ImportacaoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<AnaliseQuimica>>> Import(IFormFile formFile, Guid currentUserID)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return Response<List<AnaliseQuimica>>.GetResult(-1, "O Arquivo está vazio!");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return Response<List<AnaliseQuimica>>.GetResult(-1, "Arquivo não suportado! Formato exigido: .xlsx");
            }

            var list = new List<AnaliseQuimica>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream).ConfigureAwait(false);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        list.Add(new AnaliseQuimica
                        {
                            Id = Guid.NewGuid(), 
                            DateCreated = DateTime.Now,
                            DateUpdate = DateTime.Now,
                            UserId = currentUserID,
                            Latitude = long.Parse(worksheet.Cells[row, 2].Value.ToString().Trim()),
                            Longitude = long.Parse(worksheet.Cells[row, 3].Value.ToString().Trim()),
                            Fosforo = long.Parse(worksheet.Cells[row, 4].Value.ToString().Trim()),
                            MO = long.Parse(worksheet.Cells[row, 5].Value.ToString().Trim()),
                            Carbono = long.Parse(worksheet.Cells[row, 6].Value.ToString().Trim()),
                            pH = long.Parse(worksheet.Cells[row, 7].Value.ToString().Trim()),
                            pHTampao = long.Parse(worksheet.Cells[row, 8].Value.ToString().Trim()),
                            SatBases = long.Parse(worksheet.Cells[row, 9].Value.ToString().Trim()),
                            CTC = long.Parse(worksheet.Cells[row, 10].Value.ToString().Trim()),
                            Potassio = long.Parse(worksheet.Cells[row, 11].Value.ToString().Trim()),
                            Calcio = long.Parse(worksheet.Cells[row, 12].Value.ToString().Trim()),
                            Magnesio = long.Parse(worksheet.Cells[row, 13].Value.ToString().Trim()),
                            Enxofre = long.Parse(worksheet.Cells[row, 14].Value.ToString().Trim()),
                            Boro = long.Parse(worksheet.Cells[row, 15].Value.ToString().Trim()),
                            Cobre = long.Parse(worksheet.Cells[row, 16].Value.ToString().Trim()),
                            Ferro = long.Parse(worksheet.Cells[row, 17].Value.ToString().Trim()),
                            Manganes = long.Parse(worksheet.Cells[row, 18].Value.ToString().Trim()),
                            Zinco = long.Parse(worksheet.Cells[row, 19].Value.ToString().Trim()),
                            RelacaoCA = long.Parse(worksheet.Cells[row, 20].Value.ToString().Trim()),
                            RelacaoMg = long.Parse(worksheet.Cells[row, 21].Value.ToString().Trim()),
                            Argila = long.Parse(worksheet.Cells[row, 22].Value.ToString().Trim()),
                            Silte = long.Parse(worksheet.Cells[row, 23].Value.ToString().Trim()),
                            Areia = long.Parse(worksheet.Cells[row, 24].Value.ToString().Trim()),
                        });
                    }
                }
            }

            _unitOfWork.AnaliseQuimicaRepository.CreateListByUser(list);
            _unitOfWork.Commit();

            return Response<List<AnaliseQuimica>>.GetResult(200, "OK", list);
        }
    }
}
