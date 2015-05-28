using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio6._5._7
{
    [Serializable()]
    class Pelicula:ISerializable
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
        public Pelicula()
        {
            
        }
        public Pelicula(SerializationInfo info, StreamingContext context)
        {
            //Get the values from info and assign them to the appropriate properties
            Nombre = (String)info.GetValue("Nombre", typeof(string));
            Cantidad = (int)info.GetValue("Cantidad", typeof(int));
            Precio = (double)info.GetValue("Precio", typeof(double));
            RentaCompra = (char)info.GetValue("RentaCompra", typeof(char));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Nombre", Nombre);
            info.AddValue("Cantidad", Cantidad);
            info.AddValue("Precio", Precio);
            info.AddValue("RentaCompra", RentaCompra);
        }
    }
}
