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
        ArchivoSecuencialSerializadoBinario<Pelicula> peliculasDoc = new ArchivoSecuencialSerializadoBinario<Pelicula>("Ejercicio.dat");
        private void Form1_Load(object sender, EventArgs e)
        {
            dtgPeliculas.Columns.Add("Nombre", "Nombre");
            dtgPeliculas.Columns.Add("Precio", "Precio");
            dtgPeliculas.Columns.Add("Cantidad", "Cantidad");
            dtgPeliculas.Columns.Add("Estado", "Estado");
            dtgPeliculas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgPeliculas.ReadOnly = true;


            CargarDatos();
            btnAgregar.Click += btnAgregar_Click;
            btnModificar.Click += btnModificar_Click;
            dtgPeliculas.CellClick += dtgPeliculas_CellClick;
            btnEliminar.Click += btnEliminar_Click;
            btnVaciar.Click += btnVaciar_Click;
            btnSalir.Click += btnSalir_Click;
        }

        void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void btnVaciar_Click(object sender, EventArgs e)
        {
            peliculasDoc.EliminarArchivo();
            CargarDatos();
        }
        void btnEliminar_Click(object sender, EventArgs e)
        {
            Pelicula miPeliculaOriginal = new Pelicula();

            miPeliculaOriginal.Nombre = (dtgPeliculas.CurrentRow.Cells[0].Value.ToString());
            miPeliculaOriginal.Precio = double.Parse(dtgPeliculas.CurrentRow.Cells[1].Value.ToString());
            miPeliculaOriginal.Cantidad = int.Parse(dtgPeliculas.CurrentRow.Cells[2].Value.ToString());
            miPeliculaOriginal.RentaCompra = char.Parse(dtgPeliculas.CurrentRow.Cells[3].Value.ToString());
            try
            {
                peliculasDoc.AbrirEnModoLectura();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
                return;
            }
            ArchivoSecuencialSerializadoBinario<Pelicula> miArchivoTemporal = new ArchivoSecuencialSerializadoBinario<Pelicula>("c://Datos//ArchivoTemporal.dat");

            miArchivoTemporal.AbrirEnModoEscritura();

            Pelicula miPelicula = new Pelicula();
            while (true)
            {
                try
                {
                    miPelicula = peliculasDoc.LeerObjeto();
                }
                catch (Exception ex)
                {
                    break;
                }

                if (!miPelicula.Equals(miPeliculaOriginal))
                    miArchivoTemporal.GrabarObjeto(miPelicula);
            }

            peliculasDoc.Cerrar();
            miArchivoTemporal.Cerrar();

            peliculasDoc.EliminarArchivo();
            miArchivoTemporal.RenombrarArchivo("c://Datos//Ejercicio.dat");

            CargarDatos();
        }
        void dtgPeliculas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = (dtgPeliculas.CurrentRow.Cells[0].Value.ToString());
            txtPrecio.Text = (dtgPeliculas.CurrentRow.Cells[1].Value.ToString());
            txtCantidad.Text = (dtgPeliculas.CurrentRow.Cells[2].Value.ToString());
            txtRentaCompra.Text = (dtgPeliculas.CurrentRow.Cells[3].Value.ToString());
        }
        void btnModificar_Click(object sender, EventArgs e)
        {
            Pelicula miPeliculaOriginal = new Pelicula();

            miPeliculaOriginal.Nombre = (dtgPeliculas.CurrentRow.Cells[0].Value.ToString());
            miPeliculaOriginal.Precio = double.Parse(dtgPeliculas.CurrentRow.Cells[1].Value.ToString());
            miPeliculaOriginal.Cantidad = int.Parse(dtgPeliculas.CurrentRow.Cells[2].Value.ToString());
            miPeliculaOriginal.RentaCompra = char.Parse(dtgPeliculas.CurrentRow.Cells[3].Value.ToString());
            try
            {
                peliculasDoc.AbrirEnModoLectura();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            ArchivoSecuencialSerializadoBinario<Pelicula> miArchivoTemporal = new ArchivoSecuencialSerializadoBinario<Pelicula>("c://Datos//ArchivoTemporal.dat");
            miArchivoTemporal.AbrirEnModoEscritura();
            Pelicula miPeliculaModificada = new Pelicula();
            miPeliculaModificada.Nombre = txtNombre.Text;
            miPeliculaModificada.Precio = double.Parse(txtPrecio.Text);
            miPeliculaModificada.Cantidad = int.Parse(txtCantidad.Text);
            miPeliculaModificada.RentaCompra = char.Parse(txtRentaCompra.Text);

            Pelicula miPelicula = new Pelicula();
            while (true)
            {
                try
                {
                    miPelicula = peliculasDoc.LeerObjeto();
                }
                catch (Exception ex)
                {
                    break;
                }

                if (miPelicula.Equals(miPeliculaOriginal))
                    miArchivoTemporal.GrabarObjeto(miPeliculaModificada);
                else
                    miArchivoTemporal.GrabarObjeto(miPelicula);
            }
            peliculasDoc.Cerrar();
            miArchivoTemporal.Cerrar();
            peliculasDoc.EliminarArchivo();
            miArchivoTemporal.RenombrarArchivo("c://Datos//Ejercicio.dat");
            CargarDatos();
        }
        void btnAgregar_Click(object sender, EventArgs e)
        {
            Pelicula miPelicula = new Pelicula();

            miPelicula.Nombre = txtNombre.Text;
            miPelicula.Precio = double.Parse(txtPrecio.Text);
            miPelicula.Cantidad = int.Parse(txtCantidad.Text);
            miPelicula.RentaCompra = char.Parse(txtRentaCompra.Text);

            peliculasDoc.AbrirEnModoEscritura();
            peliculasDoc.GrabarObjeto(miPelicula);
            peliculasDoc.Cerrar();
            CargarDatos();
            MessageBox.Show("Se agregaron los datos");
            foreach (Control c in grpDatos.Controls)
                if (c is TextBox)
                    c.Text = "";
        }
        void CargarDatos()
        {
            dtgPeliculas.Rows.Clear();
            try
            {
                peliculasDoc.AbrirEnModoLectura();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            Pelicula miPelicula = new Pelicula();
            while (true)
            {
                try
                {
                    miPelicula = peliculasDoc.LeerObjeto();
                }
                catch (Exception ex)
                {
                    break;
                }
                dtgPeliculas.Rows.Add(miPelicula.Nombre, miPelicula.Precio, miPelicula.Cantidad, miPelicula.RentaCompra);
            }
            peliculasDoc.Cerrar();
        }
    }

}
