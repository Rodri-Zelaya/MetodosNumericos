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
            return double.IsNaN(resultado) ? 0 : resultado;
        }
        catch { return 0; }
    }

    // Nueva función para calcular la derivada automáticamente
    public double CalcularDerivada(string expresion, double x)
    {
        try
        {
            // Sintaxis mXparser: der( función, respecto a x, en el punto x )
            string consultaDerivada = $"der({expresion}, x, {x.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
            Expression e = new Expression(consultaDerivada);
            double resultado = e.calculate();
            return double.IsNaN(resultado) ? 0 : resultado;
        }
        catch { return 0; }
    }

    // Fíjate que agregamos "string funcion" a los parámetros
    public string Biseccion(string funcion, double a, double b, double tol, DataGridView dgv)
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
        double c = 0, c_ant, errorRelativo = 100, ea = 0;// Iniciamos error alto para que entre al ciclo
        int i = 1;

        while (errorRelativo > tol && i < 500) // Límite de seguridad de 500 para evitar infinitos
        {
            c_ant = c;
            c = (a + b) / 2;
            double fa = EvaluarFuncion(funcion, a);
            double fc = EvaluarFuncion(funcion, c);
            double fb = EvaluarFuncion(funcion, b);
            double prod = fa * fc;

            if (i > 1)
            {
                ea = Math.Abs(c - c_ant);
                errorRelativo = (ea / Math.Abs(c)) * 100;
            }

            // AHORA SÍ: 10 valores exactos para 10 columnas
            dgv.Rows.Add(i, a.ToString("F6"), b.ToString("F6"), c.ToString("F6"), fa.ToString("F8"), fc.ToString("F8"), fb.ToString("F8"), prod.ToString("F8"), (i == 1) ? "-" : ea.ToString("F6"), (i == 1) ? "-" : errorRelativo.ToString("F4") + "%");

            if (prod < 0) b = c; else a = c;
            i++;
        }

        // DEVOLVEMOS LA RAÍZ FINAL
        return c.ToString("0.########");
    }

    public string ReglaFalsa(string funcion, double a, double b, double tol, DataGridView dgv)
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
        double c = 0, c_ant, errorRelativo = 100;
        int i = 1;

        while (errorRelativo > tol && i < 500)
        {
            double fa = EvaluarFuncion(funcion, a);
            double fb = EvaluarFuncion(funcion, b);
            c_ant = c;
            c = b - ((fb * (a - b)) / (fa - fb));
            double fc = EvaluarFuncion(funcion, c);
            double prod = fa * fc;

            if (i > 1) errorRelativo = Math.Abs((c - c_ant) / c) * 100;

            // 9 valores para 9 columnas
            dgv.Rows.Add(i, a.ToString("F6"), b.ToString("F6"), fa.ToString("F8"), fb.ToString("F8"), c.ToString("F6"), fc.ToString("F8"), prod.ToString("F8"), (i == 1) ? "-" : errorRelativo.ToString("F4") + "%");

            if (prod < 0) b = c; else a = c;
            i++;
        }
        return c.ToString("0.########");
    }

    public string NewtonRaphson(string funcion, double x0, double tol, DataGridView dgv)
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
        double ci = x0, ci_ant = 0, errorRelativo = 100, ea = 0;
        int i = 1;

        while (errorRelativo > tol && i < 500)
        {
            double f = EvaluarFuncion(funcion, ci);
            double df = CalcularDerivada(funcion, ci);
            if (Math.Abs(df) < 1e-12) break;

            double ci_siguiente = ci - (f / df);

            if (i > 1)
            {
                ea = Math.Abs(ci - ci_ant);
                errorRelativo = (ea / Math.Abs(ci)) * 100;
            }

            // 7 valores para 7 columnas
            dgv.Rows.Add(i, ci.ToString("F8"), f.ToString("G8"), df.ToString("F8"), ci_siguiente.ToString("F8"), (i == 1) ? "-" : ea.ToString("F8"), (i == 1) ? "-" : errorRelativo.ToString("F6") + "%");

            ci_ant = ci;
            ci = ci_siguiente;
            i++;
        }
        return ci.ToString("0.########");
    }

    public string Secante(string funcion, double c0, double c1, double tol, DataGridView dgv)
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
        double ci = 0;
        double last_ci = 0, errorRelativo = 100;
        int i = 1;// Variable clave para calcular el error igual que el Excel

        while (errorRelativo > tol && i < 500)
        {
            double f0 = EvaluarFuncion(funcion, x0);
            double f1 = EvaluarFuncion(funcion, x1);
            ci = x1 - ((f1 * (x0 - x1)) / (f0 - f1));
            double f_ci = EvaluarFuncion(funcion, ci);

            if (i > 1) errorRelativo = Math.Abs((ci - last_ci) / ci) * 100;

            string decision = (errorRelativo < tol && i > 1) ? "Finalizar" : "Continuar";

            // 5 valores para 5 columnas
            dgv.Rows.Add(i, ci.ToString("F8"), EvaluarFuncion(funcion, ci).ToString("G8"), (i == 1) ? "-" : errorRelativo.ToString("F6") + "%", (i == 1) ? "" : decision);

            if (decision == "Finalizar") break;

            // Al terminar la vuelta, el valor actual pasa a ser el viejo, 
            // y el resultado nuevo pasa a ser el actual (Igualito al arrastre de Excel)
            x0 = x1;
            x1 = ci;

            last_ci = ci;
            i++;
        }
        return ci.ToString("0.########");
    }

    public string PuntoFijo(string funcionG, double x0, double tol, DataGridView dgv)
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
        double last_ci = 0, errorRelativo = 100;
        int i = 0;

        while (i < 500) // Usamos break interno para respetar tu formato de i=0
        {
            double g = EvaluarFuncion(funcionG, ci);
            if (i > 0) errorRelativo = Math.Abs((ci - last_ci) / ci);

            string decision = (errorRelativo < tol && i > 0) ? "Finalizar" : "Continuar";

            // 5 valores para 5 columnas
            dgv.Rows.Add(i, ci.ToString("F8"), g.ToString("F8"), (i == 0) ? "" : errorRelativo.ToString("G8"), (i == 0) ? "" : decision);

            if (i > 0 && errorRelativo < tol) break;

            last_ci = ci;
            ci = g;
            i++;
        }

        return ci.ToString("0.########");
    }
}
//Confirmación de los cambios