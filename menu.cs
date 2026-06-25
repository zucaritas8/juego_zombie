using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZombieSurvivalShooter
{
    public partial class menu : Form
    {
        private int opcion = 0;

        public menu()
        {
            InitializeComponent();

            this.KeyPreview = true;

            this.KeyDown += menu_KeyDown;
            this.Resize += menu_Resize;
            this.Load += menu_Load;
        }

        private void menu_Load(object sender, EventArgs e)
        {
            ActualizarMenu();
            CentrarControles();
        }

        private void menu_Resize(object sender, EventArgs e)
        {
            CentrarControles();
        }

        private void CentrarControles()
        {
            lblTitulo.Left = (ClientSize.Width - lblTitulo.Width) / 2;
            lblTitulo.Top = 70;

            lblNivel1.Left = (ClientSize.Width - lblNivel1.Width) / 2;
            lblNivel1.Top = 230;

            lblNivel2.Left = (ClientSize.Width - lblNivel2.Width) / 2;
            lblNivel2.Top = 310;

            lblNivel3.Left = (ClientSize.Width - lblNivel3.Width) / 2;
            lblNivel3.Top = 390;

            lblSalir.Left = (ClientSize.Width - lblSalir.Width) / 2;
            lblSalir.Top = 470;

            lblControles.Left = (ClientSize.Width - lblControles.Width) / 2;
            lblControles.Top = ClientSize.Height - 220;
        }

        private void ActualizarMenu()
        {
            lblNivel1.ForeColor = Color.White;
            lblNivel2.ForeColor = Color.White;
            lblNivel3.ForeColor = Color.White;
            lblSalir.ForeColor = Color.White;

            switch (opcion)
            {
                case 0:
                    lblNivel1.ForeColor = Color.Lime;
                    break;

                case 1:
                    lblNivel2.ForeColor = Color.Lime;
                    break;

                case 2:
                    lblNivel3.ForeColor = Color.Lime;
                    break;

                case 3:
                    lblSalir.ForeColor = Color.Red;
                    break;
            }
        }

        private void menu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                opcion--;

                if (opcion < 0)
                    opcion = 3;

                ActualizarMenu();
            }

            if (e.KeyCode == Keys.S)
            {
                opcion++;

                if (opcion > 3)
                    opcion = 0;

                ActualizarMenu();
            }

            if (e.KeyCode == Keys.D)
            {
                switch (opcion)
                {
                    case 0:
                        Hide();

                        Game nivel1 = new Game();
                        nivel1.Show();

                        break;

                    case 1:
                        Hide();

                        nivelDos nivel2 = new nivelDos();
                        nivel2.Show();

                        break;

                    case 2:
                        Hide();

                        nivelTres nivel3 = new nivelTres();
                        nivel3.Show();

                        break;

                    case 3:
                        Application.Exit();
                        break;
                }
            }
        }
    }
}