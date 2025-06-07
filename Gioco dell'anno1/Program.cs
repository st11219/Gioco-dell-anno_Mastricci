using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gioco_dell_anno1
{

    static class Program
    {
        public static class VariabiliGlobali
        {
            public static string NomeUtente;
            public static int Punteggio;
            public static int Check;
            public static bool CheckE;
        }
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {        
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            wApplication.Run(new Form1());
        }
    }
}
