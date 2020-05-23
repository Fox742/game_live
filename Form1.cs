using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLiveProject.Engine;

namespace GameLive
{
    public partial class Form1 : Form
    {
        Game _Game;

        public Form1()
        {
            InitializeComponent();

            _Game = new Game(this.pictureBox1,this.label1,this.button1, this.button2);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _Game.AddCell(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _Game.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Game.StartStop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
