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
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

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
            // Le mandas el formulario de Bisección a la función mágica
            AbrirFormularioEnPanel(new FormBiseccion());
        }

        private void btnReglaFalsa_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FormReglaFalsa());
        }

        private void btnNewton_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FormNewton());
        }

        private void btnSecante_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FormSecante());
        }

        private void btnPuntoFijo_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FormPuntoFijo());
        }

        private void btnMuller_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FormMuller());
        }

        private void btnBairstow_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FormBairstow());
        }

        private void btnHornerNewton_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FormHornerNewton());
        }

        private void btnNewtonNL_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FormNoLinealNewton());
        }
    }
}
