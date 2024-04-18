﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteDeAutos.Models
{
    public class CarroModel
    {
        public int Id { get; set; }
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string Version { get; set; } = null!;
        public int Año { get; set; }
        public decimal Precio { get; set; }
        public ushort Kilometraje { get; set; }
        public string Motor { get; set; } = null!;
        public string Transmision { get; set; } = null!;
        public string Carroceria { get; set; } = null!;
    }
}
