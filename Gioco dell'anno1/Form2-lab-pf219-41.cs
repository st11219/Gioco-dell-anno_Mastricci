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

    public partial class Form2 : Form
    {
        #region Variabili

        
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

        private int Baobab2X = 1300;
        private int Baobab2Y = 600;
        private int Baobab2Heigth = 300;
        private int Baobab2Width = 300;
        bool Baobab2V = true;

        private int BaobabX = 1300;
        private int BaobabY = 600;
        private int BaobabHeigth = 300;
        private int BaobabWidth = 300;
        bool BaobabV = true;

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

        public int Punteggio = 0;

        #endregion

        public Form2()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            // Abilita la ricezione degli eventi da tastiera
            this.KeyPreview = true;
            // Collega l'evento KeyDown (pressione tasto)
            this.KeyDown += Form2_KeyDown;
            // Avvia il timer creato dal designer
            timer1.Start();
            timer2.Start();
            // Percorso dell'immagine relativo alla cartella di output (bin/Debug o bin/Release)
            // Imposta lo sfondo del form
            this.BackgroundImage = Image.FromFile("Sfondo_Africa.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch; // oppure Zoom, Tile, Center, None
            // Impostazioni per il fullscreen forzato
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            //Bitmap spriteTrasparente = RendiTrasparente(Image.FromFile("Sprite_running1.png"));
            
            label_punteggio.Location = new Point(0, 1000);
        }


        #region Funzione_Chiusura_Finestre
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
        #endregion

        #region FunzioneTasti
        // Quando viene premuto un tasto
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            doublejump++;
            // Se si preme W e non si sta già saltando
            if (e.KeyCode == Keys.W && doublejump<3)
            {
                velocityY = jumpStrength; // Salta verso l’alto           
            }
            

        }
        #endregion

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Punteggio+=10;
            label_punteggio.Text = "Il tuo punteggio è: " + Punteggio;

            if (doublejump <= 5)
            {
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
            }
            
                
            
                
            
            
                       
            NuvolaX -= 10;
            if(NuvolaX <= -2250)
            {
                NuvolaX = this.ClientSize.Width + 1;
            }
            
            if (check == true)
            {
                BaobabX -= 35;
                if (BaobabX <= -150)
                {
                    check = false;
                    BaobabX = this.ClientSize.Width + 1;
                }
            }
            if (check2 == true)
            {
                Baobab2X -= 35;
                if (Baobab2X <= -150)
                {
                    check2 = false;
                    Baobab2X = this.ClientSize.Width + 1;
                }
            }
            


            tempo_camminata++;
            // Aggiorna la posizione della sprite sullo schermo
            //sprite.Location = new Point(playerX, playerY);
            if ((playerX + playerWidth >= BaobabX && playerY + playerHeigth >= BaobabY) || (playerX + playerWidth >= Baobab2X && playerY + playerHeigth >= Baobab2Y))
            {
                timer1.Stop();
                DialogResult result = MessageBox.Show("Hai perso"," ",MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    this.Close();          
                }         
            }
            this.Invalidate();           
        }
        
     

       

        Image Nuvola = Image.FromFile("Nuvola2.png");
        Image Baobab = Image.FromFile("Baobab.png");
        Image Baobab1= Image.FromFile("Baobab.png");
        Image Baobab2 = Image.FromFile("Baobab.png");
        Image Baobab3 = Image.FromFile("Baobab.png");

        private void Painteve(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (playerV == true) 
            {
                canvas.DrawImage(sergio,playerX,playerY,playerWidth,playerHeigth);
            }
            if (NuvolaV == true) 
            {
                canvas.DrawImage(Nuvola, NuvolaX, NuvolaY, NuvolaWidth, NuvolaHeigth);
            }
            if (BaobabV == true)
            {
                canvas.DrawImage(Baobab, BaobabX, BaobabY, BaobabWidth, BaobabHeigth);
            }            
            if (Baobab2V == true)
            {
                canvas.DrawImage(Baobab2, Baobab2X, Baobab2Y, Baobab2Width, Baobab2Heigth);
            }
                    
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Rnd <= 0) {
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

       
    }


}
