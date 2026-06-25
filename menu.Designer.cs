using System.Drawing;
using System.Windows.Forms;

namespace ZombieSurvivalShooter
{
    partial class menu
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitulo;
        private Label lblNivel1;
        private Label lblNivel2;
        private Label lblNivel3;
        private Label lblSalir;
        private Label lblControles;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblNivel1 = new Label();
            lblNivel2 = new Label();
            lblNivel3 = new Label();
            lblSalir = new Label();
            lblControles = new Label();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Impact", 42F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(801, 85);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "ZOMBIE SURVIVAL SHOOTER";
            // 
            // lblNivel1
            // 
            lblNivel1.AutoSize = true;
            lblNivel1.BackColor = Color.Transparent;
            lblNivel1.Font = new Font("Impact", 30F);
            lblNivel1.ForeColor = Color.Lime;
            lblNivel1.Location = new Point(0, 0);
            lblNivel1.Name = "lblNivel1";
            lblNivel1.Size = new Size(161, 63);
            lblNivel1.TabIndex = 1;
            lblNivel1.Text = "NIVEL 1";
            // 
            // lblNivel2
            // 
            lblNivel2.AutoSize = true;
            lblNivel2.BackColor = Color.Transparent;
            lblNivel2.Font = new Font("Impact", 30F);
            lblNivel2.ForeColor = Color.White;
            lblNivel2.Location = new Point(0, 0);
            lblNivel2.Name = "lblNivel2";
            lblNivel2.Size = new Size(167, 63);
            lblNivel2.TabIndex = 2;
            lblNivel2.Text = "NIVEL 2";
            // 
            // lblNivel3
            // 
            lblNivel3.AutoSize = true;
            lblNivel3.BackColor = Color.Transparent;
            lblNivel3.Font = new Font("Impact", 30F);
            lblNivel3.ForeColor = Color.White;
            lblNivel3.Location = new Point(0, 0);
            lblNivel3.Name = "lblNivel3";
            lblNivel3.Size = new Size(169, 63);
            lblNivel3.TabIndex = 3;
            lblNivel3.Text = "NIVEL 3";
            // 
            // lblSalir
            // 
            lblSalir.AutoSize = true;
            lblSalir.BackColor = Color.Transparent;
            lblSalir.Font = new Font("Impact", 30F);
            lblSalir.ForeColor = Color.White;
            lblSalir.Location = new Point(0, 0);
            lblSalir.Name = "lblSalir";
            lblSalir.Size = new Size(138, 63);
            lblSalir.TabIndex = 4;
            lblSalir.Text = "SALIR";
            // 
            // lblControles
            // 
            lblControles.AutoSize = true;
            lblControles.BackColor = Color.Transparent;
            lblControles.Font = new Font("Consolas", 16F);
            lblControles.ForeColor = Color.Gray;
            lblControles.Location = new Point(0, 0);
            lblControles.Name = "lblControles";
            lblControles.Size = new Size(239, 160);
            lblControles.TabIndex = 5;
            lblControles.Text = "W = SUBIR\r\n\r\nS = BAJAR\r\n\r\nD = SELECCIONAR";
            // 
            // menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.nivel1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1920, 1080);
            Controls.Add(lblTitulo);
            Controls.Add(lblNivel1);
            Controls.Add(lblNivel2);
            Controls.Add(lblNivel3);
            Controls.Add(lblSalir);
            Controls.Add(lblControles);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Zombie Survival Shooter";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}