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
        private int hits, allowed_hits = 2;
        private int pause;
        private TimeSpan timer;

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
            this.bounds.Radius = 20 * scale;
            pause = 0;
            timer = new TimeSpan();
            hits = allowed_hits;
            respawnTimer = TimeSpan.FromMilliseconds(2000);
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
            if (this.Enabled)
            {
                // TODO: Add your update code here
                this.rotation += 0.01f;
            }
            else
            {
                respawnElapsed += gameTime.ElapsedGameTime;
                if (respawnElapsed >= respawnTimer)
                {
                    this.hits = allowed_hits;
                    this.Respawn();
                    this.respawnElapsed = TimeSpan.Zero;
                    int x, y;
                    x = Game1.randy.Next(int.MinValue,int.MaxValue);
                    y = Game1.randy.Next(int.MinValue,int.MaxValue);
                    this.position = new Vector2(x, y);
                    x = Game1.randy.Next(-2, 2);
                    y = Game1.randy.Next(-2, 2);
                    this.velocity = new Vector2(x, y);
                    WrapAround();
                }
            }
            base.Update(gameTime);
        }

        public override bool Collision(GameObject obj)
        {
            return base.Collision(obj);
        }

        public override void OnCollide(GameObject obj)
        {
            if (obj is Bullet)
            {
                if (hits > 0)
                    hits--;
                else
                {
                    pause = timer.Milliseconds;
                    this.Enabled = false;
                }
            }
            base.OnCollide(obj);
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
            

            base.WrapAround();
        }

        public override void Die()
        {
            base.Die();
        }

        public override void Respawn()
        {
            this.Enabled = true;
        }
    }
}
