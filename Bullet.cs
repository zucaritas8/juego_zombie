using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ZombieSurvivalShooter
{
    internal class Bullet
    {
        public string direction;
        public int balaIzquierda;
        public int balaArriba;

        private int velocidad = 20;

        private PictureBox bala;
        private Timer balaTimer;

        private Form juego;

        public void CrearBala(Form form)
        {
            juego = form;

            bala = new PictureBox();

            bala.BackColor = Color.White;
            bala.Size = new Size(5, 5);

            bala.Tag = "bala";

            // mejor posición
            bala.Left = balaIzquierda - 3;
            bala.Top = balaArriba - 3;

            form.Controls.Add(bala);

            // delante de todo
            bala.BringToFront();

            balaTimer = new Timer();

            balaTimer.Interval = 20;
            balaTimer.Tick += balaTimerEvent;
            balaTimer.Start();
        }

        private void balaTimerEvent(object sender, EventArgs e)
        {
            if (bala == null)
            {
                balaTimer.Stop();
                balaTimer.Dispose();
                return;
            }

            // movimiento
            if (direction == "left")
            {
                bala.Left -= velocidad;
            }

            if (direction == "right")
            {
                bala.Left += velocidad;
            }

            if (direction == "up")
            {
                bala.Top -= velocidad;
            }

            if (direction == "down")
            {
                bala.Top += velocidad;
            }

            // límites reales del juego
            if (bala.Left < 0 ||
                bala.Left > juego.ClientSize.Width ||
                bala.Top < 0 ||
                bala.Top > juego.ClientSize.Height)
            {
                balaTimer.Stop();

                if (juego.Controls.Contains(bala))
                {
                    juego.Controls.Remove(bala);
                }

                bala.Dispose();
                balaTimer.Dispose();

                bala = null;
                balaTimer = null;
            }
        }
    }
}