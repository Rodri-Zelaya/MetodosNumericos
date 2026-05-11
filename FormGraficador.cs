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

            // --- NUEVA LÍNEA: Le decimos que escuche la ruedita del mouse ---
            chart1.MouseWheel += chart1_MouseWheel;
        }

        private void GraficarFuncion(double xMin, double xMax)
        {
            try
            {
                chart1.Series.Clear();
                chart1.Series.Add("Curva");
                chart1.Series["Curva"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chart1.Series["Curva"].Color = System.Drawing.Color.Red;
                chart1.Series["Curva"].BorderWidth = 2;

                chart1.ChartAreas[0].AxisX.Crossing = 0;
                chart1.ChartAreas[0].AxisY.Crossing = 0;
                chart1.ChartAreas[0].AxisX.Minimum = xMin;
                chart1.ChartAreas[0].AxisX.Maximum = xMax;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

                // --- MAGIA INTERACTIVA: Habilitar Zoom en el Eje X ---
                chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

                // --- MAGIA INTERACTIVA: Habilitar Zoom en el Eje Y ---
                chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
                chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;

                org.mariuszgromada.math.mxparser.Argument argX = new org.mariuszgromada.math.mxparser.Argument("x");
                org.mariuszgromada.math.mxparser.Expression expr = new org.mariuszgromada.math.mxparser.Expression(funcionAGraficar, argX);

                double paso = 0.1;

                for (double x = xMin; x <= xMax; x += paso)
                {
                    argX.setArgumentValue(x);
                    double y = expr.calculate();

                    if (!double.IsNaN(y) && !double.IsInfinity(y))
                    {
                        if (y > 50) y = 50;
                        if (y < -50) y = -50;
                        chart1.Series["Curva"].Points.AddXY(x, y);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al graficar la función: " + ex.Message);
            }
        }

        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                var xAxis = chart1.ChartAreas[0].AxisX;
                var yAxis = chart1.ChartAreas[0].AxisY;

                if (e.Delta < 0)
                {
                    // Si le das a la ruedita hacia ABAJO: Resetea el zoom a la vista original
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0)
                {
                    // Si le das a la ruedita hacia ARRIBA: Hace zoom justo donde tienes el mouse
                    double xPos = xAxis.PixelPositionToValue(e.Location.X);
                    double yPos = yAxis.PixelPositionToValue(e.Location.Y);

                    double xMin = xAxis.ScaleView.ViewMinimum;
                    double xMax = xAxis.ScaleView.ViewMaximum;
                    double yMin = yAxis.ScaleView.ViewMinimum;
                    double yMax = yAxis.ScaleView.ViewMaximum;

                    // Calcula el nuevo cuadro de visión (acerca a la mitad)
                    xAxis.ScaleView.Zoom(xPos - (xMax - xMin) / 4, xPos + (xMax - xMin) / 4);
                    yAxis.ScaleView.Zoom(yPos - (yMax - yMin) / 4, yPos + (yMax - yMin) / 4);
                }
            }
            catch
            {
                // Si el usuario hace zoom fuera de los límites, simplemente lo ignoramos
            }
        }
    }
}
