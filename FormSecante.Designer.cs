namespace Métodos_Numéricos
{
    partial class FormSecante
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
            txtFuncionSecante = new TextBox();
            txtVI = new TextBox();
            txtV2 = new TextBox();
            txtTolSecante = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnCalcular = new Button();
            dgvSecante = new DataGridView();
            lblRaiz = new Label();
            btnExportar = new Button();
            btnGraficar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSecante).BeginInit();
            SuspendLayout();
            // 
            // txtFuncionSecante
            // 
            txtFuncionSecante.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFuncionSecante.Location = new Point(127, 99);
            txtFuncionSecante.Name = "txtFuncionSecante";
            txtFuncionSecante.Size = new Size(458, 34);
            txtFuncionSecante.TabIndex = 0;
            // 
            // txtVI
            // 
            txtVI.Anchor = AnchorStyles.Top;
            txtVI.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtVI.Location = new Point(769, 65);
            txtVI.Name = "txtVI";
            txtVI.Size = new Size(150, 34);
            txtVI.TabIndex = 1;
            // 
            // txtV2
            // 
            txtV2.Anchor = AnchorStyles.Top;
            txtV2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtV2.Location = new Point(769, 131);
            txtV2.Name = "txtV2";
            txtV2.Size = new Size(150, 34);
            txtV2.TabIndex = 2;
            // 
            // txtTolSecante
            // 
            txtTolSecante.Anchor = AnchorStyles.Top;
            txtTolSecante.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTolSecante.Location = new Point(1074, 65);
            txtTolSecante.Name = "txtTolSecante";
            txtTolSecante.Size = new Size(206, 34);
            txtTolSecante.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 102);
            label1.Name = "label1";
            label1.Size = new Size(109, 31);
            label1.TabIndex = 5;
            label1.Text = "Ecuación";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(610, 68);
            label2.Name = "label2";
            label2.Size = new Size(140, 31);
            label2.TabIndex = 6;
            label2.Text = "Valor Inicial";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(593, 134);
            label3.Name = "label3";
            label3.Size = new Size(170, 31);
            label3.TabIndex = 7;
            label3.Text = "Segundo Valor";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(945, 68);
            label4.Name = "label4";
            label4.Size = new Size(123, 31);
            label4.TabIndex = 8;
            label4.Text = "Tolerancia";
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalcular.Location = new Point(1296, 34);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(156, 74);
            btnCalcular.TabIndex = 10;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // dgvSecante
            // 
            dgvSecante.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSecante.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSecante.BackgroundColor = SystemColors.AppWorkspace;
            dgvSecante.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSecante.Location = new Point(15, 187);
            dgvSecante.Name = "dgvSecante";
            dgvSecante.RowHeadersWidth = 51;
            dgvSecante.Size = new Size(1779, 273);
            dgvSecante.TabIndex = 11;
            // 
            // lblRaiz
            // 
            lblRaiz.Anchor = AnchorStyles.Top;
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(974, 132);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(65, 31);
            lblRaiz.TabIndex = 12;
            lblRaiz.Text = "Raiz:";
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Location = new Point(1458, 34);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(186, 74);
            btnExportar.TabIndex = 13;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // btnGraficar
            // 
            btnGraficar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGraficar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGraficar.Location = new Point(1650, 34);
            btnGraficar.Name = "btnGraficar";
            btnGraficar.Size = new Size(144, 74);
            btnGraficar.TabIndex = 14;
            btnGraficar.Text = "Graficar";
            btnGraficar.UseVisualStyleBackColor = true;
            btnGraficar.Click += btnGraficar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.Location = new Point(1650, 114);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(144, 67);
            btnLimpiar.TabIndex = 15;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // FormSecante
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1806, 472);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGraficar);
            Controls.Add(btnExportar);
            Controls.Add(lblRaiz);
            Controls.Add(dgvSecante);
            Controls.Add(btnCalcular);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTolSecante);
            Controls.Add(txtV2);
            Controls.Add(txtVI);
            Controls.Add(txtFuncionSecante);
            Name = "FormSecante";
            Text = "FormSecante";
            ((System.ComponentModel.ISupportInitialize)dgvSecante).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFuncionSecante;
        private TextBox txtVI;
        private TextBox txtV2;
        private TextBox txtTolSecante;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnCalcular;
        private DataGridView dgvSecante;
        private Label lblRaiz;
        private Button btnExportar;
        private Button btnGraficar;
        private Button btnLimpiar;
    }
}