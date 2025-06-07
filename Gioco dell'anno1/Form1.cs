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
    public partial class Form1 : Form
    {
        //variabili form1
        public string nome_giocatore;       
        int check_inserimento = 0;
        int check_nome = 0;
        public Form1()
        {
            
            InitializeComponent();
            // Imposta lo sfondo del form e la permette allungarsi quando si ingrandisce lo schermo
            this.BackgroundImage = Image.FromFile("Sfondo_Africa.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            
        }

      
        private void button1_Click_1(object sender, EventArgs e)
        {
            //se il nome è diverso da quello nella label allora si chiede di premere il tasto inserisci
            if (Program.VariabiliGlobali.NomeUtente != textBox_Nome.Text)
            {
                MessageBox.Show("Devi premere inserisci prima di giocare");
            }
            else
            {   
                //se questa variabile vale 0 significa che si deve riempire la label
                if (check_nome == 0)
                {
                    MessageBox.Show("Devi inserire prima il nome");
                }
                else
                {
                    //apre il nuovo form
                    Form2 f2 = new Form2(this);  
                    f2.Show();
                    this.Hide();  // Nasconde Form1
                }
            }
            
            
        }
      
        private void button_InserisciNome_Click_1(object sender, EventArgs e)
        {
            //se il nome corrisponde al testo della label allora il nome è gia stato inserito
            if (Program.VariabiliGlobali.NomeUtente == textBox_Nome.Text)
            {
                MessageBox.Show("Hai già inserito il nome!!");
            }
            else
            {      
                //assegnazione testo label alla variabile parola
                    string parola = textBox_Nome.Text;
                //apro la lettura del file e assegno tutte le righe ad un array
                    StreamReader fileR = new StreamReader("Salvataggi.txt");
                    string[] righe = File.ReadAllLines("Salvataggi.txt");
                    fileR.Close();//chiudo lettura
                //ciclo per ricercare la parola all'interno del array
                    for (int i = 0; i < righe.Length; i++)
                    {
                    //se e gia presente allora salvo l'indice in una variabile e attivo il checkE
                        if (righe[i].Contains(parola))
                        {
                            Program.VariabiliGlobali.Check = i;
                            Program.VariabiliGlobali.CheckE = true;
                            break;
                        }
                    }
                    //attivo flag e leggo il nome dalla label
                    check_nome = 1;
                    Program.VariabiliGlobali.NomeUtente = textBox_Nome.Text;
                
            }
           
        }
    }
}