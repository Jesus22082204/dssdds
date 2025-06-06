using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIntegradorFinal.View.Controls
{
    internal class Eventos
    {
        public static void EventoSubMenu(MenuStrip menuStrip, string menuPrincipal, string submenuText, EventHandler evento)
        {
            // Buscar el menú principal
            foreach (ToolStripMenuItem elemento in menuStrip.Items)
            {
                if (elemento.Text == menuPrincipal)
                {
                    // Buscar el submenú
                    foreach (ToolStripMenuItem subElemento in elemento.DropDownItems)
                    {
                        if (subElemento.Text == submenuText)
                        {
                            // Asignar el evento
                            subElemento.Click += evento;
                            return;
                        }
                    }
                }
            }

        }
        public static void AsignarEventoAToolStrip(ToolStrip strip, string itemText, EventHandler evento)
        {
            foreach (ToolStripItem item in strip.Items)
            {
                if (item.Text == itemText)
                {
                    // Si es botón
                    if (item is ToolStripButton boton)
                    {
                        boton.Click += evento;
                    }
                    // Si es menú
                    else if (item is ToolStripMenuItem menuItem)
                    {
                        menuItem.Click += evento;
                    }
                    return;
                }
            }
        }

    }
}
