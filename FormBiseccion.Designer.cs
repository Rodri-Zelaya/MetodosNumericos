namespace Métodos_Numéricos
{
    partial class FormBiseccion
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
            txtFuncionBiseccion = new TextBox();
            txtA = new TextBox();
            txtB = new TextBox();
            txtTolerancia = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            dgvBiseccion = new DataGridView();
            btnCalcular = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBiseccion).BeginInit();
            SuspendLayout();
            // 
            // txtFuncionBiseccion
            // 
            txtFuncionBiseccion.Location = new Point(82, 46);
            txtFuncionBiseccion.Name = "txtFuncionBiseccion";
            txtFuncionBiseccion.Size = new Size(125, 27);
            txtFuncionBiseccion.TabIndex = 0;
            // 
            // txtA
            // 
            txtA.Location = new Point(282, 16);
            txtA.Name = "txtA";
            txtA.Size = new Size(125, 27);
            txtA.TabIndex = 1;
            // 
            // txtB
            // 
            txtB.Location = new Point(282, 67);
            txtB.Name = "txtB";
            txtB.Size = new Size(125, 27);
            txtB.TabIndex = 2;
            // 
            // txtTolerancia
            // 
            txtTolerancia.Location = new Point(488, 16);
            txtTolerancia.Name = "txtTolerancia";
            txtTolerancia.Size = new Size(125, 27);
            txtTolerancia.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 53);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 5;
            label1.Text = "Función";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(226, 23);
            label2.Name = "label2";
            label2.Size = new Size(57, 20);
            label2.TabIndex = 6;
            label2.Text = "Valor A";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(226, 74);
            label3.Name = "label3";
            label3.Size = new Size(56, 20);
            label3.TabIndex = 7;
            label3.Text = "Valor B";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(413, 23);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 8;
            label4.Text = "Tolerancia";
            // 
            // dgvBiseccion
            // 
            dgvBiseccion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBiseccion.Location = new Point(12, 112);
            dgvBiseccion.Name = "dgvBiseccion";
            dgvBiseccion.RowHeadersWidth = 51;
            dgvBiseccion.Size = new Size(776, 326);
            dgvBiseccion.TabIndex = 10;
            // 
            // btnCalcular
            // 
            btnCalcular.Location = new Point(652, 34);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(113, 50);
            btnCalcular.TabIndex = 11;
            btnCalcular.Text = "Calcular";
            btnCalcular.UseVisualStyleBackColor = true;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // FormBiseccion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCalcular);
            Controls.Add(dgvBiseccion);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtTolerancia);
            Controls.Add(txtB);
            Controls.Add(txtA);
            Controls.Add(txtFuncionBiseccion);
            Name = "FormBiseccion";
            Text = "FormBiseccion";
            Load += FormBiseccion_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBiseccion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFuncionBiseccion;
        private TextBox txtA;
        private TextBox txtB;
        private TextBox txtTolerancia;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DataGridView dgvBiseccion;
        private Button btnCalcular;
    }
}