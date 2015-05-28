using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ejercicio6._5._7
{
    [Serializable]
    class ArchivoSecuencialSerializadoBinario<tipo>
    {
        private string _strNombreArchivo;

        public string NombreArchivo
        {
            get { return _strNombreArchivo; }
            set { _strNombreArchivo = value; }
        }

        System.IO.FileStream flujo;
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter seriador;

        private long _lngPosicion;

        public long Posicion
        {
            get { return _lngPosicion; }
            set { _lngPosicion = value; }
        }

        public ArchivoSecuencialSerializadoBinario(string strNombreArchivo)
        {

            NombreArchivo = strNombreArchivo;
            Crear();//va adentro por que es privado
        }

        ~ArchivoSecuencialSerializadoBinario()
        {
            if (flujo != null)
                flujo.Close();
        }

        private void Crear()
        {
            seriador = new BinaryFormatter();
            if (!File.Exists(NombreArchivo))
            {
                File.Delete(NombreArchivo);
                File.Create(NombreArchivo).Dispose();
            }

        }

        public void AbrirEnModoEscritura()
        {
            try
            {
                flujo = new FileStream(NombreArchivo, FileMode.Append);

            }
            catch (Exception)
            {
                throw new Exception("El archivo no existe");
            }
        }

        public void AbrirEnModoLectura()
        {
            try
            {
                flujo = new FileStream(NombreArchivo, FileMode.Open);

            }
            catch (Exception)
            {
                throw new Exception("El archivo no existe");
            }
        }

        public void AbrirEnModoLecturaYEscritura()
        {
            try
            {
                flujo = new FileStream(NombreArchivo, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            catch (Exception)
            {
                throw new Exception("El archivo no existe");
            }
        }

        public void GrabarObjeto(tipo miObjeto)
        {
            try
            {
                var list = LeerObjeto();
                list.Add(miObjeto);
                BinaryFormatter bin = new BinaryFormatter();
                
                bin.Serialize(flujo, miObjeto);
            }
            catch (IOException)
            {
                throw new Exception("No se pudo serializar");
            }
        }

        public List<tipo> LeerObjeto()
        {
            List<tipo> tipoValue = new List<tipo>();
            BinaryFormatter bin = new BinaryFormatter();
            try
            {
                if (flujo.Length > 0)
                {
                    var t = (List<tipo>)bin.Deserialize(flujo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return tipoValue;
        }

        public void ModificarObjeto(tipo miObjeto)
        {
            flujo.Seek(Posicion, SeekOrigin.Begin);
            this.GrabarObjeto(miObjeto);
        }

        public void Cerrar()
        {
            flujo.Close();
        }

        public void EliminarArchivo()
        {
            File.Delete(NombreArchivo);
        }

        public void RenombrarArchivo(string strNuevoNombreArchivo)
        {
            try
            {
                string OnlyName = NombreArchivo.Split('\\').Last();
                string RutaActual = NombreArchivo.Replace(OnlyName, "");
                string NombreArchivoNuevo = RutaActual + strNuevoNombreArchivo;

                File.Copy(NombreArchivo, NombreArchivoNuevo);
                EliminarArchivo();
                NombreArchivo = NombreArchivoNuevo;
            }
            catch (Exception)
            {
                throw new Exception("El archivo no existe");
            }

        }
    }
}
