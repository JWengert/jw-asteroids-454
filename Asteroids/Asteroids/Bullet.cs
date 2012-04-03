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
    public class Bullet : GameObject
    {
        // private variables
        private Player owner;

        // constructor to initialize most inherited values
        public Bullet(Game game, Texture2D picture, Player owner)
            : base(game, picture)
        {
            this.owner = owner;
            this.speed = 17f;
            this.scale = 2;
            CreateBullet();
            this.bounds.Radius = 10 * this.scale;
        }

        // any initialization needed before loading game content
        public override void Initialize()
        {
            // base must initialize first!!!
            base.Initialize();
        }

        // update all variables about 60 times a second. Done before draw.
        public override void Update(GameTime gameTime)
        {
            // base update must happen afterwards
            base.Update(gameTime);
        }

        // base collision does most collision algorithms needed
        public override bool Collision(GameObject obj)
        {
            return base.Collision(obj);
        }

        public override void OnCollide(GameObject obj)
        {
            if (obj is Asteroid)
            {
                this.alive = false;
                obj.IsAlive = false;
            }
                
            base.OnCollide(obj);
        }

        // base out of bounds algorithm handles most of this
        public override bool OutofBounds()
        {
            return base.OutofBounds();
        }

        // base drawing handles most the necessary drawing
        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }

        private void CreateBullet()
        {
            // get mouse/keyboard input from player
            MouseState mouseState = Mouse.GetState();

            // get the direction of the mouse by subtracting the mouse from the sprites location
            Vector2 mouseLocation = new Vector2(mouseState.X, mouseState.Y);
            Vector2 spriteLocation = new Vector2(this.owner.Position.X, this.owner.Position.Y);
            Vector2 direction = spriteLocation - mouseLocation;
            direction.Normalize();

            // set proper variables of this bullet before updating and drawing it
            this.position.Y = this.owner.Position.Y;
            this.position.X = this.owner.Position.X;
            this.velocity.Y = (direction.Y * this.speed * -1);
            this.velocity.X = (direction.X * this.speed * -1);
        }
    }
}
