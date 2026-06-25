using System;
using System.Collections.Generic;
using System.Drawing; 
using System.Linq;
using System.Media;
using System.Windows.Forms;
using ZombieSurvivalShooter.Properties;
using Timer = System.Windows.Forms.Timer;
using System.IO;
using System.Drawing.Text;

namespace ZombieSurvivalShooter
{
    public partial class Game : Form
    {
        bool movDerecha, movIzquierda, movArriba, movAbajo, gameOver;
        bool vidaSpawned = false;

        string facing = "up";

        int vidaJugador = 100;
        int velocidad = 10;
        int balas = 10;
        int velocidadZ = 3;
        int score;
        int bossHits = 0;

        PictureBox powerup;
        bool powerupActivo = false;
        bool speedBoostActivo = false;
        int velocidadBase;
        int powerupSpawnTimer = 0;
        int powerupSpawnDelay;
        int powerupDuracion = 20000;
        int powerupTiempoRestante = 0;

        PictureBox boss;
        ProgressBar barraBoss;
        bool bossSpawned = false;
        int bossVida = 100;
        int bossVelocidad = 2;

        Label lblGameOver;
        Label lblWin;

        Timer timerInicio;
        Label lblInicio;
        int cuentaRegresiva = 3;

        Random RandNum = new Random();
        List<PictureBox> zombiesList = new List<PictureBox>();

        Dictionary<PictureBox, int> vidaZombies = new Dictionary<PictureBox, int>();

        PrivateFontCollection fuentes = new PrivateFontCollection();
        SoundPlayer musica;

        public Game()
        {
            InitializeComponent();

            lblInicio = new Label();

            lblInicio.AutoSize = true;
            lblInicio.Font = new Font("Impact", 80, FontStyle.Bold);
            lblInicio.ForeColor = Color.White;
            lblInicio.BackColor = Color.Transparent;
            lblInicio.Text = "3";
            timerInicio = new Timer();
            timerInicio.Interval = 1000;
            timerInicio.Tick += TimerInicio_Tick;

            this.Controls.Add(lblInicio);
            lblInicio.BringToFront();
            lblInicio.Left = (ClientSize.Width - lblInicio.Width) / 2;
            lblInicio.Top = (ClientSize.Height - lblInicio.Height) / 2;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.DoubleBuffered = true;
            this.KeyPreview = true;

            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);

            player.BackColor = Color.Transparent;

            GameTimer.Interval = 40;

            lblGameOver = new Label();
            lblGameOver.Text = "GAME OVER";
            lblGameOver.ForeColor = Color.Red;
            lblGameOver.Font = new Font("Impact", 48, FontStyle.Bold);
            lblGameOver.AutoSize = true;
            lblGameOver.Visible = false;

            this.Controls.Add(lblGameOver);
            lblGameOver.BringToFront();

            lblWin = new Label();

            lblWin.Text = "YOU WIN!";
            lblWin.ForeColor = Color.Lime;

            lblWin.Font = new Font("Impact", 48, FontStyle.Bold);

            lblWin.AutoSize = true;
            lblWin.Visible = false;

            this.Controls.Add(lblWin);

            CentrarGameOver();

            barraBoss = new ProgressBar();

            barraBoss.Maximum = 100;
            barraBoss.Value = 100;

            barraBoss.Width = 400;
            barraBoss.Height = 30;

            barraBoss.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            barraBoss.Top = 20;
            barraBoss.Left = this.ClientSize.Width - barraBoss.Width - 20;
            barraBoss.Top = 20;

            barraBoss.Visible = false;

            this.Controls.Add(barraBoss);

            ReiniciarJuego();
        }

        // EVENTO PRINCIPAL DEL JUEGO, CONTROL DE COLISIONES, MOVIMIENTO Y POWER-UPS
        private void MainTimerEvent(object sender, EventArgs e)
        {
            powerupSpawnTimer += GameTimer.Interval;

            if (!powerupActivo && powerupSpawnTimer >= powerupSpawnDelay)
            { 
                CrearPowerup();
                powerupSpawnTimer = 0;
                powerupSpawnDelay = bossSpawned ? 10000 : 30000;
            }

            if (gameOver) return;

            if (vidaJugador <= 1)
            {
                GameOver();
                return;
            }

            barVida.Value = vidaJugador;
            lblBalas.Text = "Balas: " + balas;
            lblKills.Text = "Kills: " + score;

            MoverBoss();

            for (int j = this.Controls.Count - 1; j >= 0; j--)
            {
                Control c = this.Controls[j];

                if (c is PictureBox b &&
                    b.Tag != null &&
                    b.Tag.ToString() == "bala")
                {
                    if (boss != null && boss.Bounds.IntersectsWith(b.Bounds))
                    {
                        bossVida -= 3;
                        bossHits++;

                        barraBoss.Value = Math.Max(0, bossVida);


                        if (bossHits % 10 == 0)
                        {
                            bossVelocidad += 3;
                        }

                        this.Controls.Remove(b);
                        b.Dispose();

                        if (bossVida <= 0)
                        {
                            this.Controls.Remove(boss);

                            boss.Dispose();

                            boss = null;

                            barraBoss.Visible = false;

                            WinGame();
                        }
                    }
                }
            }

            if (movIzquierda && player.Left > 0) player.Left -= velocidad;
            if (movDerecha && player.Right < ClientSize.Width) player.Left += velocidad;
            if (movArriba && player.Top > 68) player.Top -= velocidad;
            if (movAbajo && player.Bottom < ClientSize.Height) player.Top += velocidad;

            for (int i = zombiesList.Count - 1; i >= 0; i--)
            {
                PictureBox z = zombiesList[i];

                MoverZombie(z);

                if (player.Bounds.IntersectsWith(z.Bounds))
                {
                    vidaJugador -= 1;

                    if (vidaJugador <= 50 && !vidaSpawned)
                    {
                        if (vidaJugador <= 50)
                        {
                            if (!vidaSpawned)
                            {
                                vidaSpawned = true;
                                CargarVida();
                            }
                        }
                        else
                        {
                            vidaSpawned = false; 
                        }
                    }
                }

                for (int j = this.Controls.Count - 1; j >= 0; j--)
                {
                    Control c = this.Controls[j];

                    if (c is PictureBox b && b.Tag != null && b.Tag.ToString() == "bala")
                    {
                        if (z.Bounds.IntersectsWith(b.Bounds))
                        {
                            if (vidaZombies.ContainsKey(z))
                            {
                                vidaZombies[z]--;
                            }

                            this.Controls.Remove(b);
                            b.Dispose();

                            if (vidaZombies[z] <= 0)
                            {
                                score++;

                                vidaZombies.Remove(z);

                                this.Controls.Remove(z);
                                z.Dispose();

                                zombiesList.RemoveAt(i);

                                if (!bossSpawned)
                                {
                                    velocidadZ = 3 + (score / 20);

                                    if (score == 15)
                                    {
                                        for (int k = 0; k < 3; k++)
                                            CrearZombies();
                                    }

                                    if (score == 20)
                                    {
                                        for (int k = 0; k < 3; k++)
                                            CrearZombies();
                                    }

                                    if (score >= 5)
                                    {
                                        CrearBoss();
                                    }

                                    CrearZombies();
                                }
                            }

                            goto NextZombie;
                        }
                    }
                }

            NextZombie:;
            }

            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                Control c = this.Controls[i];

                if (c is PictureBox pb && pb.Tag != null)
                {
                    string tag = pb.Tag.ToString();

                    if (tag == "balas" && player.Bounds.IntersectsWith(pb.Bounds))
                    {
                        this.Controls.Remove(pb);
                        pb.Dispose();
                        balas += 5;
                    }
                    else if (tag == "vida" && player.Bounds.IntersectsWith(pb.Bounds))
                    {
                        this.Controls.Remove(pb);
                        pb.Dispose();

                        vidaJugador = Math.Min(100, vidaJugador + 20);
                        vidaSpawned = false;
                    }
                    else if (tag == "powerup" && player.Bounds.IntersectsWith(pb.Bounds))
                    {
                        this.Controls.Remove(pb);
                        pb.Dispose();
                        powerup = null;

                        powerupActivo = true;
                        speedBoostActivo = true;

                        velocidad = velocidadBase + 5;

                        powerupTiempoRestante = powerupDuracion;
                    }
                }
            }

            if (speedBoostActivo)
            {
                powerupTiempoRestante -= GameTimer.Interval;

                if (powerupTiempoRestante <= 0)
                {
                    velocidad = velocidadBase;
                    speedBoostActivo = false;
                    powerupActivo = false;
                }
            }
        }

        // CONTROL DE MOVIMIENTO, DISPARO Y REINICIO CON TECLADO

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (musica != null)
                    musica.Stop();

                GameTimer.Stop();

                menu m = new menu();
                m.Show();

                this.Close();

                return;
            }

            if (gameOver) return;

            if (e.KeyCode == Keys.Left)
            {
                movIzquierda = true;
                facing = "left";
                player.Image = Resources.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                movDerecha = true;
                facing = "right";
                player.Image = Resources.right;
            }

            if (e.KeyCode == Keys.Up)
            {
                movArriba = true;
                facing = "up";
                player.Image = Resources.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                movAbajo = true;
                facing = "down";
                player.Image = Resources.down;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) movIzquierda = false;
            if (e.KeyCode == Keys.Right) movDerecha = false;
            if (e.KeyCode == Keys.Up) movArriba = false;
            if (e.KeyCode == Keys.Down) movAbajo = false;

            if (e.KeyCode == Keys.Space && balas > 0 && !gameOver)
            {
                balas--;
                DisparoBala(facing);

                if (balas < 1)
                    CargarBala();
            }

            if (e.KeyCode == Keys.Enter && gameOver)
                ReiniciarJuego();
        }

        // CREACION DE LOS POWER-UPS DE VIDA Y BALAS
        private void CargarBala()
        {
            PictureBox ammo = new PictureBox();

            ammo.Image = Resources.ammo_Image;
            ammo.Size = new Size(40, 40);
            ammo.SizeMode = PictureBoxSizeMode.StretchImage;
            ammo.BackColor = Color.Transparent;

            ammo.Left = RandNum.Next(10, ClientSize.Width - 40);
            ammo.Top = RandNum.Next(60, ClientSize.Height - 40);

            ammo.Tag = "balas";

            this.Controls.Add(ammo);
        }

        private void DisparoBala(string direction)
        {
            Bullet b = new Bullet();
            b.direction = direction;
            b.balaIzquierda = player.Left + (player.Width / 2) - 3;
            b.balaArriba = player.Top + (player.Height / 2) - 3;
            b.CrearBala(this);
        }

        private void CargarVida()
        {
            PictureBox vida = new PictureBox();

            vida.Image = Resources.botiquin;
            vida.Size = new Size(70, 70);
            vida.SizeMode = PictureBoxSizeMode.StretchImage;
            vida.BackColor = Color.Transparent;

            vida.Left = RandNum.Next(50, ClientSize.Width - 60);
            vida.Top = RandNum.Next(80, ClientSize.Height - 60);

            vida.Tag = "vida";

            this.Controls.Add(vida);
        }

        private void CrearPowerup()
        {
            powerup = new PictureBox();

            powerup.Tag = "powerup";
            powerup.Image = Resources.powerup;
            powerup.Size = new Size(60, 60);
            powerup.SizeMode = PictureBoxSizeMode.StretchImage;
            powerup.BackColor = Color.Transparent;

            powerup.Left = RandNum.Next(50, ClientSize.Width - 60);
            powerup.Top = RandNum.Next(80, ClientSize.Height - 60);

            this.Controls.Add(powerup);
            powerup.BringToFront();
        }


        //CREADO DEL ZOMBIE JEFE
        private void CrearBoss()
        {
            bossSpawned = true;
            bossVida = 100;
            barraBoss.Maximum = 100;
            barraBoss.Value = 100;

            boss = new PictureBox();

            boss.Tag = "boss";
            boss.Image = Resources.bossdown1;

            boss.Size = new Size(180, 180);

            boss.Left = RandNum.Next(100, ClientSize.Width - 250);
            boss.Top = RandNum.Next(100, ClientSize.Height - 250);

            boss.SizeMode = PictureBoxSizeMode.StretchImage;
            boss.BackColor = Color.Transparent;

            this.Controls.Add(boss);

            boss.BringToFront();
            player.BringToFront();

            bossVida = 100;

            barraBoss.Value = bossVida;
            barraBoss.Visible = true;

            powerupSpawnTimer = 0;
            powerupSpawnDelay = 10000;
        }

        private void MoverBoss()
        {
            if (boss == null) return;

            int dx = player.Left - boss.Left;
            int dy = player.Top - boss.Top;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                boss.Left += bossVelocidad * Math.Sign(dx);

                if (dx > 0)
                    boss.Image = Resources.bossright1;
                else
                    boss.Image = Resources.bossleft1;
            }
            else
            {
                boss.Top += bossVelocidad * Math.Sign(dy);

                if (dy > 0)
                    boss.Image = Resources.bossdown1;
                else
                    boss.Image = Resources.bossup1;
            }

            if (player.Bounds.IntersectsWith(boss.Bounds))
                vidaJugador -= 2;
        }

        // CREADO DE ZOMBIES
        private void CrearZombies()
        {
            PictureBox zombie = new PictureBox();

            zombie.Tag = "zombie";
            zombie.Image = Resources.zdown;

            zombie.Left = RandNum.Next(0, Math.Max(10, ClientSize.Width - 60));
            zombie.Top = RandNum.Next(60, Math.Max(100, ClientSize.Height - 80));

            zombie.Size = new Size(60, 80);
            zombie.SizeMode = PictureBoxSizeMode.StretchImage;
            zombie.BackColor = Color.Transparent;

            zombiesList.Add(zombie);
            vidaZombies.Add(zombie, 2);

            this.Controls.Add(zombie);

            zombie.BringToFront();
            player.BringToFront();
        }

        private void MoverZombie(PictureBox z)
        {
            int dx = player.Left - z.Left;
            int dy = player.Top - z.Top;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                z.Left += velocidadZ * Math.Sign(dx);
                if (dx > 0)
                    z.Image = Resources.zright;
                else
                    z.Image = Resources.zleft;
            }
            else
            {
                z.Top += velocidadZ * Math.Sign(dy);

                if (dy > 0)
                    z.Image = Resources.zdown;
                else
                    z.Image = Resources.zup;
            }
        }

        // DIBUJADO GAME OVER/WIN

        private void GameOver()
        {
            gameOver = true;

            player.Image = Resources.dead;

            GameTimer.Stop();

            lblGameOver.Visible = true;

            CentrarGameOver();

            lblGameOver.BringToFront();
            lblGameOver.Parent = this;

            this.Controls.SetChildIndex(lblGameOver, 0);

            foreach (PictureBox z in zombiesList)
                z.SendToBack();
        }

        private void WinGame()
        {
            gameOver = true;

            GameTimer.Stop();

            lblWin.Left = (ClientSize.Width - lblWin.Width) / 2;
            lblWin.Top = (ClientSize.Height - lblWin.Height) / 2;

            lblWin.Visible = true;
            lblWin.BringToFront();

            Timer espera = new Timer();
            espera.Interval = 2000;

            espera.Tick += (s, e) =>
            {
                espera.Stop();

                Timer fade = new Timer();
                fade.Interval = 30;

                fade.Tick += (s2, e2) =>
                {
                    this.Opacity -= 0.05;

                    if (this.Opacity <= 0)
                    {
                        fade.Stop();

                        nivelDos nivel2 = new nivelDos();

                        nivel2.WindowState = FormWindowState.Maximized;
                        nivel2.StartPosition = FormStartPosition.Manual;
                        nivel2.Location = this.Location;

                        nivel2.Show();

                        Application.DoEvents();

                        this.Hide();

                        this.Opacity = 1;
                    }
                };

                fade.Start();
            };

            espera.Start();
        }

        // CONTROL DE CUENTA REGRESIVA INICIAL
        private void TimerInicio_Tick(object sender, EventArgs e)
        {
            cuentaRegresiva--;

            if (cuentaRegresiva > 0)
            {
                lblInicio.Text = cuentaRegresiva.ToString();
            }
            else if (cuentaRegresiva == 0)
            {
                lblInicio.Text = "START!";
            }
            else
            {
                timerInicio.Stop();

                lblInicio.Visible = false;

                GameTimer.Start();
            }

            lblInicio.Left = (ClientSize.Width - lblInicio.Width) / 2;
            lblInicio.Top = (ClientSize.Height - lblInicio.Height) / 2;
        }


        // CONFIGURACIÓN INICIAL DEL JUEGO
        private void Game_Load(object sender, EventArgs e)
        {
            this.Focus();

            musica = new SoundPlayer(@"musica\ambiente.wav");
            musica.Load();
            musica.PlayLooping();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            lblInicio.Left = (ClientSize.Width - lblInicio.Width) / 2;
            lblInicio.Top = (ClientSize.Height - lblInicio.Height) / 2;

            this.Resize += Game_Resize;
        }

        private void Game_Resize(object sender, EventArgs e)
        {
            if (barraBoss != null)
            {
                barraBoss.Left = ClientSize.Width - barraBoss.Width - 20;
                barraBoss.Top = 20;
            }

            if (lblInicio != null)
            {
                lblInicio.Left = (ClientSize.Width - lblInicio.Width) / 2;
                lblInicio.Top = (ClientSize.Height - lblInicio.Height) / 2;
            }
        }

        private void CentrarGameOver()
        {
            lblGameOver.Left = (ClientSize.Width - lblGameOver.Width) / 2;
            lblGameOver.Top = (ClientSize.Height - lblGameOver.Height) / 2;
        }

        private void ReiniciarJuego()
        {
            player.Image = Resources.up;
            velocidadBase = velocidad;

            speedBoostActivo = false;
            powerupActivo = false;

            foreach (Control c in this.Controls.Cast<Control>().ToList())
            {
                if (c is PictureBox pb && pb.Tag != null)
                {
                    if (pb.Tag.ToString() == "vida" || pb.Tag.ToString() == "balas")
                    {
                        this.Controls.Remove(pb);
                        pb.Dispose();
                    }
                }
            }

            foreach (PictureBox z in zombiesList)
                this.Controls.Remove(z);

            zombiesList.Clear();
            vidaZombies.Clear();

            for (int i = 0; i < 4; i++)
            {
                PictureBox zombie = new PictureBox();

                zombie.Tag = "zombie";
                zombie.Image = Resources.zdown;
                zombie.Size = new Size(60, 80);
                zombie.SizeMode = PictureBoxSizeMode.StretchImage;
                zombie.BackColor = Color.Transparent;

                do
                {
                    zombie.Left = RandNum.Next(0, ClientSize.Width - zombie.Width);
                    zombie.Top = RandNum.Next(60, ClientSize.Height - zombie.Height);

                } while (
                    Math.Sqrt(
                        Math.Pow(zombie.Left - player.Left, 2) +
                        Math.Pow(zombie.Top - player.Top, 2)
                    ) < 350
                );

                zombiesList.Add(zombie);
                vidaZombies.Add(zombie, 2);
                this.Controls.Add(zombie);

                zombie.BringToFront();
                player.BringToFront();
            }

            movAbajo = movArriba = movDerecha = movIzquierda = false;

            gameOver = false;
            vidaJugador = 100;
            score = 0;
            balas = 10;
            vidaSpawned = false;

            lblGameOver.Visible = false;
            lblWin.Visible = false;

            bossSpawned = false;
            bossVida = 100;
            bossVelocidad = 2;
            bossHits = 0;

            if (boss != null)
            {
                this.Controls.Remove(boss);
                boss.Dispose();
                boss = null;
            }

            powerupSpawnTimer = 0;

            if (bossSpawned)
            {
                powerupSpawnDelay = 10000;
            }
            else
            {
                powerupSpawnDelay = 30000;
            }

            barraBoss.Visible = false;

            GameTimer.Stop();

            cuentaRegresiva = 3;

            lblInicio.Text = "3";
            lblInicio.Visible = true;

            lblInicio.Left = (ClientSize.Width - lblInicio.Width) / 2;
            lblInicio.Top = (ClientSize.Height - lblInicio.Height) / 2;

            timerInicio.Start();
        }
    }
}