﻿namespace ObligatorioP3Consumidor.Models.Articulo
{
    public class ArticuloListarDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
