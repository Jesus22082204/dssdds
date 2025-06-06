using ProyectoIntegradorFinal.Controller;
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
    public partial class Encuesta : Form
    {


        public Encuesta()
        {
            InitializeComponent();
            CRUD cRUD = new CRUD();
            List<(string Pregunta, RadioButton OpcionSi, RadioButton OpcionNo)> preguntas = new();
            List<(string Pregunta, TextBox Respuesta)> preguntasAbiertas = new();
            CreateController createController = new CreateController();
            Panel panel = createController.CrearPanelConScroll(this, 100, 10, 750, 750);            
            createController.CreateLabel("Encuesta", 20, 20, 150, 30, panel);            
            cRUD.AgregarPreguntaAbierta("Nombre y Apellido", panel, preguntasAbiertas);
            cRUD.AgregarPreguntaAbierta("Cedula", panel, preguntasAbiertas);
            cRUD.AgregarPreguntaAbierta("Edad", panel, preguntasAbiertas);
            cRUD.AgregarPreguntaSiNo("¿Has utilizado Visual Studio antes?", panel, preguntas);
            cRUD.AgregarPreguntaSiNo("¿Te gusta programar?",panel,preguntas);
            cRUD.AgregarPreguntaSiNo("¿Te gustaría aprender más sobre C#?", panel, preguntas);
            cRUD.AgregarPreguntaSiNo("¿Has trabajado en proyectos con Visual Studio?", panel, preguntas );
            cRUD.AgregarPreguntaAbierta("¿Cuál es tu lenguaje de programación favorito?",panel,preguntasAbiertas);
            cRUD.AgregarPreguntaAbierta("¿Qué mejorarías de esta encuesta?",panel, preguntasAbiertas);


            // Botón Enviar
            createController.CreateButton("Enviar", 20, cRUD.posY, 100, 30, panel, (s, e) =>
            {
                StringBuilder sb = new StringBuilder();

                foreach (var (Pregunta, OpcionSi, OpcionNo) in preguntas)
                {
                    string respuesta = OpcionSi.Checked ? "Sí" : OpcionNo.Checked ? "No" : "Sin responder";

                    sb.AppendLine($"{Pregunta} => {respuesta}");
                }
                foreach (var (Pregunta, Respuesta) in preguntasAbiertas)
                {
                    string texto = string.IsNullOrWhiteSpace(Respuesta.Text) ? "Sin responder" : Respuesta.Text;
                    sb.AppendLine($"{Pregunta} => {texto}");
                }
                File.AppendAllText("C:\\Users\\mendo\\Downloads\\RespuestaEncuesta.txt", sb.ToString() + "\n---\n");
                MessageBox.Show("Encuesta guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
           

        }
        



        private void Encuesta_Load(object sender, EventArgs e)
        {
        }
    }
}
