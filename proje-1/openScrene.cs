using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje_1
{
    public partial class openScrene : Form
    {
        public openScrene()
        {
            InitializeComponent();
        }

        private void openScrene_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gameScreen ayar = new gameScreen();
            ayar.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            settings ayar = new settings();
            ayar.Show();
            this.Hide();
        }

        private void openScrene_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void openScrene_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
