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
        private int hits, allowed_hits = 4;
        private int blink = 0;
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
            this.bounds.Radius = 30 * scale;
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
                if (hits < allowed_hits / 3)
                {
                    if (blink++ % 8 > 3)
                        this.color = Color.Transparent;
                    else
                        this.color = Color.Red;
                }
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
                    x = Game1.randy.Next(Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Width + 51);
                    y = Game1.randy.Next(Game.Window.ClientBounds.Height, Game.Window.ClientBounds.Height + 51);
                    this.position = new Vector2(x, y);
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
                    Game1.engine.Explosion(100, this.position, 0.3f);
                    this.Die();
                }
            }
            if (obj is Asteroid)
            {
                Vector2 direction = this.position - obj.Position;
                
                if (Vector2.Dot(this.velocity, direction) < 0)
                    this.velocity *= -1;
            }
            if (obj is BlackHole)
                this.Die();
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
            respawnElapsed = TimeSpan.Zero;
            this.Enabled = false;
            base.Die();
        }

        public override void Respawn()
        {
            this.Enabled = true;
            int ast_vel_x, ast_vel_y;
            ast_vel_x = Game1.randy.Next(Game1.AstMinVel, Game1.AstMaxVel);
            ast_vel_y = Game1.randy.Next(Game1.AstMinVel, Game1.AstMaxVel);
            if (Game1.randy.Next(0, 50) <= 24)
                ast_vel_x *= -1;
            if (Game1.randy.Next(0, 50) <= 24)
                ast_vel_y *= -1;
            this.velocity = new Vector2(ast_vel_x, ast_vel_y);
            this.color = Color.White;
        }
    }
}
