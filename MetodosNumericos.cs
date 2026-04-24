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

    public string Muller(string funcion, double x0, double x1, double x2, double tol, DataGridView dgv)
    {
        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "i");
        dgv.Columns.Add("Xi", "Xi");
        dgv.Columns.Add("Xi1", "Xi+1");
        dgv.Columns.Add("hi", "hi");
        dgv.Columns.Add("hi1", "hi+1");
        dgv.Columns.Add("fXi", "f(Xi)");
        dgv.Columns.Add("fXi1", "f(Xi+1)");
        dgv.Columns.Add("Di", "δi");
        dgv.Columns.Add("Di1", "δi+1");
        dgv.Columns.Add("A", "a");
        dgv.Columns.Add("B", "b");
        dgv.Columns.Add("C", "c");
        dgv.Columns.Add("BPlus", "b+√");
        dgv.Columns.Add("BMinus", "b-√");
        dgv.Columns.Add("Er", "Error %");
        dgv.Columns.Add("Decision", "Continuar");

        dgv.Rows.Clear();

        double last_x0 = 0, errorRelativo = 100;
        int i = 0;

        while (i < 500)
        {
            double h0 = x1 - x0;
            double h1 = x2 - x1;

            double fx0 = EvaluarFuncion(funcion, x0);
            double fx1 = EvaluarFuncion(funcion, x1);
            double fx2 = EvaluarFuncion(funcion, x2); // fx2 es igual a "c"

            double d0 = (fx1 - fx0) / h0;
            double d1 = (fx2 - fx1) / h1;

            double a = (d1 - d0) / (h1 + h0);
            double b = a * h1 + d1;
            double c = fx2;

            // Math.Sqrt calcula la raíz cuadrada de (b^2 - 4ac)
            double rad = Math.Sqrt(b * b - 4 * a * c);
            double b_plus = b + rad;
            double b_minus = b - rad;

            // El error en el Excel se calcula usando la variación del punto x0 respecto a la fila anterior
            if (i > 0) errorRelativo = Math.Abs((last_x0 - x0) / x0) * 100;

            string decision = (i > 0 && errorRelativo < tol) ? "Finalizar" : "Continuar";

            // Mandamos los 16 datos a la tabla (usando "G8" para que ponga la notación científica como tu Excel)
            dgv.Rows.Add(
                i,
                x0.ToString("G8"), x1.ToString("G8"),
                h0.ToString("G8"), h1.ToString("G8"),
                fx0.ToString("G8"), fx1.ToString("G8"),
                d0.ToString("G8"), d1.ToString("G8"),
                a.ToString("G8"), b.ToString("G8"), c.ToString("G8"),
                b_plus.ToString("G8"), b_minus.ToString("G8"),
                (i == 0) ? "-" : errorRelativo.ToString("G8"),
                (i == 0) ? "-" : decision
            );

            if (decision == "Finalizar") break;

            // Lógica de Müller: Elegir el denominador más grande (en valor absoluto) para evitar dividir por cero
            double denom = Math.Abs(b_plus) > Math.Abs(b_minus) ? b_plus : b_minus;

            // Calculamos el nuevo punto x3
            double x3 = x2 + (-2 * c) / denom;

            // Recorremos los valores para la siguiente vuelta
            last_x0 = x0;
            x0 = x1;
            x1 = x2;
            x2 = x3;

            i++;
        }

        // Devolvemos la raíz final al Label
        return x2.ToString("0.########");
    }

    public string Bairstow(double[] a, double r0, double s0, double tol, DataGridView dgv)
    {
        int n = a.Length - 1; // El grado es la cantidad de números menos 1

        if (n < 3) return "Error: El polinomio debe ser al menos de grado 3 (4 coeficientes).";

        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "Iter");

        // Magia: Creamos las columnas b y c de forma automática según el tamaño
        for (int i = n; i >= 0; i--) dgv.Columns.Add($"b{i}", $"b{i}");
        for (int i = n; i >= 1; i--) dgv.Columns.Add($"c{i}", $"c{i}");

        dgv.Columns.Add("Dr", "Δr"); dgv.Columns.Add("Ds", "Δs");
        dgv.Columns.Add("r", "r"); dgv.Columns.Add("s", "s");
        dgv.Columns.Add("ErrR", "Er(r)%"); dgv.Columns.Add("ErrS", "Er(s)%");
        dgv.Columns.Add("X1", "X1"); dgv.Columns.Add("X2", "X2");
        dgv.Columns.Add("Cond", "Condición");

        dgv.Rows.Clear();

        double r = r0, s = s0;
        double errorR = 100, errorS = 100;
        int iter = 0;
        // 1. AGREGA ESTA LÍNEA AQUÍ AFUERA:
        string x1_str = "", x2_str = "";

        double[] b = new double[n + 1];
        double[] c = new double[n + 1];

        while (iter < 500)
        {
            // División sintética 1
            b[n] = a[n];
            b[n - 1] = a[n - 1] + r * b[n];
            for (int i = n - 2; i >= 0; i--) b[i] = a[i] + r * b[i + 1] + s * b[i + 2];

            // División sintética 2
            c[n] = b[n];
            c[n - 1] = b[n - 1] + r * c[n];
            for (int i = n - 2; i >= 1; i--) c[i] = b[i] + r * c[i + 1] + s * c[i + 2];

            // Cramer
            double D = (c[2] * c[2]) - (c[1] * c[3]);
            double dr = 0, ds = 0;

            if (D != 0)
            {
                dr = (-b[1] * c[2] + b[0] * c[3]) / D;
                ds = (-b[0] * c[2] + b[1] * c[1]) / D;
            }

            double r_nuevo = r + dr;
            double s_nuevo = s + ds;

            if (iter > 0)
            {
                errorR = Math.Abs(dr / r_nuevo) * 100;
                errorS = Math.Abs(ds / s_nuevo) * 100;
            }

            // Chicharronera para sacar las raíces
            double raiz_interna = (r_nuevo * r_nuevo) + (4 * s_nuevo);
            x1_str = ""; x2_str = "";

            if (raiz_interna >= 0)
            {
                double raiz_calc = Math.Sqrt(raiz_interna);
                x1_str = ((r_nuevo + raiz_calc) / 2).ToString("F8");
                x2_str = ((r_nuevo - raiz_calc) / 2).ToString("F8");
            }
            else
            {
                double real = r_nuevo / 2;
                double imaginaria = Math.Sqrt(Math.Abs(raiz_interna)) / 2;
                x1_str = $"{real:F8} + {imaginaria:F8}i";
                x2_str = $"{real:F8} - {imaginaria:F8}i";
            }

            string condicion = (iter > 0 && errorR < tol && errorS < tol) ? "Finalizar" : "Continuar";

            // Armamos la fila dinámicamente juntando todos los pedazos
            System.Collections.Generic.List<object> fila = new System.Collections.Generic.List<object>();
            fila.Add(iter);
            for (int i = n; i >= 0; i--) fila.Add(b[i].ToString("F8"));
            for (int i = n; i >= 1; i--) fila.Add(c[i].ToString("F8"));
            fila.Add(dr.ToString("F8")); fila.Add(ds.ToString("F8"));
            fila.Add(r_nuevo.ToString("F8")); fila.Add(s_nuevo.ToString("F8"));
            fila.Add(iter == 0 ? "-" : errorR.ToString("F8"));
            fila.Add(iter == 0 ? "-" : errorS.ToString("F8"));
            fila.Add(x1_str); fila.Add(x2_str);
            fila.Add(iter == 0 ? "-" : condicion);

            dgv.Rows.Add(fila.ToArray());

            if (condicion == "Finalizar") break;

            r = r_nuevo; s = s_nuevo;
            iter++;
        }
        return $"X1: {x1_str}  |  X2: {x2_str}";
    }
}
//Confirmación de los cambios