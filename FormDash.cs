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
    public partial class FormDash : Form
    {
        public FormDash()
        {
            InitializeComponent();
            PersonalizarDiseno();
            AplicarTema();
        }

        // 1. Método para esconder todo al arrancar el programa
        private void PersonalizarDiseno()
        {
            panelSubMenuCerrados.Visible = false;
            panelSubMenuAbiertos.Visible = false;
            panelSubMenuPolinomios.Visible = false;
            panelSubMenuNoLineales.Visible = false;
            panelSubMenuIterativos.Visible = false;
            panelSubMenuMetodosLineales.Visible = false;
            panelSubMenuAjusteCurvas.Visible = false;
            panelSubMenuInterpolación.Visible = false;
            // Si agregas más submenús en el futuro, los ocultas aquí también
        }

        // 2. Método para cerrar los menús que estén abiertos
        private void OcultarSubMenu()
        {
            if (panelSubMenuCerrados.Visible == true)
                panelSubMenuCerrados.Visible = false;
            if (panelSubMenuAbiertos.Visible == true)
                panelSubMenuAbiertos.Visible = false;
            if (panelSubMenuPolinomios.Visible == true)
                panelSubMenuPolinomios.Visible = false;
            if (panelSubMenuNoLineales.Visible == true)
                panelSubMenuNoLineales.Visible = false;
            if (panelSubMenuIterativos.Visible == true)
                panelSubMenuIterativos.Visible = false;
            if (panelSubMenuMetodosLineales.Visible == true)
                panelSubMenuMetodosLineales.Visible = false;
            if (panelSubMenuAjusteCurvas.Visible == true)
                panelSubMenuAjusteCurvas.Visible = false;
            if (panelSubMenuInterpolación.Visible == true)
                panelSubMenuInterpolación.Visible = false;
        }

        // 3. El motor del Acordeón: Abre el que pediste y cierra los demás
        private void MostrarSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                OcultarSubMenu(); // Cierra cualquier otro abierto para mantener orden
                subMenu.Visible = true; // Abre el que seleccionaste
            }
            else
            {
                subMenu.Visible = false; // Si ya estaba abierto y le das clic, se cierra
            }
        }

        private void btnMétodosCerrados_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubMenuCerrados);
        }

        private void btnMétodoAbiertos_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubMenuAbiertos);
        }

        private void btnPolinomios_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubMenuPolinomios);
        }

        private void btnMétodosNoLineales_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubMenuNoLineales);
        }

        private void btnMetodosIterativos_Click(object sender, EventArgs e)
        {
            // Abre y cierra el submenú de forma fluida
            MostrarSubMenu(panelSubMenuIterativos);
        }

        private void btnMétodosLineales_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubMenuMetodosLineales);
        }

        private void btnAjusteCurvas_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubMenuAjusteCurvas);
        }

        private void btnInterpolación_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(panelSubMenuInterpolación);
        }
        // Variable para recordar qué formulario está abierto actualmente
        private Form formularioActivo = null;

        private void AbrirFormularioHijo(Form formHijo)
        {
            // Si ya hay una ventana abierta en el panel, la cerramos
            if (formularioActivo != null)
                formularioActivo.Close();

            formularioActivo = formHijo;

            // Le arrancamos el comportamiento de ventana de Windows
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None; // Adiós bordes de ventana
            formHijo.Dock = DockStyle.Fill; // Que llene todo el panel blanco

            // Lo inyectamos en el Panel Contenedor
            panelContenedorPrincipal.Controls.Add(formHijo);
            panelContenedorPrincipal.Tag = formHijo;
            formHijo.BringToFront();
            formHijo.Show(); // ¡Boom! Aparece dentro del dashboard

        }

        // 🧠 Variable para recordar qué botón está presionado
        private Button botonActivo = null;
        private void ActivarBoton(object btnSender)
        {
            if (btnSender != null)
            {
                // 1. Apagamos el botón anterior (lo regresamos a la normalidad)
                if (botonActivo != null)
                {
                    botonActivo.BackColor = Color.FromArgb(31, 41, 55); // Color gris oscuro original
                    botonActivo.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Letra normal
                }

                // 2. Encendemos el nuevo botón seleccionado
                botonActivo = (Button)btnSender;
                botonActivo.BackColor = Color.FromArgb(75, 85, 99); // Un gris más claro para que resalte
                botonActivo.Font = new Font("Segoe UI", 10, FontStyle.Bold); // ¡Pum! Letra en negrita
            }
        }

        private void btnBiseccion_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormBiseccion());
            ActivarBoton(sender);
        }

        private void btnReglaFalsa_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormReglaFalsa());
            ActivarBoton(sender);
        }

        private void btnNewtonRaphson_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormNewton());
            ActivarBoton(sender);
        }

        private void btnSecante_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormSecante());
            ActivarBoton(sender);
        }

        private void btnPuntoFijo_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormPuntoFijo());
            ActivarBoton(sender);
        }

        private void btnMuller_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormMuller());
            ActivarBoton(sender);
        }

        private void btnHornerNewton_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormHornerNewton());
            ActivarBoton(sender);
        }

        private void btnBairstow_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormBairstow());
            ActivarBoton(sender);
        }

        private void btnNewtonNoLineal_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormNoLinealNewton());
            ActivarBoton(sender);
        }

        private void btnGaussSeidel_Click(object sender, EventArgs e)
        {
            // Recordá verificar si tu panel central se llama 'pnlContenedor' o similar
            AbrirFormularioHijo(new FormGaussSeidel());
            ActivarBoton(sender);
        }

        // 2. Conexión para el método de Jacobi (El hermano de Seidel)
        private void btnJacobi_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormJacobi());
            ActivarBoton(sender);
        }

        private void btnEliminacionGaussiana_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormEliminaciónGaussiana());
            ActivarBoton(sender);
        }
        private void btnFactorizaciónLU_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormFactorizacionLU());
            ActivarBoton(sender);
        }
        private void btnGaussJordan_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormGaussJordan());
            ActivarBoton(sender);
        }
        private void btnReglaCramer_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormReglaCramer());
            ActivarBoton(sender);
        }
        private void btnMatrizInversa_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormMatrizInversa());
            ActivarBoton(sender);
        }

        private void btnRegresionPolinomial_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormRegresionPolinomial());
            ActivarBoton(sender);
        }

        private void btnNewtonDDividida_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormNewtonDiferenciaDividida());
            ActivarBoton(sender);
        }

        private void btnLagrange_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FormPolinomioLagrange());
            ActivarBoton(sender);
        }

        private void AplicarTema()
        {
            // 🎨 Paleta de colores
            Color colorFondoMenu = Color.FromArgb(17, 24, 39);
            Color colorSubMenu = Color.FromArgb(31, 41, 55);
            Color colorTexto = Color.FromArgb(243, 244, 246);
            Color colorHover = Color.FromArgb(55, 65, 81);

            // 1. Configuramos el panel lateral
            PanelMenuLateral.BackColor = colorFondoMenu;
            PanelMenuLateral.Width = 260;

            // 🛡️ ¡EL ESCUDO DEL LOGO! Fijamos su altura a 100px para que no lo aplasten
            PanelLogo.Height = 160;
            PanelLogo.BackColor = colorFondoMenu; // Para que el fondo combine perfecto

            // 2. Colores de los contenedores de submenú
            panelSubMenuCerrados.BackColor = colorSubMenu;
            panelSubMenuAbiertos.BackColor = colorSubMenu;
            panelSubMenuPolinomios.BackColor = colorSubMenu;
            panelSubMenuNoLineales.BackColor = colorSubMenu;

            // 3. Magia automática: Renderizado y Auto-Ajuste de Alturas
            foreach (Control control in PanelMenuLateral.Controls)
            {
                if (control is Button)
                {
                    EstilizarBoton((Button)control, colorFondoMenu, colorTexto, colorHover, 50, new Font("Segoe UI", 11, FontStyle.Bold));
                }
                // 🛠️ EL ARREGLO: Ahora solo afecta a los paneles que se llamen "SubMenu..."
                else if (control is Panel && control.Name.Contains("SubMenu"))
                {
                    int altoTotal = 0;

                    foreach (Control subControl in control.Controls)
                    {
                        if (subControl is Button)
                        {
                            // 🛠️ CAMBIO AQUÍ: Altura a 45 y Fuente a tamaño 10
                            EstilizarBoton((Button)subControl, colorSubMenu, colorTexto, colorHover, 45, new Font("Segoe UI", 10, FontStyle.Regular));
                            ((Button)subControl).Padding = new Padding(35, 0, 0, 0);

                            // 🛠️ CAMBIO AQUÍ: Ahora sumamos 45px para que no se corten
                            altoTotal += 45;
                        }
                    }

                    control.Height = altoTotal;
                }
            }
        }
        // (El método EstilizarBoton se queda exactamente igual)
        private void EstilizarBoton(Button btn, Color fondo, Color texto, Color hover, int alto, Font fuente)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = hover;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(75, 85, 99);
            btn.BackColor = fondo;
            btn.ForeColor = texto;
            btn.Height = alto;
            btn.Font = fuente;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
        }
    }
}


