namespace Métodos_Numéricos
{
    partial class FormPuntoFijo
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtFuncionPuntoFijo = new TextBox();
            txtX0PuntoFijo = new TextBox();
            txtTolPuntoFijo = new TextBox();
            btnCalcular = new Button();
            dgvPuntoFijo = new DataGridView();
            lblRaiz = new Label();
            btnExportar = new Button();
            btnGraficar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPuntoFijo).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(22, 88);
            label1.Name = "label1";
            label1.Size = new Size(109, 31);
            label1.TabIndex = 0;
            label1.Text = "Ecuación";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(633, 65);
            label2.Name = "label2";
            label2.Size = new Size(42, 31);
            label2.TabIndex = 1;
            label2.Text = "X0";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(588, 123);
            label3.Name = "label3";
            label3.Size = new Size(123, 31);
            label3.TabIndex = 2;
            label3.Text = "Tolerancia";
            // 
            // txtFuncionPuntoFijo
            // 
            txtFuncionPuntoFijo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFuncionPuntoFijo.Location = new Point(128, 88);
            txtFuncionPuntoFijo.Name = "txtFuncionPuntoFijo";
            txtFuncionPuntoFijo.Size = new Size(434, 34);
            txtFuncionPuntoFijo.TabIndex = 5;
            // 
            // txtX0PuntoFijo
            // 
            txtX0PuntoFijo.Anchor = AnchorStyles.Top;
            txtX0PuntoFijo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtX0PuntoFijo.Location = new Point(717, 62);
            txtX0PuntoFijo.Name = "txtX0PuntoFijo";
            txtX0PuntoFijo.Size = new Size(194, 34);
            txtX0PuntoFijo.TabIndex = 6;
            // 
            // txtTolPuntoFijo
            // 
            txtTolPuntoFijo.Anchor = AnchorStyles.Top;
            txtTolPuntoFijo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTolPuntoFijo.Location = new Point(717, 120);
            txtTolPuntoFijo.Name = "txtTolPuntoFijo";
            txtTolPuntoFijo.Size = new Size(194, 34);
            txtTolPuntoFijo.TabIndex = 7;
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalcular.Location = new Point(1104, 21);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(162, 75);
            btnCalcular.TabIndex = 9;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // dgvPuntoFijo
            // 
            dgvPuntoFijo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPuntoFijo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPuntoFijo.BackgroundColor = SystemColors.ActiveCaption;
            dgvPuntoFijo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPuntoFijo.Location = new Point(12, 203);
            dgvPuntoFijo.Name = "dgvPuntoFijo";
            dgvPuntoFijo.RowHeadersWidth = 51;
            dgvPuntoFijo.Size = new Size(1628, 252);
            dgvPuntoFijo.TabIndex = 10;
            // 
            // lblRaiz
            // 
            lblRaiz.Anchor = AnchorStyles.Top;
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(939, 123);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(65, 31);
            lblRaiz.TabIndex = 11;
            lblRaiz.Text = "Raiz:";
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Location = new Point(1272, 21);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(218, 75);
            btnExportar.TabIndex = 12;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnGraficar
            // 
            btnGraficar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGraficar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGraficar.Location = new Point(1496, 21);
            btnGraficar.Name = "btnGraficar";
            btnGraficar.Size = new Size(144, 75);
            btnGraficar.TabIndex = 13;
            btnGraficar.Text = "Graficar";
            btnGraficar.UseVisualStyleBackColor = true;
            btnGraficar.Click += btnGraficar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.Location = new Point(1496, 108);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(144, 71);
            btnLimpiar.TabIndex = 14;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // FormPuntoFijo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1652, 467);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGraficar);
            Controls.Add(btnExportar);
            Controls.Add(lblRaiz);
            Controls.Add(dgvPuntoFijo);
            Controls.Add(btnCalcular);
            Controls.Add(txtTolPuntoFijo);
            Controls.Add(txtX0PuntoFijo);
            Controls.Add(txtFuncionPuntoFijo);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormPuntoFijo";
            Text = "FormPuntoFijo";
            ((System.ComponentModel.ISupportInitialize)dgvPuntoFijo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtFuncionPuntoFijo;
        private TextBox txtX0PuntoFijo;
        private TextBox txtTolPuntoFijo;
        private Button btnCalcular;
        private DataGridView dgvPuntoFijo;
        private Label lblRaiz;
        private Button btnExportar;
        private Button btnGraficar;
        private Button btnLimpiar;
    }
}