using System;

namespace Ejercicio6._5._7
{
    [Serializable]
    class Pelicula:IEquatable<Pelicula>
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
        public bool Equals(Pelicula otraPelicula)
        {
            return (this.Nombre == otraPelicula.Nombre);
        }
    }
}
