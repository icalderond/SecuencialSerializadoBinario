using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio6._5._7
{
    [Serializable]
    class Pelicula
    {
        private string _strNombre;

        public string Nombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }

        private int _intCantidad;

        public int Cantidad
        {
            get { return _intCantidad; }
            set { _intCantidad = value; }
        }

        private double _dblPrecio;

        public double Precio
        {
            get { return _dblPrecio; }
            set { _dblPrecio = value; }
        }

        private char _chRentaCompra;

        public char RentaCompra
        {
            get { return _chRentaCompra; }
            set { _chRentaCompra = value; }
        }
    }
}
