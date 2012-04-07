using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;

namespace Loader
{
    public partial class frmSesSel : Form
    {
        NetworkSession networkSession;
        AvailableNetworkSessionCollection availableSessions;
        private Asteroids.Game1 game;
        private Thread gamethread;

        public frmSesSel()
        {
           // Microsoft.Xna.Framework.GameComponentCollection compcoll = new GameComponentCollection();
            
          /*  GamerServicesComponent gsc = new GamerServicesComponent(game);
            gsc.Initialize();
            compcoll.Add(gsc);
           */ 
            InitializeComponent();
            //SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(SignedInGamer_SignedIn);
            ThreadStart start = new ThreadStart(target);
            gamethread = new Thread(start);
        }

        void SignedInGamer_SignedIn(object sender, SignedInEventArgs e)
        {
            e.Gamer.Tag = "1";
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {

          
            
            this.Hide();
            gamethread.Start();
           
            gamethread.Join();
            this.Close();
           
            
        }
        public void target()
        {
            game = new Asteroids.Game1();
            game.Run();
            
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            availableSessions = NetworkSession.Find(
                   NetworkSessionType.SystemLink, 1, null);
            foreach (AvailableNetworkSession session in availableSessions)
                lstAvail.Items.Add(session);
        }
    }
}
