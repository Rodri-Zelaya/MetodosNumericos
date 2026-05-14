using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Métodos_Numéricos
{
    public partial class FormPrincipal : Form
    {
        private Form? formularioActivo = null;

        // 🎨 PALETA DE COLORES PASTELES (Solo para el menú principal)
        private Color celestitoPastel = Color.FromArgb(190, 230, 255);
        private Color blancoInactivo = Color.White;
        private Color hoverSuave = Color.FromArgb(220, 240, 255);
        public FormPrincipal()
        {
            InitializeComponent();
            ConfigurarDiseñoMenu();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        // --- MÉTODOS DE DISEÑO VISUAL ---
        private void ConfigurarDiseñoMenu()
        {
            // Lista exacta con los nombres de tus botones
            Button[] botonesMenu = { btnBiseccion, btnReglaFalsa, btnNewton, btnSecante, btnPuntoFijo, btnMuller, btnBairstow, btnHornerNewton, btnNewtonNL };

            foreach (Button btn in botonesMenu)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0; // Sin bordes para estilo pestaña
                btn.BackColor = blancoInactivo;
                btn.FlatAppearance.MouseOverBackColor = hoverSuave; // Hover por defecto
            }
        }

        private void ActivarBotonMenu(Button botonSeleccionado)
        {
            Button[] botonesMenu = { btnBiseccion, btnReglaFalsa, btnNewton, btnSecante, btnPuntoFijo, btnMuller, btnBairstow, btnHornerNewton, btnNewtonNL };

            foreach (Button btn in botonesMenu)
            {
                if (btn == botonSeleccionado)
                {
                    btn.BackColor = celestitoPastel;
                    btn.FlatAppearance.MouseOverBackColor = celestitoPastel;
                }
                else
                {
                    btn.BackColor = blancoInactivo;
                    btn.FlatAppearance.MouseOverBackColor = hoverSuave;
                }
            }
        }

        private void AbrirFormularioEnPanel(Form formHijo)
        {
            // Si ya hay un método abierto, lo cerramos para limpiar la pantalla
            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            // Configuramos la nueva ventana para que se comporte como un "dibujo" dentro del panel
            formularioActivo = formHijo;
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;

            // Agregamos la ventana al panelContenedor y la mostramos
            panelContenedor.Controls.Add(formHijo);
            panelContenedor.Tag = formHijo;
            formHijo.BringToFront();
            formHijo.Show();
        }
        private void btnBiseccion_Click(object sender, EventArgs e)
        {
            ActivarBotonMenu((Button)sender);
            // Le mandas el formulario de Bisección a la función mágica
            AbrirFormularioEnPanel(new FormBiseccion());
        }

        private void btnReglaFalsa_Click(object sender, EventArgs e)
        {
            ActivarBotonMenu((Button)sender);
            AbrirFormularioEnPanel(new FormReglaFalsa());
        }

        private void btnNewton_Click(object sender, EventArgs e)
        {
            ActivarBotonMenu((Button)sender);
            AbrirFormularioEnPanel(new FormNewton());
        }

        private void btnSecante_Click(object sender, EventArgs e)
        {
            ActivarBotonMenu((Button)sender);
            AbrirFormularioEnPanel(new FormSecante());
        }

        private void btnPuntoFijo_Click(object sender, EventArgs e)
        {
            ActivarBotonMenu((Button)sender);
            AbrirFormularioEnPanel(new FormPuntoFijo());
        }

        private void btnMuller_Click(object sender, EventArgs e)
        {
            ActivarBotonMenu((Button)sender);
            AbrirFormularioEnPanel(new FormMuller());
        }

        private void btnBairstow_Click(object sender, EventArgs e)
        {
            ActivarBotonMenu((Button)sender);
            AbrirFormularioEnPanel(new FormBairstow());
        }

        private void btnHornerNewton_Click(object sender, EventArgs e)
        {
            ActivarBotonMenu((Button)sender);
            AbrirFormularioEnPanel(new FormHornerNewton());
        }

        private void btnNewtonNL_Click(object sender, EventArgs e)
        {
            ActivarBotonMenu((Button)sender);
            AbrirFormularioEnPanel(new FormNoLinealNewton());
        }
    }
}
