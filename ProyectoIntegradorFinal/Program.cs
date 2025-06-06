using ProyectoIntegradorFinal.View;

namespace ProyectoIntegradorFinal
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] usuario = { "admin", "admin123" };
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Encuesta());
        }
    }
}