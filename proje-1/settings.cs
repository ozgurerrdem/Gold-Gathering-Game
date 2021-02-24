using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje_1
{
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
        }
        public static int _dikey = 20;
        public static int _yatay= 20;
        public static int _altinIhtimal = 20;
        public static int _adimMaaliyeti = 5;
        public static int _gizliAltinIhtimal = 10;
        public static int _oyuncuAltini = 200;
        public static int _adimSayisi = 3;
        public static int _aMaaliyet = 5;
        public static int _bMaaliyet = 10;
        public static int _cMaaliyet = 15;
        public static int _cOzelAltinSayisi = 2;
        public static int _dMaaliyet = 20;

        public int getDikey ()
        {
            return _dikey;
        }
        public int getYatay ()
        {
            return _yatay;
        }
        public int getAltinIhtimal ()
        {
            return _altinIhtimal;
        }
        public int getGizliAltinIhtimal()
        {
            return _gizliAltinIhtimal;
        }
        public int getOyuncuAltini()
        {
            return _oyuncuAltini;
        }
        public int getAdimSayisi()
        {
            return _adimSayisi;
        }
        public int getAMaaliyet()
        {
            return _aMaaliyet;
        }
        public int getBMaaliyet()
        {
            return _bMaaliyet;
        }
        public int getCMaaliyet()
        {
            return _cMaaliyet;
        }
        public int getDMaaliyet()
        {
            return _dMaaliyet;
        }
        public int getCOzelAltinSayisi()
        {
            return _cOzelAltinSayisi;
        }
        public int getAdımMaaliyeti()
        {
            return _adimMaaliyeti;
        }
        /*public static int _dikey { get; set; }
        public static int _yatay { get; set; }
        public static int _altinIhtimal { get; set; }
        public static int _adimMaaliyeti { get; set; }
        public static int _gizliAltinIhtimal { get; set; }
        public static int _oyuncuAltini { get; set; }
        public static int _adimSayisi { get; set; }
        public static int _aMaaliyet { get; set; }
        public static int _bMaaliyet { get; set; }
        public static int _cMaaliyet { get; set; }
        public static int _cOzelAltinSayisi { get; set; }
        public static int _dMaaliyet { get; set; }*/


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {            
            if(!(textBox3.Text == "" && textBox4.Text == ""))
            {
                _dikey = Convert.ToInt32(textBox3.Text);
                _yatay = Convert.ToInt32(textBox4.Text);
            }

            if(!(textBox5.Text == ""))
            {
                if(Convert.ToInt32(textBox5.Text) >100 || Convert.ToInt32(textBox5.Text) < 10)
                {
                    MessageBox.Show("Girdiğiniz sayı 10 ile 100 arasında olmalıdır.","Hatalı Giriş Yaptınız");
                }
                _altinIhtimal = Convert.ToInt32(textBox5.Text);
            }
            if (!(textBox13.Text == ""))
            {
                _adimMaaliyeti = Convert.ToInt32(textBox13.Text);
            }
            if (!(textBox6.Text == ""))
            {
                _gizliAltinIhtimal = Convert.ToInt32(textBox6.Text);
            }
            if (!(textBox7.Text == ""))
            {
                _oyuncuAltini = Convert.ToInt32(textBox7.Text);
            }
            if (!(textBox8.Text == ""))
            {
                _adimSayisi = Convert.ToInt32(textBox8.Text);
            }
            if (!(textBox9.Text == ""))
            {
                _aMaaliyet = Convert.ToInt32(textBox9.Text);
            }
            if (!(textBox10.Text == ""))
            {
                _bMaaliyet = Convert.ToInt32(textBox10.Text);
            }
            if (!(textBox11.Text == ""))
            {
                _cMaaliyet = Convert.ToInt32(textBox11.Text);
            }
            if (!(textBox12.Text == ""))
            {
                _cOzelAltinSayisi = Convert.ToInt32(textBox12.Text);
            }
            if (!(textBox14.Text == ""))
            {
                _dMaaliyet = Convert.ToInt32(textBox14.Text);
            }
            gameScreen oyna = new gameScreen();
            oyna.Show();
            this.Hide();
        }

        private void settings_Load(object sender, EventArgs e)
        {

        }

        private void settings_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            openScrene goster = new openScrene();
            goster.Show();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
