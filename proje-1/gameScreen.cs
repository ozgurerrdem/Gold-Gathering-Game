using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje_1
{
    public partial class gameScreen : Form
    {

        public gameScreen()
        {
            InitializeComponent();
        }

        Button butonBul(String btn_name)
        {
            return panel1.Controls[btn_name] as Button;
        }
        void gizliAltinGorme(int[,] gizlAltin, int oyuncuX, int oyuncuY)
        {
            int uzaklik = 600, kontrol, x = 0, y = 0;
            for (int i = 0; i < gizlAltin.GetLength(1); i++)
            {
                for (int j = 0; j < gizlAltin.GetLength(0); j++)
                {
                    if (gizlAltin[j, i] == 1)
                    {
                        kontrol = Math.Abs(j - oyuncuX) + Math.Abs(i - oyuncuY);
                        if (kontrol < uzaklik)
                        {
                            uzaklik = kontrol;
                            x = j;
                            y = i;
                        }
                    }
                }
            }
            if (x != 0 && y != 0)
            {
                gizliAltinYerleri[x, y] = 0;
                altinYerleri[x, y] = 1;
                dHedef[x, y] = 1;
                gizliAltınSayacı--;
                btn = butonBul("btn-" + y.ToString() + "," + x.ToString());
                btn.Text = altinDegerleri[x, y].ToString();
            }
        }

        (int, int, int) altinUzaklık(int[,] altin, int oyuncuX, int oyuncuY)
        {

            int uzaklik = 600, kontrol;
            int x = 0, y = 0;
            Boolean bol = false;
            for (int i = 0; i < altin.GetLength(1); i++)
            {
                for (int j = 0; j < altin.GetLength(0); j++)
                {
                    if (altin[j, i] == 1)
                    {
                        kontrol = Math.Abs(j - oyuncuX) + Math.Abs(i - oyuncuY);
                        if (kontrol < uzaklik)
                        {
                            uzaklik = kontrol;
                            x = j;
                            y = i;
                            bol = true;
                        }
                    }
                }
            }
            if (bol != true) return (-1, -1, -1);
            else return (x, y, uzaklik);
        }
        (int, int, int, int) karHesapla(int[,] altin, int[,] deger, int oyuncuX, int oyuncuY)
        {
            settings ayar = new settings();
            int uzaklik = 0, kar = -2000, karKontrol, kontrol;
            int x = 0, y = 0;
            Boolean bol = false;
            for (int i = 0; i < deger.GetLength(1); i++)
            {
                for (int j = 0; j < deger.GetLength(0); j++)
                {
                    if (deger[j, i] > 0 && altin[j, i] != 0)
                    {
                        karKontrol = deger[j, i] - ayar.getAdımMaaliyeti();
                        kontrol = Math.Abs(j - oyuncuX) + Math.Abs(i - oyuncuY);
                        while (kontrol > ayar.getAdimSayisi())
                        {
                            kontrol -= ayar.getAdimSayisi();
                            karKontrol -= ayar.getAdımMaaliyeti();
                        }
                        if (karKontrol >= kar)
                        {
                            if (karKontrol == kar)
                            {
                                if (Math.Abs(oyuncuX - x) + Math.Abs(oyuncuY - y) > Math.Abs(j - oyuncuX) + Math.Abs(i - oyuncuY))
                                {
                                    kar = karKontrol;
                                    x = j;
                                    y = i;
                                    uzaklik = Math.Abs(oyuncuX - x) + Math.Abs(oyuncuY - y);
                                    bol = true;
                                }
                            }
                            else
                            {
                                kar = karKontrol;
                                x = j;
                                y = i;
                                uzaklik = Math.Abs(oyuncuX - x) + Math.Abs(oyuncuY - y);
                                bol = true;
                            }
                        }

                    }
                }
            }
            if (bol != true) return (-1, -1, -1, -1);
            else return (x, y, kar, uzaklik);
        }

        Boolean altinKontrol(int[,] altin)
        {
            Boolean a = false;
            for (int i = 0; i < altin.GetLength(1); i++)
            {
                for (int j = 0; j < altin.GetLength(0); j++)
                {
                    if (altin[j, i] == 1)
                    {
                        a = true;
                        break;
                    }
                }
            }
            return a;
        }

        private void gameScreen_Load(object sender, EventArgs e)
        {
            settings ayar = new settings();
            altinDegerleri = new int[ayar.getYatay(), ayar.getDikey()];
            gizliAltinYerleri = new int[ayar.getYatay(), ayar.getDikey()];
            altinYerleri = new int[ayar.getYatay(), ayar.getDikey()];
            dHedef = new int[ayar.getYatay(), ayar.getDikey()];
            panel1.BackColor = Color.Aquamarine;

            for (int i = 0; i < ayar.getDikey(); i++)
            {
                for (int j = 0; j < ayar.getYatay(); j++)
                {
                    altinDegerleri[j, i] = 0;
                    gizliAltinYerleri[j, i] = 0;
                    altinYerleri[j, i] = 0;
                    dHedef[j, i] = 0;
                }
            }
        }
        public static int[,] altinDegerleri;
        public static int[,] gizliAltinYerleri;
        public static int[,] altinYerleri;
        public static int[,] dHedef;
        int altınSayacı = 0;
        int gizliAltınSayacı = 0;
        int altinX = 0;
        int altinY = 0;
        int altinMesafe = 0;
        int aAltin;
        int bAltin;
        int cAltin;
        int dAltin;

        int aToplamAdim = 0;
        int bToplamAdim = 0;
        int cToplamAdim = 0;
        int dToplamAdim = 0;

        int aHarcanan = 0;
        int bHarcanan = 0;
        int cHarcanan = 0;
        int dHarcanan = 0;

        int aToplanan = 0;
        int bToplanan = 0;
        int cToplanan = 0;
        int dToplanan = 0;
        Boolean firstkntrl = false;
        Button btn;




        private void gameScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            openScrene goster = new openScrene();
            goster.Show();
        }

        int aX = 0, aY = 0, bX = 0, bY = 0, cX = 0, cY = 0, dX = 0, dY;

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        bool a = false, b = false, c = false, d = false;
        private void button1_Click(object sender, EventArgs e)
        {
            settings ayar = new settings();
            Random rastgele = new Random();

            if (firstkntrl == false)
            {
                aAltin = ayar.getOyuncuAltini();
                bAltin = ayar.getOyuncuAltini();
                cAltin = ayar.getOyuncuAltini();
                dAltin = ayar.getOyuncuAltini();

                lblA.Text = ayar.getOyuncuAltini().ToString();
                lblB.Text = ayar.getOyuncuAltini().ToString();
                lblC.Text = ayar.getOyuncuAltini().ToString();
                lblD.Text = ayar.getOyuncuAltini().ToString();

                aX = 0;
                aY = 0;
                bX = ayar.getYatay() - 1;
                bY = 0;
                cX = ayar.getYatay() - 1;
                cY = ayar.getDikey() - 1;
                dX = 0;
                dY = ayar.getDikey() - 1;
                int boyut;
                if (ayar.getYatay() > ayar.getDikey())
                {
                    boyut = 680 / ayar.getYatay();
                }
                else
                {
                    boyut = 680 / ayar.getDikey();
                }
                panel1.Width = boyut * ayar.getYatay();
                panel1.Height = boyut * ayar.getDikey();

                for (int i = 0; i < ayar.getDikey(); i++)
                {
                    for (int j = 0; j < ayar.getYatay(); j++)
                    {
                        Button buton = new Button();

                        if (j == aX && i == aY)
                        {
                            buton.Text = "A";
                            buton.BackColor = Color.Red;
                        }
                        else if (j == bX && i == bY)
                        {
                            buton.Text = "B";
                            buton.BackColor = Color.MediumSeaGreen;
                        }
                        else if (j == cX && i == cY)
                        {
                            buton.Text = "C";
                            buton.BackColor = Color.LightGray;
                        }
                        else if (j == dX && i == dY)
                        {
                            buton.Text = "D";
                            buton.BackColor = Color.Cyan;
                        }


                        else if (rastgele.Next(1, 101) <= ayar.getAltinIhtimal() && !(i == 0 && j == 0) && !(i == ayar.getDikey() - 1 && j == 0) && !(i == 0 && j == ayar.getYatay() - 1) && !(i == ayar.getDikey() - 1 && j == ayar.getYatay() - 1))
                        {
                            altinDegerleri[j, i] = rastgele.Next(1, 5) * 5;
                            if (rastgele.Next(1, 101) <= ayar.getGizliAltinIhtimal())//gizli altın
                            {
                                buton.Text = altinDegerleri[j, i].ToString() + "*";
                                gizliAltinYerleri[j, i] = 1;
                                gizliAltınSayacı++;
                            }
                            else//normal altın
                            {
                                buton.Text = altinDegerleri[j, i].ToString();
                                altinYerleri[j, i] = 1;
                                dHedef[j, i] = 1;
                            }
                            altınSayacı++;
                        }
                        buton.Enabled = false;
                        buton.Width = boyut;
                        buton.Height = boyut;
                        buton.Name = "btn-" + i.ToString() + "," + j.ToString();
                        buton.Location = new Point(j * boyut, (i * boyut));
                        this.panel1.Controls.Add(buton);
                        if (!panel1.Controls.ContainsKey(buton.Name))
                        {
                            panel1.Controls.Add(buton);
                        }
                    }
                }
                firstkntrl = true;
            }
            else if (firstkntrl == true)
            {
                string kntrl;

                if (altinKontrol(altinYerleri))
                {
                   
                    if (aAltin > 0)
                    {
                        playA();
                    }
                    else
                    {
                        if (a == false)
                        {
                            MessageBox.Show("A'nin altini bitti", "Uyari!");
                        }
                        a = true;
                    }
                    if (bAltin > 0)
                    {
                        playB();
                    }
                    else
                    {
                        if (b == false)
                        {
                            MessageBox.Show("B'nin altini bitti", "Uyari!");
                        }
                        b = true;
                    }
                    if (cAltin > 0)
                    {
                        playC();
                    }
                    else
                    {
                        if (c == false)
                        {
                            MessageBox.Show("C'nin altini bitti", "Uyari!");
                        }
                        c = true;
                    }
                    
                    if (dAltin > 0)
                    {
                        playD();
                    }
                    else
                    {
                        if (d == false)
                        {
                            MessageBox.Show("D'nin altini bitti", "Uyari!");
                        }
                        d = true;
                    }
                    adimLbl += "\n";

                    lblA.Text = aAltin.ToString();
                    lblB.Text = bAltin.ToString();
                    lblC.Text = cAltin.ToString();
                    lblD.Text = dAltin.ToString();
                }
                else
                {
                    MessageBox.Show("altın bitti", "oyun bitti");
                    dosyaYazdir();
                    adimYazdir();
                }
            }

        }

        private void dosyaYazdir()
        {
            string dosya_yolu = @"metinbelgesi.txt";
            //İşlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);
            //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
            sw.WriteLine("A oyuncusunun toplam adim sayisi:" + aToplamAdim);
            sw.WriteLine("B oyuncusunun toplam adim sayisi:" + bToplamAdim);
            sw.WriteLine("C oyuncusunun toplam adim sayisi:" + cToplamAdim);
            sw.WriteLine("D oyuncusunun toplam adim sayisi:" + dToplamAdim);
            sw.WriteLine("\n");

            sw.WriteLine("A oyuncusunun harcadigi altin sayisi:" + aHarcanan);
            sw.WriteLine("B oyuncusunun harcadigi altin sayisi:" + bHarcanan);
            sw.WriteLine("C oyuncusunun harcadigi altin sayisi:" + cHarcanan);
            sw.WriteLine("D oyuncusunun harcadigi altin sayisi:" + dHarcanan);
            sw.WriteLine("\n");

            sw.WriteLine("A oyuncusunun kasasindaki altin miktari:" + aAltin);
            sw.WriteLine("B oyuncusunun kasasindaki altin miktari:" + bAltin);
            sw.WriteLine("C oyuncusunun kasasindaki altin miktari:" + cAltin);
            sw.WriteLine("D oyuncusunun kasasindaki altin miktari:" + dAltin);
            sw.WriteLine("\n");

            sw.WriteLine("A oyuncusunun topladigi altin miktari:" + aToplanan);
            sw.WriteLine("B oyuncusunun topladigi altin miktari:" + bToplanan);
            sw.WriteLine("C oyuncusunun topladigi altin miktari:" + cToplanan);
            sw.WriteLine("D oyuncusunun topladigi altin miktari:" + dToplanan);
            sw.WriteLine("\n");


            //Dosyaya ekleyeceğimiz iki satırlık yazıyı WriteLine() metodu ile yazacağız.
            sw.Flush();
            //Veriyi tampon bölgeden dosyaya aktardık.
            sw.Close();
            fs.Close();

        }
        private void adimYazdir()
        {
            string abc = @"adimlar.txt";
            //İşlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(abc, FileMode.OpenOrCreate, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(adimLbl);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        string uzaklikKontrol(int ax, int ay, int bx, int by, int cx, int cy, int dx, int dy, int[,] altin)
        {
            List<int> a = new List<int>();
            List<int> b = new List<int>();
            int k = 0;
            string cvp = "";
            for (int i = 0; i < altin.GetLength(1); i++)
            {
                for (int j = 0; j < altin.GetLength(0); j++)
                {
                    if (altin[j, i] == 1)
                    {
                        a.Add(altinUzaklık(altin, ax, ay).Item3);
                        b.Add(altinUzaklık(altin, ax, ay).Item3);
                        a.Add(altinUzaklık(altin, bx, by).Item3);
                        b.Add(altinUzaklık(altin, bx, by).Item3);
                        a.Add(altinUzaklık(altin, cx, cy).Item3);
                        b.Add(altinUzaklık(altin, cx, cy).Item3);
                        a.Add(altinUzaklık(altin, dx, dy).Item3);
                        b.Add(altinUzaklık(altin, dx, dy).Item3);
                        a.Sort();
                        if (a[0] == b[0] && a[0] != -1)
                        {

                        }
                        break;
                    }
                }
            }
            return cvp;
        }

        string adimLbl;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        int aXhdf = 0, aYhdf = 0;
        int bXhdf = 0, bYhdf = 0;
        int cXhdf = 0, cYhdf = 0;
        int tutx, tuty;
        private void playD()
        {
            settings ayar = new settings();

            if (altinX != -1)
            {
                if (altinUzaklık(altinYerleri, dX, dY).Item3 >= altinUzaklık(altinYerleri, aX, aY).Item3)
                {
                    dHedef[aXhdf, aYhdf] = 0;
                }
                if (altinUzaklık(altinYerleri, dX, dY).Item3 >= altinUzaklık(altinYerleri, bX, bY).Item3)
                {
                    dHedef[bXhdf, bYhdf] = 0;
                }
                if (altinUzaklık(altinYerleri, dX, dY).Item3 >= altinUzaklık(altinYerleri, cX, cY).Item3)
                {
                    dHedef[cXhdf, cYhdf] = 0;
                }
                altinX = karHesapla(dHedef, altinDegerleri, dX, dY).Item1;
                altinY = karHesapla(dHedef, altinDegerleri, dX, dY).Item2;
                tutx = dX;
                tuty = dY;
                btn = butonBul("btn-" + altinY.ToString() + "," + altinX.ToString());
                btn.BackColor = Color.Orange;
                Thread.Sleep(500);
                System.Windows.Forms.Application.DoEvents();

                kar = karHesapla(dHedef, altinDegerleri, dX, dY).Item3;
                altinMesafe = karHesapla(dHedef, altinDegerleri, dX, dY).Item4;
                int kalan = 0, tut, tut1;
                if (altinMesafe > ayar.getAdimSayisi())
                {
                    dToplamAdim += 3;
                    dAltin -= ayar.getAdımMaaliyeti();
                    dHarcanan += ayar.getAdımMaaliyeti();
                    btn = butonBul("btn-" + dY.ToString() + "," + dX.ToString());
                    btn.Text = "";
                    btn.BackColor = Color.Aquamarine;
                    if (Math.Abs(altinX - dX) >= ayar.getAdimSayisi())
                    {
                        tut = dX;
                        if (altinX < dX)
                        {
                            dX -= ayar.getAdimSayisi();
                            if (dX < 0)
                            {
                                dX = 0;
                            }
                            for (int i = dX; i < tut; i++)
                            {
                                if (gizliAltinYerleri[i, dY] == 1)
                                {
                                    gizliAltinYerleri[i, dY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, dY] = 1;
                                    btn = butonBul("btn-" + dY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, dY].ToString();
                                    MessageBox.Show(i.ToString() + "," + dY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            dX += ayar.getAdimSayisi();
                            if (dX > ayar.getYatay() - 1)
                            {
                                dX = ayar.getYatay() - 1;
                            }
                            for (int i = tut; i < dX; i++)
                            {
                                if (gizliAltinYerleri[i, dY] == 1)
                                {
                                    gizliAltinYerleri[i, dY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, dY] = 1;
                                    btn = butonBul("btn-" + dY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, dY].ToString();
                                    MessageBox.Show(i.ToString() + "," + dY.ToString() + " görünür oldu");
                                }
                            }
                        }


                        btn = butonBul("btn-" + dY.ToString() + "," + dX.ToString());
                        btn.Text = "D";
                        btn.BackColor = Color.Cyan;
                    }
                    else if (Math.Abs(altinX - dX) < ayar.getAdimSayisi())
                    {
                        kalan = ayar.getAdimSayisi() - Math.Abs(altinX - dX);
                        dX = altinX;
                        tut = dY;
                        if (altinY > dY)
                        {
                            dY += kalan;
                            for (int i = tut; i < dY; i++)
                            {
                                if (gizliAltinYerleri[dX, i] == 1)
                                {
                                    gizliAltinYerleri[dX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[dX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + dX.ToString());
                                    btn.Text = altinDegerleri[dX, i].ToString();
                                    MessageBox.Show(dX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            dY -= kalan;
                            for (int i = dY; i < tut; i++)
                            {
                                if (gizliAltinYerleri[dX, i] == 1)
                                {
                                    gizliAltinYerleri[dX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[dX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + dX.ToString());
                                    btn.Text = altinDegerleri[dX, i].ToString();
                                    MessageBox.Show(dX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        btn = butonBul("btn-" + dY.ToString() + "," + dX.ToString());
                        btn.Text = "D";
                        btn.BackColor = Color.Cyan;

                    }

                    else if (Math.Abs(altinY - dY) >= ayar.getAdimSayisi())
                    {
                        tut = dY;
                        if (altinY < dY)
                        {
                            dY -= ayar.getAdimSayisi();
                            if (dY < 0)
                            {
                                dY = 0;
                            }
                            for (int i = dY; i < tut; i++)
                            {
                                if (gizliAltinYerleri[dX, i] == 1)
                                {
                                    gizliAltinYerleri[dX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[dX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + dX.ToString());
                                    btn.Text = altinDegerleri[dX, i].ToString();
                                    MessageBox.Show(dX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            dY += ayar.getAdimSayisi();
                            if (dY > ayar.getDikey() - 1)
                            {
                                dY = ayar.getDikey() - 1;
                            }
                            for (int i = tut; i < dY; i++)
                            {
                                if (gizliAltinYerleri[dX, i] == 1)
                                {
                                    gizliAltinYerleri[dX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[dX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + dX.ToString());
                                    btn.Text = altinDegerleri[dX, i].ToString();
                                    MessageBox.Show(dX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }


                        btn = butonBul("btn-" + dY.ToString() + "," + dX.ToString());
                        btn.Text = "D";
                        btn.BackColor = Color.Cyan;
                    }
                    else
                    {
                        kalan = ayar.getAdimSayisi() - Math.Abs(altinY - dY);
                        dY = altinY;
                        tut = dX;
                        if (altinX > dX)
                        {
                            dX += kalan;
                            for (int i = tut; i < dX; i++)
                            {
                                if (gizliAltinYerleri[i, dY] == 1)
                                {
                                    gizliAltinYerleri[i, dY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, dY] = 1;
                                    btn = butonBul("btn-" + dY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, dY].ToString();
                                    MessageBox.Show(i.ToString() + "," + dY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            dX -= kalan;
                            for (int i = dX; i < tut; i++)
                            {
                                if (gizliAltinYerleri[i, dY] == 1)
                                {
                                    gizliAltinYerleri[i, dY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, dY] = 1;
                                    btn = butonBul("btn-" + dY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, dY].ToString();
                                    MessageBox.Show(i.ToString() + "," + dY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        btn = butonBul("btn-" + dY.ToString() + "," + dX.ToString());
                        btn.Text = "D";
                        btn.BackColor = Color.Cyan;
                    }
                }
                else
                {
                    dAltin -= ayar.getAdımMaaliyeti();
                    dAltin -= ayar.getDMaaliyet();
                    dHarcanan += ayar.getAdımMaaliyeti();
                    dHarcanan += ayar.getDMaaliyet();
                    dAltin += altinDegerleri[altinX, altinY];
                    dToplanan += altinDegerleri[altinX, altinY];
                    dToplamAdim += altinUzaklık(altinYerleri, dX, dY).Item3;
                    altınSayacı--;
                    btn = butonBul("btn-" + altinY.ToString() + "," + altinX.ToString());
                    btn.Text = "D";
                    btn.BackColor = Color.Cyan;
                    btn = butonBul("btn-" + dY.ToString() + "," + dX.ToString());
                    btn.Text = "";
                    btn.BackColor = Color.Aquamarine;
                    tut = dX;
                    dX = altinX;
                    if (tut > dX)
                    {
                        for (int i = dX; i < tut; i++)
                        {
                            if (gizliAltinYerleri[i, dY] == 1)
                            {
                                gizliAltinYerleri[i, dY] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[i, dY] = 1;
                                btn = butonBul("btn-" + dY.ToString() + "," + i.ToString());
                                btn.Text = altinDegerleri[i, dY].ToString();
                                MessageBox.Show(i.ToString() + "," + dY.ToString() + " görünür oldu");
                            }
                        }
                    }
                    else
                    {
                        for (int i = tut; i < dX; i++)
                        {
                            if (gizliAltinYerleri[i, dY] == 1)
                            {
                                gizliAltinYerleri[i, dY] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[i, dY] = 1;
                                btn = butonBul("btn-" + dY.ToString() + "," + i.ToString());
                                btn.Text = altinDegerleri[i, dY].ToString();
                                MessageBox.Show(i.ToString() + "," + dY.ToString() + " görünür oldu");
                            }
                        }
                    }
                    tut = dY;
                    dY = altinY;
                    if (tut > dY)
                    {
                        for (int i = dY; i < tut; i++)
                        {
                            if (gizliAltinYerleri[dX, i] == 1)
                            {
                                gizliAltinYerleri[dX, i] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[dX, i] = 1;
                                btn = butonBul("btn-" + i.ToString() + "," + dX.ToString());
                                btn.Text = altinDegerleri[dX, i].ToString();
                                MessageBox.Show(dX.ToString() + "," + i.ToString() + " görünür oldu");
                            }
                        }
                    }
                    else
                    {
                        for (int i = tut; i < dY; i++)
                        {
                            if (gizliAltinYerleri[dX, i] == 1)
                            {
                                gizliAltinYerleri[dX, i] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[dX, i] = 1;
                                btn = butonBul("btn-" + i.ToString() + "," + dX.ToString());
                                btn.Text = altinDegerleri[dX, i].ToString();
                                MessageBox.Show(dX.ToString() + "," + i.ToString() + " görünür oldu");
                            }
                        }
                    }
                    altinYerleri[dX, dY] = 0;
                    dHedef[dX, dY] = 0;

                }
                adimLbl += ("D oyuncusu " + tutx + ", " + tuty + " konumundan " + dX + ", " + dY + " konumuna ilerlemistir\n");
            }

        }

        private void playC()
        {

            settings ayar = new settings();

            altinX = karHesapla(altinYerleri, altinDegerleri, cX, cY).Item1;
            altinY = karHesapla(altinYerleri, altinDegerleri, cX, cY).Item2;
            cXhdf = altinX;
            cYhdf = altinY;
            if (altinX != -1)
            {
                for (int i = 0; i < ayar.getCOzelAltinSayisi(); i++)
                {
                    if (gizliAltınSayacı >= 0)
                    {
                        gizliAltinGorme(gizliAltinYerleri, cX, cY);
                    }
                    else break;
                }
                btn = butonBul("btn-" + altinY.ToString() + "," + altinX.ToString());
                btn.BackColor = Color.Orange;
                Thread.Sleep(500);
                System.Windows.Forms.Application.DoEvents();
                tutx = cX;
                tuty = cY;
                kar = karHesapla(altinYerleri, altinDegerleri, cX, cY).Item3;
                altinMesafe = karHesapla(altinYerleri, altinDegerleri, cX, cY).Item4;
                int kalan = 0, tut, tut1;
                if (altinMesafe > ayar.getAdimSayisi())
                {
                    cToplamAdim += 3;
                    cHarcanan += ayar.getAdımMaaliyeti();
                    cAltin -= ayar.getAdımMaaliyeti();
                    btn = butonBul("btn-" + cY.ToString() + "," + cX.ToString());
                    btn.Text = "";
                    btn.BackColor = Color.Aquamarine;
                    if (Math.Abs(altinX - cX) >= ayar.getAdimSayisi())
                    {
                        tut = cX;
                        if (altinX < cX)
                        {
                            cX -= ayar.getAdimSayisi();
                            if (cX < 0)
                            {
                                cX = 0;
                            }
                            for (int i = cX; i < tut; i++)
                            {
                                if (gizliAltinYerleri[i, cY] == 1)
                                {
                                    gizliAltinYerleri[i, cY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, cY] = 1;
                                    btn = butonBul("btn-" + cY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, cY].ToString();
                                    MessageBox.Show(i.ToString() + "," + cY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            cX += ayar.getAdimSayisi();
                            if (cX > ayar.getYatay() - 1)
                            {
                                cX = ayar.getYatay() - 1;
                            }
                            for (int i = tut; i < cX; i++)
                            {
                                if (gizliAltinYerleri[i, cY] == 1)
                                {
                                    gizliAltinYerleri[i, cY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, cY] = 1;
                                    btn = butonBul("btn-" + cY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, cY].ToString();
                                    MessageBox.Show(i.ToString() + "," + cY.ToString() + " görünür oldu");
                                }
                            }
                        }


                        btn = butonBul("btn-" + cY.ToString() + "," + cX.ToString());
                        btn.Text = "C";
                        btn.BackColor = Color.LightGray;
                    }
                    else if (Math.Abs(altinX - cX) < ayar.getAdimSayisi())
                    {
                        kalan = ayar.getAdimSayisi() - Math.Abs(altinX - cX);
                        cX = altinX;
                        tut = cY;
                        if (altinY > cY)
                        {
                            cY += kalan;
                            for (int i = tut; i < cY; i++)
                            {
                                if (gizliAltinYerleri[cX, i] == 1)
                                {
                                    gizliAltinYerleri[cX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[cX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + cX.ToString());
                                    btn.Text = altinDegerleri[cX, i].ToString();
                                    MessageBox.Show(cX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            cY -= kalan;
                            for (int i = cY; i < tut; i++)
                            {
                                if (gizliAltinYerleri[cX, i] == 1)
                                {
                                    gizliAltinYerleri[cX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[cX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + cX.ToString());
                                    btn.Text = altinDegerleri[cX, i].ToString();
                                    MessageBox.Show(cX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        btn = butonBul("btn-" + cY.ToString() + "," + cX.ToString());
                        btn.Text = "C";
                        btn.BackColor = Color.LightGray;

                    }

                    else if (Math.Abs(altinY - cY) >= ayar.getAdimSayisi())
                    {
                        tut = cY;
                        if (altinY < cY)
                        {
                            cY -= ayar.getAdimSayisi();
                            if (cY < 0)
                            {
                                cY = 0;
                            }
                            for (int i = cY; i < tut; i++)
                            {
                                if (gizliAltinYerleri[cX, i] == 1)
                                {
                                    gizliAltinYerleri[cX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[cX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + cX.ToString());
                                    btn.Text = altinDegerleri[cX, i].ToString();
                                    MessageBox.Show(cX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            cY += ayar.getAdimSayisi();
                            if (cY > ayar.getDikey() - 1)
                            {
                                cY = ayar.getDikey() - 1;
                            }
                            for (int i = tut; i < cY; i++)
                            {
                                if (gizliAltinYerleri[cX, i] == 1)
                                {
                                    gizliAltinYerleri[cX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[cX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + cX.ToString());
                                    btn.Text = altinDegerleri[cX, i].ToString();
                                    MessageBox.Show(cX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }


                        btn = butonBul("btn-" + cY.ToString() + "," + cX.ToString());
                        btn.Text = "C";
                        btn.BackColor = Color.LightGray;
                    }
                    else
                    {
                        kalan = ayar.getAdimSayisi() - Math.Abs(altinY - cY);
                        cY = altinY;
                        tut = cX;
                        if (altinX > cX)
                        {
                            cX += kalan;
                            for (int i = tut; i < cX; i++)
                            {
                                if (gizliAltinYerleri[i, cY] == 1)
                                {
                                    gizliAltinYerleri[i, cY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, cY] = 1;
                                    btn = butonBul("btn-" + cY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, cY].ToString();
                                    MessageBox.Show(i.ToString() + "," + cY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            cX -= kalan;
                            for (int i = cX; i < tut; i++)
                            {
                                if (gizliAltinYerleri[i, cY] == 1)
                                {
                                    gizliAltinYerleri[i, cY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, cY] = 1;
                                    btn = butonBul("btn-" + cY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, cY].ToString();
                                    MessageBox.Show(i.ToString() + "," + cY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        btn = butonBul("btn-" + cY.ToString() + "," + cX.ToString());
                        btn.Text = "C";
                        btn.BackColor = Color.LightGray;
                    }
                }
                else
                {
                    cAltin -= ayar.getAdımMaaliyeti();
                    cAltin -= ayar.getCMaaliyet();
                    cHarcanan += ayar.getAdımMaaliyeti();
                    cHarcanan += ayar.getCMaaliyet();
                    cAltin += altinDegerleri[altinX, altinY];
                    cToplanan += altinDegerleri[altinX, altinY];
                    cToplamAdim += altinUzaklık(altinYerleri, cX, cY).Item3;
                    altınSayacı--;
                    btn = butonBul("btn-" + altinY.ToString() + "," + altinX.ToString());
                    btn.Text = "C";
                    btn.BackColor = Color.LightGray;
                    btn = butonBul("btn-" + cY.ToString() + "," + cX.ToString());
                    btn.Text = "";
                    btn.BackColor = Color.Aquamarine;
                    tut = cX;
                    cX = altinX;
                    if (tut > cX)
                    {
                        for (int i = cX; i < tut; i++)
                        {
                            if (gizliAltinYerleri[i, cY] == 1)
                            {
                                gizliAltinYerleri[i, cY] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[i, cY] = 1;
                                btn = butonBul("btn-" + cY.ToString() + "," + i.ToString());
                                btn.Text = altinDegerleri[i, cY].ToString();
                                MessageBox.Show(i.ToString() + "," + cY.ToString() + " görünür oldu");
                            }
                        }
                    }
                    else
                    {
                        for (int i = tut; i < cX; i++)
                        {
                            if (gizliAltinYerleri[i, cY] == 1)
                            {
                                gizliAltinYerleri[i, cY] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[i, cY] = 1;
                                btn = butonBul("btn-" + cY.ToString() + "," + i.ToString());
                                btn.Text = altinDegerleri[i, cY].ToString();
                                MessageBox.Show(i.ToString() + "," + cY.ToString() + " görünür oldu");
                            }
                        }
                    }
                    tut = cY;
                    cY = altinY;
                    if (tut > cY)
                    {
                        for (int i = cY; i < tut; i++)
                        {
                            if (gizliAltinYerleri[cX, i] == 1)
                            {
                                gizliAltinYerleri[cX, i] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[cX, i] = 1;
                                btn = butonBul("btn-" + i.ToString() + "," + cX.ToString());
                                btn.Text = altinDegerleri[cX, i].ToString();
                                MessageBox.Show(cX.ToString() + "," + i.ToString() + " görünür oldu");
                            }
                        }
                    }
                    else
                    {
                        for (int i = tut; i < cY; i++)
                        {
                            if (gizliAltinYerleri[cX, i] == 1)
                            {
                                gizliAltinYerleri[cX, i] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[cX, i] = 1;
                                btn = butonBul("btn-" + i.ToString() + "," + cX.ToString());
                                btn.Text = altinDegerleri[cX, i].ToString();
                                MessageBox.Show(cX.ToString() + "," + i.ToString() + " görünür oldu");
                            }
                        }
                    }
                    altinYerleri[cX, cY] = 0;
                    dHedef[dX, dY] = 0;
                }
                btn = butonBul("btn-" + cY.ToString() + "," + cX.ToString());
                adimLbl += ("C oyuncusu " + tutx + ", " + tuty + " konumundan " + cX + ", " + cY + " konumuna ilerlemistir\n");
            }

        }

        int kar;
        private void playB()
        {

            settings ayar = new settings();
            altinX = karHesapla(altinYerleri, altinDegerleri, bX, bY).Item1;
            altinY = karHesapla(altinYerleri, altinDegerleri, bX, bY).Item2;
            bXhdf = altinX;
            bYhdf = altinY;
            if (altinX != -1)
            {

                btn = butonBul("btn-" + altinY.ToString() + "," + altinX.ToString());
                btn.BackColor = Color.Orange;
                Thread.Sleep(500);
                System.Windows.Forms.Application.DoEvents();
                tutx = bX;
                tuty = bY;
                kar = karHesapla(altinYerleri, altinDegerleri, bX, bY).Item3;
                altinMesafe = karHesapla(altinYerleri, altinDegerleri, bX, bY).Item4;
                int kalan = 0, tut, tut1;
                //MediumSeaGreen
                if (altinMesafe > ayar.getAdimSayisi())
                {
                    bToplamAdim += 3;
                    bHarcanan += ayar.getAdımMaaliyeti();
                    bAltin -= ayar.getAdımMaaliyeti();
                    btn = butonBul("btn-" + bY.ToString() + "," + bX.ToString());
                    btn.Text = "";
                    btn.BackColor = Color.Aquamarine;
                    if (Math.Abs(altinX - bX) >= ayar.getAdimSayisi())
                    {
                        tut = bX;
                        if (altinX < bX)
                        {
                            bX -= ayar.getAdimSayisi();
                            if (bX < 0)
                            {
                                bX = 0;
                            }
                            for (int i = bX; i < tut; i++)
                            {
                                if (gizliAltinYerleri[i, bY] == 1)
                                {
                                    gizliAltinYerleri[i, bY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, bY] = 1;
                                    btn = butonBul("btn-" + bY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, bY].ToString();
                                    MessageBox.Show(i.ToString() + "," + bY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            bX += ayar.getAdimSayisi();
                            if (bX > ayar.getYatay() - 1)
                            {
                                bX = ayar.getYatay() - 1;
                            }
                            for (int i = tut; i < bX; i++)
                            {
                                if (gizliAltinYerleri[i, bY] == 1)
                                {
                                    gizliAltinYerleri[i, bY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, bY] = 1;
                                    btn = butonBul("btn-" + bY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, bY].ToString();
                                    MessageBox.Show(i.ToString() + "," + bY.ToString() + " görünür oldu");
                                }
                            }
                        }


                        btn = butonBul("btn-" + bY.ToString() + "," + bX.ToString());
                        btn.Text = "B";
                        btn.BackColor = Color.MediumSeaGreen;
                    }
                    else if (Math.Abs(altinX - bX) < ayar.getAdimSayisi())
                    {
                        kalan = ayar.getAdimSayisi() - Math.Abs(altinX - bX);
                        bX = altinX;
                        tut = bY;
                        if (altinY > bY)
                        {
                            bY += kalan;
                            for (int i = tut; i < bY; i++)
                            {
                                if (gizliAltinYerleri[bX, i] == 1)
                                {
                                    gizliAltinYerleri[bX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[bX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + bX.ToString());
                                    btn.Text = altinDegerleri[bX, i].ToString();
                                    MessageBox.Show(bX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            bY -= kalan;
                            for (int i = bY; i < tut; i++)
                            {
                                if (gizliAltinYerleri[bX, i] == 1)
                                {
                                    gizliAltinYerleri[bX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[bX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + bX.ToString());
                                    btn.Text = altinDegerleri[bX, i].ToString();
                                    MessageBox.Show(bX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        btn = butonBul("btn-" + bY.ToString() + "," + bX.ToString());
                        btn.Text = "B";
                        btn.BackColor = Color.MediumSeaGreen;

                    }

                    else if (Math.Abs(altinY - bY) >= ayar.getAdimSayisi())
                    {
                        tut = bY;
                        if (altinY < bY)
                        {
                            bY -= ayar.getAdimSayisi();
                            if (bY < 0)
                            {
                                bY = 0;
                            }
                            for (int i = bY; i < tut; i++)
                            {
                                if (gizliAltinYerleri[bX, i] == 1)
                                {
                                    gizliAltinYerleri[bX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[bX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + bX.ToString());
                                    btn.Text = altinDegerleri[bX, i].ToString();
                                    MessageBox.Show(bX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            bY += ayar.getAdimSayisi();
                            if (bY > ayar.getDikey() - 1)
                            {
                                bY = ayar.getDikey() - 1;
                            }
                            for (int i = tut; i < bY; i++)
                            {
                                if (gizliAltinYerleri[bX, i] == 1)
                                {
                                    gizliAltinYerleri[bX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[bX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + bX.ToString());
                                    btn.Text = altinDegerleri[bX, i].ToString();
                                    MessageBox.Show(bX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }


                        btn = butonBul("btn-" + bY.ToString() + "," + bX.ToString());
                        btn.Text = "B";
                        btn.BackColor = Color.MediumSeaGreen;
                    }
                    else
                    {
                        kalan = ayar.getAdimSayisi() - Math.Abs(altinY - bY);
                        bY = altinY;
                        tut = bX;
                        if (altinX > bX)
                        {
                            bX += kalan;
                            for (int i = tut; i < bX; i++)
                            {
                                if (gizliAltinYerleri[i, bY] == 1)
                                {
                                    gizliAltinYerleri[i, bY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, bY] = 1;
                                    btn = butonBul("btn-" + bY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, bY].ToString();
                                    MessageBox.Show(i.ToString() + "," + bY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            bX -= kalan;
                            for (int i = bX; i < tut; i++)
                            {
                                if (gizliAltinYerleri[i, bY] == 1)
                                {
                                    gizliAltinYerleri[i, bY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, bY] = 1;
                                    btn = butonBul("btn-" + bY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, bY].ToString();
                                    MessageBox.Show(i.ToString() + "," + bY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        btn = butonBul("btn-" + bY.ToString() + "," + bX.ToString());
                        btn.Text = "B";
                        btn.BackColor = Color.MediumSeaGreen;
                    }
                }
                else
                {

                    bAltin -= ayar.getAdımMaaliyeti();
                    bAltin -= ayar.getBMaaliyet();
                    bHarcanan += ayar.getAdımMaaliyeti();
                    bHarcanan += ayar.getBMaaliyet();
                    bAltin += altinDegerleri[altinX, altinY];
                    bToplanan += altinDegerleri[altinX, altinY];
                    bToplamAdim += altinUzaklık(altinYerleri, bX, bY).Item3;
                    altınSayacı--;
                    btn = butonBul("btn-" + altinY.ToString() + "," + altinX.ToString());
                    btn.Text = "B";
                    btn.BackColor = Color.MediumSeaGreen;
                    btn = butonBul("btn-" + bY.ToString() + "," + bX.ToString());
                    btn.Text = "";
                    btn.BackColor = Color.Aquamarine;
                    tut = bX;
                    bX = altinX;
                    if (tut > bX)
                    {
                        for (int i = bX; i < tut; i++)
                        {
                            if (gizliAltinYerleri[i, bY] == 1)
                            {
                                gizliAltinYerleri[i, bY] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[i, bY] = 1;
                                btn = butonBul("btn-" + bY.ToString() + "," + i.ToString());
                                btn.Text = altinDegerleri[i, bY].ToString();
                                MessageBox.Show(i.ToString() + "," + bY.ToString() + " görünür oldu");
                            }
                        }
                    }
                    else
                    {
                        for (int i = tut; i < bX; i++)
                        {
                            if (gizliAltinYerleri[i, bY] == 1)
                            {
                                gizliAltinYerleri[i, bY] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[i, bY] = 1;
                                btn = butonBul("btn-" + bY.ToString() + "," + i.ToString());
                                btn.Text = altinDegerleri[i, bY].ToString();
                                MessageBox.Show(i.ToString() + "," + bY.ToString() + " görünür oldu");
                            }
                        }
                    }
                    tut = bY;
                    bY = altinY;
                    if (tut > bY)
                    {
                        for (int i = bY; i < tut; i++)
                        {
                            if (gizliAltinYerleri[bX, i] == 1)
                            {
                                gizliAltinYerleri[bX, i] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[bX, i] = 1;
                                btn = butonBul("btn-" + i.ToString() + "," + bX.ToString());
                                btn.Text = altinDegerleri[bX, i].ToString();
                                MessageBox.Show(bX.ToString() + "," + i.ToString() + " görünür oldu");
                            }
                        }
                    }
                    else
                    {
                        for (int i = tut; i < bY; i++)
                        {
                            if (gizliAltinYerleri[bX, i] == 1)
                            {
                                gizliAltinYerleri[bX, i] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[bX, i] = 1;
                                btn = butonBul("btn-" + i.ToString() + "," + bX.ToString());
                                btn.Text = altinDegerleri[bX, i].ToString();
                                MessageBox.Show(bX.ToString() + "," + i.ToString() + " görünür oldu");
                            }
                        }
                    }
                    altinYerleri[bX, bY] = 0;
                    dHedef[dX, dY] = 0;
                }

                adimLbl += ("B oyuncusu " + tutx + ", " + tuty + " konumundan " + bX + ", " + bY + " konumuna ilerlemistir\n");
            }

        }

        Boolean kntrl = false;
        private void playA()
        {

            settings ayar = new settings();
            altinX = altinUzaklık(altinYerleri, aX, aY).Item1;
            altinY = altinUzaklık(altinYerleri, aX, aY).Item2;
            aXhdf = altinX;
            aYhdf = altinY;
            if (altinX != -1)
            {

                btn = butonBul("btn-" + altinY.ToString() + "," + altinX.ToString());
                btn.BackColor = Color.Orange;
                Thread.Sleep(500);
                System.Windows.Forms.Application.DoEvents();
                altinMesafe = altinUzaklık(altinYerleri, aX, aY).Item3;
                int kalan = 0, tut;
                tutx = aX;
                tuty = aY;
                if (altinMesafe > ayar.getAdimSayisi())
                {
                    aToplamAdim += 3;
                    aAltin -= ayar.getAdımMaaliyeti();
                    aHarcanan += ayar.getAdımMaaliyeti();
                    btn = butonBul("btn-" + aY.ToString() + "," + aX.ToString());
                    btn.Text = "";
                    btn.BackColor = Color.Aquamarine;
                    if (Math.Abs(altinX - aX) >= ayar.getAdimSayisi())
                    {
                        tut = aX;
                        if (altinX < aX)
                        {
                            aX -= ayar.getAdimSayisi();
                            if (aX < 0)
                            {
                                aX = 0;
                            }
                            for (int i = aX; i < tut; i++)
                            {
                                if (gizliAltinYerleri[i, aY] == 1)
                                {
                                    gizliAltinYerleri[i, aY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, aY] = 1;
                                    btn = butonBul("btn-" + aY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, aY].ToString();
                                    MessageBox.Show(i.ToString() + "," + aY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            aX += ayar.getAdimSayisi();
                            if (aX > ayar.getYatay() - 1)
                            {
                                aX = ayar.getYatay() - 1;
                            }
                            for (int i = tut; i < aX; i++)
                            {
                                if (gizliAltinYerleri[i, aY] == 1)
                                {
                                    gizliAltinYerleri[i, aY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, aY] = 1;
                                    btn = butonBul("btn-" + aY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, aY].ToString();
                                    MessageBox.Show(i.ToString() + "," + aY.ToString() + " görünür oldu");
                                }
                            }
                        }


                        btn = butonBul("btn-" + aY.ToString() + "," + aX.ToString());
                        btn.Text = "A";
                        btn.BackColor = Color.Red;
                    }
                    else if (Math.Abs(altinX - aX) < ayar.getAdimSayisi())
                    {
                        kalan = ayar.getAdimSayisi() - Math.Abs(altinX - aX);
                        aX = altinX;
                        tut = aY;
                        if (altinY > aY)
                        {
                            aY += kalan;
                            for (int i = tut; i < aY; i++)
                            {
                                if (gizliAltinYerleri[aX, i] == 1)
                                {
                                    gizliAltinYerleri[aX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[aX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + aX.ToString());
                                    btn.Text = altinDegerleri[aX, i].ToString();
                                    MessageBox.Show(aX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            aY -= kalan;
                            for (int i = aY; i < tut; i++)
                            {
                                if (gizliAltinYerleri[aX, i] == 1)
                                {
                                    gizliAltinYerleri[aX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[aX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + aX.ToString());
                                    btn.Text = altinDegerleri[aX, i].ToString();
                                    MessageBox.Show(aX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        btn = butonBul("btn-" + aY.ToString() + "," + aX.ToString());
                        btn.Text = "A";
                        btn.BackColor = Color.Red;

                    }

                    else if (Math.Abs(altinY - aY) >= ayar.getAdimSayisi())
                    {
                        tut = aY;
                        if (altinY < aY)
                        {
                            aY -= ayar.getAdimSayisi();
                            if (aY < 0)
                            {
                                aY = 0;
                            }
                            for (int i = aY; i < tut; i++)
                            {
                                if (gizliAltinYerleri[aX, i] == 1)
                                {
                                    gizliAltinYerleri[aX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[aX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + aX.ToString());
                                    btn.Text = altinDegerleri[aX, i].ToString();
                                    MessageBox.Show(aX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            aY += ayar.getAdimSayisi();
                            if (aY > ayar.getDikey() - 1)
                            {
                                aY = ayar.getDikey() - 1;
                            }
                            for (int i = tut; i < aY; i++)
                            {
                                if (gizliAltinYerleri[aX, i] == 1)
                                {
                                    gizliAltinYerleri[aX, i] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[aX, i] = 1;
                                    btn = butonBul("btn-" + i.ToString() + "," + aX.ToString());
                                    btn.Text = altinDegerleri[aX, i].ToString();
                                    MessageBox.Show(aX.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        btn = butonBul("btn-" + aY.ToString() + "," + aX.ToString());
                        btn.Text = "A";
                        btn.BackColor = Color.Red;
                    }
                    else
                    {
                        kalan = ayar.getAdimSayisi() - Math.Abs(altinY - aY);
                        aY = altinY;
                        tut = aX;
                        if (altinX > aX)
                        {
                            aX += kalan;
                            for (int i = tut; i < aX; i++)
                            {
                                if (gizliAltinYerleri[i, aY] == 1)
                                {
                                    gizliAltinYerleri[i, aY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, aY] = 1;
                                    btn = butonBul("btn-" + aY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, aY].ToString();
                                    MessageBox.Show(aY.ToString() + "," + i.ToString() + " görünür oldu");
                                }
                            }
                        }
                        else
                        {
                            aX -= kalan;
                            for (int i = aX; i < tut; i++)
                            {
                                if (gizliAltinYerleri[i, aY] == 1)
                                {
                                    gizliAltinYerleri[i, aY] = 0;
                                    gizliAltınSayacı--;
                                    altinYerleri[i, aY] = 1;
                                    btn = butonBul("btn-" + aY.ToString() + "," + i.ToString());
                                    btn.Text = altinDegerleri[i, aY].ToString();
                                    MessageBox.Show(i.ToString() + "," + aY.ToString() + " görünür oldu");
                                }
                            }
                        }
                        btn = butonBul("btn-" + aY.ToString() + "," + aX.ToString());
                        btn.Text = "A";
                        btn.BackColor = Color.Red;
                    }
                }
                else
                {
                    aAltin -= ayar.getAdımMaaliyeti();
                    aAltin -= ayar.getAMaaliyet();
                    aHarcanan += ayar.getAdımMaaliyeti();
                    aHarcanan += ayar.getAMaaliyet();
                    aAltin += altinDegerleri[altinX, altinY];
                    aToplanan += altinDegerleri[altinX, altinY];
                    aToplamAdim += altinUzaklık(altinYerleri, aX, aY).Item3;
                    altınSayacı--;
                    btn = butonBul("btn-" + altinY.ToString() + "," + altinX.ToString());
                    btn.Text = "A";
                    btn.BackColor = Color.Red;
                    btn = butonBul("btn-" + aY.ToString() + "," + aX.ToString());
                    btn.Text = "";
                    btn.BackColor = Color.Aquamarine;
                    tut = aX;
                    aX = altinX;
                    if (tut > aX)
                    {
                        for (int i = aX; i < tut; i++)
                        {
                            if (gizliAltinYerleri[i, aY] == 1)
                            {
                                gizliAltinYerleri[i, aY] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[i, aY] = 1;
                                btn = butonBul("btn-" + aY.ToString() + "," + i.ToString());
                                btn.Text = altinDegerleri[i, aY].ToString();
                                MessageBox.Show(i.ToString() + "," + aY.ToString() + " görünür oldu");
                            }
                        }
                    }
                    else
                    {
                        for (int i = tut; i < aX; i++)
                        {
                            if (gizliAltinYerleri[i, aY] == 1)
                            {
                                gizliAltinYerleri[i, aY] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[i, aY] = 1;
                                btn = butonBul("btn-" + aY.ToString() + "," + i.ToString());
                                btn.Text = altinDegerleri[i, aY].ToString();
                                MessageBox.Show(i.ToString() + "," + aY.ToString() + " görünür oldu");
                            }
                        }
                    }
                    tut = aY;
                    aY = altinY;
                    if (tut > aY)
                    {
                        for (int i = aY; i < tut; i++)
                        {
                            if (gizliAltinYerleri[aX, i] == 1)
                            {
                                gizliAltinYerleri[aX, i] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[aX, i] = 1;
                                btn = butonBul("btn-" + i.ToString() + "," + aX.ToString());
                                btn.Text = altinDegerleri[aX, i].ToString();
                                MessageBox.Show(aX.ToString() + "," + i.ToString() + " görünür oldu");
                            }
                        }
                    }
                    else
                    {
                        for (int i = tut; i < aY; i++)
                        {
                            if (gizliAltinYerleri[aX, i] == 1)
                            {
                                gizliAltinYerleri[aX, i] = 0;
                                gizliAltınSayacı--;
                                altinYerleri[aX, i] = 1;
                                btn = butonBul("btn-" + i.ToString() + "," + aX.ToString());
                                btn.Text = altinDegerleri[aX, i].ToString();
                                MessageBox.Show(aX.ToString() + "," + i.ToString() + " görünür oldu");
                            }
                        }
                    }
                    altinYerleri[aX, aY] = 0;
                    dHedef[dX, dY] = 0;
                }
                adimLbl += ("A oyuncusu " + tutx + ", " + tuty + " konumundan " + aX + ", " + aY + " konumuna ilerlemistir\n");
            }

        }
    }
}