using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Asteroids
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class livesdisplay : guiitem
    {
        private Texture2D icon;
        private Vector2 offset, offset2;
       
        private Player player;
        public livesdisplay(Game game)
            : base(game)
        {
           
            // TODO: Construct any child components here
        }

        public livesdisplay(Game game, Player player, int playernum, Texture2D lives)
            : base(game)
        {
            if (playernum == 1)
                position = new Vector2(70.0f, 10.0f);
            this.icon = lives;
            this.player = player;
            offset = new Vector2(icon.Width, 0);
            offset2 = new Vector2(0, icon.Height);
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content. 
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            for (int i = 0; i < player.Lives; i++)
            {
                    sb.Draw(icon, position + (i%3) * offset + (i/3)*offset2, Color.White);
            }
        }
    }
}
