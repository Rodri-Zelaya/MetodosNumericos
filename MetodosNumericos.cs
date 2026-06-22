using System;
using System.Windows.Forms;
using org.mariuszgromada.math.mxparser;
using Excel = Microsoft.Office.Interop.Excel;

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

            // Si hay división por cero, devolverá NaN (Not a Number)
            return e.calculate();
        }
        catch { return 0; }
    }

    // 🛡️ EL TRADUCTOR UNIVERSAL (NUEVO) 🛡️
    // Convierte cualquier texto a double respetando el punto decimal, sin importar la PC
    public double ConvertirADouble(string numeroTexto)
    {
        if (string.IsNullOrWhiteSpace(numeroTexto)) return 0;
        string textoLimpio = numeroTexto.Replace(',', '.');
        return double.Parse(textoLimpio, System.Globalization.CultureInfo.InvariantCulture);
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

        // 🎨 LLAMAMOS A LA BROCHA AQUÍ:
        FormatearTabla(dgv);

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

            // 🛡️ AQUÍ PEGAS EL ESCUDO (Revisando la variable 'fxr')
            if (double.IsNaN(fa) || double.IsInfinity(fa) ||
                double.IsNaN(fc) || double.IsInfinity(fc) ||
                double.IsNaN(fb) || double.IsInfinity(fb))
            {
                MessageBox.Show("¡El método colapsó en la iteración " + i + "!\n\nEl punto chocó con una asíntota o vacío matemático.", "Colapso Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Divergió (NaN)";
            }

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

        // 🚨 EL ESCUDO DE SEGURIDAD FINAL POR LÍMITE DE ITERACIONES
        if (i >= 500)
        {
            MessageBox.Show("Se alcanzó el límite de (500 iteraciones). El método no pudo converger a la tolerancia deseada.", "Límite Alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return "No convergió";
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

        // 🎨 LLAMAMOS A LA BROCHA AQUÍ:
        FormatearTabla(dgv);

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

            // 🛡️ AQUÍ PEGAS EL ESCUDO (Revisando la variable 'fxr')
            if (double.IsNaN(fa) || double.IsInfinity(fa) ||
                double.IsNaN(fb) || double.IsInfinity(fb) ||
                double.IsNaN(fc) || double.IsInfinity(fc))
            {
                MessageBox.Show("¡El método colapsó en la iteración " + i + "!\n\nEl punto chocó con una asíntota o vacío matemático.", "Colapso Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Divergió (NaN)";
            }

            double prod = fa * fc;

            if (i > 1) errorRelativo = Math.Abs((c - c_ant) / c) * 100;

            // 9 valores para 9 columnas
            dgv.Rows.Add(i, a.ToString("F6"), b.ToString("F6"), fa.ToString("F8"), fb.ToString("F8"), c.ToString("F6"), fc.ToString("F8"), prod.ToString("F8"), (i == 1) ? "-" : errorRelativo.ToString("F4") + "%");

            if (prod < 0) b = c; else a = c;
            i++;
        }

        // 🚨 EL ESCUDO DE SEGURIDAD FINAL POR LÍMITE DE ITERACIONES
        if (i >= 500)
        {
            MessageBox.Show("Se alcanzó el límite de (500 iteraciones). El método no pudo converger a la tolerancia deseada.", "Límite Alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return "No convergió";
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

        // 🎨 LLAMAMOS A LA BROCHA AQUÍ:
        FormatearTabla(dgv);

        dgv.Rows.Clear();
        double ci = x0, ci_ant = 0, errorRelativo = 100, ea = 0;
        int i = 1;

        while (errorRelativo > tol && i < 500)
        {
            double f = EvaluarFuncion(funcion, ci);
            double df = CalcularDerivada(funcion, ci);
            if (Math.Abs(df) < 1e-12)
            {
                MessageBox.Show("¡La derivada se hizo cero en la iteración " + i + "!\n\nEsto genera una tangente horizontal. El método falló.", "Falla de Newton", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "Falla (Derivada 0)";
            }

            double ci_siguiente = ci - (f / df);

            // 🛡️ TU ESCUDO (¡Excelente posición!)
            if (double.IsNaN(ci_siguiente) || double.IsInfinity(ci_siguiente))
            {
                // Ajustamos el texto para que sea más preciso matemáticamente
                MessageBox.Show("¡El método colapsó en la iteración " + i + "!\n\nEl cálculo intentó resolver una raíz imaginaria, logaritmo inválido o divergencia al infinito.", "Colapso Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Divergió (NaN)";
            }

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
        // 🚨 CORRECCIÓN 2: El escudo anti-ciclos infinitos por si llega a 500
        if (i >= 500)
        {
            MessageBox.Show("Se alcanzó el límite (500 iteraciones). El método osciló y no pudo converger.", "Límite Alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return "No convergió";
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
        
        // 🎨 LLAMAMOS A LA BROCHA AQUÍ:
        FormatearTabla(dgv);

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

            // 🛡️ AQUÍ PEGAS EL ESCUDO (Revisando la variable 'x2')
            if (double.IsNaN(f0) || double.IsInfinity(f0) ||
                double.IsNaN(f1) || double.IsInfinity(f1) ||
                double.IsNaN(f_ci) || double.IsInfinity(f_ci))
            {
                MessageBox.Show("¡El método colapsó en la iteración " + i + "!\n\nSe formó una recta horizontal causando división por cero.", "Colapso Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Divergió (NaN)";
            }

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
        // 🚨 EL ESCUDO DE SEGURIDAD FINAL POR LÍMITE DE ITERACIONES
        if (i >= 500)
        {
            MessageBox.Show("Se alcanzó el límite de (500 iteraciones). El método no pudo converger a la tolerancia deseada.", "Límite Alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return "No convergió";
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

        // 🎨 LLAMAMOS A LA BROCHA AQUÍ:
        FormatearTabla(dgv);

        dgv.Rows.Clear();

        double ci = x0;
        double last_ci = 0, errorRelativo = 100;
        int i = 0;

        while (i < 500) // Usamos break interno para respetar tu formato de i=0
        {
            double g = EvaluarFuncion(funcionG, ci);

            // 🛡️ AQUÍ PEGAS EL ESCUDO (Revisando la variable 'g')
            if (double.IsNaN(g) || double.IsInfinity(g))
            {
                MessageBox.Show("¡El método colapsó en la iteración " + i + "!\n\nEl cálculo generó un error matemático (raíz imaginaria o división por cero).", "Colapso Matemático", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Divergió (NaN)";
            }

            if (i > 0) errorRelativo = Math.Abs((ci - last_ci) / ci)*100;

            string decision = (errorRelativo < tol && i > 0) ? "Finalizar" : "Continuar";

            // 5 valores para 5 columnas
            dgv.Rows.Add(i, ci.ToString("F8"), g.ToString("F8"), (i == 0) ? "" : errorRelativo.ToString("G8"), (i == 0) ? "" : decision);

            if (i > 0 && errorRelativo < tol) break;

            last_ci = ci;
            ci = g;
            i++;
        }
        // 🚨 EL ESCUDO DE SEGURIDAD FINAL POR LÍMITE DE ITERACIONES
        if (i >= 500)
        {
            MessageBox.Show("Se alcanzó el límite de (500 iteraciones). El método no pudo converger a la tolerancia deseada.", "Límite Alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return "No convergió";
        }

        return ci.ToString("0.########");
    }

    public string Muller(double[] a, double x0, double x1, double x2, double tol, DataGridView dgv)
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

        // 🎨 LLAMAMOS A LA BROCHA AQUÍ:
        FormatearTabla(dgv);

        dgv.Rows.Clear();

        double last_x0 = 0, errorRelativo = 100;
        int i = 0;
        int n = a.Length - 1; // Grado del polinomio

        while (i < 500)
        {
            double h0 = x1 - x0;
            double h1 = x2 - x1;

            // 💡 EVALUACIÓN DIRECTA DEL POLINOMIO (Sin métodos extra)
            double fx0 = a[n]; for (int j = n - 1; j >= 0; j--) fx0 = fx0 * x0 + a[j];
            double fx1 = a[n]; for (int j = n - 1; j >= 0; j--) fx1 = fx1 * x1 + a[j];
            double fx2 = a[n]; for (int j = n - 1; j >= 0; j--) fx2 = fx2 * x2 + a[j]; // fx2 es igual a "c"

            double d0 = (fx1 - fx0) / h0;
            double d1 = (fx2 - fx1) / h1;

            double a_coef = (d1 - d0) / (h1 + h0); // Le cambié el nombre a 'a_coef' para no confundir con el arreglo 'a'
            double b_coef = a_coef * h1 + d1;
            double c_coef = fx2;

            double rad = Math.Sqrt(b_coef * b_coef - 4 * a_coef * c_coef);
            double b_plus = b_coef + rad;
            double b_minus = b_coef - rad;

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
                a_coef.ToString("G8"), b_coef.ToString("G8"), c_coef.ToString("G8"),
                b_plus.ToString("G8"), b_minus.ToString("G8"),
                (i == 0) ? "-" : errorRelativo.ToString("G8"),
                (i == 0) ? "-" : decision
            );

            if (decision == "Finalizar") break;

            // Lógica de Müller: Elegir el denominador más grande (en valor absoluto) para evitar dividir por cero
            double denom = Math.Abs(b_plus) > Math.Abs(b_minus) ? b_plus : b_minus;

            // Calculamos el nuevo punto x3
            double x3 = x2 + (-2 * c_coef) / denom;

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

    public string Bairstow(double[] a, double tol, DataGridView dgv, out double r0_out, out double s0_out)
    {
        int n = a.Length - 1;
        r0_out = 0; s0_out = 0;

        if (n < 3) return "Error: El polinomio debe ser al menos de grado 3.";

        // --- LÓGICA DEL PROFE (Basada en tu script de Python) ---
        // Por defecto usar Pequeñas a menos que a[2] sea 0 
        if (a[2] != 0)
        {
            // Rama de Raíces Pequeñas
            r0_out = a[1] / a[2];
            s0_out = a[0] / a[2];
        }
        else
        {
            // Rama de Raíces Muy Grandes
            r0_out = a[n - 1] / a[n];
            s0_out = a[n - 2] / a[n];
        }
        // --------------------------------------------------------

        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "Iter");
        for (int i = n; i >= 0; i--) dgv.Columns.Add($"b{i}", $"b{i}");
        for (int i = n; i >= 1; i--) dgv.Columns.Add($"c{i}", $"c{i}");

        dgv.Columns.Add("Dr", "Δr"); dgv.Columns.Add("Ds", "Δs");
        dgv.Columns.Add("r", "r"); dgv.Columns.Add("s", "s");
        dgv.Columns.Add("ErrR", "Er(r)%"); dgv.Columns.Add("ErrS", "Er(s)%");
        dgv.Columns.Add("X1", "X1"); dgv.Columns.Add("X2", "X2");
        dgv.Columns.Add("Cond", "Condición");

        // 🎨 LLAMAMOS A LA BROCHA AQUÍ:
        FormatearTabla(dgv);

        dgv.Rows.Clear();

        // AQUÍ ESTABAN LOS ERRORES. Ya quedó limpio con una sola declaración:
        double r = r0_out, s = s0_out;
        double errorR = 100, errorS = 100;
        int iter = 0;

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
                // Nota: Aquí estamos usando el error real matemático, sin el "hack" de clonar el Excel.
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

        // --- MAGIA FINAL: CALCULAMOS LAS RAÍCES RESTANTES ---
        string extras = "";

        if (n == 3)
        {
            double x3 = -b[2] / b[3];
            extras = $"  |  X3: {x3:F8}";
        }
        else if (n == 4)
        {
            double a_cuad = b[4], b_cuad = b[3], c_cuad = b[2];
            double disc = (b_cuad * b_cuad) - (4 * a_cuad * c_cuad);

            if (disc >= 0)
            {
                double x3 = (-b_cuad + Math.Sqrt(disc)) / (2 * a_cuad);
                double x4 = (-b_cuad - Math.Sqrt(disc)) / (2 * a_cuad);
                extras = $"  |  X3: {x3:F8}  |  X4: {x4:F8}";
            }
            else
            {
                double real = -b_cuad / (2 * a_cuad);
                double imag = Math.Sqrt(Math.Abs(disc)) / (2 * a_cuad);
                extras = $"  |  X3: {real:F8} + {imag:F8}i  |  X4: {real:F8} - {imag:F8}i";
            }
        }

        return $"X1: {x1_str}  |  X2: {x2_str}" + extras;
    }

    public string HornerNewton(double[] a, double r0, double tol, DataGridView dgv)
    {
        int n = a.Length - 1; // El grado del polinomio

        if (n < 1) return "Error: El polinomio debe ser al menos de grado 1.";

        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "Iteración");

        // Creamos las columnas b y c de forma automática y dinámica
        for (int i = n; i >= 0; i--) dgv.Columns.Add($"b{i}", $"b{i}");
        for (int i = n; i >= 1; i--) dgv.Columns.Add($"c{i}", $"c{i}");

        dgv.Columns.Add("r", "r");
        dgv.Columns.Add("Er", "Er");

        // 🎨 LLAMAMOS A LA BROCHA AQUÍ:
        FormatearTabla(dgv);

        dgv.Rows.Clear();

        double r = r0;
        double errorRelativo = 100;
        int iter = 1;

        double[] b = new double[n + 1];
        double[] c = new double[n + 1];

        while (iter < 500)
        {
            // Primera división sintética (Genera las "b")
            // b0 termina siendo el valor de f(r)
            b[n] = a[n];
            for (int i = n - 1; i >= 0; i--)
            {
                b[i] = a[i] + r * b[i + 1];
            }

            // Segunda división sintética (Genera las "c")
            // c1 termina siendo el valor de la derivada f'(r)
            c[n] = b[n];
            for (int i = n - 1; i >= 1; i--)
            {
                c[i] = b[i] + r * c[i + 1];
            }

            // Aplicamos Newton-Raphson para predecir la nueva raíz
            double r_nuevo = r - (b[0] / c[1]);

            // Calculamos el error basándonos en la iteración anterior
            if (iter > 1)
            {
                errorRelativo = Math.Abs((r_nuevo - r) / r_nuevo) * 100;
            }

            // Armamos la fila juntando todo como si fuera legos
            System.Collections.Generic.List<object> fila = new System.Collections.Generic.List<object>();
            fila.Add(iter);
            for (int i = n; i >= 0; i--) fila.Add(b[i].ToString("G8"));
            for (int i = n; i >= 1; i--) fila.Add(c[i].ToString("G8"));
            fila.Add(r_nuevo.ToString("F8"));

            // A la primera iteración le ponemos vacío en el error, y a las demás su porcentaje
            if (iter == 1)
                fila.Add("");
            else
                fila.Add(errorRelativo.ToString("F4") + "%");

            dgv.Rows.Add(fila.ToArray());

            // Si llegamos a la tolerancia, nos salimos
            if (iter > 1 && errorRelativo < tol) break;

            r = r_nuevo;
            iter++;
        }

        return r.ToString("0.########");
    }

    private double EvaluarNVar(string funcion, string[] variables, double[] valores)
    {
        org.mariuszgromada.math.mxparser.Expression exp = new org.mariuszgromada.math.mxparser.Expression(funcion);
        for (int i = 0; i < variables.Length; i++)
        {
            exp.addArguments(new org.mariuszgromada.math.mxparser.Argument(variables[i], valores[i]));
        }
        return exp.calculate();
    }

    private double[,] InvertirMatriz(double[,] matriz)
    {
        int n = matriz.GetLength(0);
        double[,] result = new double[n, n];
        double[,] a = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                a[i, j] = matriz[i, j];
                result[i, j] = i == j ? 1.0 : 0.0;
            }
        }

        for (int i = 0; i < n; i++)
        {
            double pivot = a[i, i];
            if (Math.Abs(pivot) < 1e-12) throw new Exception("Matriz singular, no se puede invertir.");

            for (int j = 0; j < n; j++)
            {
                a[i, j] /= pivot;
                result[i, j] /= pivot;
            }
            for (int k = 0; k < n; k++)
            {
                if (k != i)
                {
                    double factor = a[k, i];
                    for (int j = 0; j < n; j++)
                    {
                        a[k, j] -= factor * a[i, j];
                        result[k, j] -= factor * result[i, j];
                    }
                }
            }
        }
        return result;
    }

    public void NewtonRaphsonGeneral(string[] funciones, double[] X0, double tol, DataGridView dgv)
    {
        int N = funciones.Length;
        // Asignamos variables: si es 2 usa x,y. Si es 3 usa x,y,z.
        string[] vars = new string[N];
        if (N == 2) { vars[0] = "x"; vars[1] = "y"; }
        else if (N == 3) { vars[0] = "x"; vars[1] = "y"; vars[2] = "z"; }
        else { for (int i = 0; i < N; i++) vars[i] = "x" + (i + 1); }

        dgv.Columns.Clear();
        dgv.Columns.Add("X", "X");
        for (int i = 0; i < N; i++) dgv.Columns.Add($"J{i}", $"J(X)_{i + 1}");
        for (int i = 0; i < N; i++) dgv.Columns.Add($"Jinv{i}", $"[J(X)]^-1_{i + 1}");
        dgv.Columns.Add("F", "F(X)");
        dgv.Columns.Add("Delta", "[J(X)]^-1 * F(X)");
        dgv.Columns.Add("Er", "Er");

        // 🎨 LLAMAMOS A LA BROCHA AQUÍ:
        FormatearTabla(dgv);

        dgv.Rows.Clear();

        double[] X = (double[])X0.Clone();
        double[] Delta = new double[N];
        double[] X_next = new double[N];
        double[] Er = new double[N];
        int iter = 0;

        // Colores para los bloques igual que tu Excel
        System.Drawing.Color[] colores = {
            System.Drawing.Color.LightGoldenrodYellow, System.Drawing.Color.LightGray,
            System.Drawing.Color.LightSteelBlue, System.Drawing.Color.LightSalmon,
            System.Drawing.Color.PaleGoldenrod, System.Drawing.Color.DarkSeaGreen
        };

        while (iter < 100)
        {
            double[,] J = new double[N, N];
            double[] F = new double[N];
            double h = 0.0001;

            // 1. Evaluar Funciones y Matriz Jacobiana
            for (int i = 0; i < N; i++)
            {
                F[i] = EvaluarNVar(funciones[i], vars, X);
                for (int j = 0; j < N; j++)
                {
                    double[] X_temp = (double[])X.Clone();
                    X_temp[j] += h;
                    double f_plus = EvaluarNVar(funciones[i], vars, X_temp);
                    X_temp[j] -= 2 * h;
                    double f_minus = EvaluarNVar(funciones[i], vars, X_temp);
                    J[i, j] = (f_plus - f_minus) / (2 * h);
                }
            }

            // 2. Invertir Jacobiano y Calcular Deltas
            double[,] Jinv = InvertirMatriz(J);
            double errorMax = 0;

            for (int i = 0; i < N; i++)
            {
                Delta[i] = 0;
                for (int k = 0; k < N; k++) Delta[i] += Jinv[i, k] * F[k];
                X_next[i] = X[i] - Delta[i];

                // La fórmula del error según tu Excel
                Er[i] = Math.Abs(Delta[i] / X_next[i]) * 100;
                if (Er[i] > errorMax) errorMax = Er[i];
            }

            // 3. Pintar el bloque de N filas en el DataGridView
            System.Drawing.Color colorBloque = colores[iter % colores.Length];
            for (int r = 0; r < N; r++)
            {
                System.Collections.Generic.List<object> fila = new System.Collections.Generic.List<object>();
                fila.Add(X[r].ToString("G8"));

                for (int c = 0; c < N; c++) fila.Add(J[r, c].ToString("G8"));
                for (int c = 0; c < N; c++) fila.Add(Jinv[r, c].ToString("G8"));

                fila.Add(F[r].ToString("G8"));
                fila.Add(Delta[r].ToString("G8"));
                fila.Add(iter == 0 ? "" : Er[r].ToString("F4") + "%");

                int index = dgv.Rows.Add(fila.ToArray());
                dgv.Rows[index].DefaultCellStyle.BackColor = colorBloque;
            }

            if (iter > 0 && errorMax < tol) break;

            X = (double[])X_next.Clone();
            iter++;
        }
    }


    public void GaussSeidel(double[,] A, double[] b, double[] X0, double tol, int maxIter, DataGridView dgv)
    {
        int n = b.Length;

        // 🛡️ REGLA MATEMÁTICA: Ceros en la diagonal principal
        for (int i = 0; i < n; i++)
        {
            if (Math.Abs(A[i, i]) < 1e-12)
            {
                throw new Exception($"El elemento en la diagonal A[{i + 1},{i + 1}] es cero. Gauss-Seidel requiere dividir entre la diagonal. Reordena las filas de tu sistema.");
            }
        }

        // Preparar columnas del DataGridView al estilo de tus Excels
        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "Iter");

        // Columnas para Valores Iniciales (Viejos)
        for (int i = 0; i < n; i++) dgv.Columns.Add($"x{i + 1}_old", $"x{i + 1} (Inicial)");

        // Columnas para Valores Nuevos
        for (int i = 0; i < n; i++) dgv.Columns.Add($"x{i + 1}_new", $"x{i + 1} (Nuevo)");

        // Columnas para Errores Porcentuales
        for (int i = 0; i < n; i++) dgv.Columns.Add($"E{i + 1}", $"Error x{i + 1} (%)");

        // 🚀 Múltiples columnas de Decisión (Una por variable)
        for (int i = 0; i < n; i++) dgv.Columns.Add($"Dec{i + 1}", $"Decisión x{i + 1}");

        double[] X_viejo = new double[n];
        double[] X_nuevo = new double[n];

        // Copiamos los valores iniciales a ambos vectores
        Array.Copy(X0, X_viejo, n);
        Array.Copy(X0, X_nuevo, n); // ⚡ ¡Vital para que Gauss-Seidel pueda arrancar!

        for (int iter = 1; iter <= maxIter; iter++)
        {
            // Fila para agregar al DGV
            List<string> filaDatos = new List<string> { iter.ToString() };

            // Banderas y arreglos para las decisiones
            bool todosCumplen = true;
            string[] decisiones = new string[n];

            // 1. Guardar valores viejos para la tabla
            for (int i = 0; i < n; i++) filaDatos.Add(X_viejo[i].ToString("F8"));

            // 2. Ejecutar la fórmula de GAUSS-SEIDEL (Usando X_nuevo)
            for (int i = 0; i < n; i++)
            {
                double suma = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        // 🚀 LA DIFERENCIA CON JACOBI: Aquí usamos X_nuevo para usar datos frescos
                        suma += A[i, j] * X_nuevo[j];
                    }
                }
                X_nuevo[i] = (b[i] - suma) / A[i, i];
                filaDatos.Add(X_nuevo[i].ToString("F8"));
            }

            // 3. Calcular Errores y Tomar Decisiones por separado
            for (int i = 0; i < n; i++)
            {
                if (iter == 1)
                {
                    // Iteración 1 no tiene error previo
                    filaDatos.Add("-");
                    decisiones[i] = "Continuar";
                    todosCumplen = false; // Obligamos a que siga iterando
                }
                else
                {
                    double errorVariable = 0;
                    if (X_nuevo[i] != 0)
                    {
                        errorVariable = Math.Abs((X_nuevo[i] - X_viejo[i]) / X_nuevo[i]);
                        // NOTA: Lo dejé como lo mandaste, imprimiendo el decimal directo con el símbolo %
                        filaDatos.Add(errorVariable.ToString("F8") + "%");
                    }
                    else
                    {
                        filaDatos.Add("0%");
                    }

                    // Tomar la decisión para ESTA variable en específico comparando con tu Tolerancia
                    if (errorVariable <= tol)
                    {
                        decisiones[i] = "Finalizar";
                    }
                    else
                    {
                        decisiones[i] = "Continuar";
                        todosCumplen = false; // Con UNO que falle, el bucle general continúa
                    }
                }
            }

            // 4. Agregar las decisiones al final de la fila
            for (int i = 0; i < n; i++) filaDatos.Add(decisiones[i]);

            // Imprimir la fila en pantalla
            dgv.Rows.Add(filaDatos.ToArray());

            // Actualizamos el vector viejo para la SIGUIENTE iteración
            Array.Copy(X_nuevo, X_viejo, n);

            // 5. 🛑 Condición de parada (El Freno de Mano)
            if (iter > 1 && todosCumplen)
            {
                break; // Todas las variables dijeron "Finalizar", ¡cortamos el bucle!
            }
        }
    }
    public void Jacobi(double[,] A, double[] b, double[] X0, double tol, int maxIter, DataGridView dgv)
    {
        int n = b.Length;

        // 🛡️ REGLA MATEMÁTICA: Ceros en la diagonal principal
        for (int i = 0; i < n; i++)
        {
            if (Math.Abs(A[i, i]) < 1e-12)
            {
                throw new Exception($"El elemento en la diagonal A[{i + 1},{i + 1}] es cero. Jacobi requiere dividir entre la diagonal. Reordena las filas de tu sistema.");
            }
        }

        // Preparar columnas del DataGridView al estilo de tus Excels
        dgv.Columns.Clear();
        dgv.Columns.Add("Iter", "Iter");

        // Columnas para Valores Iniciales (Viejos)
        for (int i = 0; i < n; i++) dgv.Columns.Add($"x{i + 1}_old", $"x{i + 1} (Inicial)");

        // Columnas para Valores Nuevos
        for (int i = 0; i < n; i++) dgv.Columns.Add($"x{i + 1}_new", $"x{i + 1} (Nuevo)");

        // Columnas para Errores Porcentuales
        for (int i = 0; i < n; i++) dgv.Columns.Add($"E{i + 1}", $"Error x{i + 1} (%)");

        // 🚀 EL CAMBIO: Múltiples columnas de Decisión (Una por variable)
        for (int i = 0; i < n; i++) dgv.Columns.Add($"Dec{i + 1}", $"Decisión x{i + 1}");

        double[] X_viejo = new double[n];
        double[] X_nuevo = new double[n];

        // Copiamos los valores iniciales al vector viejo
        Array.Copy(X0, X_viejo, n);

        for (int iter = 1; iter <= maxIter; iter++)
        {
            // Fila para agregar al DGV
            List<string> filaDatos = new List<string> { iter.ToString() };

            // Banderas y arreglos para las decisiones
            bool todosCumplen = true;
            string[] decisiones = new string[n];

            // 1. Guardar valores viejos para la tabla
            for (int i = 0; i < n; i++) filaDatos.Add(X_viejo[i].ToString("F8"));

            // 2. Ejecutar la fórmula de JACOBI (Usando estrictamente X_viejo)
            for (int i = 0; i < n; i++)
            {
                double suma = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        suma += A[i, j] * X_viejo[j];
                    }
                }
                X_nuevo[i] = (b[i] - suma) / A[i, i];
                filaDatos.Add(X_nuevo[i].ToString("F8"));
            }

            // 3. Calcular Errores y Tomar Decisiones por separado
            for (int i = 0; i < n; i++)
            {
                if (iter == 1)
                {
                    // Iteración 1 no tiene error previo
                    filaDatos.Add("-");
                    decisiones[i] = "Continuar";
                    todosCumplen = false; // Obligamos a que siga iterando
                }
                else
                {
                    double errorVariable = 0;
                    if (X_nuevo[i] != 0)
                    {
                        errorVariable = Math.Abs((X_nuevo[i] - X_viejo[i]) / X_nuevo[i]);
                        filaDatos.Add(errorVariable.ToString("F8") + "%");
                    }
                    else
                    {
                        filaDatos.Add("0%");
                    }

                    // Tomar la decisión para ESTA variable en específico
                    if (errorVariable <= tol)
                    {
                        decisiones[i] = "Finalizar";
                    }
                    else
                    {
                        decisiones[i] = "Continuar";
                        todosCumplen = false; // Con UNO que falle, el bucle general continúa
                    }
                }
            }

            // 4. Agregar las decisiones al final de la fila
            for (int i = 0; i < n; i++) filaDatos.Add(decisiones[i]);

            // Imprimir la fila en pantalla
            dgv.Rows.Add(filaDatos.ToArray());

            // Actualizamos el vector viejo para la SIGUIENTE iteración
            Array.Copy(X_nuevo, X_viejo, n);

            // 5. 🛑 Condición de parada (El Freno de Mano)
            if (iter > 1 && todosCumplen)
            {
                break; // Todas las variables dijeron "Finalizar", ¡cortamos el bucle!
            }
        }
    }

    public void EliminacionGaussianaPasoAPaso(double[,] matrizA, double[] vectorB, DataGridView dgv)
    {
        int n = vectorB.Length;
        double[,] M = new double[n, n + 1];

        // 1. Construir la Matriz Aumentada [A | b]
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++) M[i, j] = matrizA[i, j];
            M[i, n] = vectorB[i];
        }

        // 2. Configurar DGV
        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.Columns.Add("Op", "Operación / Renglón");

        string[] letras = { "X", "Y", "Z", "T", "U", "V", "W" };
        for (int j = 0; j < n; j++)
        {
            string colName = (j < letras.Length) ? letras[j] : $"X{j + 1}";
            dgv.Columns.Add(colName, colName);
        }
        dgv.Columns.Add("b", "Términos Ind.");

        ImprimirPasoMatriz(M, "Matriz Inicial Aumentada [A | b]", dgv, Color.FromArgb(243, 244, 246));

        // 3. Proceso de Triangulación Superior (Hacer ceros solo abajo)
        for (int k = 0; k < n - 1; k++) // Llegamos hasta n-1 porque la última fila no elimina a nadie
        {
            // 🔥 PIVOTEO PARCIAL (Blindaje de computadora)
            int filaMayor = k;
            double maxVal = Math.Abs(M[k, k]);
            for (int i = k + 1; i < n; i++)
            {
                if (Math.Abs(M[i, k]) > maxVal)
                {
                    maxVal = Math.Abs(M[i, k]);
                    filaMayor = i;
                }
            }

            if (filaMayor != k)
            {
                for (int j = k; j <= n; j++)
                {
                    double temp = M[k, j];
                    M[k, j] = M[filaMayor, j];
                    M[filaMayor, j] = temp;
                }
                ImprimirPasoMatriz(M, $"Fila {k + 1} ↔ Fila {filaMayor + 1} (Pivoteo)", dgv, Color.FromArgb(254, 243, 199));
            }

            if (Math.Abs(M[k, k]) < 1e-10)
            {
                throw new Exception("El sistema no tiene solución única (El pivote se hizo cero).");
            }

            // 💥 ELIMINACIÓN HACIA ADELANTE
            bool huboCambios = false;
            List<string> operaciones = new List<string>();

            for (int i = k + 1; i < n; i++) // OJO: i arranca en k+1 (Solo filas de abajo)
            {
                double factor = M[i, k] / M[k, k]; // No hicimos el pivote 1, así que dividimos aquí
                if (Math.Abs(factor) > 1e-9)
                {
                    for (int j = k; j <= n; j++)
                    {
                        M[i, j] -= factor * M[k, j];
                    }
                    huboCambios = true;

                    string signo = (factor > 0) ? "-" : "+";
                    string operacion = $"F{i + 1} ➔ F{i + 1} {signo} {Math.Abs(factor).ToString("F4")}F{k + 1}";
                    operaciones.Add(operacion);
                }
            }

            if (huboCambios)
            {
                string textoOperaciones = string.Join("   ||   ", operaciones);
                ImprimirPasoMatriz(M, textoOperaciones, dgv, Color.FromArgb(209, 250, 229));
            }
        }

        // ====================================================================
        // 4. SUSTITUCIÓN HACIA ATRÁS (De la última variable a la primera)
        // ====================================================================
        double[] X = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double suma = 0;
            for (int j = i + 1; j < n; j++)
            {
                suma += M[i, j] * X[j]; // Sumar los términos ya conocidos
            }
            X[i] = (M[i, n] - suma) / M[i, i]; // Despejar la variable actual
        }

        // ====================================================================
        // 5. MOSTRAR RESULTADOS
        // ====================================================================
        int idxEspacio = dgv.Rows.Add();
        dgv.Rows[idxEspacio].Height = 15;

        int idxTituloFinal = dgv.Rows.Add();
        dgv.Rows[idxTituloFinal].Cells[0].Value = "🌟 SOLUCIÓN (Sustitución hacia atrás)";
        dgv.Rows[idxTituloFinal].DefaultCellStyle.BackColor = Color.FromArgb(79, 70, 229);
        dgv.Rows[idxTituloFinal].DefaultCellStyle.ForeColor = Color.White;
        dgv.Rows[idxTituloFinal].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

        int idxResultados = dgv.Rows.Add();
        dgv.Rows[idxResultados].Cells[0].Value = "Valores Exactos ➔";
        dgv.Rows[idxResultados].DefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
        dgv.Rows[idxResultados].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

        for (int i = 0; i < n; i++)
        {
            dgv.Rows[idxResultados].Cells[i + 1].Value = X[i].ToString("F4");
            dgv.Rows[idxResultados].Cells[i + 1].Style.ForeColor = Color.FromArgb(220, 38, 38);
            dgv.Rows[idxResultados].Cells[i + 1].Style.Font = new Font("Consolas", 14, FontStyle.Bold);
        }
    }

    // 🚀 MÉTODO PRINCIPAL DE FACTORIZACIÓN LU
    public void FactorizacionLUPasoAPaso(double[,] matrizA, double[] vectorB, DataGridView dgv)
    {
        int n = vectorB.Length;
        double[,] L = new double[n, n];
        double[,] U = new double[n, n];

        // 1. Inicializar L con 1s en la diagonal y U con ceros
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                L[i, j] = (i == j) ? 1.0 : 0.0;
                U[i, j] = 0.0;
            }
        }

        // 2. Algoritmo de Descomposición (Doolittle)
        for (int i = 0; i < n; i++)
        {
            // Calcular fila de U
            for (int k = i; k < n; k++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++) sum += (L[i, j] * U[j, k]);
                U[i, k] = matrizA[i, k] - sum;
            }

            // Calcular columna de L
            for (int k = i + 1; k < n; k++)
            {
                if (Math.Abs(U[i, i]) < 1e-10) throw new Exception("Pivote cero detectado. Se requiere permutación (LU con pivoteo no implementado en forma simple).");
                double sum = 0;
                for (int j = 0; j < i; j++) sum += (L[k, j] * U[j, i]);
                L[k, i] = (matrizA[k, i] - sum) / U[i, i];
            }
        }

        // Preparar DGV
        dgv.Columns.Clear(); dgv.Rows.Clear();
        dgv.Columns.Add("Op", "Etapa Analítica");
        for (int j = 0; j < n; j++) dgv.Columns.Add($"C{j}", $"Col {j + 1}");

        // Imprimir Matriz L
        int rL = dgv.Rows.Add("📉 MATRIZ INFERIOR (L)");
        dgv.Rows[rL].DefaultCellStyle.BackColor = Color.FromArgb(254, 243, 199); dgv.Rows[rL].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        for (int i = 0; i < n; i++) { int r = dgv.Rows.Add($"L - Fila {i + 1}"); for (int j = 0; j < n; j++) dgv.Rows[r].Cells[j + 1].Value = L[i, j].ToString("F4"); }
        dgv.Rows.Add();

        // Imprimir Matriz U
        int rU = dgv.Rows.Add("📈 MATRIZ SUPERIOR (U)");
        dgv.Rows[rU].DefaultCellStyle.BackColor = Color.FromArgb(219, 234, 254); dgv.Rows[rU].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        for (int i = 0; i < n; i++) { int r = dgv.Rows.Add($"U - Fila {i + 1}"); for (int j = 0; j < n; j++) dgv.Rows[r].Cells[j + 1].Value = U[i, j].ToString("F4"); }
        dgv.Rows.Add();

        // 3. Resolver L*y = B (Sustitución hacia adelante)
        double[] y = new double[n];
        for (int i = 0; i < n; i++)
        {
            double suma = 0;
            for (int j = 0; j < i; j++) suma += L[i, j] * y[j];
            y[i] = vectorB[i] - suma;
        }

        int ry = dgv.Rows.Add("⚙️ VECTOR INTERMEDIO (y) desde L*y = b");
        dgv.Rows[ry].DefaultCellStyle.BackColor = Color.FromArgb(209, 250, 229);
        int ryVals = dgv.Rows.Add("Valores de y ➔");
        for (int i = 0; i < n; i++) dgv.Rows[ryVals].Cells[i + 1].Value = y[i].ToString("F4");
        dgv.Rows.Add();

        // 4. Resolver U*x = y (Sustitución hacia atrás)
        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double suma = 0;
            for (int j = i + 1; j < n; j++) suma += U[i, j] * x[j];
            x[i] = (y[i] - suma) / U[i, i];
        }

        // Imprimir Solución Final
        int idxFinal = dgv.Rows.Add("🌟 SOLUCIÓN FINAL (x) desde U*x = y");
        dgv.Rows[idxFinal].DefaultCellStyle.BackColor = Color.FromArgb(79, 70, 229);
        dgv.Rows[idxFinal].DefaultCellStyle.ForeColor = Color.White;
        dgv.Rows[idxFinal].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

        int resRow = dgv.Rows.Add("Valores de X ➔");
        for (int i = 0; i < n; i++)
        {
            dgv.Rows[resRow].Cells[i + 1].Value = x[i].ToString("F4");
            dgv.Rows[resRow].Cells[i + 1].Style.ForeColor = Color.FromArgb(220, 38, 38);
            dgv.Rows[resRow].Cells[i + 1].Style.Font = new Font("Consolas", 14, FontStyle.Bold);
        }
    }

    public void GaussJordanPasoAPaso(double[,] matrizA, double[] vectorB, DataGridView dgv)
    {
        int n = vectorB.Length;
        double[,] M = new double[n, n + 1];

        // 1. Construir la Matriz Aumentada inicial [A | b]
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++) M[i, j] = matrizA[i, j];
            M[i, n] = vectorB[i];
        }

        // 2. Configurar las columnas del DataGridView dinámicamente
        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.Columns.Add("Op", "Operación / Renglón");

        string[] letras = { "X", "Y", "Z", "T", "U", "V", "W" };
        for (int j = 0; j < n; j++)
        {
            string colName = (j < letras.Length) ? letras[j] : $"X{j + 1}";
            dgv.Columns.Add(colName, colName);
        }
        dgv.Columns.Add("b", "Términos Ind.");

        // 🚀 REGISTRAR MATRIZ INICIAL
        ImprimirPasoMatriz(M, "Matriz Inicial Aumentada [A | b]", dgv, Color.FromArgb(243, 244, 246));

        // 3. Proceso de Operaciones Elementales
        for (int k = 0; k < n; k++)
        {
            // 🔥 PIVOTEO PARCIAL
            int filaMayor = k;
            double maxVal = Math.Abs(M[k, k]);
            for (int i = k + 1; i < n; i++)
            {
                if (Math.Abs(M[i, k]) > maxVal)
                {
                    maxVal = Math.Abs(M[i, k]);
                    filaMayor = i;
                }
            }

            if (filaMayor != k)
            {
                for (int j = k; j <= n; j++)
                {
                    double temp = M[k, j];
                    M[k, j] = M[filaMayor, j];
                    M[filaMayor, j] = temp;
                }
                ImprimirPasoMatriz(M, $"Fila {k + 1} ↔ Fila {filaMayor + 1} (Pivoteo)", dgv, Color.FromArgb(254, 243, 199)); // Color ámbar
            }

            if (Math.Abs(M[k, k]) < 1e-10)
            {
                throw new Exception("El sistema no tiene solución única.");
            }

            // 🌟 HACER EL PIVOTE DE LA DIAGONAL = 1
            double pivote = M[k, k];
            if (Math.Abs(pivote - 1.0) > 1e-9)
            {
                for (int j = k; j <= n; j++) M[k, j] /= pivote;
                ImprimirPasoMatriz(M, $"Fila {k + 1} ➔ Fila {k + 1} / {pivote.ToString("F4")}", dgv, Color.FromArgb(219, 234, 254)); // Color azul claro
            }

            // 💥 ELIMINACIÓN (Hacer ceros arriba y abajo del pivote)
            bool huboCambios = false;
            List<string> operaciones = new List<string>(); // Para guardar las fórmulas paso a paso

            for (int i = 0; i < n; i++)
            {
                if (i != k)
                {
                    double factor = M[i, k];
                    if (Math.Abs(factor) > 1e-9)
                    {
                        for (int j = k; j <= n; j++)
                        {
                            M[i, j] -= factor * M[k, j];
                        }
                        huboCambios = true;

                        // 🚀 TRADUCCIÓN EXACTA AL LENGUAJE DEL CUADERNO
                        string signo = (factor > 0) ? "-" : "+";
                        string operacion = $"F{i + 1} ➔ F{i + 1} {signo} {Math.Abs(factor).ToString("F4")}F{k + 1}";
                        operaciones.Add(operacion);
                    }
                }
            }

            if (huboCambios)
            {
                // Unir todas las operaciones de ese paso en una sola línea clara
                string textoOperaciones = string.Join("   ||   ", operaciones);
                ImprimirPasoMatriz(M, textoOperaciones, dgv, Color.FromArgb(209, 250, 229)); // Color verde claro
            }
        }
        // ====================================================================
        // 4. AISLAR Y MOSTRAR LOS RESULTADOS FINALES EN EL DATAGRIDVIEW
        // ====================================================================

        // Añadir un espacio en blanco para separar
        int idxEspacio = dgv.Rows.Add();
        dgv.Rows[idxEspacio].Height = 15;

        // Fila de Título
        int idxTituloFinal = dgv.Rows.Add();
        dgv.Rows[idxTituloFinal].Cells[0].Value = "🌟 SOLUCIÓN FINAL DEL SISTEMA";
        dgv.Rows[idxTituloFinal].DefaultCellStyle.BackColor = Color.FromArgb(79, 70, 229); // Morado Tuani
        dgv.Rows[idxTituloFinal].DefaultCellStyle.ForeColor = Color.White;
        dgv.Rows[idxTituloFinal].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

        // Fila con los valores exactos
        int idxResultados = dgv.Rows.Add();
        dgv.Rows[idxResultados].Cells[0].Value = "Valores Exactos ➔";
        dgv.Rows[idxResultados].DefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
        dgv.Rows[idxResultados].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

        // Rellenar las celdas correspondientes a cada variable (X, Y, Z, T...)
        for (int i = 0; i < n; i++)
        {
            dgv.Rows[idxResultados].Cells[i + 1].Value = M[i, n].ToString("F4");
            dgv.Rows[idxResultados].Cells[i + 1].Style.ForeColor = Color.FromArgb(220, 38, 38); // Letra Roja
            dgv.Rows[idxResultados].Cells[i + 1].Style.Font = new Font("Consolas", 14, FontStyle.Bold); // Letra grande
        }
    }

    // Función auxiliar para plasmar el bloque de la matriz en la tabla
    private void ImprimirPasoMatriz(double[,] M, string descripcion, DataGridView dgv, Color colorEncabezado)
    {
        int n = M.GetLength(0);

        // 1. Insertar fila de título/operación
        int idxTitulo = dgv.Rows.Add();
        dgv.Rows[idxTitulo].Cells[0].Value = descripcion;
        dgv.Rows[idxTitulo].DefaultCellStyle.BackColor = colorEncabezado;
        dgv.Rows[idxTitulo].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

        // 2. Insertar las filas con los datos numéricos de la matriz en ese instante
        for (int i = 0; i < n; i++)
        {
            int idxFila = dgv.Rows.Add();
            dgv.Rows[idxFila].Cells[0].Value = $"Renglón {i + 1}";

            for (int j = 0; j <= n; j++)
            {
                // Formateo a 4 decimales para que se vea limpio
                dgv.Rows[idxFila].Cells[j + 1].Value = M[i, j].ToString("F4");
            }
        }

        // 3. Dejar una fila vacía como separador estético
        dgv.Rows.Add();
    }

    // 🛠️ Función Auxiliar para Calcular Determinantes (Rápida y blindada)
    private double CalcularDeterminante(double[,] matriz)
    {
        int n = matriz.GetLength(0);
        double[,] M = (double[,])matriz.Clone();
        double det = 1.0;

        for (int i = 0; i < n; i++)
        {
            int pivot = i;
            for (int j = i + 1; j < n; j++)
                if (Math.Abs(M[j, i]) > Math.Abs(M[pivot, i])) pivot = j;

            if (Math.Abs(M[pivot, i]) < 1e-10) return 0; // Determinante cero

            if (pivot != i)
            {
                for (int j = i; j < n; j++) { double tmp = M[i, j]; M[i, j] = M[pivot, j]; M[pivot, j] = tmp; }
                det *= -1; // Al cambiar filas, el determinante cambia de signo
            }

            det *= M[i, i];
            for (int j = i + 1; j < n; j++)
            {
                double factor = M[j, i] / M[i, i];
                for (int k = i; k < n; k++) M[j, k] -= factor * M[i, k];
            }
        }
        return det;
    }

    // 🚀 MÉTODO PRINCIPAL DE CRAMER
    public void ReglaCramerPasoAPaso(double[,] matrizA, double[] vectorB, DataGridView dgv)
    {
        int n = vectorB.Length;
        string[] letras = { "X", "Y", "Z", "T", "U", "V", "W" };

        dgv.Columns.Clear(); dgv.Rows.Clear();
        dgv.Columns.Add("Op", "Parámetro / Matriz");
        for (int j = 0; j < n; j++) dgv.Columns.Add($"C{j}", $"Col {j + 1}");

        // 1. Calcular el Determinante Principal (Δ)
        double detA = CalcularDeterminante(matrizA);

        int idxTitulo = dgv.Rows.Add($"Determinante Principal (Δ) = {detA.ToString("F6")}");
        dgv.Rows[idxTitulo].DefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55);
        dgv.Rows[idxTitulo].DefaultCellStyle.ForeColor = Color.White;
        dgv.Rows[idxTitulo].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

        if (Math.Abs(detA) < 1e-10) throw new Exception("El determinante principal es 0. El sistema no tiene solución única (Regla de Cramer no aplicable).");

        double[] soluciones = new double[n];

        // 2. Calcular Determinantes Modificados (Δx, Δy, Δz...)
        for (int k = 0; k < n; k++)
        {
            double[,] matrizModificada = (double[,])matrizA.Clone();

            // Sustituir la columna k por el vector B
            for (int i = 0; i < n; i++) matrizModificada[i, k] = vectorB[i];

            double detMod = CalcularDeterminante(matrizModificada);
            soluciones[k] = detMod / detA;

            string varName = (k < letras.Length) ? letras[k] : $"X{k + 1}";

            // Título de la variable
            int idxVar = dgv.Rows.Add();
            dgv.Rows[idxVar].Cells[0].Value = $"Matriz Modificada para [{varName}]";
            dgv.Rows[idxVar].DefaultCellStyle.BackColor = Color.FromArgb(219, 234, 254);
            dgv.Rows[idxVar].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Imprimir la matriz modificada
            for (int i = 0; i < n; i++)
            {
                int row = dgv.Rows.Add($"Renglón {i + 1}");
                for (int j = 0; j < n; j++) dgv.Rows[row].Cells[j + 1].Value = matrizModificada[i, j].ToString("F4");
            }

            // Imprimir el cálculo de la variable
            int idxCalc = dgv.Rows.Add();
            dgv.Rows[idxCalc].Cells[0].Value = $"Δ{varName} = {detMod.ToString("F4")}  ➔  {varName} = Δ{varName}/Δ = {soluciones[k].ToString("F6")}";
            dgv.Rows[idxCalc].DefaultCellStyle.BackColor = Color.FromArgb(209, 250, 229);
            dgv.Rows[idxCalc].DefaultCellStyle.Font = new Font("Consolas", 11, FontStyle.Bold);
            dgv.Rows.Add(); // Espacio
        }

        // 3. Imprimir Resultado Final
        int idxFinal = dgv.Rows.Add("🌟 SOLUCIÓN EXACTA DE CRAMER");
        dgv.Rows[idxFinal].DefaultCellStyle.BackColor = Color.FromArgb(79, 70, 229);
        dgv.Rows[idxFinal].DefaultCellStyle.ForeColor = Color.White;
        dgv.Rows[idxFinal].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

        int resRow = dgv.Rows.Add("Valores ➔");
        for (int i = 0; i < n; i++)
        {
            dgv.Rows[resRow].Cells[i + 1].Value = soluciones[i].ToString("F4");
            dgv.Rows[resRow].Cells[i + 1].Style.ForeColor = Color.FromArgb(220, 38, 38);
            dgv.Rows[resRow].Cells[i + 1].Style.Font = new Font("Consolas", 14, FontStyle.Bold);
        }
    }

    public void MatrizInversaPasoAPaso(double[,] matrizA, double[] vectorB, DataGridView dgv)
    {
        int n = vectorB.Length;
        double[,] M = new double[n, 2 * n]; // Matriz aumentada gigante [A | I]

        // 1. Construir la Matriz Aumentada inicial [A | I]
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++) M[i, j] = matrizA[i, j]; // Lado izquierdo (A)
            for (int j = 0; j < n; j++) M[i, j + n] = (i == j) ? 1.0 : 0.0; // Lado derecho (Identidad)
        }

        // 2. Configurar las columnas del DataGridView dinámicamente
        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.Columns.Add("Op", "Operación / Renglón");

        string[] letras = { "X", "Y", "Z", "T", "U", "V", "W" };

        // Columnas para la matriz original (lado izquierdo)
        for (int j = 0; j < n; j++)
        {
            string colName = (j < letras.Length) ? letras[j] : $"X{j + 1}";
            dgv.Columns.Add(colName, colName);
        }

        // Columnas para la matriz identidad/inversa (lado derecho)
        for (int j = 0; j < n; j++)
        {
            dgv.Columns.Add($"I{j + 1}", $"Inv {j + 1}");
        }

        // 🚀 REGISTRAR MATRIZ INICIAL
        ImprimirPasoMatrizInversa(M, "Matriz Inicial Aumentada [A | I]", dgv, Color.FromArgb(243, 244, 246));

        // 3. Proceso de Operaciones Elementales
        for (int k = 0; k < n; k++)
        {
            // 🔥 PIVOTEO PARCIAL
            int filaMayor = k;
            double maxVal = Math.Abs(M[k, k]);
            for (int i = k + 1; i < n; i++)
            {
                if (Math.Abs(M[i, k]) > maxVal)
                {
                    maxVal = Math.Abs(M[i, k]);
                    filaMayor = i;
                }
            }

            if (filaMayor != k)
            {
                for (int j = k; j < 2 * n; j++) // Ojo aquí: recorre el doble de columnas
                {
                    double temp = M[k, j];
                    M[k, j] = M[filaMayor, j];
                    M[filaMayor, j] = temp;
                }
                ImprimirPasoMatrizInversa(M, $"Fila {k + 1} ↔ Fila {filaMayor + 1} (Pivoteo)", dgv, Color.FromArgb(254, 243, 199));
            }

            if (Math.Abs(M[k, k]) < 1e-10)
            {
                throw new Exception("La matriz es singular. No tiene inversa y por ende no se puede resolver por este método.");
            }

            // 🌟 HACER EL PIVOTE DE LA DIAGONAL = 1
            double pivote = M[k, k];
            if (Math.Abs(pivote - 1.0) > 1e-9)
            {
                for (int j = k; j < 2 * n; j++) M[k, j] /= pivote;
                ImprimirPasoMatrizInversa(M, $"Fila {k + 1} ➔ Fila {k + 1} / {pivote.ToString("F4")}", dgv, Color.FromArgb(219, 234, 254));
            }

            // 💥 ELIMINACIÓN (Hacer ceros arriba y abajo del pivote)
            bool huboCambios = false;
            List<string> operaciones = new List<string>();

            for (int i = 0; i < n; i++)
            {
                if (i != k)
                {
                    double factor = M[i, k];
                    if (Math.Abs(factor) > 1e-9)
                    {
                        for (int j = k; j < 2 * n; j++)
                        {
                            M[i, j] -= factor * M[k, j];
                        }
                        huboCambios = true;

                        string signo = (factor > 0) ? "-" : "+";
                        string operacion = $"F{i + 1} ➔ F{i + 1} {signo} {Math.Abs(factor).ToString("F4")}F{k + 1}";
                        operaciones.Add(operacion);
                    }
                }
            }

            if (huboCambios)
            {
                string textoOperaciones = string.Join("   ||   ", operaciones);
                ImprimirPasoMatrizInversa(M, textoOperaciones, dgv, Color.FromArgb(209, 250, 229));
            }
        }

        // ====================================================================
        // 4. MULTIPLICAR LA MATRIZ INVERSA POR EL VECTOR B: X = A^(-1) * B
        // ====================================================================
        double[] resultados = new double[n];
        for (int i = 0; i < n; i++)
        {
            resultados[i] = 0;
            for (int j = 0; j < n; j++)
            {
                // La inversa quedó guardada en el lado derecho de la matriz (columnas de la 'n' a la '2n-1')
                resultados[i] += M[i, j + n] * vectorB[j];
            }
        }

        // Añadir un espacio en blanco
        int idxEspacio = dgv.Rows.Add();
        dgv.Rows[idxEspacio].Height = 15;

        // Fila de Título
        int idxTituloFinal = dgv.Rows.Add();
        dgv.Rows[idxTituloFinal].Cells[0].Value = "🌟 SOLUCIÓN (X = A⁻¹ * b)";
        dgv.Rows[idxTituloFinal].DefaultCellStyle.BackColor = Color.FromArgb(79, 70, 229);
        dgv.Rows[idxTituloFinal].DefaultCellStyle.ForeColor = Color.White;
        dgv.Rows[idxTituloFinal].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

        // Fila con los valores exactos
        int idxResultados = dgv.Rows.Add();
        dgv.Rows[idxResultados].Cells[0].Value = "Valores Exactos ➔";
        dgv.Rows[idxResultados].DefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
        dgv.Rows[idxResultados].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

        for (int i = 0; i < n; i++)
        {
            dgv.Rows[idxResultados].Cells[i + 1].Value = resultados[i].ToString("F4");
            dgv.Rows[idxResultados].Cells[i + 1].Style.ForeColor = Color.FromArgb(220, 38, 38);
            dgv.Rows[idxResultados].Cells[i + 1].Style.Font = new Font("Consolas", 14, FontStyle.Bold);
        }
    }

    // Método auxiliar exclusivo para imprimir matrices anchas [A | I]
    private void ImprimirPasoMatrizInversa(double[,] M, string descripcion, DataGridView dgv, Color colorEncabezado)
    {
        int n = M.GetLength(0);
        int totalColumnas = M.GetLength(1);

        int idxTitulo = dgv.Rows.Add();
        dgv.Rows[idxTitulo].Cells[0].Value = descripcion;
        dgv.Rows[idxTitulo].DefaultCellStyle.BackColor = colorEncabezado;
        dgv.Rows[idxTitulo].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

        for (int i = 0; i < n; i++)
        {
            int idxFila = dgv.Rows.Add();
            dgv.Rows[idxFila].Cells[0].Value = $"Renglón {i + 1}";

            for (int j = 0; j < totalColumnas; j++)
            {
                dgv.Rows[idxFila].Cells[j + 1].Value = M[i, j].ToString("F4");
            }
        }

        dgv.Rows.Add(); // Espacio separador
    }

    public void RegresionPolinomialCompleta(double[] X, double[] Y, int grado, DataGridView dgvSumas, DataGridView dgvCoefs, out double[] coeficientes, out string ecuacionFinal, out double r2, out double r)
    {
        int n = X.Length;
        int m = grado + 1;

        // 1. Crear las matrices para el sistema de ecuaciones normales (Sumatorias)
        double[,] matrizA = new double[m, m];
        double[] vectorB = new double[m];

        // Configurar columnas de la tabla de sumatorias dinámicamente
        dgvSumas.Columns.Clear();
        dgvSumas.Rows.Clear();
        dgvSumas.Columns.Add("Punto", "Punto");
        dgvSumas.Columns.Add("X", "X");
        dgvSumas.Columns.Add("Y", "Y");
        for (int k = 2; k <= 2 * grado; k++) dgvSumas.Columns.Add($"X{k}", $"X^{k}");
        for (int k = 1; k <= grado; k++) dgvSumas.Columns.Add($"X{k}Y", $"X^{k}·Y");

        // Llenar datos fila por fila
        for (int i = 0; i < n; i++)
        {
            int rIdx = dgvSumas.Rows.Add();
            dgvSumas.Rows[rIdx].Cells[0].Value = $"P{i + 1}";
            dgvSumas.Rows[rIdx].Cells[1].Value = X[i].ToString("F4");
            dgvSumas.Rows[rIdx].Cells[2].Value = Y[i].ToString("F4");

            int colIdx = 3;
            for (int k = 2; k <= 2 * grado; k++) dgvSumas.Rows[rIdx].Cells[colIdx++].Value = Math.Pow(X[i], k).ToString("F4");
            for (int k = 1; k <= grado; k++) dgvSumas.Rows[rIdx].Cells[colIdx++].Value = (Math.Pow(X[i], k) * Y[i]).ToString("F4");
        }

        // Calcular las sumatorias para armar el sistema
        double[] sumX = new double[2 * grado + 1];
        double[] sumXY = new double[grado + 1];

        for (int i = 0; i < n; i++)
        {
            for (int k = 0; k <= 2 * grado; k++) sumX[k] += Math.Pow(X[i], k);
            for (int k = 0; k <= grado; k++) sumXY[k] += Math.Pow(X[i], k) * Y[i];
        }

        // Inyectar Fila de Totales (∑) al DataGridView de sumatorias
        int totalIdx = dgvSumas.Rows.Add();
        dgvSumas.Rows[totalIdx].Cells[0].Value = "∑ TOTAL";
        dgvSumas.Rows[totalIdx].DefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
        dgvSumas.Rows[totalIdx].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        dgvSumas.Rows[totalIdx].Cells[1].Value = sumX[1].ToString("F4");
        dgvSumas.Rows[totalIdx].Cells[2].Value = sumXY[0].ToString("F4");

        int colSumIdx = 3;
        for (int k = 2; k <= 2 * grado; k++) dgvSumas.Rows[totalIdx].Cells[colSumIdx++].Value = sumX[k].ToString("F4");
        for (int k = 1; k <= grado; k++) dgvSumas.Rows[totalIdx].Cells[colSumIdx++].Value = sumXY[k].ToString("F4");

        // Armar matriz del sistema [A|b]
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < m; j++) matrizA[i, j] = sumX[i + j];
            vectorB[i] = sumXY[i];
        }

        // Resolver el sistema usando tu Gauss-Jordan blindado
        coeficientes = GaussJordanEcuacionesPuras(matrizA, vectorB);

        // 2. Llenar la tabla de coeficientes (b0, b1, b2...) estrictamente a F8
        dgvCoefs.Columns.Clear();
        dgvCoefs.Rows.Clear();
        dgvCoefs.Columns.Add("Coef", "Coeficiente");
        dgvCoefs.Columns.Add("Val", "Valor Preciso (F8)");
        for (int i = 0; i < coeficientes.Length; i++)
        {
            dgvCoefs.Rows.Add($"b{i}", coeficientes[i].ToString("F8"));
        }

        // 3. Construir la Ecuación Final SIN REDONDEOS (F8 parejo en ambos lados)
        List<string> terminos = new List<string>();
        terminos.Add(coeficientes[0].ToString("F8"));
        for (int i = 1; i < coeficientes.Length; i++)
        {
            double val = coeficientes[i];
            string signo = (val >= 0) ? "+" : "-";
            terminos.Add($"{signo} {Math.Abs(val).ToString("F8")}·x^{i}");
        }
        ecuacionFinal = "Y = " + string.Join(" ", terminos);

        // 4. Cálculo estricto de R² y del Coeficiente de Correlación r
        double sumaY = sumXY[0];
        double mediaY = sumaY / n;
        double St = 0, Sr = 0;

        for (int i = 0; i < n; i++)
        {
            St += Math.Pow(Y[i] - mediaY, i);

            // Evaluar el polinomio construido en el punto X[i]
            double yPredicho = 0;
            for (int j = 0; j < coeficientes.Length; j++) yPredicho += coeficientes[j] * Math.Pow(X[i], j);
            Sr += Math.Pow(Y[i] - yPredicho, 2);
        }

        r2 = (St - Sr) / St;
        if (r2 < 0) r2 = 0; // Control por si los datos son un caos absoluto
        r = Math.Sqrt(r2);  // Coeficiente de correlación pura r
    }

    // Función interna simplificada para no interferir con el DGV visual de tu otra pantalla
    private double[] GaussJordanEcuacionesPuras(double[,] matrizA, double[] vectorB)
    {
        int n = vectorB.Length; double[,] M = new double[n, n + 1];
        for (int i = 0; i < n; i++) { for (int j = 0; j < n; j++) M[i, j] = matrizA[i, j]; M[i, n] = vectorB[i]; }
        for (int k = 0; k < n; k++)
        {
            int maxRow = k; double maxVal = Math.Abs(M[k, k]);
            for (int i = k + 1; i < n; i++) if (Math.Abs(M[i, k]) > maxVal) { maxVal = Math.Abs(M[i, k]); maxRow = i; }
            if (maxRow != k) for (int j = k; j <= n; j++) { double t = M[k, j]; M[k, j] = M[maxRow, j]; M[maxRow, j] = t; }
            double piv = M[k, k];
            for (int j = k; j <= n; j++) M[k, j] /= piv;
            for (int i = 0; i < n; i++) if (i != k) { double fac = M[i, k]; for (int j = k; j <= n; j++) M[i, j] -= fac * M[k, j]; }
        }
        double[] res = new double[n]; for (int i = 0; i < n; i++) res[i] = M[i, n]; return res;
    }

    public void ExportarRegresionAExcelCompleto(DataGridView dgvSumas, DataGridView dgvCoefs)
    {
        Type excelType = Type.GetTypeFromProgID("Excel.Application");
        if (excelType == null) throw new Exception("Microsoft Excel no está instalado.");

        dynamic excelApp = Activator.CreateInstance(excelType);
        excelApp.Visible = true;
        dynamic workbook = excelApp.Workbooks.Add();
        dynamic sheet = workbook.ActiveSheet;
        sheet.Name = "Regresión Polinomial";

        int fila = 1;

        // 1. TÍTULO Y TABLA DE SUMATORIAS
        sheet.Cells[fila, 1].Value = "📊 TABLA DE CONTROL DE SUMATORIAS";
        sheet.Cells[fila, 1].Font.Bold = true;
        sheet.Cells[fila, 1].Font.Size = 12;
        fila += 2;

        for (int c = 1; c <= dgvSumas.Columns.Count; c++)
        {
            sheet.Cells[fila, c].Value = dgvSumas.Columns[c - 1].HeaderText;
            sheet.Cells[fila, c].Interior.Color = ColorTranslator.ToOle(Color.FromArgb(17, 24, 39));
            sheet.Cells[fila, c].Font.Color = ColorTranslator.ToOle(Color.White);
            sheet.Cells[fila, c].Font.Bold = true;
        }
        fila++;

        for (int i = 0; i < dgvSumas.Rows.Count; i++)
        {
            if (dgvSumas.Rows[i].IsNewRow) continue;
            for (int j = 0; j < dgvSumas.Columns.Count; j++)
                sheet.Cells[fila, j + 1].Value = dgvSumas.Rows[i].Cells[j].Value?.ToString() ?? "";
            fila++;
        }

        fila += 3; // Espacio separador

        // 2. TÍTULO Y TABLA DE RESULTADOS/MÉTRICAS
        sheet.Cells[fila, 1].Value = "📉 COEFICIENTES Y MODELO MATEMÁTICO";
        sheet.Cells[fila, 1].Font.Bold = true;
        sheet.Cells[fila, 1].Font.Size = 12;
        fila += 2;

        for (int c = 1; c <= dgvCoefs.Columns.Count; c++)
        {
            sheet.Cells[fila, c].Value = dgvCoefs.Columns[c - 1].HeaderText;
            sheet.Cells[fila, c].Interior.Color = ColorTranslator.ToOle(Color.FromArgb(55, 65, 81));
            sheet.Cells[fila, c].Font.Color = ColorTranslator.ToOle(Color.White);
            sheet.Cells[fila, c].Font.Bold = true;
        }
        fila++;

        for (int i = 0; i < dgvCoefs.Rows.Count; i++)
        {
            if (dgvCoefs.Rows[i].IsNewRow) continue;
            for (int j = 0; j < dgvCoefs.Columns.Count; j++)
            {
                sheet.Cells[fila, j + 1].Value = dgvCoefs.Rows[i].Cells[j].Value?.ToString() ?? "";
            }

            // Si la fila contiene la Ecuación o el R2, pintarla en Excel también
            string celdaIzquierda = dgvCoefs.Rows[i].Cells[0].Value?.ToString() ?? "";
            if (celdaIzquierda.Contains("Modelo"))
            {
                sheet.Range[sheet.Cells[fila, 1], sheet.Cells[fila, 2]].Interior.Color = ColorTranslator.ToOle(Color.FromArgb(16, 185, 129));
                sheet.Range[sheet.Cells[fila, 1], sheet.Cells[fila, 2]].Font.Color = ColorTranslator.ToOle(Color.White);
                sheet.Range[sheet.Cells[fila, 1], sheet.Cells[fila, 2]].Font.Bold = true;
            }
            else if (celdaIzquierda.Contains("Coef."))
            {
                sheet.Range[sheet.Cells[fila, 1], sheet.Cells[fila, 2]].Interior.Color = ColorTranslator.ToOle(Color.FromArgb(31, 41, 55));
                sheet.Range[sheet.Cells[fila, 1], sheet.Cells[fila, 2]].Font.Color = ColorTranslator.ToOle(Color.White);
                sheet.Range[sheet.Cells[fila, 1], sheet.Cells[fila, 2]].Font.Bold = true;
            }
            fila++;
        }

        excelApp.Columns.AutoFit();
    }

    public void NewtonDiferenciasDivididasPasoAPaso(double[] X, double[] Y, double? xEstimar, DataGridView dgv, out string ecuacionFinal, out int gradoReal, out double? yEstimado)
    {
        int n = X.Length;
        double[,] F = new double[n, n];

        // 1. Inicializar la primera columna con los valores de Y
        for (int i = 0; i < n; i++) F[i, 0] = Y[i];

        // 2. Calcular la tabla de diferencias divididas
        for (int j = 1; j < n; j++)
        {
            for (int i = 0; i < n - j; i++)
            {
                double denominador = X[i + j] - X[i];
                if (Math.Abs(denominador) < 1e-10) throw new Exception("Hay dos valores de X idénticos. No se puede dividir entre cero.");

                F[i, j] = (F[i + 1, j - 1] - F[i, j - 1]) / denominador;
            }
        }

        // 3. DETECTAR EL GRADO REAL DEL POLINOMIO (Buscar el último coeficiente != 0 en la primera fila)
        gradoReal = 0;
        for (int j = n - 1; j >= 0; j--)
        {
            if (Math.Abs(F[0, j]) > 1e-8) // Tolerancia para considerar cero real
            {
                gradoReal = j;
                break;
            }
        }

        // 4. Configurar las columnas del DataGridView visual
        dgv.Columns.Clear(); dgv.Rows.Clear();
        dgv.Columns.Add("Punto", "Punto");
        dgv.Columns.Add("X", "X");
        dgv.Columns.Add("Y", "Y (F[i,0])");
        for (int j = 1; j < n; j++) dgv.Columns.Add($"D{j}", $"Dif. Div. Orden {j}");

        // Rellenar el DataGridView con los números calculados
        for (int i = 0; i < n; i++)
        {
            int rIdx = dgv.Rows.Add();
            dgv.Rows[rIdx].Cells[0].Value = $"P{i + 1}";
            dgv.Rows[rIdx].Cells[1].Value = X[i].ToString("F4");

            for (int j = 0; j < n - i; j++)
            {
                dgv.Rows[rIdx].Cells[j + 2].Value = F[i, j].ToString("F4");
            }
        }

        // 5. CONSTRUIR EL POLINOMIO SÓLO HASTA EL GRADO REAL DETECTADO
        List<string> terminos = new List<string>();
        terminos.Add(F[0, 0].ToString("F4")); // El primer término a0

        for (int j = 1; j <= gradoReal; j++)
        {
            double coef = F[0, j];
            if (Math.Abs(coef) < 1e-8) continue; // Si es cero intermedio, se salta

            string signo = (coef > 0) ? "+" : "-";
            string constructorX = "";

            for (int k = 0; k < j; k++)
            {
                string signoX = (X[k] >= 0) ? "-" : "+";
                constructorX += $"(x {signoX} {Math.Abs(X[k]).ToString("F2")})";
            }

            terminos.Add($"{signo} {Math.Abs(coef).ToString("F4")}{constructorX}");
        }
        ecuacionFinal = "P(x) = " + string.Join(" ", terminos);

        // 6. ESTIMACIÓN OPTIONAL (Sólo si el usuario metió un valor de X)
        yEstimado = null;
        if (xEstimar.HasValue)
        {
            double xVal = xEstimar.Value;
            double suma = F[0, 0];
            double acumuladoX = 1.0;

            for (int j = 1; j <= gradoReal; j++)
            {
                acumuladoX *= (xVal - X[j - 1]);
                suma += F[0, j] * acumuladoX;
            }
            yEstimado = suma;
        }
    }
    public void LagrangePasoAPaso(double[] X, double[] Y, double? xEval, DataGridView dgv, out string ecuacionFinal, out double? yEstimado)
    {
        int n = X.Length;
        yEstimado = null;

        // 1. Configurar la tabla de salida dinámicamente
        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.Columns.Add("i", "i");
        dgv.Columns.Add("X", "X_i");
        dgv.Columns.Add("Y", "Y_i");

        // Solo mostramos L_i y el producto parcial si el usuario metió un X a evaluar
        if (xEval.HasValue)
        {
            dgv.Columns.Add("L", $"L_i({xEval.Value})");
            dgv.Columns.Add("Termino", $"Y_i · L_i");
        }

        List<string> terminosEcuacion = new List<string>();
        double sumaTotal = 0;

        // 2. Calcular los factores L_i y construir la fórmula
        for (int i = 0; i < n; i++)
        {
            List<string> numTerm = new List<string>();
            double denTotal = 1.0;
            double L_i = 1.0;

            for (int j = 0; j < n; j++)
            {
                if (i != j)
                {
                    // Construir el string del numerador (x - X_j)
                    string signoXj = X[j] >= 0 ? "-" : "+";
                    numTerm.Add($"(x {signoXj} {Math.Abs(X[j])})");

                    // Calcular el denominador real (X_i - X_j)
                    denTotal *= (X[i] - X[j]);

                    // Calcular el valor numérico de L_i si hay un X de estimación
                    if (xEval.HasValue)
                    {
                        L_i *= (xEval.Value - X[j]) / (X[i] - X[j]);
                    }
                }
            }

            // Ensamblar el bloque de texto de este término específico
            string numeradorStr = string.Join("", numTerm);
            string terminoEcuacion = $"{Y[i]} * [{numeradorStr} / {denTotal}]";
            terminosEcuacion.Add(terminoEcuacion);

            // Llenar la fila correspondiente en la tabla visual
            int rIdx = dgv.Rows.Add();
            dgv.Rows[rIdx].Cells[0].Value = i.ToString();
            dgv.Rows[rIdx].Cells[1].Value = X[i].ToString("F4");
            dgv.Rows[rIdx].Cells[2].Value = Y[i].ToString("F4");

            if (xEval.HasValue)
            {
                dgv.Rows[rIdx].Cells[3].Value = L_i.ToString("F6");
                double terminoActual = Y[i] * L_i;
                dgv.Rows[rIdx].Cells[4].Value = terminoActual.ToString("F6");
                sumaTotal += terminoActual;
            }
        }

        // 3. Unir todo con el formato de sumatoria de Lagrange
        ecuacionFinal = "P(x) = " + string.Join("  +  ", terminosEcuacion);
        if (xEval.HasValue) yEstimado = sumaTotal;
    }

    // 🚀 MOTOR DE DIFERENCIACIÓN FOCALIZADA
    public void DiferenciacionNumericaFocalizada(double[] X, double[] Y, double x0, double h, double? exacto, DataGridView dgv)
    {
        double xBack = Math.Round(x0 - h, 6);
        double xForw = Math.Round(x0 + h, 6);

        // Buscar los 3 puntos clave en la matriz de datos
        bool hasY0 = BuscarYExacto(x0, X, Y, out double y0);
        bool hasYBack = BuscarYExacto(xBack, X, Y, out double yBack);
        bool hasYForw = BuscarYExacto(xForw, X, Y, out double yForw);

        if (!hasY0)
            throw new Exception($"El punto central X₀ = {x0} no se encontró en la tabla de datos. Es obligatorio para derivar.");

        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.Columns.Add("Metodo", "Método Diferencial");
        dgv.Columns.Add("Sustitucion", "Sustitución en la Fórmula");
        dgv.Columns.Add("Resultado", "Aproximación");
        dgv.Columns.Add("Error", "Error Absoluto |E|");

        // 1. HACIA ADELANTE
        if (hasYForw)
        {
            double res = (yForw - y0) / h;
            string sust = $"f' ≈ [f({xForw}) - f({x0})] / h\n= [{yForw} - {y0}] / {h}";
            dgv.Rows.Add("Hacia Adelante", sust, res.ToString("F6"), EvaluarError(res, exacto));
        }
        else dgv.Rows.Add("Hacia Adelante", $"Falta f({xForw}) en la tabla", "---", "---");

        // 2. HACIA ATRÁS
        if (hasYBack)
        {
            double res = (y0 - yBack) / h;
            string sust = $"f' ≈ [f({x0}) - f({xBack})] / h\n= [{y0} - {yBack}] / {h}";
            dgv.Rows.Add("Hacia Atrás", sust, res.ToString("F6"), EvaluarError(res, exacto));
        }
        else dgv.Rows.Add("Hacia Atrás", $"Falta f({xBack}) en la tabla", "---", "---");

        // 3. CENTRAL (1ra Derivada)
        if (hasYForw && hasYBack)
        {
            double res = (yForw - yBack) / (2 * h);
            string sust = $"f' ≈ [f({xForw}) - f({xBack})] / 2h\n= [{yForw} - {yBack}] / {2 * h}";
            int idxCentral = dgv.Rows.Add("Central (Mejor Aproximación)", sust, res.ToString("F6"), EvaluarError(res, exacto));

            dgv.Rows[idxCentral].DefaultCellStyle.BackColor = Color.FromArgb(16, 185, 129); // Verde
            dgv.Rows[idxCentral].DefaultCellStyle.ForeColor = Color.White;
            dgv.Rows[idxCentral].DefaultCellStyle.Font = new Font("Consolas", 11, FontStyle.Bold);
        }
        else dgv.Rows.Add("Central (1ra Derivada)", $"Faltan puntos perimetrales", "---", "---");

        // 4. CENTRAL (2da Derivada / Aceleración)
        if (hasYForw && hasYBack)
        {
            double res2 = (yForw - 2 * y0 + yBack) / (h * h);
            string sust2 = $"f'' ≈ [f({xForw}) - 2f({x0}) + f({xBack})] / h²\n= [{yForw} - 2({y0}) + {yBack}] / {h * h}";
            int idxSegunda = dgv.Rows.Add("Segunda Derivada f''(X₀)", sust2, res2.ToString("F6"), "--- (Requiere Exacto f'')");

            dgv.Rows[idxSegunda].DefaultCellStyle.BackColor = Color.FromArgb(31, 41, 55); // Oscuro
            dgv.Rows[idxSegunda].DefaultCellStyle.ForeColor = Color.White;
        }
        else dgv.Rows.Add("Segunda Derivada f''(X₀)", $"Faltan puntos perimetrales", "---", "---");
    }

    private bool BuscarYExacto(double target, double[] X, double[] Y, out double yVal)
    {
        yVal = 0;
        for (int i = 0; i < X.Length; i++)
        {
            if (Math.Abs(X[i] - target) < 1e-5)
            {
                yVal = Y[i];
                return true;
            }
        }
        return false;
    }

    private string EvaluarError(double aproximado, double? exacto)
    {
        if (!exacto.HasValue) return "---";
        return Math.Abs(exacto.Value - aproximado).ToString("F6");
    }
    // 🎨 LA BROCHA MÁGICA PARA EL DISEÑO DE LAS TABLAS
    public void FormatearTabla(DataGridView dgv)
    {
        // Colores del Dashboard
        Color azulOscuro = Color.FromArgb(17, 24, 39);
        Color azulHover = Color.FromArgb(55, 65, 81);

        dgv.BackgroundColor = Color.White;
        dgv.BorderStyle = BorderStyle.None;
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgv.RowHeadersVisible = false;

        // Apagamos el estilo feo de Windows para los encabezados
        dgv.EnableHeadersVisualStyles = false;
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgv.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        dgv.ColumnHeadersHeight = 40;

        // Estilo de las filas
        dgv.DefaultCellStyle.SelectionBackColor = azulHover;
        dgv.DefaultCellStyle.SelectionForeColor = Color.White;
        dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 242, 255); // Azul hielo muy suave (reemplaza al verde)
    }

    // 🚀 TIENE QUE TENER EXACTAMENTE ESTOS 5 PARÁMETROS
    public void ExtrapolacionRichardson(double Dh, double Dh2, double n, double? exacto, DataGridView dgv)
    {
        // Cálculo dinámico del denominador usando el Orden 'n' (2^n - 1)
        double denominador = Math.Pow(2, n) - 1;

        // Fórmula Extrapolación de Richardson
        double ER = Dh2 + (Dh2 - Dh) / denominador;

        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.Columns.Add("Metrica", "Métrica / Parámetro");
        dgv.Columns.Add("Sustitucion", "Sustitución Analítica");
        dgv.Columns.Add("Valor", "Resultado F8");

        // 1. RESULTADO DE LA EXTRAPOLACIÓN
        string sustER = $"D(h/2) + [D(h/2) - D(h)] / (2^{n} - 1)\n= {Dh2} + [{Dh2} - {Dh}] / {denominador}";
        int idxER = dgv.Rows.Add("Extrapolación (ER)", sustER, ER.ToString("F8"));

        dgv.Rows[idxER].DefaultCellStyle.BackColor = Color.FromArgb(79, 70, 229); // Color Indigo
        dgv.Rows[idxER].DefaultCellStyle.ForeColor = Color.White;
        dgv.Rows[idxER].DefaultCellStyle.Font = new Font("Consolas", 11, FontStyle.Bold);

        // 2. ERROR APROXIMADO RELATIVO
        double ea = Math.Abs((ER - Dh2) / ER);
        string sustEa = $"|(Actual - Anterior) / Actual|\n= |({ER:F6} - {Dh2:F6}) / {ER:F6}|";
        dgv.Rows.Add("Error Aproximado |Ea|", sustEa, ea.ToString("F8"));

        // 3. ERROR REAL RELATIVO
        if (exacto.HasValue)
        {
            double et = Math.Abs((exacto.Value - ER) / exacto.Value);
            string sustEt = $"|(Real - ER) / Real|\n= |({exacto.Value} - {ER:F6}) / {exacto.Value}|";
            dgv.Rows.Add("Error Real Relativo |Er|", sustEt, et.ToString("F8"));
        }
        else
        {
            dgv.Rows.Add("Error Real Relativo |Er|", "---", "Requiere Valor Real (Exacto)");
        }
    }

    // VERSIÓN 1D: Se activa sola cuando le pasas Func<double, double>
    public void EjecutarTrapecio(Func<double, double> f, double a, double b, int n, DataGridView dgv)
    {
        if (n < 1) throw new Exception("El número de intervalos n debe ser >= 1.");

        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.Columns.Add("Punto", "xᵢ");
        dgv.Columns.Add("Evaluacion", "f(xᵢ)");
        dgv.Columns.Add("Operacion", "Peso × f(xᵢ)");

        double h = (b - a) / n;
        double sumaTotal = 0;

        for (int i = 0; i <= n; i++)
        {
            double x = a + i * h;
            double y = f(x);

            int peso = (i == 0 || i == n) ? 1 : 2;
            double valorParcial = peso * y;
            sumaTotal += valorParcial;

            dgv.Rows.Add($"x{i} = {x:F4}", $"f({x:F4}) = {y:F6}", $"[{peso}] × {y:F6} = {valorParcial:F6}");
        }

        double integral = (h / 2.0) * sumaTotal;
        int idxFinal = dgv.Rows.Add("🔮 INTEGRAL SIMPLE", $"h = {h:F4}  ➔  I = (h/2) · Σ", integral.ToString("F8"));
        AplicarEstiloGranTotal(dgv.Rows[idxFinal]);
    }

    // VERSIÓN 2D: Se activa sola cuando le pasas Func<double, double, double>
    public void EjecutarTrapecio(Func<double, double, double> f, double ax, double bx, int nx, double cy, double dy, int ny, DataGridView dgv)
    {
        if (nx < 1 || ny < 1) throw new Exception("Los intervalos nx y ny deben ser >= 1.");

        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.Columns.Add("Coordenada", "(xᵢ, yⱼ)");
        dgv.Columns.Add("Evaluacion", "f(x, y)");
        dgv.Columns.Add("Operacion", "Peso Wᵢⱼ × f(x, y)");

        double hx = (bx - ax) / nx;
        double hy = (dy - cy) / ny;
        double sumaTotal = 0;

        for (int i = 0; i <= nx; i++)
        {
            double x = ax + i * hx;
            int pesoX = (i == 0 || i == nx) ? 1 : 2;

            for (int j = 0; j <= ny; j++)
            {
                double y = cy + j * hy;
                int pesoY = (j == 0 || j == ny) ? 1 : 2;

                int pesoFinal = pesoX * pesoY;
                double evaluacion = f(x, y);
                double valorParcial = pesoFinal * evaluacion;
                sumaTotal += valorParcial;

                dgv.Rows.Add($"({x:F2}, {y:F2})", $"f = {evaluacion:F6}", $"[{pesoFinal}] × {evaluacion:F6} = {valorParcial:F6}");
            }
        }

        double integral = (hx * hy / 4.0) * sumaTotal;
        int idxFinal = dgv.Rows.Add("🔮 INTEGRAL DOBLE", $"hx = {hx:F4}, hy = {hy:F4}", integral.ToString("F8"));
        AplicarEstiloGranTotal(dgv.Rows[idxFinal]);
    }

    // =====================================================================
    // 2. HERRAMIENTAS DE DISEÑO Y EXPORTACIÓN
    // =====================================================================
    private void AplicarEstiloGranTotal(DataGridViewRow fila)
    {
        fila.DefaultCellStyle.BackColor = Color.FromArgb(14, 116, 144);
        fila.DefaultCellStyle.ForeColor = Color.White;
        fila.DefaultCellStyle.Font = new Font("Consolas", 12, FontStyle.Bold);
        fila.DefaultCellStyle.SelectionBackColor = Color.FromArgb(21, 94, 117);
        fila.DefaultCellStyle.SelectionForeColor = Color.White;
    }

    // =====================================================================
    // MOTOR DE INTEGRACIÓN: SIMPSON 1/3 (Exige 'n' PAR)
    // Fórmulas: (h/3) para 1D  |  (hx*hy/9) para 2D
    // Patrón de Pesos 1D: 1, 4, 2, 4, 2 ... 4, 1
    // =====================================================================
    public void EjecutarSimpson13(Func<double, double> f, double a, double b, int n, DataGridView dgv)
    {
        if (n < 2 || n % 2 != 0) throw new Exception("Para Simpson 1/3, el número de intervalos 'n' debe ser PAR (ej: 2, 4, 6).");

        dgv.Columns.Clear(); dgv.Rows.Clear();
        dgv.Columns.Add("Punto", "xᵢ"); dgv.Columns.Add("Evaluacion", "f(xᵢ)"); dgv.Columns.Add("Operacion", "Peso × f(xᵢ)");

        double h = (b - a) / n;
        double sumaTotal = 0;

        for (int i = 0; i <= n; i++)
        {
            double x = a + i * h;
            double y = f(x);

            // Lógica de pesos Simpson 1/3
            int peso = 1;
            if (i > 0 && i < n) peso = (i % 2 == 0) ? 2 : 4;

            double valorParcial = peso * y;
            sumaTotal += valorParcial;
            dgv.Rows.Add($"x{i} = {x:F4}", $"f({x:F4}) = {y:F6}", $"[{peso}] × {y:F6} = {valorParcial:F6}");
        }

        double integral = (h / 3.0) * sumaTotal;
        int idxFinal = dgv.Rows.Add("🔮 SIMPSON 1/3 (1D)", $"h = {h:F4}  ➔  I = (h/3) · Σ", integral.ToString("F8"));
        AplicarEstiloGranTotal(dgv.Rows[idxFinal]);
    }

    public void EjecutarSimpson13(Func<double, double, double> f, double ax, double bx, int nx, double cy, double dy, int ny, DataGridView dgv)
    {
        if (nx < 2 || nx % 2 != 0 || ny < 2 || ny % 2 != 0) throw new Exception("Para Simpson 1/3 en 2D, los intervalos 'nx' y 'ny' deben ser PARES.");

        dgv.Columns.Clear(); dgv.Rows.Clear();
        dgv.Columns.Add("Coordenada", "(xᵢ, yⱼ)"); dgv.Columns.Add("Evaluacion", "f(x, y)"); dgv.Columns.Add("Operacion", "Peso Wᵢⱼ × f(x, y)");

        double hx = (bx - ax) / nx;
        double hy = (dy - cy) / ny;
        double sumaTotal = 0;

        for (int i = 0; i <= nx; i++)
        {
            double x = ax + i * hx;
            int pesoX = 1; if (i > 0 && i < nx) pesoX = (i % 2 == 0) ? 2 : 4;

            for (int j = 0; j <= ny; j++)
            {
                double y = cy + j * hy;
                int pesoY = 1; if (j > 0 && j < ny) pesoY = (j % 2 == 0) ? 2 : 4;

                int pesoFinal = pesoX * pesoY; // Multiplicación de la matriz cruzada
                double evaluacion = f(x, y);
                double valorParcial = pesoFinal * evaluacion;
                sumaTotal += valorParcial;

                dgv.Rows.Add($"({x:F2}, {y:F2})", $"f = {evaluacion:F6}", $"[{pesoFinal}] × {evaluacion:F6} = {valorParcial:F6}");
            }
        }

        double integral = (hx * hy / 9.0) * sumaTotal;
        int idxFinal = dgv.Rows.Add("🔮 SIMPSON 1/3 (2D)", $"hx = {hx:F4}, hy = {hy:F4}", integral.ToString("F8"));
        AplicarEstiloGranTotal(dgv.Rows[idxFinal]);
    }

    // =====================================================================
    // MOTOR DE INTEGRACIÓN: SIMPSON 3/8 (Exige 'n' Múltiplo de 3)
    // Fórmulas: (3h/8) para 1D  |  (9*hx*hy/64) para 2D
    // Patrón de Pesos 1D: 1, 3, 3, 2, 3, 3, 2 ... 3, 3, 1
    // =====================================================================
    public void EjecutarSimpson38(Func<double, double> f, double a, double b, int n, DataGridView dgv)
    {
        if (n < 3 || n % 3 != 0) throw new Exception("Para Simpson 3/8, el número de intervalos 'n' debe ser MÚLTIPLO DE 3 (ej: 3, 6, 9).");

        dgv.Columns.Clear(); dgv.Rows.Clear();
        dgv.Columns.Add("Punto", "xᵢ"); dgv.Columns.Add("Evaluacion", "f(xᵢ)"); dgv.Columns.Add("Operacion", "Peso × f(xᵢ)");

        double h = (b - a) / n;
        double sumaTotal = 0;

        for (int i = 0; i <= n; i++)
        {
            double x = a + i * h;
            double y = f(x);

            // Lógica de pesos Simpson 3/8
            int peso = 1;
            if (i > 0 && i < n) peso = (i % 3 == 0) ? 2 : 3;

            double valorParcial = peso * y;
            sumaTotal += valorParcial;
            dgv.Rows.Add($"x{i} = {x:F4}", $"f({x:F4}) = {y:F6}", $"[{peso}] × {y:F6} = {valorParcial:F6}");
        }

        double integral = (3.0 * h / 8.0) * sumaTotal;
        int idxFinal = dgv.Rows.Add("🔮 SIMPSON 3/8 (1D)", $"h = {h:F4}  ➔  I = (3h/8) · Σ", integral.ToString("F8"));
        AplicarEstiloGranTotal(dgv.Rows[idxFinal]);
    }

    public void EjecutarSimpson38(Func<double, double, double> f, double ax, double bx, int nx, double cy, double dy, int ny, DataGridView dgv)
    {
        if (nx < 3 || nx % 3 != 0 || ny < 3 || ny % 3 != 0) throw new Exception("Para Simpson 3/8 en 2D, los intervalos 'nx' y 'ny' deben ser MÚLTIPLOS DE 3.");

        dgv.Columns.Clear(); dgv.Rows.Clear();
        dgv.Columns.Add("Coordenada", "(xᵢ, yⱼ)"); dgv.Columns.Add("Evaluacion", "f(x, y)"); dgv.Columns.Add("Operacion", "Peso Wᵢⱼ × f(x, y)");

        double hx = (bx - ax) / nx;
        double hy = (dy - cy) / ny;
        double sumaTotal = 0;

        for (int i = 0; i <= nx; i++)
        {
            double x = ax + i * hx;
            int pesoX = 1; if (i > 0 && i < nx) pesoX = (i % 3 == 0) ? 2 : 3;

            for (int j = 0; j <= ny; j++)
            {
                double y = cy + j * hy;
                int pesoY = 1; if (j > 0 && j < ny) pesoY = (j % 3 == 0) ? 2 : 3;

                int pesoFinal = pesoX * pesoY;
                double evaluacion = f(x, y);
                double valorParcial = pesoFinal * evaluacion;
                sumaTotal += valorParcial;

                dgv.Rows.Add($"({x:F2}, {y:F2})", $"f = {evaluacion:F6}", $"[{pesoFinal}] × {evaluacion:F6} = {valorParcial:F6}");
            }
        }

        double integral = (9.0 * hx * hy / 64.0) * sumaTotal;
        int idxFinal = dgv.Rows.Add("🔮 SIMPSON 3/8 (2D)", $"hx = {hx:F4}, hy = {hy:F4}", integral.ToString("F8"));
        AplicarEstiloGranTotal(dgv.Rows[idxFinal]);
    }

    // =====================================================================
    // MOTOR SILENCIOSO: ROMBERG INTERNO (Para usar dentro de la doble)
    // =====================================================================
    private double CalcularRombergInterno(Func<double, double> f, double a, double b, int niveles)
    {
        double[,] R = new double[niveles, niveles];

        // Columna 0: Regla del Trapecio
        for (int j = 0; j < niveles; j++)
        {
            int n = (int)Math.Pow(2, j);
            double h = (b - a) / n;
            double suma = 0;
            for (int i = 1; i < n; i++) suma += f(a + i * h);
            R[j, 0] = (h / 2.0) * (f(a) + 2 * suma + f(b));
        }

        // Extrapolación de Richardson
        for (int k = 1; k < niveles; k++)
        {
            double factor = Math.Pow(4, k);
            for (int j = 0; j < niveles - k; j++)
            {
                R[j, k] = (factor * R[j + 1, k - 1] - R[j, k - 1]) / (factor - 1);
            }
        }
        return R[0, niveles - 1]; // El valor más preciso
    }

    // =====================================================================
    // MOTOR DE INTEGRACIÓN: ROMBERG 1D
    // =====================================================================
    public void EjecutarRomberg(Func<double, double> f, double a, double b, int niveles, DataGridView dgv)
    {
        if (niveles < 1) throw new Exception("Para Romberg, el número de niveles debe ser >= 1.");

        dgv.Columns.Clear(); dgv.Rows.Clear();

        // Crear columnas dinámicas según los niveles
        dgv.Columns.Add("Nivel", "Nivel (j)");
        dgv.Columns.Add("K0", "k=0 [Trapecio]");
        for (int k = 1; k < niveles; k++)
        {
            dgv.Columns.Add($"K{k}", $"k={k} O(h^{2 * (k + 1)})");
        }

        double[,] R = new double[niveles, niveles];

        // Columna 0: Trapecio
        for (int j = 0; j < niveles; j++)
        {
            int n = (int)Math.Pow(2, j);
            double h = (b - a) / n;
            double suma = 0;
            for (int i = 1; i < n; i++) suma += f(a + i * h);
            R[j, 0] = (h / 2.0) * (f(a) + 2 * suma + f(b));
        }

        // Extrapolación de Richardson
        for (int k = 1; k < niveles; k++)
        {
            double factor = Math.Pow(4, k);
            for (int j = 0; j < niveles - k; j++)
            {
                R[j, k] = (factor * R[j + 1, k - 1] - R[j, k - 1]) / (factor - 1);
            }
        }

        // Imprimir Matriz Triangular en la tabla
        for (int j = 0; j < niveles; j++)
        {
            string[] filaDatos = new string[niveles + 1];
            filaDatos[0] = $"j={j} (n={(int)Math.Pow(2, j)})";

            for (int k = 0; k < niveles; k++)
            {
                if (k <= niveles - 1 - j) filaDatos[k + 1] = R[j, k].ToString("F8");
                else filaDatos[k + 1] = "---"; // Espacios vacíos de la matriz triangular
            }
            dgv.Rows.Add(filaDatos);
        }

        double integral = R[0, niveles - 1];

        // Agregar fila de Gran Total adaptada a múltiples columnas
        int idxFinal = dgv.Rows.Add();
        dgv.Rows[idxFinal].Cells[0].Value = "🔮 ROMBERG 1D";
        dgv.Rows[idxFinal].Cells[1].Value = "Extrapolación Final ➔";
        dgv.Rows[idxFinal].Cells[2].Value = integral.ToString("F8");
        AplicarEstiloGranTotal(dgv.Rows[idxFinal]);
    }

    // =====================================================================
    // MOTOR DE INTEGRACIÓN: ROMBERG 2D (Funciones Anidadas)
    // =====================================================================
    public void EjecutarRomberg(Func<double, double, double> f, double ax, double bx, int nx, double cy, double dy, int ny, DataGridView dgv)
    {
        if (nx < 1 || ny < 1) throw new Exception("El número de niveles nx y ny debe ser >= 1.");

        // LA MAGIA: Creamos una función de 1D que por dentro resuelve el Romberg de Y
        Func<double, double> funcionExterior = (x) => CalcularRombergInterno((y) => f(x, y), cy, dy, ny);

        // Mandamos a resolver y dibujar esa función exterior usando el Romberg 1D
        EjecutarRomberg(funcionExterior, ax, bx, nx, dgv);

        // Ajustamos la etiqueta final para que diga 2D
        int ultimaFila = dgv.Rows.Count - 1;
        dgv.Rows[ultimaFila].Cells[0].Value = "🔮 ROMBERG 2D";
    }

    // =====================================================================
    // MOTOR DE EDO: MÉTODO DE EULER SIMPLE
    // Fórmula: yᵢ₊₁ = yᵢ + h · f(xᵢ, yᵢ)
    // =====================================================================
    public void EjecutarEulerSimple(Func<double, double, double> f, double x0, double y0, double xf, int n, DataGridView dgv)
    {
        if (n < 1) throw new Exception("El número de pasos 'n' debe ser mayor o igual a 1.");

        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.Columns.Add("Iteracion", "i");
        dgv.Columns.Add("X", "xᵢ");
        dgv.Columns.Add("Y", "yᵢ");
        dgv.Columns.Add("Derivada", "f(xᵢ, yᵢ)");
        dgv.Columns.Add("Siguiente", "yᵢ₊₁ = yᵢ + h·f");

        double h = (xf - x0) / n;
        double x = x0;
        double y = y0;

        for (int i = 0; i <= n; i++)
        {
            // En el último paso solo mostramos el punto final, ya no calculamos el siguiente
            if (i == n)
            {
                dgv.Rows.Add(i.ToString(), x.ToString("F4"), y.ToString("F6"), "---", "---");
                break;
            }

            double derivada = f(x, y);
            double ySiguiente = y + h * derivada;

            dgv.Rows.Add(i.ToString(), x.ToString("F4"), y.ToString("F6"), derivada.ToString("F6"), ySiguiente.ToString("F6"));

            // Avanzamos al siguiente punto (x se calcula así para evitar errores de redondeo en double)
            x = x0 + (i + 1) * h;
            y = ySiguiente;
        }

        // Fila del gran total resaltada
        int idxFinal = dgv.Rows.Add("🎯 RESULTADO", $"x = {xf:F4}", $"y ≈ {y:F8}", "", "");

        // Aplicamos el estilo a la última fila (reutilizando el método que ya tienes)
        dgv.Rows[idxFinal].DefaultCellStyle.BackColor = Color.FromArgb(14, 116, 144);
        dgv.Rows[idxFinal].DefaultCellStyle.ForeColor = Color.White;
        dgv.Rows[idxFinal].DefaultCellStyle.Font = new Font("Consolas", 12, FontStyle.Bold);
        dgv.Rows[idxFinal].DefaultCellStyle.SelectionBackColor = Color.FromArgb(21, 94, 117);
        dgv.Rows[idxFinal].DefaultCellStyle.SelectionForeColor = Color.White;
    }

    // =====================================================================
    // MOTOR DE EDO: MÉTODO DE EULER MEJORADO (HEUN)
    // Predictora: y* = yᵢ + h·f(xᵢ, yᵢ)
    // Correctora: yᵢ₊₁ = yᵢ + (h/2)·[f(xᵢ, yᵢ) + f(xᵢ₊₁, y*)]
    // =====================================================================
    public void EjecutarEulerMejorado(Func<double, double, double> f, double x0, double y0, double xf, int n, DataGridView dgv)
    {
        if (n < 1) throw new Exception("El número de pasos 'n' debe ser mayor o igual a 1.");

        dgv.Columns.Clear();
        dgv.Rows.Clear();
        dgv.Columns.Add("Iteracion", "i");
        dgv.Columns.Add("X", "xᵢ");
        dgv.Columns.Add("Y", "yᵢ");
        dgv.Columns.Add("m1", "m₁ = f(xᵢ, yᵢ)");
        dgv.Columns.Add("Y_Predictora", "y* (Predictora)");
        dgv.Columns.Add("m2", "m₂ = f(xᵢ₊₁, y*)");
        dgv.Columns.Add("Y_Correctora", "yᵢ₊₁ (Correctora)");

        double h = (xf - x0) / n;
        double x = x0;
        double y = y0;

        for (int i = 0; i <= n; i++)
        {
            if (i == n)
            {
                // Fila final, ya no hay más predicciones
                dgv.Rows.Add(i.ToString(), x.ToString("F4"), y.ToString("F6"), "---", "---", "---", "---");
                break;
            }

            // 1. Calcular m1 (Pendiente inicial)
            double m1 = f(x, y);

            // 2. Aplicar fórmula Predictora (Euler Simple)
            double yPredictora = y + h * m1;

            // Avanzamos X al siguiente punto para evaluar m2
            double xSiguiente = x0 + (i + 1) * h;

            // 3. Calcular m2 (Pendiente en el punto predicho)
            double m2 = f(xSiguiente, yPredictora);

            // 4. Aplicar fórmula Correctora (Promedio de pendientes)
            double yCorrectora = y + (h / 2.0) * (m1 + m2);

            // Agregar fila a la tabla con todo el desglose
            dgv.Rows.Add(i.ToString(),
                         x.ToString("F4"),
                         y.ToString("F6"),
                         m1.ToString("F6"),
                         yPredictora.ToString("F6"),
                         m2.ToString("F6"),
                         yCorrectora.ToString("F6"));

            // Preparar variables para la siguiente iteración
            x = xSiguiente;
            y = yCorrectora;
        }

        // Fila del gran total resaltada
        int idxFinal = dgv.Rows.Add("🎯 RESULTADO", $"x = {xf:F4}", $"y ≈ {y:F8}", "", "", "", "");

        dgv.Rows[idxFinal].DefaultCellStyle.BackColor = Color.FromArgb(14, 116, 144);
        dgv.Rows[idxFinal].DefaultCellStyle.ForeColor = Color.White;
        dgv.Rows[idxFinal].DefaultCellStyle.Font = new Font("Consolas", 12, FontStyle.Bold);
        dgv.Rows[idxFinal].DefaultCellStyle.SelectionBackColor = Color.FromArgb(21, 94, 117);
        dgv.Rows[idxFinal].DefaultCellStyle.SelectionForeColor = Color.White;
    }

    // =====================================================================
    // 🧠 EVALUADOR DE FUNCIONES DE 2 VARIABLES f(x, y) CON MXPARSER
    // =====================================================================
    public double EvaluarXY(string ecuacion, double valorX, double valorY)
    {
        try
        {
            // 1. Creamos las variables (Argumentos) para mXparser
            // Nota: mXparser es sensible a mayúsculas/minúsculas. Usamos "x" y "y" en minúscula por convención.
            org.mariuszgromada.math.mxparser.Argument argX = new org.mariuszgromada.math.mxparser.Argument("x", valorX);
            org.mariuszgromada.math.mxparser.Argument argY = new org.mariuszgromada.math.mxparser.Argument("y", valorY);

            // 2. Inicializamos la expresión inyectándole la ecuación de texto y los argumentos
            org.mariuszgromada.math.mxparser.Expression expresion = new org.mariuszgromada.math.mxparser.Expression(ecuacion, argX, argY);

            // 3. Mandamos a calcular
            double resultado = expresion.calculate();

            // 4. Blindaje: mxparser devuelve "NaN" (Not a Number) si escribís mal la fórmula (ej. "xy" en vez de "x*y")
            if (double.IsNaN(resultado))
            {
                throw new Exception("Sintaxis inválida en la ecuación. Asegúrate de usar operadores explícitos (ej. 'x*y' en lugar de 'xy').");
            }

            return resultado;
        }
        catch (Exception ex)
        {
            throw new Exception("Error crítico en el motor mXparser: " + ex.Message);
        }
    }

    // 🧹 LA ESCOBA MÁGICA (VERSIÓN INTELIGENTE)
    public void LimpiarPantalla(DataGridView tabla, TextBox[] cajasDeTexto, Label[] etiquetasResultados = null)
    {
        // 1. Limpiamos la tabla
        if (tabla != null)
        {
            tabla.Rows.Clear();
        }

        // 2. Vaciamos las cajas de texto y regresamos el cursor
        if (cajasDeTexto != null && cajasDeTexto.Length > 0)
        {
            foreach (TextBox txt in cajasDeTexto)
            {
                txt.Clear();
            }
            cajasDeTexto[0].Focus();
        }

        // 3. 🪄 TRUCO PRO: Restauramos los Labels usando el corte inteligente
        if (etiquetasResultados != null)
        {
            foreach (Label lbl in etiquetasResultados)
            {
                // Si el label tiene dos puntos (ej. "Raíz: 5.43"), cortamos el número
                if (lbl.Text.Contains(":"))
                {
                    lbl.Text = lbl.Text.Substring(0, lbl.Text.IndexOf(':') + 1) + " ";
                }
                else
                {
                    // Si no tiene dos puntos, solo lo dejamos en blanco para que no salgan rayitas
                    lbl.Text = "";
                }
            }
        }
    }

    // 🛡️ EL ESCUDO SINTÁCTICO DEFINITIVO
    public bool EsFuncionValida(string funcionStr)
    {
        // 1. Que no esté vacía
        if (string.IsNullOrWhiteSpace(funcionStr))
        {
            MessageBox.Show("La función no puede estar vacía.", "Campo Vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // 2. Usamos el motor de mXparser para validar si es matemática real
        Function f = new Function("f(x) = " + funcionStr);

        // checkSyntax() devuelve 'true' si es válida, y 'false' si es basura como "sdfw"
        if (!f.checkSyntax())
        {
            MessageBox.Show($"El sistema no reconoce '{funcionStr}' como una función matemática válida.\n\nVerifica que uses la variable 'x' y símbolos correctos (ej. x^2 + sin(x) - 4).",
                            "Sintaxis Matemática Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return true;
    }

    // 🛡️ ESCUDO 2: PARA COEFICIENTES Y VALORES INICIALES (Solo números)
    public bool SonNumerosValidos(string texto, string nombreCampo)
    {
        if (string.IsNullOrWhiteSpace(texto)) return false; // Ya lo validamos antes de llamar

        // Partimos el texto por espacios o saltos de línea
        string[] partes = texto.Split(new char[] { ' ', '\r', '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string parte in partes)
        {
            try
            {
                // Intentamos usar tu traductor universal
                ConvertirADouble(parte);
            }
            catch
            {
                // Si falla, es porque metió letras o basura
                MessageBox.Show(
                    $"¡Error detectado en {nombreCampo}!\n\nEl valor '{parte}' no es un número válido. Recuerda que aquí solo van números puros separados por espacios.",
                    "Sintaxis Inválida (No es número)",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
        return true;
    }

    // 🛡️ ESCUDO 3: PARA SISTEMAS NO LINEALES (Múltiples variables)
    public bool EsSistemaNoLinealValido(string[] funciones)
    {
        foreach (string func in funciones)
        {
            if (string.IsNullOrWhiteSpace(func)) continue;

            // TRUCO PRO DE mXparser: Declaramos una función fantasma con las variables 
            // más comunes (x, y, z, x1, x2, x3) para que no nos marque error de sintaxis si el usuario las usa.
            org.mariuszgromada.math.mxparser.Function f =
                new org.mariuszgromada.math.mxparser.Function($"f(x, y, z, w, x1, x2, x3) = {func}");

            if (!f.checkSyntax())
            {
                MessageBox.Show(
                    $"La ecuación '{func}' tiene errores o una sintaxis incorrecta.\n\nRevisa los operadores matemáticos y asegúrate de usar variables válidas (ej. x, y, x1, x2).",
                    "Ecuación Inválida en Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }
        return true;
    }

    // 🛡️ ESCUDO 4: TOLERANCIA POSITIVA
    public bool EsToleranciaValida(string textoTol)
    {
        try
        {
            // Usamos tu traductor universal para convertir el texto
            double tol = ConvertirADouble(textoTol);

            // La tolerancia no puede ser menor o igual a cero
            if (tol <= 0)
            {
                MessageBox.Show("¡La tolerancia no puede ser negativa ni cero!\n\nTiene que ser un valor positivo (ej. 0.001 o 1E-05).",
                                "Tolerancia Inválida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        catch
        {
            // Si llega a fallar la conversión aquí, significa que metió letras, 
            // pero el escudo de SonNumerosValidos ya lo atrapó antes.
            return false;
        }
    }

    // 🛡️ ESCUDO 5: CONTINUIDAD Y DIVISIÓN POR CERO
    public bool EsEvaluacionValida(double fa, double fb)
    {
        // Revisamos si f(a) o f(b) dieron "NaN" o "Infinito" (División por cero, logaritmos negativos, etc.)
        if (double.IsNaN(fa) || double.IsInfinity(fa) || double.IsNaN(fb) || double.IsInfinity(fb))
        {
            MessageBox.Show("¡Alerta Matemática!\n\nLa función se indefine en uno de tus valores iniciales (ej. División por cero o asíntota).\n\nCambia tus Valores A y B por un intervalo donde la función sea continua.",
                            "Indeterminación Detectada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        return true;
    }

    // 🛡️ ESCUDO 5.1: CONTINUIDAD PARA UN SOLO PUNTO (Newton y Punto Fijo)
    public bool EsPuntoValido(double fx)
    {
        if (double.IsNaN(fx) || double.IsInfinity(fx))
        {
            MessageBox.Show("¡Alerta Matemática, bro!\n\nLa función se indefine en tu Valor Inicial (ej. División por cero, raíz negativa o asíntota).\n\nCambia el punto de inicio por uno donde la curva exista de verdad.",
                            "Indeterminación Detectada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        return true;
    }

    public void ExportarAExcel(DataGridView dgv)
    {
        if (dgv.Rows.Count == 0) return;

        // 1. Usamos el alias 'Excel' para que no haya duda
        Excel.Application excelApp = new Excel.Application();
        Excel.Workbook workbook = excelApp.Workbooks.Add(true);
        Excel._Worksheet worksheet = (Excel._Worksheet)workbook.ActiveSheet;

        try
        {
            // 2. Exportamos los Encabezados
            int indiceColumna = 0;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                indiceColumna++;
                // IMPORTANTE: En .NET moderno hay que ser específicos con el rango
                Excel.Range celda = (Excel.Range)worksheet.Cells[1, indiceColumna];
                celda.Value = col.HeaderText;
                celda.Font.Bold = true;
                celda.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
            }

            // 3. Exportamos las Filas
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (!dgv.Rows[i].IsNewRow)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value?.ToString();
                    }
                }
            }

            excelApp.Columns.AutoFit();
            excelApp.Visible = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error bro: " + ex.Message);
        }
    }
}
//Confirmación de los cambios 