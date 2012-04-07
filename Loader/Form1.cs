using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            ThreadStart start = new ThreadStart(target);
            Thread gamethread = new Thread(start);
            this.Hide();
            gamethread.Start();
           
            gamethread.Join();
            this.Close();
           
            
        }
        void target()
        {
            Asteroids.Game1 game = new Asteroids.Game1();
            game.Run();
            
        }
    }
}
