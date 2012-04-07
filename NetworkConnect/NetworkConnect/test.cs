using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Xna.Framework.Net;

namespace NetworkConnect
{
    public partial class test : Form
    {
        Game1 game;
        public test(Game1 game)
        {
            InitializeComponent();
            this.game = game;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart start = new ThreadStart(target);
            Thread gamethread = new Thread(start);

            gamethread.Start();
            this.Close();
            game.Exit();
            gamethread.Join();

        }

        public void target()
        {
            Asteroids.Game1 game = new Asteroids.Game1();
            //game.Run();

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            game.FindGames();

        }

        private void btnHost_Click(object sender, EventArgs e)
        {
            game.HostGame();
        }

        public void CreateSuccess()
        {
            lblStat.Text = "Session Creation Successful";
        }

        internal void PopulateList(IAsyncResult result)
        {
            AvailableNetworkSessionCollection availsessions = NetworkSession.EndFind(result);
            foreach (AvailableNetworkSession session in availsessions)
            {
                lstAvail.Items.Add(session);
            }
        }
    }
}
