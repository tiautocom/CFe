namespace PVe
{
    partial class frmCancelaItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCancelaItem));
            this.label1 = new System.Windows.Forms.Label();
            this.txtCancelaItem = new System.Windows.Forms.TextBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnCancelaItem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "* Informe Item Desejado";
            // 
            // txtCancelaItem
            // 
            this.txtCancelaItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCancelaItem.Location = new System.Drawing.Point(9, 32);
            this.txtCancelaItem.Name = "txtCancelaItem";
            this.txtCancelaItem.Size = new System.Drawing.Size(202, 29);
            this.txtCancelaItem.TabIndex = 0;
            this.txtCancelaItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCancelaItem_KeyPress);
            this.txtCancelaItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCancelaItem_KeyUp);
            // 
            // btnSair
            // 
            this.btnSair.Image = global::PVe.Properties.Resources.sair;
            this.btnSair.Location = new System.Drawing.Point(136, 75);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 57);
            this.btnSair.TabIndex = 2;
            this.btnSair.Text = "Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnCancelaItem
            // 
            this.btnCancelaItem.Image = global::PVe.Properties.Resources.Carrinho_de_Compra;
            this.btnCancelaItem.Location = new System.Drawing.Point(9, 75);
            this.btnCancelaItem.Name = "btnCancelaItem";
            this.btnCancelaItem.Size = new System.Drawing.Size(85, 57);
            this.btnCancelaItem.TabIndex = 1;
            this.btnCancelaItem.Text = "Remover Item";
            this.btnCancelaItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelaItem.UseVisualStyleBackColor = true;
            this.btnCancelaItem.Click += new System.EventHandler(this.btnCancelaItem_Click);
            // 
            // frmCancelaItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 143);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCancelaItem);
            this.Controls.Add(this.btnCancelaItem);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCancelaItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cancelar Item Venda";
            this.Load += new System.EventHandler(this.frmCancelaItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnCancelaItem;
        private System.Windows.Forms.TextBox txtCancelaItem;


    }
}