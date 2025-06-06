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
       

        // Velocità verticale della sprite (verso l'alto o verso il basso)
        private float velocityY = 0;
        private bool isJumping = false;

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

        private int BaobabX = 1300;
        private int BaobabY = 600;
        private int BaobabHeigth = 300;
        private int BaobabWidth = 300;
        bool BaobabV = true;

        private int Baobab1X = 1300;
        private int Baobab1Y = 600;
        private int Baobab1Heigth = 300;
        private int Baobab1Width = 300;
        bool Baobab1V = true;

        private int Baobab2X = 1300;
        private int Baobab2Y = 600;
        private int Baobab2Heigth = 300;
        private int Baobab2Width = 300;
        bool Baobab2V = true;

        private int Baobab3X = 1300;
        private int Baobab3Y = 600;
        private int Baobab3Heigth = 300;
        private int Baobab3Width = 300;
        bool Baobab3V = true;
  

        // Gravità e forza del salto
        private float gravity = 4.2f;
        private float jumpStrength = -65f;
        
        Image sergio = Image.FromFile("Sprite_running1.png");     
        int tempo_camminata = 0;
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
            // Percorso dell'immagine relativo alla cartella di output (bin/Debug o bin/Release)
            // Imposta lo sfondo del form
            this.BackgroundImage = Image.FromFile("Sfondo_Africa.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch; // oppure Zoom, Tile, Center, None
            // Impostazioni per il fullscreen forzato
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            //Bitmap spriteTrasparente = RendiTrasparente(Image.FromFile("Sprite_running1.png"));         
        }

        #region RendiTrasparente
        private Bitmap RendiTrasparente(string percorso)
        {
            Bitmap bitmap = new Bitmap(percorso);
            bitmap.MakeTransparent(Color.White);
            return bitmap;
        }
        #endregion

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
            // Se si preme W e non si sta già saltando
            if (e.KeyCode == Keys.W && !isJumping)
            {
                velocityY = jumpStrength; // Salta verso l’alto
                
                isJumping = true;         // Impedisce doppi salti
            }
        }
        #endregion




        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
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
                    if (velocityY>0)
                sergio = Image.FromFile("Sprite_Jump2.png");
            else sergio = Image.FromFile("Sprite_Jump.png");
            // Applica la gravità
            velocityY += gravity;

            

            // Aggiorna la posizione verticale
            playerY += (int)velocityY;

            // Calcola la "terra"
            int groundY = 687;

            // Se la sprite tocca terra, resetta il salto
            if (playerY >= groundY)
            {
                sergio = Image.FromFile("Sprite_running1.png");
                playerY = groundY;
                velocityY = 0;
                isJumping = false;
            }
            
            
            NuvolaX -= 10;
            if(NuvolaX <= -2250)
            {
                NuvolaX = this.ClientSize.Width + 1;
            }
            BaobabX -= 35;
            if (BaobabX <= -150)
            {
                BaobabX = this.ClientSize.Width + 1;
            }
            tempo_camminata++;
            // Aggiorna la posizione della sprite sullo schermo
            //sprite.Location = new Point(playerX, playerY);
            if (playerX + playerWidth >= BaobabX && playerY + playerHeigth >= BaobabY)
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
        }
    }


}
