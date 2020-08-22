using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace AppPan_PanaderoCliente.Models
{
    public class Usuario
    {
        internal double latitud;

        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public byte[] Foto_usuario { get; set; }
        public string Nombre_vendedor { get; set; }
        public string Nombre_negocio { get; set; }
        public Distance radioDeEntrega { get; set; }
        public Position ubicacion { get; set; }
        public bool Estado { get; set; }

        //public ICollection<Producto> Productos { get; set; }
        //public ICollection<Pedido> Pedidos { get; set; }
    }
}
