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
    public partial class FormGraficador : Form
    {
        // Variable global para guardar la función que nos manden
        private string funcionAGraficar;

        public FormGraficador(string funcionRecibida)
        {
            InitializeComponent();
            funcionAGraficar = funcionRecibida;

            // Le ponemos un título bonito a la ventana emergente
            this.Text = "Visor Gráfico - Función: " + funcionAGraficar;

            // Bloqueamos el tamaño para que no lo hagan muy pequeño
            this.MinimumSize = new System.Drawing.Size(600, 400);

            // Mandamos a graficar en cuanto nazca la ventana
            GraficarFuncion(-10, 10);

            // --- ⚡ EVENTOS MÁGICOS INTERACTIVOS ---
            chart1.MouseWheel += chart1_MouseWheel; // Ruedita del mouse para Zoom
            chart1.MouseMove += chart1_MouseMove;   // Radar para coordenadas
        }

        private void GraficarFuncion(double xMin, double xMax)
        {
            try
            {
                chart1.Series.Clear();
                chart1.Series.Add("Curva");

                // 🛠️ CAMBIO 1: Adiós Spline. Usamos 'Line' que es matemáticamente estricto y exacto.
                chart1.Series["Curva"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["Curva"].Color = System.Drawing.Color.Red;
                chart1.Series["Curva"].BorderWidth = 2;

                var area = chart1.ChartAreas[0];

                // 🎨 ESTILOS GEOGEBRA
                area.AxisX.Crossing = 0;
                area.AxisY.Crossing = 0;
                area.AxisX.LineWidth = 2;
                area.AxisY.LineWidth = 2;
                area.AxisX.LineColor = System.Drawing.Color.Black;
                area.AxisY.LineColor = System.Drawing.Color.Black;
                area.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                area.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

                // 🎯 CROSSHAIR
                area.CursorX.IsUserEnabled = true;
                area.CursorX.IsUserSelectionEnabled = true;
                area.CursorX.LineColor = System.Drawing.Color.DarkBlue;
                area.CursorX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;

                area.CursorY.IsUserEnabled = true;
                area.CursorY.IsUserSelectionEnabled = true;
                area.CursorY.LineColor = System.Drawing.Color.DarkBlue;
                area.CursorY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;

                area.AxisX.ScaleView.Zoomable = true;
                area.AxisY.ScaleView.Zoomable = true;
                area.AxisX.ScrollBar.IsPositionedInside = true;
                area.AxisY.ScrollBar.IsPositionedInside = true;

                // 🛠️ CAMBIO 2: Límites iniciales más estéticos (Para que no se vea tan alejado)
                area.AxisX.Minimum = xMin;
                area.AxisX.Maximum = xMax;
                area.AxisY.Minimum = -10; // Tope inferior visual
                area.AxisY.Maximum = 10;  // Tope superior visual

                org.mariuszgromada.math.mxparser.Argument argX = new org.mariuszgromada.math.mxparser.Argument("x");
                org.mariuszgromada.math.mxparser.Expression expr = new org.mariuszgromada.math.mxparser.Expression(funcionAGraficar, argX);

                double paso = 0.05;

                for (double x = xMin; x <= xMax; x += paso)
                {
                    argX.setArgumentValue(x);
                    double y = expr.calculate();

                    // 🛡️ EL ESCUDO ANTI-ASÍNTOTAS REFORZADO
                    if (double.IsNaN(y) || double.IsInfinity(y) || Math.Abs(y) > 50)
                    {
                        // 🛠️ CAMBIO 3: En C#, la forma correcta de "levantar el lápiz" es declarar un punto Vacío (Empty)
                        var puntoVacio = new System.Windows.Forms.DataVisualization.Charting.DataPoint(x, 0);
                        puntoVacio.IsEmpty = true;
                        chart1.Series["Curva"].Points.Add(puntoVacio);
                    }
                    else
                    {
                        chart1.Series["Curva"].Points.AddXY(x, y);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al graficar la función: " + ex.Message);
            }
        }

        // 🔭 EL RADAR: Captura el movimiento del mouse y muestra coordenadas exactas
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                var area = chart1.ChartAreas[0];

                // Convertimos los píxeles de la pantalla a valores matemáticos X y Y
                double xVal = area.AxisX.PixelPositionToValue(e.X);
                double yVal = area.AxisY.PixelPositionToValue(e.Y);

                // Mostramos las coordenadas dinámicamente en el título de la ventana emergente
                this.Text = $"GeoVisor - f(x)={funcionAGraficar}   |   📍 Coordenadas: X: {xVal.ToString("F4")} , Y: {yVal.ToString("F4")}";
            }
            catch
            {
                // Si el mouse sale de la zona de la gráfica, ignoramos para que no falle
            }
        }

        // 🔍 EL ZOOM INTELIGENTE
        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                var xAxis = chart1.ChartAreas[0].AxisX;
                var yAxis = chart1.ChartAreas[0].AxisY;

                if (e.Delta < 0)
                {
                    // Hacia ABAJO: Quitar zoom (Reset)
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0)
                {
                    // Hacia ARRIBA: Hacer zoom in justo donde está apuntando el mouse
                    double xPos = xAxis.PixelPositionToValue(e.Location.X);
                    double yPos = yAxis.PixelPositionToValue(e.Location.Y);

                    double xMin = xAxis.ScaleView.ViewMinimum;
                    double xMax = xAxis.ScaleView.ViewMaximum;
                    double yMin = yAxis.ScaleView.ViewMinimum;
                    double yMax = yAxis.ScaleView.ViewMaximum;

                    xAxis.ScaleView.Zoom(xPos - (xMax - xMin) / 4, xPos + (xMax - xMin) / 4);
                    yAxis.ScaleView.Zoom(yPos - (yMax - yMin) / 4, yPos + (yMax - yMin) / 4);
                }
            }
            catch
            {
                // Ignorar si hace zoom en el vacío
            }
        }
    }
}