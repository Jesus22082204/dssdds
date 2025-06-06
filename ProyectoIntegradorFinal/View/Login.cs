using ProyectoIntegradorFinal.Model;
using ProyectoIntegradorFinal.View.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace ProyectoIntegradorFinal.View
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();


            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Size = new Size(1067, 600);

            CreateController controller = new CreateController();


            Panel panelIzquierdo = new Panel();
            panelIzquierdo.Size = new Size(320, this.ClientSize.Height);
            panelIzquierdo.Dock = DockStyle.Left;
            panelIzquierdo.BackgroundImage = Properties.Resources.Imagen_de_WhatsApp_2025_05_13_a_las_22_06_07_c831dbe2;
            panelIzquierdo.BackgroundImageLayout = ImageLayout.Stretch;
            this.Controls.Add(panelIzquierdo);
            
            
            PictureBox userImage = new PictureBox();
            userImage.Image = Properties.Resources.LOGO_NUEVO_UPCSA;
            userImage.SizeMode = PictureBoxSizeMode.StretchImage;
            userImage.Size = new Size(200, 150);
            userImage.Location = new Point(410, 30);
            this.Controls.Add(userImage);
            userImage.BringToFront();



            controller.CreateLabel("Usuario", 350, 200, 100, 30, this);
            controller.CreateLabel("Contraseña", 350, 250, 100, 30, this);
            TextBox txtUsuario = controller.CreateTextBox(450, 200, 200, 30, this);
            TextBox txtContraseña = controller.CreateTextBox(450, 250, 200, 30, this, true);

            Button btnIngresar = controller.CreateButton("Ingresar", 450, 300, 100, 30, this, (s, e) => 
            {
                string Usuario = txtUsuario.Text;
                string Clave = txtContraseña.Text;
                if (!File.Exists(Storage.archivoUsuarios ) && Usuario != "admin")
                {
                    MessageBox.Show("No Hay Usuarios Registrados", "HOLA", MessageBoxButtons.OK);
                    return;
                }
                MainSesion form1 = new MainSesion();
                string[] lineas = File.ReadAllLines(Storage.archivoUsuarios);
                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split('|');
                    if (datos.Length == 7 && datos[1] == Usuario && datos[5] == HashPassword(Clave))
                    {

                        



                        if (datos[6] == "Admin")
                        {
                            MessageBox.Show("inicio De Sesion Exitoso ", datos[6], MessageBoxButtons.OK);
                            
                        }
                        else if (datos[6] == "Normal")
                        {
                            MessageBox.Show("Inicio De Sesion Exitoso ", datos[6], MessageBoxButtons.OK);
                           
                        }


                        form1.Show();
                        
                        //this.Hide();

                       

                        return;
                    }
                    if(Usuario == "admin" && Clave == "admin123")
                    {
                        form1.Show();
                    }



                    if (datos.Length == 7 && datos[1] != Usuario && datos[5] != HashPassword(Clave) && Usuario != "admin")
                    {
                        MessageBox.Show("Usuario o Contraseña Incorrecta", "HOLA", MessageBoxButtons.OK);

                    }
                }

                
               
            });

            Button btnCancelar = controller.CreateButton("Cancelar", 560, 300, 100, 30, this, (s, e) => 
            {
                Application.Exit(); // Cierra la aplicación
            });



        }
        static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }

        }
        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
