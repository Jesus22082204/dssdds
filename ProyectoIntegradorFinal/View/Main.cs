using ProyectoIntegradorFinal.View;
using ProyectoIntegradorFinal.View.Controls;

namespace ProyectoIntegradorFinal
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            
          

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Size = new Size(1067, 600);

            // crear men� principal
            string[] opciones = { "Archivo","Encuesta", "Configuraci�n", "Ayuda" };
            CreateController controller = new CreateController(); 
            MenuStrip menu = controller.CreateMenuStrip(opciones); 
            // crear opciones para cada opci�n del men� principal 
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
            controller.AddToolStripMenuItems(menu, itemsArchivo, opciones[0]);
            controller.AddToolStripMenuItems(menu, itemsEncuesta, opciones[1]);          
            Controls.Add(menu); // Aseg�rate de agregar el men� al formulario
            
            Eventos.EventoSubMenu(menu, opciones[0], "Iniciar Sesion", (s, e) =>
            {
                Login login = new Login();
                login.Show();

                this.Hide(); // Oculta el formulario principal al abrir el login
            });
            Eventos.AsignarEventoAToolStrip(menu, opciones[3], (s, e) =>
            {
                MessageBox.Show("Funciones de los �conos\n�Bienvenido a la secci�n de ayuda! " +
               "Aqu� te explicamos las funciones de los �conos principales en la interfaz de usuario:\nAgregar Alumnos\nDescripci�n:" +
               " Haz clic en el primer �cono (s�mbolo de \"+\") para agregar nuevos alumnos al sistema.\nAgregar Usuarios\nDescripci�n: " +
               "El segundo �cono (figura de usuario con \"a�adir\") te permite agregar nuevos usuarios al sistema." +
               "\nListar Todos los Alumnos\nDescripci�n: Al hacer clic en el tercer �cono," +
               " podr�s listar todos los alumnos que has agregado.", "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
            Eventos.EventoSubMenu(menu, opciones[1], "Registrar usuarios", (s, e) =>
            {
                MessageBox.Show("Registrar usuarios clicked", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            CreateController.BlockToolStripItems(menu, opciones[0], false);
            CreateController.SetMenuItemEnabled(menu, opciones[0],"Iniciar Sesion", true);
            CreateController.SetMenuItemEnabled(menu, opciones[0], "Salir", true);
            CreateController.BlockToolStripItems(menu, opciones[1], false);
            CreateController.BlockToolStripItems(menu, opciones[2], false);
            CreateController.BlockToolStripItems(menu, opciones[3], false);
            
        }


        private void Main_Load(object sender, EventArgs e)
        {

        }
    }

    
}
