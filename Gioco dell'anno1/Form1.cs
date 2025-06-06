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
        public string nome_giocatore;

       //variabili form1
        int check_inserimento = 0;
        int check_nome = 0;
        public Form1()
        {
            InitializeComponent();

            // Percorso corretto della cartella "Images"
            // Imposta lo sfondo del form
            this.BackgroundImage = Image.FromFile("Sfondo_Africa.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            
        }

      
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Program.VariabiliGlobali.NomeUtente != textBox_Nome.Text)
            {
                MessageBox.Show("Devi premere inserisci prima di giocare");
            }
            else
            {
                if (check_nome == 0)
                {
                    MessageBox.Show("Devi inserire prima il nome");
                }
                else
                {
                    Form2 f2 = new Form2(this);  // Passa "this", cioè l'istanza attuale di Form1
                    f2.Show();
                    this.Hide();  // Nasconde Form1
                }
            }
            
            
        }
      
        private void button_InserisciNome_Click_1(object sender, EventArgs e)
        {
            if (Program.VariabiliGlobali.NomeUtente == textBox_Nome.Text)
            {
                MessageBox.Show("Hai già inserito il nome!!");
            }
            else
            {
                    string parola = textBox_Nome.Text;
                    StreamReader fileR = new StreamReader("Salvataggi.txt");
                    string[] righe = File.ReadAllLines("Salvataggi.txt");
                    fileR.Close();
                    for (int i = 0; i < righe.Length; i++)
                    {
                        if (righe[i].Contains(parola))
                        {
                            Program.VariabiliGlobali.Check = i;
                            Program.VariabiliGlobali.CheckE = true;
                            break;
                        }
                    }

                    check_nome = 1;
                    Program.VariabiliGlobali.NomeUtente = textBox_Nome.Text;
                
            }
           
        }
    }
}