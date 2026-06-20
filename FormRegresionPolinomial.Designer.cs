namespace Métodos_Numéricos
{
    partial class FormRegresionPolinomial
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
            dgvDatos = new DataGridView();
            ColX = new DataGridViewTextBoxColumn();
            ColY = new DataGridViewTextBoxColumn();
            btnCalcular = new Button();
            btnExportar = new Button();
            btnLimpiar = new Button();
            dgvRegresion = new DataGridView();
            numGrado = new NumericUpDown();
            label1 = new Label();
            lblTipoRegresion = new Label();
            dgvSumatorias = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvDatos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRegresion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numGrado).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSumatorias).BeginInit();
            SuspendLayout();
            // 
            // dgvDatos
            // 
            dgvDatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDatos.Columns.AddRange(new DataGridViewColumn[] { ColX, ColY });
            dgvDatos.Location = new Point(12, 51);
            dgvDatos.Name = "dgvDatos";
            dgvDatos.RowHeadersWidth = 51;
            dgvDatos.Size = new Size(303, 60);
            dgvDatos.TabIndex = 0;
            // 
            // ColX
            // 
            ColX.HeaderText = "Valor X ";
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
            // btnCalcular
            // 
            btnCalcular.Location = new Point(442, 51);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 1;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(542, 51);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(134, 29);
            btnExportar.TabIndex = 2;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(682, 51);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 3;
            btnLimpiar.Text = "Limpiar ";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvRegresion
            // 
            dgvRegresion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRegresion.Location = new Point(12, 148);
            dgvRegresion.Name = "dgvRegresion";
            dgvRegresion.RowHeadersWidth = 51;
            dgvRegresion.Size = new Size(764, 290);
            dgvRegresion.TabIndex = 4;
            // 
            // numGrado
            // 
            numGrado.Location = new Point(153, 18);
            numGrado.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numGrado.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numGrado.Name = "numGrado";
            numGrado.Size = new Size(150, 27);
            numGrado.TabIndex = 5;
            numGrado.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(135, 20);
            label1.TabIndex = 6;
            label1.Text = "Número de Grados";
            // 
            // lblTipoRegresion
            // 
            lblTipoRegresion.AutoSize = true;
            lblTipoRegresion.Location = new Point(321, 20);
            lblTipoRegresion.Name = "lblTipoRegresion";
            lblTipoRegresion.Size = new Size(215, 20);
            lblTipoRegresion.TabIndex = 7;
            lblTipoRegresion.Text = "Línea Recta (Regresión Simple)";
            // 
            // dgvSumatorias
            // 
            dgvSumatorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSumatorias.Location = new Point(143, 220);
            dgvSumatorias.Name = "dgvSumatorias";
            dgvSumatorias.RowHeadersWidth = 51;
            dgvSumatorias.Size = new Size(300, 188);
            dgvSumatorias.TabIndex = 8;
            // 
            // FormRegresionPolinomial
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvSumatorias);
            Controls.Add(lblTipoRegresion);
            Controls.Add(label1);
            Controls.Add(numGrado);
            Controls.Add(dgvRegresion);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Controls.Add(dgvDatos);
            Name = "FormRegresionPolinomial";
            Text = "FormRegresionPolinomial";
            ((System.ComponentModel.ISupportInitialize)dgvDatos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRegresion).EndInit();
            ((System.ComponentModel.ISupportInitialize)numGrado).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSumatorias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDatos;
        private DataGridViewTextBoxColumn ColX;
        private DataGridViewTextBoxColumn ColY;
        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
        private DataGridView dgvRegresion;
        private NumericUpDown numGrado;
        private Label label1;
        private Label lblTipoRegresion;
        private DataGridView dgvSumatorias;
    }
}