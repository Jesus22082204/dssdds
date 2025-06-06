using ProyectoIntegradorFinal.Controller;
using ProyectoIntegradorFinal.Model;
using ProyectoIntegradorFinal.View.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoIntegradorFinal.View
{
    public partial class TablaView : Form
    {
        public TablaView()
        {
            InitializeComponent();
            CRUD cRUD = new CRUD();

            // Configuración de la ventana
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Size = new Size(1067, 600);
            BackgroundImage = Properties.Resources.Imagen_de_WhatsApp_2025_05_13_a_las_22_06_07_c831dbe2;
            BackgroundImageLayout = ImageLayout.Stretch;

            // Instanciador de UI
            CreateController controller = new CreateController();
            this.Text = "Tabla de Usuarios";
            controller.CreateLabel("Tabla de Usuarios", 450, 20, 200, 30, this);

            
            controller.CreateLabel("ID a Eliminar", 450, 40, 150, 20, this);
            TextBox txtEliminarID = controller.CreateTextBox(450, 60, 150, 25, this);
            

            
            controller.CreateLabel("ID del Usuario", 450, 100, 150, 20, this);
            TextBox txtActualizarID = controller.CreateTextBox(450, 120, 150, 25, this);

            controller.CreateLabel("Nombre de Usuario", 450, 140, 150, 20, this);
            TextBox txtUsuario = controller.CreateTextBox(450, 160, 150, 25, this);

            controller.CreateLabel("Contraseña", 450, 180, 150, 20, this);
            TextBox txtClave = controller.CreateTextBox(450, 200, 150, 25, this);

            controller.CreateLabel("Correo Electrónico", 450, 220, 150, 20, this);
            TextBox txtCorreo = controller.CreateTextBox(450, 240, 150, 25, this);

            controller.CreateLabel("Teléfono", 450, 260, 150, 20, this);
            TextBox txtTelefono = controller.CreateTextBox(450, 280, 150, 25, this);

            controller.CreateLabel("Rol", 450, 300, 150, 20, this);
            TextBox txtRol = controller.CreateTextBox(450, 320, 150, 25, this);
           

            controller.CreateButton("Actualizar Usuario", 620, 150, 150, 30, this, (s, e) =>
            {
                string id = txtActualizarID.Text.Trim();
                string usuario = txtUsuario.Text.Trim();
                string clave = txtClave.Text.Trim();
                string correo = txtCorreo.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string rol = txtRol.Text.Trim();

                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Ingrese un ID válido para actualizar.");
                    return;
                }
                string[] lineas = File.ReadAllLines(Storage.archivoUsuarios);
                foreach (string linea in lineas)
                {
                    if (linea.Contains(id))
                    {
                        cRUD.Update(id, usuario, clave, correo, telefono, rol);
                        MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cRUD.CargarDatos(Storage.archivoUsuarios);
                        return;
                    }
                   
                }
                MessageBox.Show("Usuario no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            });

            controller.CreateButton("Eliminar Usuario", 620, 200, 150, 30, this, (s, e) =>
            {
                string id = txtEliminarID.Text.Trim();
                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Por favor, ingrese un ID válido.");
                    return;
                }

                DialogResult confirm = MessageBox.Show($"¿Seguro que desea eliminar el usuario con ID: {id}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    cRUD.Delete(id);
                    MessageBox.Show("Usuario eliminado correctamente.");
                    txtEliminarID.Clear();
                    cRUD.CargarDatos(Storage.archivoUsuarios);
                }
            });



            
            cRUD.dataGrid = new DataGridView
            {
                Location = new Point(20, 370),
                Size = new Size(1000, 200),
                ColumnCount = 5,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            cRUD.dataGrid.Columns[0].Name = "No";
            cRUD.dataGrid.Columns[1].Name = "ID";
            cRUD.dataGrid.Columns[2].Name = "Usuario";
            cRUD.dataGrid.Columns[3].Name = "Correo";
            cRUD.dataGrid.Columns[4].Name = "Telefono";

            Controls.Add(cRUD.dataGrid);
           

            // Cargar datos al iniciar
            cRUD.CargarDatos(Storage.archivoUsuarios);



        }
    }
}
