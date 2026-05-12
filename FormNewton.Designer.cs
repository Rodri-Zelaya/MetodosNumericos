namespace Métodos_Numéricos
{
    partial class FormNewton
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
            label3 = new Label();
            label4 = new Label();
            txtFuncionNewton = new TextBox();
            txtVl = new TextBox();
            txtTolNewton = new TextBox();
            btnCalcular = new Button();
            dgvNewton = new DataGridView();
            lblRaiz = new Label();
            btnExportar = new Button();
            btnGraficar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvNewton).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(32, 98);
            label1.Name = "label1";
            label1.Size = new Size(109, 31);
            label1.TabIndex = 0;
            label1.Text = "Ecuación";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(611, 73);
            label3.Name = "label3";
            label3.Size = new Size(140, 31);
            label3.TabIndex = 2;
            label3.Text = "Valor Inicial";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(611, 129);
            label4.Name = "label4";
            label4.Size = new Size(123, 31);
            label4.TabIndex = 3;
            label4.Text = "Tolerancia";
            // 
            // txtFuncionNewton
            // 
            txtFuncionNewton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFuncionNewton.Location = new Point(138, 98);
            txtFuncionNewton.Name = "txtFuncionNewton";
            txtFuncionNewton.Size = new Size(414, 34);
            txtFuncionNewton.TabIndex = 5;
            // 
            // txtVl
            // 
            txtVl.Anchor = AnchorStyles.Top;
            txtVl.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtVl.Location = new Point(757, 70);
            txtVl.Name = "txtVl";
            txtVl.Size = new Size(185, 34);
            txtVl.TabIndex = 7;
            // 
            // txtTolNewton
            // 
            txtTolNewton.Anchor = AnchorStyles.Top;
            txtTolNewton.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTolNewton.Location = new Point(757, 126);
            txtTolNewton.Name = "txtTolNewton";
            txtTolNewton.Size = new Size(185, 34);
            txtTolNewton.TabIndex = 8;
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalcular.Location = new Point(1148, 30);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(172, 78);
            btnCalcular.TabIndex = 10;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // dgvNewton
            // 
            dgvNewton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvNewton.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNewton.BackgroundColor = SystemColors.ActiveCaption;
            dgvNewton.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNewton.Location = new Point(12, 204);
            dgvNewton.Name = "dgvNewton";
            dgvNewton.RowHeadersWidth = 51;
            dgvNewton.Size = new Size(1662, 234);
            dgvNewton.TabIndex = 11;
            // 
            // lblRaiz
            // 
            lblRaiz.Anchor = AnchorStyles.Top;
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(972, 129);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(65, 31);
            lblRaiz.TabIndex = 12;
            lblRaiz.Text = "Raiz:";
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Location = new Point(1326, 30);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(188, 78);
            btnExportar.TabIndex = 13;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnGraficar
            // 
            btnGraficar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGraficar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGraficar.Location = new Point(1520, 30);
            btnGraficar.Name = "btnGraficar";
            btnGraficar.Size = new Size(154, 78);
            btnGraficar.TabIndex = 14;
            btnGraficar.Text = "Graficar";
            btnGraficar.UseVisualStyleBackColor = true;
            btnGraficar.Click += btnGraficar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.Location = new Point(1520, 115);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(154, 71);
            btnLimpiar.TabIndex = 15;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // FormNewton
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1686, 450);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGraficar);
            Controls.Add(btnExportar);
            Controls.Add(lblRaiz);
            Controls.Add(dgvNewton);
            Controls.Add(btnCalcular);
            Controls.Add(txtTolNewton);
            Controls.Add(txtVl);
            Controls.Add(txtFuncionNewton);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "FormNewton";
            Text = "FormNewton";
            ((System.ComponentModel.ISupportInitialize)dgvNewton).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label3;
        private Label label4;
        private TextBox txtFuncionNewton;
        private TextBox txtVl;
        private TextBox txtTolNewton;
        private Button btnCalcular;
        private DataGridView dgvNewton;
        private Label lblRaiz;
        private Button btnExportar;
        private Button btnGraficar;
        private Button btnLimpiar;
    }
}