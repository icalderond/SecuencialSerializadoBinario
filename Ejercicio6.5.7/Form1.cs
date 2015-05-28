using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio6._5._7
{

    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ArchivoSecuencialSerializadoBinario<Pelicula> peliculasDoc = new ArchivoSecuencialSerializadoBinario<Pelicula>(@"C:\Users\Public\doc.txt");
                //peliculasDoc.AbrirEnModoEscritura();
                peliculasDoc.AbrirEnModoLecturaYEscritura();
                for (int i = 0; i < 10; i++)
                {
                    var t = new Pelicula()
                 {
                     Cantidad = i,
                     Nombre = "Condorito",
                     Precio = 20.40,
                     RentaCompra = 'D'
                 };
                    peliculasDoc.GrabarObjeto(t);

                }
                peliculasDoc.Cerrar();

                peliculasDoc.AbrirEnModoLectura();
                var topo = peliculasDoc.LeerObjeto();
                peliculasDoc.Cerrar();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
