namespace Métodos_Numéricos
{
    partial class FormNoLinealNewton
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
            txtTol = new TextBox();
            btnCalcular = new Button();
            label1 = new Label();
            dgvNoLinealNewton = new DataGridView();
            btnExportar = new Button();
            txtFunciones = new TextBox();
            label2 = new Label();
            txtValoresIniciales = new TextBox();
            label3 = new Label();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvNoLinealNewton).BeginInit();
            SuspendLayout();
            // 
            // txtTol
            // 
            txtTol.Anchor = AnchorStyles.Top;
            txtTol.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTol.Location = new Point(1069, 114);
            txtTol.Name = "txtTol";
            txtTol.Size = new Size(175, 34);
            txtTol.TabIndex = 4;
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalcular.Location = new Point(1250, 45);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(193, 71);
            btnCalcular.TabIndex = 5;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(940, 117);
            label1.Name = "label1";
            label1.Size = new Size(123, 31);
            label1.TabIndex = 6;
            label1.Text = "Tolerancia";
            // 
            // dgvNoLinealNewton
            // 
            dgvNoLinealNewton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvNoLinealNewton.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNoLinealNewton.BackgroundColor = SystemColors.ActiveCaption;
            dgvNoLinealNewton.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNoLinealNewton.Location = new Point(9, 253);
            dgvNoLinealNewton.Name = "dgvNoLinealNewton";
            dgvNoLinealNewton.RowHeadersWidth = 51;
            dgvNoLinealNewton.Size = new Size(1782, 206);
            dgvNoLinealNewton.TabIndex = 12;
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Location = new Point(1449, 45);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(206, 71);
            btnExportar.TabIndex = 13;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // txtFunciones
            // 
            txtFunciones.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFunciones.Location = new Point(133, 32);
            txtFunciones.Multiline = true;
            txtFunciones.Name = "txtFunciones";
            txtFunciones.Size = new Size(287, 206);
            txtFunciones.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(-4, 114);
            label2.Name = "label2";
            label2.Size = new Size(131, 31);
            label2.TabIndex = 15;
            label2.Text = "Ecuaciones";
            // 
            // txtValoresIniciales
            // 
            txtValoresIniciales.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtValoresIniciales.Location = new Point(616, 32);
            txtValoresIniciales.Multiline = true;
            txtValoresIniciales.Name = "txtValoresIniciales";
            txtValoresIniciales.Size = new Size(287, 206);
            txtValoresIniciales.TabIndex = 16;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(426, 114);
            label3.Name = "label3";
            label3.Size = new Size(184, 31);
            label3.TabIndex = 17;
            label3.Text = "Valores iniciales";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.Location = new Point(1661, 45);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(143, 71);
            btnLimpiar.TabIndex = 18;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // FormNoLinealNewton
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1806, 474);
            Controls.Add(btnLimpiar);
            Controls.Add(label3);
            Controls.Add(txtValoresIniciales);
            Controls.Add(label2);
            Controls.Add(txtFunciones);
            Controls.Add(btnExportar);
            Controls.Add(dgvNoLinealNewton);
            Controls.Add(label1);
            Controls.Add(btnCalcular);
            Controls.Add(txtTol);
            Name = "FormNoLinealNewton";
            Text = "FormNoLinealNewton";
            ((System.ComponentModel.ISupportInitialize)dgvNoLinealNewton).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtTol;
        private Button btnCalcular;
        private Label label1;
        private DataGridView dgvNoLinealNewton;
        private Button btnExportar;
        private TextBox txtFunciones;
        private Label label2;
        private TextBox txtValoresIniciales;
        private Label label3;
        private Button btnLimpiar;
    }
}