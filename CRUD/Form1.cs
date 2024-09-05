using CRUD.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void CargarDatos()
        {
            using (NegocioEntities1 db = new NegocioEntities1())
            {
                dgvPersonas.DataSource = db.Productos.ToList();
            }
        }
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            using (NegocioEntities1 db = new NegocioEntities1())
            {
                Producto producto = new Producto();
                producto.Nombre = txtNombre.Text;
                producto.Marca = txtMarca.Text;
                producto.Precio = txtPrecio.Text.Length;
                db.Productos.Add(producto);
                db.SaveChanges();
                MessageBox.Show("Se agergo con exito el producto");
            }
            this.CargarDatos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvPersonas.CurrentRow.Cells["id"].Value);
            int precio = Convert.ToInt32(txtPrecio.Text);
            using (NegocioEntities1 db = new NegocioEntities1())
            {
                Producto producto = new Producto();
                producto.Id = id;
                producto.Nombre = txtNombre.Text;
                producto.Marca = txtMarca.Text;
                producto.Precio = precio;
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
            }
            this.CargarDatos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvPersonas.CurrentRow.Cells["id"].Value);
            using (NegocioEntities1 db = new NegocioEntities1())
            {
                var lst = db.Productos.Where(x => x.Id == id).FirstOrDefault();
                db.Productos.Remove(lst);
                db.SaveChanges();
            }
            this.CargarDatos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (NegocioEntities1 db = new NegocioEntities1())
            {
                var buscar = db.Productos.Where(x => x.Nombre == txtNombre.Text || x.Marca == txtMarca.Text);
                dgvPersonas.DataSource = buscar.ToList();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtMarca.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}
