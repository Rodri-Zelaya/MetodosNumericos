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
                throw new Exception($"El elemento en la diagonal principal A[{i + 1},{i + 1}] es cero. Gauss-Seidel requiere dividir entre la diagonal. Reordena las filas de tu sistema para evitar el cero.");
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

        double[] X_viejo = new double[n];
        double[] X_nuevo = new double[n];

        // Copiamos los valores iniciales
        Array.Copy(X0, X_nuevo, n);

        for (int iter = 1; iter <= maxIter; iter++)
        {
            Array.Copy(X_nuevo, X_viejo, n);
            double errorMaximo = 0;
            double[] erroresFila = new double[n];

            // Fila para agregar al DGV
            List<string> filaDatos = new List<string> { iter.ToString() };

            // 1. Guardar valores viejos para la tabla
            for (int i = 0; i < n; i++) filaDatos.Add(X_viejo[i].ToString("F8"));

            // 2. Ejecutar la fórmula de Gauss-Seidel
            for (int i = 0; i < n; i++)
            {
                double suma = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        // Usa X_nuevo porque si j < i, ya se calculó en esta misma iteración (La magia de Seidel)
                        suma += A[i, j] * X_nuevo[j];
                    }
                }
                X_nuevo[i] = (b[i] - suma) / A[i, i];
                filaDatos.Add(X_nuevo[i].ToString("F8"));
            }

            // 3. Calcular Errores Relativos Porcentuales
            for (int i = 0; i < n; i++)
            {
                if (iter == 1)
                {
                    // En la iteración 1 no hay error calculable válido aún
                    filaDatos.Add("-");
                }
                else
                {
                    if (X_nuevo[i] != 0)
                    {
                        double errorVariable = Math.Abs((X_nuevo[i] - X_viejo[i]) / X_nuevo[i]) * 100;
                        erroresFila[i] = errorVariable;
                        filaDatos.Add(errorVariable.ToString("F8") + "%");

                        if (errorVariable > errorMaximo) errorMaximo = errorVariable;
                    }
                    else
                    {
                        filaDatos.Add("0%");
                    }
                }
            }

            dgv.Rows.Add(filaDatos.ToArray());

            // Condición de parada: Si el error más grande de TODAS las variables es menor a la tolerancia
            if (iter > 1 && errorMaximo <= tol)
            {
                break; // Convergencia alcanzada
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

        // Columna Final de Decisión
        dgv.Columns.Add("Decision", "Decisión");


        double[] X_viejo = new double[n];
        double[] X_nuevo = new double[n];

        // Copiamos los valores iniciales al vector viejo
        Array.Copy(X0, X_viejo, n);

        for (int iter = 1; iter <= maxIter; iter++)
        {
            double errorMaximo = 0;
            double[] erroresFila = new double[n];

            // Fila para agregar al DGV
            List<string> filaDatos = new List<string> { iter.ToString() };

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
                        // 🚀 LA DIFERENCIA CON SEIDEL: Aquí SÓLO usamos X_viejo
                        suma += A[i, j] * X_viejo[j];
                    }
                }
                X_nuevo[i] = (b[i] - suma) / A[i, i];
                filaDatos.Add(X_nuevo[i].ToString("F8"));
            }

            // 3. Calcular Errores Relativos Porcentuales
            for (int i = 0; i < n; i++)
            {
                if (iter == 1)
                {
                    // Iteración 1 no tiene error previo
                    filaDatos.Add("-");
                }
                else
                {
                    if (X_nuevo[i] != 0)
                    {
                        double errorVariable = Math.Abs((X_nuevo[i] - X_viejo[i]) / X_nuevo[i]) * 100;
                        erroresFila[i] = errorVariable;
                        filaDatos.Add(errorVariable.ToString("F8") + "%");

                        if (errorVariable > errorMaximo) errorMaximo = errorVariable;
                    }
                    else
                    {
                        filaDatos.Add("0%");
                    }
                }
            }

            // 4. Decisión Final de la iteración
            if (iter == 1)
            {
                filaDatos.Add("Continuar");
            }
            else if (errorMaximo <= tol)
            {
                filaDatos.Add("Finalizar");
            }
            else
            {
                filaDatos.Add("Continuar");
            }

            dgv.Rows.Add(filaDatos.ToArray());

            // Actualizamos el vector viejo para la SIGUIENTE iteración
            Array.Copy(X_nuevo, X_viejo, n);

            // Condición de parada
            if (iter > 1 && errorMaximo <= tol)
            {
                break; // Convergencia alcanzada
            }
        }
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