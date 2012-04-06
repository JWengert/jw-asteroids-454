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
    public class lifebar : guiitem
    {
        private Texture2D full, empty;
        private Vector2[] pos;
        private Player player;
        public lifebar(Game game)
            : base(game)
        {
           
            // TODO: Construct any child components here
        }

        public lifebar(Game game, Player player, int playernum, Texture2D full, Texture2D empty)
            : base(game)
        {
            if (playernum == 1)
                position = new Vector2(10.0f, 50.0f);
            this.full = full;
            this.empty = empty;
            this.player = player;
            pos = new Vector2[3];
            pos[0] = position;
            pos[1] = position + new Vector2(0, -full.Height);
            pos[2] = pos[1] + new Vector2(0, -full.Height);
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
            for (int i = 0; i < 3; i++)
            {
                if (player.Shields > i)
                    sb.Draw(full, pos[i], Color.White);
                else
                    sb.Draw(empty, pos[i], Color.White);
            }
        }
            
    }
}
