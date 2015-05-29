using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ejercicio6._5._7
{
    class ArchivoSecuencialSerializadoBinario<T>
    {
        private string _strNombreArchivo;

        public string NombreArchivo
        {
            get { return _strNombreArchivo; }
            set { _strNombreArchivo = value; }
        }

        FileStream flujo;
        BinaryFormatter seriador;

        private long _lngPosicion;

        public long Posicion
        {
            get { return _lngPosicion; }
            set { _lngPosicion = value; }
        }

        public ArchivoSecuencialSerializadoBinario(string strNombreArchivo)
        {
            NombreArchivo = strNombreArchivo;
        }

        ~ArchivoSecuencialSerializadoBinario()
        {
            this.Crear();
        }

        private void Crear()
        {
            flujo = new FileStream(NombreArchivo, FileMode.Create);

        }

        public void AbrirEnModoEscritura()
        {
            if (File.Exists(NombreArchivo))
                flujo = new FileStream(NombreArchivo, FileMode.Append);

            else
                Crear();
            seriador = new BinaryFormatter();
        }

        public void AbrirEnModoLectura()
        {
            if (File.Exists(NombreArchivo))
            {
                flujo = new FileStream(NombreArchivo, FileMode.Open);
                seriador = new BinaryFormatter();
            }
            else
                throw new Exception("No existe");
        }

        public void AbrirEnModoLecturaYEscritura()
        {
            if (File.Exists(NombreArchivo))
            {
                flujo = new FileStream(NombreArchivo, FileMode.Open, FileAccess.ReadWrite);
            }
            else
                this.Crear();
            seriador = new BinaryFormatter();
        }

        public void GrabarObjeto(T miObjeto)
        {
            seriador.Serialize(flujo, miObjeto);
        }

        public T LeerObjeto()
        {
            Posicion = flujo.Position;
            return ((T)seriador.Deserialize(flujo));
        }

        public void ModificarObjeto(T miObjeto)
        {
            flujo.Seek(Posicion, SeekOrigin.Begin);
            this.GrabarObjeto(miObjeto);
        }

        public void Cerrar()
        {
            if (flujo != null)
                flujo.Close();
        }

        public void EliminarArchivo()
        {
            File.Delete(NombreArchivo);
        }

        public void RenombrarArchivo(string strNuevoNombreArchivo)
        {
            File.Move(NombreArchivo, strNuevoNombreArchivo);
        }
    }
}
