using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorFinal.Controller
{
    internal class EncuestaController
    {
        public void ListarRespuestasPorCedula(string rutaArchivo, string cedulaBuscada, ListBox listBox)
        {
            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("El archivo no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] lineas = File.ReadAllLines(rutaArchivo);
            listBox.Items.Clear();

            List<string> bloqueActual = new List<string>();
            bool encontrado = false;

            foreach (string linea in lineas)
            {
                if (linea.Trim() == "---")
                {
                    // Al llegar al final del bloque, verifica si contiene la cédula
                    if (bloqueActual.Any(l => l.ToLower().Contains("cedula") && l.Contains(cedulaBuscada)))
                    {
                        foreach (var item in bloqueActual)
                            listBox.Items.Add(item);

                        listBox.Items.Add("----");
                        encontrado = true;
                    }

                    bloqueActual.Clear();
                }
                else
                {
                    bloqueActual.Add(linea);
                }
            }

            // Verificar el último bloque si el archivo no termina con "---"
            if (bloqueActual.Any(l => l.ToLower().Contains("cedula") && l.Contains(cedulaBuscada)))
            {
                foreach (var item in bloqueActual)
                    listBox.Items.Add(item);

                listBox.Items.Add("----");
                encontrado = true;
            }

            if (!encontrado)
            {
                MessageBox.Show("No se encontró ninguna encuesta con esa cédula.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void BuscarYMostrarEncuesta(string rutaArchivo, string cedulaBuscada,
        TextBox txtNombre, TextBox txtCedula, TextBox txtEdad,
        Dictionary<string, (RadioButton rbSi, RadioButton rbNo)> preguntasRadio,
        Dictionary<string, TextBox> preguntasAbiertas)
        {
            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("El archivo no existe.");
                return;
            }

            string[] lineas = File.ReadAllLines(rutaArchivo);
            List<string> bloqueActual = new List<string>();
            bool encontrado = false;

            foreach (string linea in lineas)
            {
                if (linea.Trim() == "---")
                {
                    if (bloqueActual.Any(l => l.ToLower().Contains("cedula") && l.Contains(cedulaBuscada)))
                    {
                        foreach (string l in bloqueActual)
                        {
                            if (l.StartsWith("Nombre y Apellido")) txtNombre.Text = l.Split("=>")[1].Trim();
                            else if (l.StartsWith("Cedula")) txtCedula.Text = l.Split("=>")[1].Trim();
                            else if (l.StartsWith("Edad")) txtEdad.Text = l.Split("=>")[1].Trim();
                            else
                            {
                                foreach (var pregunta in preguntasRadio)
                                {
                                    if (l.StartsWith(pregunta.Key))
                                    {
                                        string respuesta = l.Split("=>")[1].Trim().ToLower();
                                        pregunta.Value.rbSi.Checked = respuesta == "sí" || respuesta == "si";
                                        pregunta.Value.rbNo.Checked = respuesta == "no";
                                    }
                                }

                                foreach (var pregunta in preguntasAbiertas)
                                {
                                    if (l.StartsWith(pregunta.Key))
                                    {
                                        pregunta.Value.Text = l.Split("=>")[1].Trim();
                                    }
                                }
                            }
                        }

                        encontrado = true;
                        break;
                    }

                    bloqueActual.Clear();
                }
                else
                {
                    bloqueActual.Add(linea);
                }
            }

            if (!encontrado)
            {
                MessageBox.Show("No se encontró la cédula.", "Aviso");
            }
        }
        public void LimpiarCampos(TextBox txtNombre, TextBox txtCedula, TextBox txtEdad,
            Dictionary<string, (RadioButton rbSi, RadioButton rbNo)> preguntasRadio,
            Dictionary<string, TextBox> preguntasAbiertas)
        {
            txtNombre.Clear();
            txtCedula.Clear();
            txtEdad.Clear();
            foreach (var pregunta in preguntasRadio)
            {
                pregunta.Value.rbSi.Checked = false;
                pregunta.Value.rbNo.Checked = false;
            }
            foreach (var pregunta in preguntasAbiertas)
            {
                pregunta.Value.Clear();
            }
        }

    }
}
