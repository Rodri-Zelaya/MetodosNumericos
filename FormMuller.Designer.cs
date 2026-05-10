namespace Métodos_Numéricos
{
    partial class FormMuller
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
            txtX1 = new TextBox();
            txtX0 = new TextBox();
            txtFuncionMuller = new TextBox();
            txtX2 = new TextBox();
            txtTolerancia = new TextBox();
            lblRaiz = new Label();
            dgvMuller = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnCalcular = new Button();
            btnExportar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvMuller).BeginInit();
            SuspendLayout();
            // 
            // txtX1
            // 
            txtX1.Anchor = AnchorStyles.Top;
            txtX1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtX1.Location = new Point(510, 48);
            txtX1.Name = "txtX1";
            txtX1.Size = new Size(125, 34);
            txtX1.TabIndex = 0;
            // 
            // txtX0
            // 
            txtX0.Anchor = AnchorStyles.Top;
            txtX0.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtX0.Location = new Point(510, 6);
            txtX0.Name = "txtX0";
            txtX0.Size = new Size(125, 34);
            txtX0.TabIndex = 1;
            // 
            // txtFuncionMuller
            // 
            txtFuncionMuller.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFuncionMuller.Location = new Point(130, 47);
            txtFuncionMuller.Name = "txtFuncionMuller";
            txtFuncionMuller.Size = new Size(374, 34);
            txtFuncionMuller.TabIndex = 2;
            // 
            // txtX2
            // 
            txtX2.Anchor = AnchorStyles.Top;
            txtX2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtX2.Location = new Point(510, 92);
            txtX2.Name = "txtX2";
            txtX2.Size = new Size(125, 34);
            txtX2.TabIndex = 3;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Anchor = AnchorStyles.Top;
            txtTolerancia.Location = new Point(844, 26);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(192, 27);
            txtTolerancia.TabIndex = 4;
            // 
            // lblRaiz
            // 
            lblRaiz.Anchor = AnchorStyles.Top;
            lblRaiz.AutoSize = true;
            lblRaiz.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRaiz.Location = new Point(736, 82);
            lblRaiz.Name = "lblRaiz";
            lblRaiz.Size = new Size(65, 31);
            lblRaiz.TabIndex = 5;
            lblRaiz.Text = "Raiz:";
            // 
            // dgvMuller
            // 
            dgvMuller.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMuller.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMuller.BackgroundColor = SystemColors.ActiveCaption;
            dgvMuller.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMuller.Location = new Point(12, 134);
            dgvMuller.Name = "dgvMuller";
            dgvMuller.RowHeadersWidth = 51;
            dgvMuller.Size = new Size(1321, 306);
            dgvMuller.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(24, 48);
            label1.Name = "label1";
            label1.Size = new Size(100, 31);
            label1.TabIndex = 7;
            label1.Text = "Función";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(462, 9);
            label2.Name = "label2";
            label2.Size = new Size(42, 31);
            label2.TabIndex = 8;
            label2.Text = "X0";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(462, 51);
            label3.Name = "label3";
            label3.Size = new Size(42, 31);
            label3.TabIndex = 9;
            label3.Text = "X1";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(462, 95);
            label4.Name = "label4";
            label4.Size = new Size(42, 31);
            label4.TabIndex = 10;
            label4.Text = "X2";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(715, 26);
            label5.Name = "label5";
            label5.Size = new Size(123, 31);
            label5.TabIndex = 11;
            label5.Text = "Tolerancia";
            // 
            // btnCalcular
            // 
            btnCalcular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCalcular.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalcular.Location = new Point(987, 6);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(159, 66);
            btnCalcular.TabIndex = 12;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnExportar
            // 
            btnExportar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportar.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.Location = new Point(1155, 6);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(178, 66);
            btnExportar.TabIndex = 13;
            btnExportar.Text = "Exportar Excel";
            btnExportar.UseVisualStyleBackColor = true;
            btnExportar.Click += btnExportar_Click;
            // 
            // FormMuller
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1345, 467);
            Controls.Add(btnExportar);
            Controls.Add(btnCalcular);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvMuller);
            Controls.Add(lblRaiz);
            Controls.Add(txtTolerancia);
            Controls.Add(txtX2);
            Controls.Add(txtFuncionMuller);
            Controls.Add(txtX0);
            Controls.Add(txtX1);
            Name = "FormMuller";
            Text = "FormMuller";
            ((System.ComponentModel.ISupportInitialize)dgvMuller).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtX1;
        private TextBox txtX0;
        private TextBox txtFuncionMuller;
        private TextBox txtX2;
        private TextBox txtTolerancia;
        private Label lblRaiz;
        private DataGridView dgvMuller;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnCalcular;
        private Button btnExportar;
    }
}