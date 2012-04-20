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
    public class BlackHole : GameObject
    {
        public BlackHole(Game game, Texture2D picture, Vector2 startpos, Vector2 velocity)
            : base(game, picture)
        {
            // TODO: Construct any child components here
            this.position = startpos;

            this.velocity = velocity;
            this.speed = .5f;
            this.scale = 0.5f;
            this.rotation = .1f;
            this.bounds.Radius = 100 * scale;
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
            if (OutofBounds())
                WrapAround();
            base.Update(gameTime);
        }

        public override void OnCollide(GameObject obj)
        {
            base.OnCollide(obj);
        }

        public override bool Collision(GameObject obj)
        {
            return base.Collision(obj);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }

        public override bool OutofBounds()
        {
            return base.OutofBounds();
        }
    }
}
