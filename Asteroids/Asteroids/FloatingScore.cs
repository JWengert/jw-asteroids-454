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
    public class FloatingScore : Microsoft.Xna.Framework.GameComponent
    {
        private Vector2 position;
        private TimeSpan timealive;
        private SpriteFont sf;
        public FloatingScore(Game game, Vector2 position, SpriteFont sf)
            : base(game)
        {
            this.position = position;
            this.sf = sf;
            timealive = TimeSpan.Zero;
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
            position.Y -= 2;
            timealive += gameTime.ElapsedGameTime;
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(sf, "10", position, Color.LimeGreen);
        }

        public TimeSpan TimeAlive { get { return timealive; } }
    }
}
