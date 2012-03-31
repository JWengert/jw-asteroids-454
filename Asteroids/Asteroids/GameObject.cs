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
        protected Color color;
        protected Texture2D picture;
        protected Rectangle screenbounds;
        protected SpriteEffects effects = SpriteEffects.None;
        protected BoundingSphere bounds;

        public GameObject(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public GameObject(Game game, Texture2D picture, Vector2 startposition, Vector2 velocity, Rectangle screenbounds)
            : base(game)
        {
            this.picture = picture;
            this.position = startposition;
            this.velocity = velocity;
            this.screenbounds = screenbounds;
            color = Color.White;
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
            base.Update(gameTime);
        }

        /// <summary>
        /// Checks for collision between two GameObjects
        /// </summary>
        /// <param name="obj">GameObject to compare with</param>
        /// <returns>Reslut of comparisson</returns>
        public virtual bool Collision(GameObject obj)
        {
            if (Math.Abs(Vector2.Distance(this.position, obj.position)) <= picture.Height)
                return true;
            else
                return false;
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
            if (position.X < 0)
                return true;
            // off right
            else if (position.X > Game.Window.ClientBounds.Width)
                return true;
            // off top
            if (position.Y < 0)
                return true;
            // off bottom
            else if (position.Y > Game.Window.ClientBounds.Height)
                return true;
            // not out of bounds
            else
                return false;
        }

        /// <summary>
        /// Check if the gameobject is alive
        /// </summary>
        public bool IsAlive { get { return alive; } }

        public virtual void DrawMe(SpriteBatch sb)
        {
            // only draw if it's alive
            if (IsAlive)
                sb.Draw(picture, position, null, color, rotation, origin, scale, effects, depth); // use null to draw entire picture
            
        }
    }
}
