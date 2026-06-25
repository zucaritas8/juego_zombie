using ZombieSurvivalShooter.Properties;

namespace ZombieSurvivalShooter
{
    partial class nivelDos
    {
        /// <summary>
        /// Variable del diseñador.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar recursos.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblBalas = new Label();
            lblKills = new Label();
            label1 = new Label();
            barVida = new ProgressBar();
            player = new PictureBox();
            GameTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            SuspendLayout();
            // 
            // lblBalas
            // 
            lblBalas.AutoSize = true;
            lblBalas.BackColor = Color.Transparent;
            lblBalas.Font = new Font("Nirmala Text", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBalas.ForeColor = Color.White;
            lblBalas.Location = new Point(30, 29);
            lblBalas.Name = "lblBalas";
            lblBalas.Size = new Size(71, 23);
            lblBalas.TabIndex = 0;
            lblBalas.Text = "Balas: 0";
            // 
            // lblKills
            // 
            lblKills.AutoSize = true;
            lblKills.BackColor = Color.Transparent;
            lblKills.Font = new Font("Nirmala Text", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKills.ForeColor = Color.White;
            lblKills.Location = new Point(223, 29);
            lblKills.Name = "lblKills";
            lblKills.Size = new Size(63, 23);
            lblKills.TabIndex = 1;
            lblKills.Text = "Kills: 0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Nirmala Text", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(808, 23);
            label1.Name = "label1";
            label1.Size = new Size(51, 23);
            label1.TabIndex = 2;
            label1.Text = "Vida:";
            // 
            // barVida
            // 
            barVida.Location = new Point(865, 23);
            barVida.Name = "barVida";
            barVida.Size = new Size(212, 29);
            barVida.TabIndex = 3;
            barVida.Value = 100;
            // 
            // player
            // 
            player.BackColor = Color.Transparent;
            player.Image = Resources.up;
            player.Location = new Point(550, 448);
            player.Name = "player";
            player.Size = new Size(71, 100);
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.TabIndex = 4;
            player.TabStop = false;
            // 
            // GameTimer
            // 
            GameTimer.Enabled = true;
            GameTimer.Interval = 30;
            GameTimer.Tick += MainTimerEvent;
            // 
            // nivelDos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = Resources.fondoLab;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1089, 574);
            Controls.Add(player);
            Controls.Add(barVida);
            Controls.Add(label1);
            Controls.Add(lblKills);
            Controls.Add(lblBalas);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "nivelDos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Zombie Survival Shooter";
            WindowState = FormWindowState.Maximized;
            Load += nivelDos_Load;
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBalas;
        private Label lblKills;
        private Label label1;
        private ProgressBar barVida;
        private PictureBox player;
        private System.Windows.Forms.Timer GameTimer;
    }
}