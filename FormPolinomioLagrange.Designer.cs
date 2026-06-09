namespace Métodos_Numéricos
{
    partial class FormPolinomioLagrange
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
            dgvPolinomiosBase = new DataGridView();
            dgvResultados = new DataGridView();
            txtXEvaluar = new TextBox();
            btnCalcular = new Button();
            btnExportar = new Button();
            btnLimpiar = new Button();
            label1 = new Label();
            label2 = new Label();
            lblTipoPolinomio = new Label();
            ColX = new DataGridViewTextBoxColumn();
            ColY = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvPuntos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPolinomiosBase).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).BeginInit();
            SuspendLayout();
            // 
            // dgvPuntos
            // 
            dgvPuntos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPuntos.Columns.AddRange(new DataGridViewColumn[] { ColX, ColY });
            dgvPuntos.Location = new Point(12, 40);
            dgvPuntos.Name = "dgvPuntos";
            dgvPuntos.RowHeadersWidth = 51;
            dgvPuntos.Size = new Size(297, 72);
            dgvPuntos.TabIndex = 0;
            // 
            // dgvPolinomiosBase
            // 
            dgvPolinomiosBase.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPolinomiosBase.Location = new Point(12, 130);
            dgvPolinomiosBase.Name = "dgvPolinomiosBase";
            dgvPolinomiosBase.RowHeadersWidth = 51;
            dgvPolinomiosBase.Size = new Size(300, 73);
            dgvPolinomiosBase.TabIndex = 1;
            // 
            // dgvResultados
            // 
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultados.Location = new Point(12, 225);
            dgvResultados.Name = "dgvResultados";
            dgvResultados.RowHeadersWidth = 51;
            dgvResultados.Size = new Size(300, 80);
            dgvResultados.TabIndex = 2;
            // 
            // txtXEvaluar
            // 
            txtXEvaluar.Location = new Point(318, 147);
            txtXEvaluar.Name = "txtXEvaluar";
            txtXEvaluar.Size = new Size(125, 27);
            txtXEvaluar.TabIndex = 3;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(472, 62);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 4;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(572, 62);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(128, 29);
            btnExportar.TabIndex = 5;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(706, 62);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(94, 29);
            btnLimpiar.TabIndex = 6;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 17);
            label1.Name = "label1";
            label1.Size = new Size(154, 20);
            label1.TabIndex = 7;
            label1.Text = "Introduzca los puntos:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(316, 124);
            label2.Name = "label2";
            label2.Size = new Size(127, 20);
            label2.TabIndex = 8;
            label2.Text = "Valor X a predecir";
            // 
            // lblTipoPolinomio
            // 
            lblTipoPolinomio.AutoSize = true;
            lblTipoPolinomio.Location = new Point(402, 272);
            lblTipoPolinomio.Name = "lblTipoPolinomio";
            lblTipoPolinomio.Size = new Size(142, 20);
            lblTipoPolinomio.TabIndex = 9;
            lblTipoPolinomio.Text = "Polinomio Lagrange";
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
            // FormPolinomioLagrange
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTipoPolinomio);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLimpiar);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Controls.Add(txtXEvaluar);
            Controls.Add(dgvResultados);
            Controls.Add(dgvPolinomiosBase);
            Controls.Add(dgvPuntos);
            Name = "FormPolinomioLagrange";
            Text = "FormPolinomioLagrange";
            ((System.ComponentModel.ISupportInitialize)dgvPuntos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPolinomiosBase).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvResultados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPuntos;
        private DataGridView dgvPolinomiosBase;
        private DataGridView dgvResultados;
        private TextBox txtXEvaluar;
        private Button btnCalcular;
        private Button btnExportar;
        private Button btnLimpiar;
        private Label label1;
        private Label label2;
        private Label lblTipoPolinomio;
        private DataGridViewTextBoxColumn ColX;
        private DataGridViewTextBoxColumn ColY;
    }
}