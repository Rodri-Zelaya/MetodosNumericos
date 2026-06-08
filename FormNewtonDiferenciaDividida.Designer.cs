namespace Métodos_Numéricos
{
    partial class FormNewtonDiferenciaDividida
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
            dgvPuntos = new DataGridView();
            ColX = new DataGridViewTextBoxColumn();
            ColY = new DataGridViewTextBoxColumn();
            dgvMatrizDiferencias = new DataGridView();
            dgvResultadosInterpolacion = new DataGridView();
            txtXEvaluar = new TextBox();
            label1 = new Label();
            lblTipoPolinomio = new Label();
            btnCalcular = new Button();
            btnExportar = new Button();
            btnLimpiar = new Button();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPuntos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMatrizDiferencias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultadosInterpolacion).BeginInit();
            SuspendLayout();
            // 
            // dgvPuntos
            // 
            dgvPuntos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPuntos.Columns.AddRange(new DataGridViewColumn[] { ColX, ColY });
            dgvPuntos.Location = new Point(12, 50);
            dgvPuntos.Name = "dgvPuntos";
            dgvPuntos.RowHeadersWidth = 51;
            dgvPuntos.Size = new Size(315, 94);
            dgvPuntos.TabIndex = 0;
            // 
            // ColX
            // 
            ColX.HeaderText = "Valor X";
            ColX.MinimumWidth = 6;
            ColX.Name = "ColX";
            ColX.Width = 125;
            // 
            // ColY
            // 
            ColY.HeaderText = "Valor Y";
            ColY.MinimumWidth = 6;
            ColY.Name = "ColY";
            ColY.Width = 125;
            // 
            // dgvMatrizDiferencias
            // 
            dgvMatrizDiferencias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMatrizDiferencias.Location = new Point(12, 163);
            dgvMatrizDiferencias.Name = "dgvMatrizDiferencias";
            dgvMatrizDiferencias.RowHeadersWidth = 51;
            dgvMatrizDiferencias.Size = new Size(315, 83);
            dgvMatrizDiferencias.TabIndex = 1;
            // 
            // dgvResultadosInterpolacion
            // 
            dgvResultadosInterpolacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultadosInterpolacion.Location = new Point(12, 263);
            dgvResultadosInterpolacion.Name = "dgvResultadosInterpolacion";
            dgvResultadosInterpolacion.RowHeadersWidth = 51;
            dgvResultadosInterpolacion.Size = new Size(315, 85);
            dgvResultadosInterpolacion.TabIndex = 2;
            // 
            // txtXEvaluar
            // 
            txtXEvaluar.Location = new Point(359, 73);
            txtXEvaluar.Name = "txtXEvaluar";
            txtXEvaluar.Size = new Size(125, 27);
            txtXEvaluar.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(359, 50);
            label1.Name = "label1";
            label1.Size = new Size(127, 20);
            label1.TabIndex = 4;
            label1.Text = "Valor X a predecir";
            // 
            // lblTipoPolinomio
            // 
            lblTipoPolinomio.AutoSize = true;
            lblTipoPolinomio.Location = new Point(359, 124);
            lblTipoPolinomio.Name = "lblTipoPolinomio";
            lblTipoPolinomio.Size = new Size(124, 20);
            lblTipoPolinomio.TabIndex = 5;
            lblTipoPolinomio.Text = "Polinomio exacto";
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(490, 71);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 6;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(590, 71);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(94, 29);
            btnExportar.TabIndex = 7;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(694, 71);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 8;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 18);
            label2.Name = "label2";
            label2.Size = new Size(151, 20);
            label2.TabIndex = 9;
            label2.Text = "Introduzca los puntos";
            // 
            // FormNewtonDiferenciaDividida
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Controls.Add(lblTipoPolinomio);
            Controls.Add(label1);
            Controls.Add(txtXEvaluar);
            Controls.Add(dgvResultadosInterpolacion);
            Controls.Add(dgvMatrizDiferencias);
            Controls.Add(dgvPuntos);
            Name = "FormNewtonDiferenciaDividida";
            Text = "FormNewtonDiferenciaDividida";
            ((System.ComponentModel.ISupportInitialize)dgvPuntos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMatrizDiferencias).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultadosInterpolacion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPuntos;
        private DataGridViewTextBoxColumn ColX;
        private DataGridViewTextBoxColumn ColY;
        private DataGridView dgvMatrizDiferencias;
        private DataGridView dgvResultadosInterpolacion;
        private TextBox txtXEvaluar;
        private Label label1;
        private Label lblTipoPolinomio;
        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
        private Label label2;
    }
}