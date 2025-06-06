using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ProyectoIntegradorFinal.View.Controls
{
    internal class CreateController
    {


        public Label CreateLabel(string text, int x, int y, int width, int height, Control parent)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y);
            label.Size = new Size(width, height);
            parent.Controls.Add(label);
            return label;
        }
        public Button CreateButton(string text, int x, int y, int width, int height, Control parent, EventHandler onClick)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Size = new Size(width, height);
            button.Click += onClick;
            parent.Controls.Add(button);
            return button;
        }

        public TextBox CreateTextBox(int x, int y, int width, int height, Control parent, bool useSystemPasswordChar = false)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, height);
            textBox.UseSystemPasswordChar = useSystemPasswordChar;
            parent.Controls.Add(textBox);
            return textBox;
        }
        public ToolStrip CreateToolstrip(string[] items, Control parent)
        {
            ToolStrip toolStrip = new ToolStrip();
            foreach (var item in items)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(item);
                toolStrip.Items.Add(menuItem);
            }
            parent.Controls.Add(toolStrip);
            return toolStrip;
        }
        public Panel CreatePanel(int width, int height, DockStyle dockStyle, Image backgroundImage, Control parent)
        {
            Panel panel = new Panel();
            panel.Size = new Size(width, height);
            panel.Dock = dockStyle;
            panel.BackgroundImage = backgroundImage;
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            parent.Controls.Add(panel);
            return panel;
        }
        public ToolStrip CreateToolStrip(string[] items, Control parent)
        {
            ToolStrip toolStrip = new ToolStrip();
            foreach (var item in items)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(item);
                toolStrip.Items.Add(menuItem);
            }
            parent.Controls.Add(toolStrip);
            return toolStrip;
        }
        public ToolStripDropDown CreateDropDown(string[] items, Control parent)
        {
            ToolStripDropDown dropDown = new ToolStripDropDown();
            foreach (var item in items)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(item);
                dropDown.Items.Add(menuItem);
            }
            parent.Controls.Add(dropDown);
            return dropDown;
        }

        // crear y retornar elemento ToolStripMenuItem
        public static ToolStripMenuItem CreateToolStripItem(string text)
        {
            ToolStripMenuItem newItem = new ToolStripMenuItem(text);
            newItem.Name = $"toolstrip{text.ToLower().Trim()}";
            return newItem;
        }

        public bool AddToolStripItem(MenuStrip menuStrip, string menuPrincipal, ToolStripMenuItem newItem)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if (item.Text == menuPrincipal)
                {
                    item.Name = $"toolstrip{item.Text.ToLower().Trim()}"; // asignar un nombre único al menú
                    item.DropDownItems.Add(newItem);
                    return true; // Se agregó el nuevo ítem
                }
            }
            return false; // No se encontró el menú principal
        }

        public void AddToolStripMenuItems(MenuStrip menuStrip, ToolStripMenuItem[] items, string menu)
        {
            foreach (ToolStripMenuItem item in items)
            {
                try
                {
                    if (AddToolStripItem(menuStrip, menu, item)) continue;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar los items al menú usuario\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        public MenuStrip CreateMenuStrip(string[] menuOptions)
        {
            // Crear nuevo MenuStrip  
            MenuStrip menuStrip = new MenuStrip();
            menuStrip.Name = "menuStrip1";
            // Agregar cada opción como un ítem del menú  
            foreach (string option in menuOptions)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(option);
                menuItem.Name = $"toolstrip{option.ToLower().Trim()}"; // Asignar un nombre único al menú
                menuStrip.Items.Add(menuItem);
            }

            // Devolver el MenuStrip creado  
            return menuStrip;
        }
        public ToolStrip CrearBarraDeHerramientas()
        {
            ToolStrip barra = new ToolStrip
            {
                Dock = DockStyle.Top,
                ImageScalingSize = new Size(24, 24),
                RenderMode = ToolStripRenderMode.Professional
            };

            return barra;
        }
        public ToolStripButton CrearBoton(string toolTip, string rutaImagen, EventHandler alClic)
        {
            ToolStripButton boton = new ToolStripButton
            {
                Image = Image.FromFile(rutaImagen),
                ToolTipText = toolTip,
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };

            if (alClic != null)
                boton.Click += alClic;

            return boton;
        }
        public static void BlockToolStripItems(MenuStrip menuStrip, string menu, bool value)
        {
            try
            {
                // recorrer las opciones principales del menú
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    // buscar el menú a bloquear o desbloquear
                    if (item.Text == menu)
                    {
                        // almacenar los submenúes en una variable y establecer el valor
                        foreach (ToolStripMenuItem subItem in item.DropDownItems)
                        {
                            subItem.Enabled = value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al bloquear los items del menú {menu}\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public RadioButton CreateRadioButton(string text, int x, int y, Control parent)
        {
            RadioButton radioButton = new RadioButton
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true
            };
            parent.Controls.Add(radioButton);
            return radioButton;
        }
        public static void SetMenuItemEnabled(MenuStrip menuStrip, string menuPrincipal, string subItemNombre, bool enabled)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if (item.Text == menuPrincipal)
                {
                    foreach (ToolStripItem subItem in item.DropDownItems)
                    {
                        if (subItem.Text == subItemNombre)
                        {
                            subItem.Enabled = enabled;
                            return;
                        }
                    }
                }
            }
        }
        public Panel CrearPanelConScroll(Control contenedorPadre, int x, int y, int ancho, int alto)
        {
            Panel panelScroll = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(ancho, alto),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            contenedorPadre.Controls.Add(panelScroll);
            return panelScroll;
        }





    }
}
