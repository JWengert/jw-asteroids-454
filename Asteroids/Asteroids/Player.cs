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
    public class Player : GameObject
    {
        public Player(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
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
            
            KeyboardState state = Keyboard.GetState();



            if (state.IsKeyDown(Keys.Left))
            {
                velocity.X = velocity.X - 1;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                velocity.X = velocity.X + 1;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                velocity.Y = velocity.Y - 1;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                velocity.Y = velocity.Y + 1;
            }
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
    }
}
