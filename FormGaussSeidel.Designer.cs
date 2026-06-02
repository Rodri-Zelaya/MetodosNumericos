namespace Métodos_Numéricos
{
    partial class FormGaussSeidel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvGaussSeidel = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtMatrizA = new TextBox();
            txtVectorB = new TextBox();
            txtValoresIniciales = new TextBox();
            txtTolerancia = new TextBox();
            btnCalcular = new Button();
            btnExportar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvGaussSeidel).BeginInit();
            SuspendLayout();
            // 
            // dgvGaussSeidel
            // 
            dgvGaussSeidel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGaussSeidel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGaussSeidel.Location = new Point(12, 225);
            dgvGaussSeidel.Name = "dgvGaussSeidel";
            dgvGaussSeidel.RowHeadersWidth = 51;
            dgvGaussSeidel.Size = new Size(945, 213);
            dgvGaussSeidel.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 59);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 1;
            label1.Text = "Matrix A";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(684, 153);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 2;
            label2.Text = "Vector B";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 160);
            label3.Name = "label3";
            label3.Size = new Size(114, 20);
            label3.TabIndex = 3;
            label3.Text = "Valores Iniciales";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Location = new Point(456, 58);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 4;
            label4.Text = "Tolerancia";
            // 
            // txtMatrizA
            // 
            txtMatrizA.Location = new Point(126, 16);
            txtMatrizA.Multiline = true;
            txtMatrizA.Name = "txtMatrizA";
            txtMatrizA.Size = new Size(167, 104);
            txtMatrizA.TabIndex = 5;
            // 
            // txtVectorB
            // 
            txtVectorB.Location = new Point(755, 97);
            txtVectorB.Multiline = true;
            txtVectorB.Name = "txtVectorB";
            txtVectorB.Size = new Size(167, 104);
            txtVectorB.TabIndex = 6;
            // 
            // txtValoresIniciales
            // 
            txtValoresIniciales.Location = new Point(159, 130);
            txtValoresIniciales.Multiline = true;
            txtValoresIniciales.Name = "txtValoresIniciales";
            txtValoresIniciales.Size = new Size(134, 89);
            txtValoresIniciales.TabIndex = 7;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Anchor = AnchorStyles.Top;
            txtTolerancia.Location = new Point(443, 81);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(102, 27);
            txtTolerancia.TabIndex = 8;
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.Location = new Point(568, 28);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(110, 41);
            btnCalcular.TabIndex = 9;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Location = new Point(684, 28);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(138, 41);
            btnExportar.TabIndex = 10;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Location = new Point(828, 28);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 41);
            btnLimpiar.TabIndex = 11;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // FormGaussSeidel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(969, 450);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Controls.Add(txtTolerancia);
            Controls.Add(txtValoresIniciales);
            Controls.Add(txtVectorB);
            Controls.Add(txtMatrizA);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvGaussSeidel);
            Name = "FormGaussSeidel";
            Text = "FormGaussSeidel";
            ((System.ComponentModel.ISupportInitialize)dgvGaussSeidel).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvGaussSeidel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtMatrizA;
        private TextBox txtVectorB;
        private TextBox txtValoresIniciales;
        private TextBox txtTolerancia;
        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
    }
}