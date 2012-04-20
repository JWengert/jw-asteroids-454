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
    public class GameObject : Microsoft.Xna.Framework.GameComponent
    {
        protected Vector2 position, velocity, origin;
        protected float speed;
        protected bool alive = true;
        protected float scale, rotation, depth;
        protected Color color = Color.White;
        protected Texture2D picture;
        protected SpriteEffects effects = SpriteEffects.None;
        protected BoundingSphere bounds;
        protected TimeSpan respawnTimer, respawnElapsed = TimeSpan.Zero;
        protected TimeSpan mercyLength, mercytime = TimeSpan.Zero;
        protected Vector2 force = new Vector2(0);

        public Vector2 Position 
        { 
            get { return position; } 
            set { position = value; } 
        }

        public GameObject(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public GameObject(Game game, Texture2D picture)
            : base(game)
        {
            this.picture = picture;
            this.origin = new Vector2(picture.Width / 2, picture.Height / 2);
            this.bounds.Center.X = this.position.X;
            this.bounds.Center.Y = this.position.Y;
            this.bounds.Center.Z = 0;
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
            position += velocity;
            position += force;
            force = new Vector2(0);
            base.Update(gameTime);
        }

        /// <summary>
        /// Checks for collision between two GameObjects
        /// </summary>
        /// <param name="obj">GameObject to compare with</param>
        /// <returns>Reslut of comparisson</returns>
        public virtual bool Collision(GameObject obj)
        {
            this.bounds.Center.X = this.position.X;
            this.bounds.Center.Y = this.position.Y;
            return this.bounds.Intersects(obj.bounds);
        }

        /// <summary>
        /// Returns true if object is off screen
        /// </summary>
        /// <returns>Boolean of question out of bounds</returns>
        public virtual bool OutofBounds()
        {
            //+----------X
            //|
            //|
            //Y
            // off left
            if (position.X + origin.X < 0)
                return true;
            // off right
            else if (position.X + origin.X > Game.Window.ClientBounds.Width)
                return true;
            // off top
            if (position.Y + origin.Y < 0)
                return true;
            // off bottom
            else if (position.Y + origin.Y > Game.Window.ClientBounds.Height)
                return true;
            // not out of bounds
            else
                return false;
        }

        /// <summary>
        /// Check if the gameobject is alive
        /// </summary>
        public bool IsAlive { get { return alive; } set { alive = value; } }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb)
        {
            // only draw if it's alive
            if (IsAlive && this.Enabled)
                sb.Draw(picture, position, null, color, rotation, origin, scale, effects, depth);   // use null to draw entire picture
        }

        public virtual void WrapAround()
        {
            int screenWidth = Game.Window.ClientBounds.Width;
            int screenHeight = Game.Window.ClientBounds.Height;

            // off left
            if (position.X < 0)
            {
                position.X = screenWidth;
            }
            // off right
            else if (position.X > screenWidth)
            {
                position.X = 0;
            }
            // off top
            if (position.Y < 0)
            {
                position.Y = screenHeight;
            }
            // off bottom
            else if (position.Y > screenHeight)
            {
                position.Y = 0;
            }
        }

        public virtual void OnCollide(GameObject obj)
        {
            
        }

        public virtual void Die()
        {
        }

        public virtual void Respawn()
        {
        }

        public virtual void SuckIn(GameObject obj)
        {
            Vector2 toBH = this.position - obj.position;
            float r2 = (float) toBH.LengthSquared();
            toBH.Normalize();
            force -= Game1.Gravity / r2 * toBH;

        }
    }
}
