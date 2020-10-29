using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Domain.Entities
{
    public class AnaliseQuimica : BaseEntity
    {
        public Guid UserId { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public decimal Fosforo { get; set; }
        public decimal MO { get; set; }
        public decimal Carbono { get; set; }
        public decimal pH { get; set; }
        public decimal pHTampao { get; set; }
        public decimal SatBases { get; set; }
        public decimal CTC { get; set; }
        public decimal Potassio { get; set; }
        public decimal Calcio { get; set; }
        public decimal Magnesio { get; set; }
        public decimal Enxofre { get; set; }
        public decimal Boro { get; set; }
        public decimal Cobre { get; set; }
        public decimal Ferro { get; set; }
        public decimal Manganes { get; set; }
        public decimal Zinco { get; set; }
        public decimal RelacaoCA { get; set; }
        public decimal RelacaoMg { get; set; }
        public decimal Argila { get; set; }
        public decimal Silte { get; set; }
        public decimal Areia { get; set; }
    }
}
