namespace PVe
{
    partial class frmQtdeR
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
            this.txtQtdeReimprimir = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtQtdeReimprimir
            // 
            this.txtQtdeReimprimir.Location = new System.Drawing.Point(5, 3);
            this.txtQtdeReimprimir.MaxLength = 1;
            this.txtQtdeReimprimir.Name = "txtQtdeReimprimir";
            this.txtQtdeReimprimir.Size = new System.Drawing.Size(157, 20);
            this.txtQtdeReimprimir.TabIndex = 0;
            this.txtQtdeReimprimir.Text = "1";
            this.txtQtdeReimprimir.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQtdeReimprimir_KeyUp);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(84, 29);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 1;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // frmQtdeR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(171, 61);
            this.ControlBox = false;
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.txtQtdeReimprimir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQtdeR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quantidade Cópia";
            this.Load += new System.EventHandler(this.frmQtdeR_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQtdeReimprimir;
        private System.Windows.Forms.Button btnSalvar;
    }
}