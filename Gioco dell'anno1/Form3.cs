using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Gioco_dell_anno1
{
    public partial class Form3 : Form
    {
        #region VARIABILI
        Random rnd = new Random();
        Random rnd2 = new Random();
        // Velocità verticale della sprite (verso l'alto o verso il basso)
        private float velocityY = 0;


        // Posizione del personaggio
        private int playerX = 200;
        private int playerY = 687;
        private int playerHeigth = 200;
        private int playerWidth = 177;
        bool playerV = true;

        private int NuvolaX = 1300;
        private int NuvolaY = 0;
        private int NuvolaHeigth = 200;
        private int NuvolaWidth = 2800;
        bool NuvolaV = true;

        private int Piccione2X = 1300;
        private int Piccione2Y = 600;
        private int Piccione2Heigth = 164;
        private int Piccione2Width = 272;
        bool Piccione2V = true;

        private int PiccioneX = 1300;
        private int PiccioneY = 600;
        private int PiccioneHeigth = 214;
        private int PiccioneWidth = 322;
        bool PiccioneV = true;


        private Form1 formChiamante;


        // Gravità e forza del salto
        private float gravity = 4.2f;
        private float jumpStrength = -65f;

        Image sergio = Image.FromFile("Sprite_running1.png");
        int tempo_camminata = 0;

        public int Rnd = 0;
        public int Rnd2 = 0;
        public int Rnd3 = 0;
        public bool check = false;
        public bool check2 = false;
        public bool check3 = false;
        public int doublejump = 0;

        public int check_punteggio = 0;
        public int Punteggio = 500;

#endregion
        public Form3(Form1 chiamante)
        {
            InitializeComponent();
            formChiamante = chiamante;
            this.DoubleBuffered = true;
            // Abilita la ricezione degli eventi da tastiera
            this.KeyPreview = true;
            // Collega l'evento KeyDown (pressione tasto)
            this.KeyDown += form3_KeyDown;
            // Avvia il timer creato dal designer
            timer1.Start();
            timer2.Start();
            // Impostazioni per il fullscreen forzato
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

            label_punteggio.Location = new Point(0, 1000);
        }

        Image SfondoL2 = Image.FromFile("ocean.png");
        Image LampedusaLine = Image.FromFile("LampedusaLine.png");
        Image Nuvola = Image.FromFile("Nuvola2.png");
        Image Piccione = Image.FromFile("Piccione.png");
        Image Piccione2 = Image.FromFile("Piccione.png");

        #region Paint Event
        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if (Punteggio > 100)
                canvas.DrawImage(SfondoL2, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

            if (playerV == true)
            {
                canvas.DrawImage(sergio, playerX, playerY, playerWidth, playerHeigth);
                canvas.DrawImage(LampedusaLine, 0, 585, 450, 445);
            }
            if (NuvolaV == true)
            {
                canvas.DrawImage(Nuvola, NuvolaX, NuvolaY, NuvolaWidth, NuvolaHeigth);
            }
            if (PiccioneV == true)
            {
                canvas.DrawImage(Piccione, PiccioneX, PiccioneY, PiccioneWidth, PiccioneHeigth);
            }
            if (Piccione2V == true)
            {
                canvas.DrawImage(Piccione2, Piccione2X, Piccione2Y, Piccione2Width, Piccione2Heigth);
            }
        }
        #endregion

        #region Tasti Premuti
        private void form3_KeyDown(object sender, KeyEventArgs e)
        {
            // Se si preme W e non si sta già saltando
            if ((e.KeyCode == Keys.W || e.KeyCode == Keys.Space || e.KeyCode == Keys.Up) && doublejump < 3)
            {
                velocityY = jumpStrength; // Salta verso l’alto
                doublejump++;

            }

            if (e.KeyCode == Keys.Escape)
            {
                formChiamante.Show();  // Mostra Form1
                this.Close(); // Chiude la finestra
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            check_punteggio++;
            if (check_punteggio == 5)
            {
                Punteggio += 10;
                check_punteggio = 0;
            }

            label_punteggio.Text = "Il tuo punteggio è: " + Punteggio;

            
                switch (tempo_camminata)
                {
                    case 1:
                        sergio = Image.FromFile("Sprite_running1.png");

                        break;
                    case 2:
                        sergio = Image.FromFile("Sprite_running2.png");

                        break;
                    case 3:
                        sergio = Image.FromFile("Sprite_running3.png");

                        break;
                }
                if (velocityY > 0)
                    sergio = Image.FromFile("Sprite_Jump2.png");
                else sergio = Image.FromFile("Sprite_Jump.png");

                // Applica la gravità
                velocityY += gravity;

                // Aggiorna la posizione verticale
                playerY += (int)velocityY;

                int groundY = 687;

                // Se la sprite tocca terra, resetta il salto
                if (playerY >= groundY)
                {
                    sergio = Image.FromFile("Sprite_running1.png");
                    playerY = groundY;
                    velocityY = 0;
                    doublejump = 0;
                }
            
                NuvolaX -= 10;
                if (NuvolaX <= -2250)
                {
                    NuvolaX = this.ClientSize.Width + 1;
                }

                if (check == true)
                {
                    PiccioneX -= 35;
                    if (PiccioneX <= -150)
                    {
                        check = false;
                        PiccioneX = this.ClientSize.Width + 1;
                    }
                }

                if (check2 == true)
                {
                    Piccione2X -= 35;
                    if (Piccione2X <= -150)
                    {
                        check2 = false;
                        Piccione2X = this.ClientSize.Width + 1;
                    }
                }

                tempo_camminata++;
                //Collisioni        
                if ((playerX + playerWidth >= PiccioneX && playerY + playerHeigth >= PiccioneY) || (playerX + playerWidth >= Piccione2X && playerY + playerHeigth >= Piccione2Y))
                {
                    timer1.Stop();
                    DialogResult result = MessageBox.Show("Hai perso", " ", MessageBoxButtons.OK);

                    StreamReader fileR = new StreamReader("Salvataggi.txt");
                    string[] righe = File.ReadAllLines("Salvataggi.txt");
                    fileR.Close();

                    if (Program.VariabiliGlobali.CheckE == true)
                    {
                    try
                    {
                        righe[Program.VariabiliGlobali.Check] = null;
                    righe[Program.VariabiliGlobali.Check] = Program.VariabiliGlobali.NomeUtente + ";" + Punteggio;

                     StreamWriter file = new StreamWriter("Salvataggi.txt");

                    File.WriteAllLines("Salvataggi.txt", righe);
                        file.Close();
                    }
                    catch (Exception s)
                    {
                        MessageBox.Show("Tipo di errore: " + s.ToString());
                    }

                   
                }
                else{
                        StreamWriter file = new StreamWriter("Salvataggi.txt", append: true);
                        file.WriteLine(Program.VariabiliGlobali.NomeUtente + ";" + Punteggio);
                        file.Close();
                    }


                    if (result == DialogResult.OK)
                        {
                        formChiamante.Show();
                        this.Close();
                        }
                    }

                    this.Invalidate();
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Rnd <= 0)
            {
                Rnd = rnd.Next(1000, 2001);
                check = true;
            }
            Rnd -= 100;
            if (Rnd2 <= 0)
            {
                Rnd2 = rnd.Next(1000, 2001);
                check2 = true;
            }
            Rnd2 -= 100;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                // Chiudi l'app oppure esci dal fullscreen
                this.Close(); // Oppure metti il codice qui sotto per uscire dal fullscreen
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
