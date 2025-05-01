namespace ConexionDBTest
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CmdPrueba001 = new Button();
            CmdPrueba002 = new Button();
            CmdPrueba003 = new Button();
            CmdUltimoMensajeDeError = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            CmdPrueba004 = new Button();
            label6 = new Label();
            CmdPrueba005 = new Button();
            label7 = new Label();
            CmdPrueba006 = new Button();
            label8 = new Label();
            CmdPrueba007 = new Button();
            label9 = new Label();
            CmdPrueba008 = new Button();
            SuspendLayout();
            // 
            // CmdPrueba001
            // 
            CmdPrueba001.Location = new Point(12, 55);
            CmdPrueba001.Name = "CmdPrueba001";
            CmdPrueba001.Size = new Size(130, 23);
            CmdPrueba001.TabIndex = 0;
            CmdPrueba001.Text = "Prueba001";
            CmdPrueba001.UseVisualStyleBackColor = true;
            CmdPrueba001.Click += CmdPrueba001_Click;
            // 
            // CmdPrueba002
            // 
            CmdPrueba002.Location = new Point(12, 84);
            CmdPrueba002.Name = "CmdPrueba002";
            CmdPrueba002.Size = new Size(130, 23);
            CmdPrueba002.TabIndex = 1;
            CmdPrueba002.Text = "Prueba002";
            CmdPrueba002.UseVisualStyleBackColor = true;
            CmdPrueba002.Click += CmdPrueba002_Click;
            // 
            // CmdPrueba003
            // 
            CmdPrueba003.Location = new Point(12, 113);
            CmdPrueba003.Name = "CmdPrueba003";
            CmdPrueba003.Size = new Size(130, 23);
            CmdPrueba003.TabIndex = 2;
            CmdPrueba003.Text = "Prueba003";
            CmdPrueba003.UseVisualStyleBackColor = true;
            CmdPrueba003.Click += CmdPrueba003_Click;
            // 
            // CmdUltimoMensajeDeError
            // 
            CmdUltimoMensajeDeError.Location = new Point(12, 26);
            CmdUltimoMensajeDeError.Name = "CmdUltimoMensajeDeError";
            CmdUltimoMensajeDeError.Size = new Size(130, 23);
            CmdUltimoMensajeDeError.TabIndex = 3;
            CmdUltimoMensajeDeError.Text = "Ultimo mensaje";
            CmdUltimoMensajeDeError.UseVisualStyleBackColor = true;
            CmdUltimoMensajeDeError.Click += CmdUltimoMensajeDeError_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(148, 30);
            label1.Name = "label1";
            label1.Size = new Size(253, 15);
            label1.TabIndex = 4;
            label1.Text = "Ver el ultimo mensaje de error en el servicio DB";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(148, 59);
            label2.Name = "label2";
            label2.Size = new Size(215, 15);
            label2.TabIndex = 5;
            label2.Text = "insertar un registro en la tabla dbo.Logs";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(148, 88);
            label3.Name = "label3";
            label3.Size = new Size(455, 15);
            label3.TabIndex = 6;
            label3.Text = "Intentar insertar un registro en la tabla dbo.Logs pero con una columna que no existe";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(148, 113);
            label4.Name = "label4";
            label4.Size = new Size(274, 15);
            label4.TabIndex = 7;
            label4.Text = "Contar la cantidad de registros de la tabla dbo.logs";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(148, 142);
            label5.Name = "label5";
            label5.Size = new Size(157, 15);
            label5.TabIndex = 9;
            label5.Text = " Probar ExecuteReaderAsync";
            // 
            // CmdPrueba004
            // 
            CmdPrueba004.Location = new Point(12, 142);
            CmdPrueba004.Name = "CmdPrueba004";
            CmdPrueba004.Size = new Size(130, 23);
            CmdPrueba004.TabIndex = 8;
            CmdPrueba004.Text = "Prueba004";
            CmdPrueba004.UseVisualStyleBackColor = true;
            CmdPrueba004.Click += CmdPrueba004_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(148, 171);
            label6.Name = "label6";
            label6.Size = new Size(106, 15);
            label6.TabIndex = 11;
            label6.Text = "Probar transaccion";
            // 
            // CmdPrueba005
            // 
            CmdPrueba005.Location = new Point(12, 171);
            CmdPrueba005.Name = "CmdPrueba005";
            CmdPrueba005.Size = new Size(130, 23);
            CmdPrueba005.TabIndex = 10;
            CmdPrueba005.Text = "Prueba005";
            CmdPrueba005.UseVisualStyleBackColor = true;
            CmdPrueba005.Click += CmdPrueba005_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(148, 200);
            label7.Name = "label7";
            label7.Size = new Size(151, 15);
            label7.TabIndex = 13;
            label7.Text = "Lllamada a Store procedure";
            // 
            // CmdPrueba006
            // 
            CmdPrueba006.Location = new Point(12, 200);
            CmdPrueba006.Name = "CmdPrueba006";
            CmdPrueba006.Size = new Size(130, 23);
            CmdPrueba006.TabIndex = 12;
            CmdPrueba006.Text = "Prueba006";
            CmdPrueba006.UseVisualStyleBackColor = true;
            CmdPrueba006.Click += CmdPrueba006_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(148, 229);
            label8.Name = "label8";
            label8.Size = new Size(239, 15);
            label8.TabIndex = 15;
            label8.Text = " Probar ExecuteReaderAsync mas Helper Sql";
            // 
            // CmdPrueba007
            // 
            CmdPrueba007.Location = new Point(12, 229);
            CmdPrueba007.Name = "CmdPrueba007";
            CmdPrueba007.Size = new Size(130, 23);
            CmdPrueba007.TabIndex = 14;
            CmdPrueba007.Text = "Prueba007";
            CmdPrueba007.UseVisualStyleBackColor = true;
            CmdPrueba007.Click += CmdPrueba007_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(148, 258);
            label9.Name = "label9";
            label9.Size = new Size(263, 15);
            label9.TabIndex = 17;
            label9.Text = " Probar ExecuteReaderAsync mas Helper Sql Fast";
            // 
            // CmdPrueba008
            // 
            CmdPrueba008.Location = new Point(12, 258);
            CmdPrueba008.Name = "CmdPrueba008";
            CmdPrueba008.Size = new Size(130, 23);
            CmdPrueba008.TabIndex = 16;
            CmdPrueba008.Text = "Prueba008";
            CmdPrueba008.UseVisualStyleBackColor = true;
            CmdPrueba008.Click += CmdPrueba008_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label9);
            Controls.Add(CmdPrueba008);
            Controls.Add(label8);
            Controls.Add(CmdPrueba007);
            Controls.Add(label7);
            Controls.Add(CmdPrueba006);
            Controls.Add(label6);
            Controls.Add(CmdPrueba005);
            Controls.Add(label5);
            Controls.Add(CmdPrueba004);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(CmdUltimoMensajeDeError);
            Controls.Add(CmdPrueba003);
            Controls.Add(CmdPrueba002);
            Controls.Add(CmdPrueba001);
            Name = "Form1";
            Text = "Pruebas Clase ConexionDB";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button CmdPrueba001;
        private Button CmdPrueba002;
        private Button CmdPrueba003;
        private Button CmdUltimoMensajeDeError;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button CmdPrueba004;
        private Label label6;
        private Button CmdPrueba005;
        private Label label7;
        private Button CmdPrueba006;
        private Label label8;
        private Button CmdPrueba007;
        private Label label9;
        private Button CmdPrueba008;
    }
}
