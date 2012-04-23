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

        private Vector2 offset;
        private Player player;
        private SpriteFont score;
        public lifebar(Game game)
            : base(game)
        {
           
            // TODO: Construct any child components here
        }

        public lifebar(Game game, Player player, int playernum, Texture2D full, Texture2D empty, SpriteFont score)
            : base(game)
        {
            if (playernum == 1)
                position = new Vector2(10.0f, 50.0f);
            this.full = full;
            this.empty = empty;
            this.player = player;
            this.score = score;
            offset = new Vector2(0, -full.Height);

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
            for (int i = 0; i < player.MaxShields; i++)
            {
                if (player.Shields > i)
                    sb.Draw(full, position + offset * i, Color.White);
                else
                    sb.Draw(empty, position + offset * i, Color.White);
            }
            string stringToDraw = String.Format("Level = " + this.player.Level + "\n" +
                  "Score = " + this.player.Score + "\n"
                + "High Score = " + Game1.HighScore);
            sb.DrawString(score, stringToDraw, position - offset, Color.White);
        }
            
    }
}
