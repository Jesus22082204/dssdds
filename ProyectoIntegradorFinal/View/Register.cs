using ProyectoIntegradorFinal.Controller;
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

namespace ProyectoIntegradorFinal.View
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            CRUD cRUD = new CRUD();
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Size = new Size(1067, 600);
            BackgroundImage = Properties.Resources.Imagen_de_WhatsApp_2025_05_13_a_las_22_06_07_c831dbe2;
            BackgroundImageLayout = ImageLayout.Stretch;
           
            
            string[] etiquetas = { "ID", "Usuario", "Contraseña", "Confirmar Contraseña", "Correo Electronico", "Telefono" };
            TextBox[] campos = new TextBox[etiquetas.Length];
            Font fuente = new Font("Arial", 9.5f, FontStyle.Italic);

            // Crear etiquetas y campos de texto
            for (int i = 0; i < etiquetas.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = etiquetas[i];
                lbl.Font = fuente;
                lbl.AutoSize = true;
                lbl.Location = new Point(30, 20 + i * 40);
                Controls.Add(lbl);

                TextBox txt = new TextBox();
                txt.Size = new Size(150, 20);
                txt.Location = new Point(200, 20 + i * 40);
                if (etiquetas[i].ToLower().Contains("contraseña"))
                    txt.UseSystemPasswordChar = true;
                this.Controls.Add(txt);
                campos[i] = txt;
            }
            CreateController controller = new CreateController();
            controller.CreateButton("Registrar", 200, 350, 100, 30, this, (s, e) =>
            {
                string ID = campos[0].Text;
                string registrarUsuario = campos[1].Text;
                string registrarClave = campos[2].Text;
                string ConfirmarClave = campos[3].Text;
                string RegistrarCorreo = campos[4].Text;
                string NumeroTelefono = campos[5].Text;
                string RolIdentificador = "Normal";



                if(ID == "" || registrarUsuario == "" || registrarClave == "" || ConfirmarClave == "" || RegistrarCorreo == "" || NumeroTelefono == "")
                {
                    MessageBox.Show("Por favor, complete todos los campos", "HOLA", MessageBoxButtons.OK);
                    return;
                }

                if (CRUD.ExisteUsuario(registrarUsuario))
                {
                    MessageBox.Show("Se encuentra registrado", "HOLA", MessageBoxButtons.OK);

                    return;
                }
                if (registrarClave == ConfirmarClave)
                {
                    string hash = CRUD.HashPassword(registrarClave);
                    string linea = $"{ID}|{registrarUsuario}|{registrarClave}|{RegistrarCorreo}|{NumeroTelefono}|{hash}|{RolIdentificador}";
                    File.AppendAllLines(Storage.archivoUsuarios, new[] { linea });

                    MessageBox.Show("Registro Exitoso", "HOLA", MessageBoxButtons.OK);
                }
                if (registrarClave != ConfirmarClave)
                {
                    MessageBox.Show("Contraseñas no coinciden", "HOLA", MessageBoxButtons.OK);
                }

            });
            

            
        }
    }
}

