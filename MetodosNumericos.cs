using System;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;

public class MetodosNumericos
{
    public MetodosNumericos()
    {
        License.iConfirmNonCommercialUse("Uso educativo");
    }
    // Ahora f(x) recibe el texto de la función y el valor de X
    public double EvaluarFuncion(string expresion, double x)
    {
        try
        {
            // mXparser entiende "x" como un argumento
            Argument argX = new Argument("x", x);

            // Evaluamos la expresión con ese argumento
            Expression e = new Expression(expresion, argX);

            double resultado = e.calculate();

            // Si el usuario mete algo ilógico (ej. dividir entre 0), mXparser devuelve "NaN"
            if (double.IsNaN(resultado)) return 0;

            return resultado;
        }
        catch { return 0; }
    }

    // Fíjate que agregamos "string funcion" a los parámetros
    public void Biseccion(string funcion, double a, double b, double tol, int maxIter, DataGridView dgv)
    {
        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "Iteración");
        dgv.Columns.Add("A", "a");
        dgv.Columns.Add("B", "b");
        dgv.Columns.Add("C", "c");
        dgv.Columns.Add("Fa", "f(a)");
        dgv.Columns.Add("Fc", "f(c)");
        dgv.Columns.Add("Fb", "f(b)");
        dgv.Columns.Add("FaFc", "f(a)*f(c)");
        dgv.Columns.Add("Ea", "E. Absoluto");
        dgv.Columns.Add("Er", "E. Relativo %");

        dgv.Rows.Clear();
        double c = 0;
        double c_anterior;
        double errorRelativo = 0;

        for (int i = 1; i <= maxIter; i++)
        {
            c_anterior = c;
            c = (a + b) / 2;

            // Usamos el nuevo método EvaluarFuncion pasándole el texto de la función
            double fa = EvaluarFuncion(funcion, a);
            double fc = EvaluarFuncion(funcion, c);
            double fb = EvaluarFuncion(funcion, b);
            double producto = fa * fc;

            double ea = Math.Abs(c - c_anterior);
            if (i > 1) errorRelativo = Math.Abs((c - c_anterior) / c) * 100;

            int n = dgv.Rows.Add();
            dgv.Rows[n].Cells[0].Value = i;
            dgv.Rows[n].Cells[1].Value = a.ToString("F6");
            dgv.Rows[n].Cells[2].Value = c.ToString("F6");
            dgv.Rows[n].Cells[3].Value = b.ToString("F6");
            dgv.Rows[n].Cells[4].Value = fa.ToString("F8");
            dgv.Rows[n].Cells[5].Value = fc.ToString("F8");
            dgv.Rows[n].Cells[6].Value = fb.ToString("F8");
            dgv.Rows[n].Cells[7].Value = producto.ToString("F10");

            dgv.Rows[n].Cells[8].Value = (i == 1) ? "-" : ea.ToString("F4");
            dgv.Rows[n].Cells[9].Value = (i == 1) ? "-" : errorRelativo.ToString("F4") + "%";

            // Colores rojos para negativos (Opcional, pero se ve pro)
            if (fa < 0) dgv.Rows[n].Cells[4].Style.ForeColor = System.Drawing.Color.Red;
            if (fc < 0) dgv.Rows[n].Cells[5].Style.ForeColor = System.Drawing.Color.Red;
            if (producto < 0) dgv.Rows[n].Cells[7].Style.ForeColor = System.Drawing.Color.Red;

            if (i > 1 && errorRelativo < tol) break;

            if (producto < 0)
                b = c;
            else
                a = c;
        }
    }

    public void ReglaFalsa(string funcion, double a, double b, double tol, int maxIter, DataGridView dgv)
    {
        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "Iteración");
        dgv.Columns.Add("A", "a");
        dgv.Columns.Add("B", "b");
        dgv.Columns.Add("Fa", "f(a)");
        dgv.Columns.Add("Fb", "f(b)");
        dgv.Columns.Add("C", "c");
        dgv.Columns.Add("Fc", "f(c)");
        dgv.Columns.Add("FaFc", "f(a)*f(c)");
        dgv.Columns.Add("Er", "Er%");

        dgv.Rows.Clear();
        double c = 0;
        double c_anterior = 0;
        double errorRelativo = 0;

        for (int i = 1; i <= maxIter; i++)
        {
            double fa = EvaluarFuncion(funcion, a);
            double fb = EvaluarFuncion(funcion, b);

            c_anterior = c;

            // ¡LA NUEVA FÓRMULA DE REGLA FALSA!
            c = b - ((fb * (a - b)) / (fa - fb));

            double fc = EvaluarFuncion(funcion, c);
            double producto = fa * fc;

            // Cálculo del Error Relativo
            if (i > 1) errorRelativo = Math.Abs((c - c_anterior) / c) * 100;

            // Agregamos la fila (Orden exacto de tu captura)
            int n = dgv.Rows.Add();
            dgv.Rows[n].Cells[0].Value = i;
            dgv.Rows[n].Cells[1].Value = a.ToString("F8");
            dgv.Rows[n].Cells[2].Value = b.ToString("F8");
            dgv.Rows[n].Cells[3].Value = fa.ToString("F9");
            dgv.Rows[n].Cells[4].Value = fb.ToString("F9");
            dgv.Rows[n].Cells[5].Value = c.ToString("F9"); // c en verde en tu Excel
            dgv.Rows[n].Cells[6].Value = fc.ToString("F8");
            dgv.Rows[n].Cells[7].Value = producto.ToString("F8");
            dgv.Rows[n].Cells[8].Value = (i == 1) ? "" : errorRelativo.ToString("F4");

            // Colores para que quede Pro (rojos y verdes como en tu imagen)
            if (fa < 0) dgv.Rows[n].Cells[3].Style.ForeColor = System.Drawing.Color.DarkRed;
            dgv.Rows[n].Cells[5].Style.ForeColor = System.Drawing.Color.DarkGreen; // c en verde
            if (producto < 0) dgv.Rows[n].Cells[7].Style.ForeColor = System.Drawing.Color.DarkRed;

            // Parada por tolerancia
            if (i > 1 && errorRelativo < tol) break;

            // Actualización de intervalos
            if (producto < 0)
                b = c;
            else
                a = c;
        }
    }

    public void NewtonRaphson(string funcion, string derivada, double x0, double tol, int maxIter, DataGridView dgv)
    {
        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "Iteración");
        dgv.Columns.Add("Ci", "Ci");
        dgv.Columns.Add("FCi", "f(ci)");
        dgv.Columns.Add("DFCi", "f'(ci)");
        dgv.Columns.Add("Ci1", "ci+1");
        dgv.Columns.Add("Ea", "Ea");
        dgv.Columns.Add("Er", "Er%");

        dgv.Rows.Clear();
        double ci = x0;
        double ci_siguiente = 0;
        double ci_anterior = 0; // ¡Nueva variable para recordar el paso anterior!
        double errorAbsoluto = 0;
        double errorRelativo = 0;

        for (int i = 1; i <= maxIter; i++)
        {
            double f_val = EvaluarFuncion(funcion, ci);
            double df_val = EvaluarFuncion(derivada, ci);

            if (Math.Abs(df_val) < 1e-12)
            {
                MessageBox.Show("Error: La derivada es cero. El método no puede continuar.");
                break;
            }

            ci_siguiente = ci - (f_val / df_val);

            // --- EL TRUCO ESTÁ AQUÍ ---
            // Calculamos el error usando el Ci actual y el Ci de la fila anterior
            if (i > 1)
            {
                errorAbsoluto = Math.Abs(ci - ci_anterior);
                errorRelativo = (errorAbsoluto / Math.Abs(ci)) * 100;
            }

            int n = dgv.Rows.Add();
            dgv.Rows[n].Cells[0].Value = i;

            // Usamos "0.########" para que quite los ceros extra y se vea como Excel
            dgv.Rows[n].Cells[1].Value = ci.ToString("0.########");
            dgv.Rows[n].Cells[2].Value = f_val.ToString("G8"); // G permite notación científica si es necesario
            dgv.Rows[n].Cells[3].Value = df_val.ToString("0.########");
            dgv.Rows[n].Cells[4].Value = ci_siguiente.ToString("0.########");

            dgv.Rows[n].Cells[5].Value = (i == 1) ? "-" : errorAbsoluto.ToString("0.########");
            dgv.Rows[n].Cells[6].Value = (i == 1) ? "-" : errorRelativo.ToString("0.########");

            if (i > 1 && errorRelativo < tol) break;

            // Guardamos el Ci actual para usarlo en la siguiente vuelta
            ci_anterior = ci;
            ci = ci_siguiente;
        }
    }

    public void Secante(string funcion, double c0, double c1, double tol, int maxIter, DataGridView dgv)
    {
        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "Iteración");
        dgv.Columns.Add("Ci", "Ci");
        dgv.Columns.Add("FCi", "f(ci)");
        dgv.Columns.Add("Er", "Er%");
        dgv.Columns.Add("Decision", "Decisión");

        dgv.Rows.Clear();

        double x0 = c0;
        double x1 = c1;
        double errorRelativo = 0;
        double last_ci = 0; // Variable clave para calcular el error igual que el Excel

        for (int i = 1; i <= maxIter; i++)
        {
            double f0 = EvaluarFuncion(funcion, x0);
            double f1 = EvaluarFuncion(funcion, x1);

            if (Math.Abs(f0 - f1) < 1e-12)
            {
                MessageBox.Show("Error: f(c0) y f(c1) son casi iguales. El método falla aquí.");
                break;
            }

            // Calculamos el nuevo Ci
            double ci = x1 - ((f1 * (x0 - x1)) / (f0 - f1));
            double f_ci = EvaluarFuncion(funcion, ci);

            string decision = "CONTINUAR";

            // Cálculo de Error a partir de la iteración 2, usando el Ci anterior
            if (i > 1)
            {
                errorRelativo = Math.Abs((ci - last_ci) / ci) * 100;

                if (errorRelativo < tol)
                {
                    decision = "FINALIZAR";
                }
            }

            // Agregamos la fila a la tabla
            int n = dgv.Rows.Add();
            dgv.Rows[n].Cells[0].Value = i;
            dgv.Rows[n].Cells[1].Value = ci.ToString("0.########");
            dgv.Rows[n].Cells[2].Value = f_ci.ToString("G8");
            dgv.Rows[n].Cells[3].Value = (i == 1) ? "-" : errorRelativo.ToString("0.########");
            dgv.Rows[n].Cells[4].Value = (i == 1) ? "" : decision;

            if (decision == "FINALIZAR") break;

            // --- LA MAGIA PARA CLONAR EL EXCEL EXACTAMENTE ---
            if (i == 1)
            {
                // Replicamos el comportamiento del Excel en la fila 1: 
                // x0 se queda anclado y solo actualizamos x1.
                x1 = ci;
            }
            else
            {
                // A partir de la fila 2, retomamos el arrastre normal del Excel.
                x0 = last_ci;
                x1 = ci;
            }

            // Guardamos el Ci actual para usarlo en el cálculo de error de la próxima vuelta
            last_ci = ci;
        }
    }

    public void PuntoFijo(string funcionG, double x0, double tol, int maxIter, DataGridView dgv)
    {
        // Configuración exacta a tu Excel
        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "i");
        dgv.Columns.Add("Ci", "ci");
        dgv.Columns.Add("GCi", "g(ci)");
        dgv.Columns.Add("Er", "Er%");
        dgv.Columns.Add("Decision", "Decisión");

        dgv.Rows.Clear();

        double ci = x0;
        double last_ci = 0;
        double errorRelativo = 0;

        // Fíjate que el ciclo ahora empieza en i = 0
        for (int i = 0; i <= maxIter; i++)
        {
            // En Punto Fijo evaluamos g(ci)
            double g_ci = EvaluarFuncion(funcionG, ci);
            string decision = "Continuar";

            // Cálculo de Error a partir de la iteración 1 (la segunda fila)
            if (i > 0)
            {
                // OJO: Le quitamos el "* 100" para que calce con tu Excel
                errorRelativo = Math.Abs((ci - last_ci) / ci);

                if (errorRelativo < tol)
                {
                    decision = "Finalizar";
                }
            }

            // Agregamos la fila a la tabla
            int n = dgv.Rows.Add();
            dgv.Rows[n].Cells[0].Value = i;
            dgv.Rows[n].Cells[1].Value = ci.ToString("0.########");
            dgv.Rows[n].Cells[2].Value = g_ci.ToString("0.########");

            // En i=0 dejamos el error en blanco. Usamos "G8" para permitir notación científica (E-05)
            dgv.Rows[n].Cells[3].Value = (i == 0) ? "" : errorRelativo.ToString("G8");
            dgv.Rows[n].Cells[4].Value = (i == 0) ? "" : decision;

            if (decision == "Finalizar") break;

            // Actualizamos los valores para la siguiente vuelta
            last_ci = ci;
            ci = g_ci;
        }
    }

}