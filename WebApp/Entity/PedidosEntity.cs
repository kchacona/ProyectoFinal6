using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PedidosEntity
    {
        public int? Codigo { get; set; }
        public int Cliente { get; set; }
        public DateTime FechaPedido { get; set; }
        public int Producto { get; set; }
        public int Cantidad { get; set; }
        public float Subtotal { get; set; }
        public string Envio { get; set; }
        public float Impuestos { get; set; }
        public float Total { get; set; }

    }
}
