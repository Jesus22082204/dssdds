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
    public partial class MainSesion : Form
    {
        public MainSesion()
        {
            InitializeComponent();
            
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Size = new Size(1067, 600);

            // crear menú principal
            string[] opciones = { "Archivo", "Encuesta", "Configuración", "Ayuda" };
            CreateController controller = new CreateController();
            MenuStrip menu = controller.CreateMenuStrip(opciones);
            // crear opciones para cada opción del menú principal 
            ToolStripMenuItem[] itemsArchivo =
            {
                CreateController.CreateToolStripItem("Iniciar Sesion"),
                CreateController.CreateToolStripItem("Cerrar Sesion"),
                CreateController.CreateToolStripItem("Salir")
            };           

            ToolStripMenuItem[] itemsEncuesta =
            {
                    CreateController.CreateToolStripItem("Crear preguntas"),
                    CreateController.CreateToolStripItem("Actualizar preguntas"),
                    CreateController.CreateToolStripItem("Eliminar preguntas"),
                    CreateController.CreateToolStripItem("Listar preguntas"),
            };
            Eventos.AsignarEventoAToolStrip(menu, opciones[3], (s, e) =>
            {
                MessageBox.Show("Funciones de los Íconos\n¡Bienvenido a la sección de ayuda! " +
               "Aquí te explicamos las funciones de los íconos principales en la interfaz de usuario:\nAgregar Alumnos\nDescripción:" +
               " Haz clic en el primer ícono (símbolo de \"+\") para agregar nuevos alumnos al sistema.\nAgregar Usuarios\nDescripción: " +
               "El segundo ícono (figura de usuario con \"añadir\") te permite agregar nuevos usuarios al sistema." +
               "\nListar Todos los Alumnos\nDescripción: Al hacer clic en el tercer ícono," +
               " podrás listar todos los alumnos que has agregado.", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
            ToolStrip barra = controller.CrearBarraDeHerramientas();
            barra.Items.Add(controller.CrearBoton("Agregar", @"C:\Users\mendo\source\repos\ProyectoIntegradorFinal\ProyectoIntegradorFinal\IMG\Agregar.png", (s, e) =>
            {
                MessageBox.Show("Agregar clicked", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }));
            barra.Items.Add(controller.CrearBoton("Tabla", @"C:\Users\mendo\source\repos\ProyectoIntegradorFinal\ProyectoIntegradorFinal\IMG\1827235.png", (s, e) =>
            {
                TablaView tablaView = new TablaView();
                tablaView.Show();
            }));
            Controls.Add(barra); // Asegúrate de agregar la barra de herramientas al formulario



            controller.AddToolStripMenuItems(menu, itemsArchivo, opciones[0]);
            controller.AddToolStripMenuItems(menu, itemsEncuesta, opciones[1]);
            Controls.Add(menu); // Asegúrate de agregar el menú al formulario


            Eventos.EventoSubMenu(menu, opciones[1], "Registrar usuarios", (s, e) =>
            {
                MessageBox.Show("Registrar usuarios clicked", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
            Eventos.EventoSubMenu(menu, opciones[0], "Salir", (s, e) =>
            {
                Main main = new Main();
                Application.Exit();
            });

        }
    }
}
