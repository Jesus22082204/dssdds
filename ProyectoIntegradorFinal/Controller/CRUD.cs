using ProyectoIntegradorFinal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ProyectoIntegradorFinal.Controller
{
    internal class CRUD
    {
        public DataGridView dataGrid;
        public string Find(string id)
        {
            if (!File.Exists(Storage.archivoUsuarios)) return "";

            string[] lineas = File.ReadAllLines(Storage.archivoUsuarios);
            foreach (string linea in lineas)
            {
                string[] datos = linea.Split('|');
                if (datos.Length >= 1 && datos[0] == id)
                    return datos[0] + "|" + datos[1] + "|" + datos[2] + "|" + datos[3] + "|" + datos[4] + "|" + datos[6];
            }
            return "";
        }
        public void Delete(string id)
        {
            if (!File.Exists(Storage.archivoUsuarios)) return;
            string[] lineas = File.ReadAllLines(Storage.archivoUsuarios);
            List<string> nuevasLineas = new List<string>();
            foreach (string linea in lineas)
            {
                string[] datos = linea.Split('|');
                if (datos.Length >= 1 && datos[0] != id)
                    nuevasLineas.Add(linea);
            }
            File.WriteAllLines(Storage.archivoUsuarios, nuevasLineas);
        }
        public void Update(string id, string usuario, string clave, string correo, string telefono, string rol)
        {
            if (!File.Exists(Storage.archivoUsuarios)) return;
            string[] lineas = File.ReadAllLines(Storage.archivoUsuarios);
            List<string> nuevasLineas = new List<string>();
            foreach (string linea in lineas)
            {
                string[] datos = linea.Split('|');
                if (datos.Length >= 1 && datos[0] == id)
                {
                    string hash = HashPassword(clave);
                    nuevasLineas.Add($"{id}|{usuario}|{clave}|{correo}|{telefono}|{hash}|{rol}");
                }
                else
                {
                    nuevasLineas.Add(linea);
                }
            }
            File.WriteAllLines(Storage.archivoUsuarios, nuevasLineas);
        }
       
        public static bool ExisteUsuario(string usuario)
        {
            if (!File.Exists(Storage.archivoUsuarios)) return false;

            string[] lineas = File.ReadAllLines(Storage.archivoUsuarios);
            foreach (string linea in lineas)
            {
                string[] datos = linea.Split('|');
                if (datos.Length >= 0 && datos[1] == usuario)
                    return true;
            }
            return false;
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        public void CargarDatos(string ruta)
        {
            if (!File.Exists(ruta))
            {
                MessageBox.Show("Archivo no encontrado.");
                return;
            }

            // Limpiar la tabla antes de cargar nuevos datos
            dataGrid.Rows.Clear();

            string[] lineas = File.ReadAllLines(ruta);
            int contador = 1;

            foreach (string linea in lineas)
            {
                string[] partes = linea.Split('|');
                if (partes.Length >= 5) // Asegurarse que tenga al menos 5 columnas esperadas
                {
                    string ID = partes[0];
                    string usuario = partes[1];
                    string correo = partes[3];
                    string telefono = partes[4];

                    dataGrid.Rows.Add(contador, ID, usuario, correo, telefono);
                    contador++;
                }
            }
        }
        public int posY = 60;
        public void AgregarPreguntaSiNo(string texto,Control parent,List<(string Pregunta, RadioButton OpcionSi, RadioButton OpcionNo)> lista)
        {
            // Crear un contenedor Panel para aislar los botones de esta pregunta
            Panel panel = new Panel
            {
                Location = new Point(20, posY),
                Size = new Size(400, 60)
            };
            parent.Controls.Add(panel);

            // Etiqueta dentro del panel
            Label lbl = new Label
            {
                Text = texto,
                Location = new Point(0, 0),
                AutoSize = true
            };
            panel.Controls.Add(lbl);

            // Botones de radio dentro del panel
            RadioButton rbSi = new RadioButton
            {
                Text = "Sí",
                Location = new Point(20, 25),
                AutoSize = true
            };
            RadioButton rbNo = new RadioButton
            {
                Text = "No",
                Location = new Point(100, 25),
                AutoSize = true
            };
            panel.Controls.Add(rbSi);
            panel.Controls.Add(rbNo);

            lista.Add((texto, rbSi, rbNo));
            posY += 70;
        }
        public void AgregarPreguntaAbierta(string texto , Control parent, List<(string Pregunta, TextBox Respuesta)> lista)
        {
            Label lbl = new Label
            {
                Text = texto,
                Location = new Point(20, posY),
                AutoSize = true
            };
            parent.Controls.Add(lbl);
            posY += 25;

            TextBox txt = new TextBox
            {
                Location = new Point(20, posY),
                Width = 400
            };
            parent.Controls.Add(txt);

            lista.Add((texto, txt));
            posY += 40;
        }
    }
}
