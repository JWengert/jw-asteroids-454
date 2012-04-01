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
    public class Asteroid : GameObject
    {
        public Asteroid(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Asteroid(Game game, Texture2D picture, Vector2 startposition, Vector2 velocity)
            : base(game, picture)
        {
            this.position = startposition;
            this.velocity = velocity;
            this.scale = 1;
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
            this.rotation += 0.01f;
            base.Update(gameTime);
        }

        public override bool Collision(GameObject obj)
        {
            return base.Collision(obj);
        }

        public override bool OutofBounds()
        {
            return base.OutofBounds();
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }

        public override void WrapAround()
        {
            // off left
            if (position.X + origin.X < 0)
            {
                position.X = Game.Window.ClientBounds.Width;
            }
            // off right
            else if (position.X + origin.X > Game.Window.ClientBounds.Width)
            {
                position.X = 0;
            }
            // off top
            if (position.Y + origin.Y < 0)
            {
                position.Y = Game.Window.ClientBounds.Height;
            }
            // off bottom
            else if (position.Y + origin.Y > Game.Window.ClientBounds.Height)
            {
                position.Y = 0;
            }

            base.WrapAround();
        }
    }
}
