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
        
        public frmSesSel()
        {
            Microsoft.Xna.Framework.GameComponentCollection compcoll = new GameComponentCollection();
            Asteroids.Game1 game = new Asteroids.Game1();
            compcoll.Add(new GamerServicesComponent(game));
            InitializeComponent();
            compcoll[0].Initialize();
            availableSessions = NetworkSession.Find(
               NetworkSessionType.SystemLink, 1, null);
            foreach (AvailableNetworkSession session in availableSessions)
                lstAvail.Items.Add(session);
            SignedInGamer.SignedIn +=
    new EventHandler<SignedInEventArgs>(SignedInGamer_SignedIn);
        }

        void SignedInGamer_SignedIn(object sender, SignedInEventArgs e)
        {
            e.Gamer.Tag = "1";
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
