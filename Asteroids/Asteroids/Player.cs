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
    // Player inherits all traits from GameObject
    public class Player : GameObject
    {
        // public variables
        public static bool createBullet = false;

        // variables to spread out the time between each bullet
        private TimeSpan timeNewBullet = TimeSpan.FromMilliseconds(250);
        private TimeSpan timeBulleElapsed = TimeSpan.Zero;

        // constructor does most the initialization of the inherited variables
        public Player(Game game, Texture2D picture, Vector2 startposition, Vector2 velocity, Rectangle screenbounds)
            : base(game, picture, startposition, velocity, screenbounds)
        {
            this.picture = picture;
            this.position = startposition;
            this.velocity = velocity;
            this.screenbounds = screenbounds;
            this.scale = 0.5f;
            this.speed = 7f;
        }

        // any initialization needed  before loading game content
        public override void Initialize()
        {
            // base must initialize first!!!
            base.Initialize();
        }

        // update all variables about 60 times a second. Done before draw.
        public override void Update(GameTime gameTime)
        {
            // get mouse/keyboard input from player
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            // get the direction of the mouse by subtracting the mouse from the sprites location
            Vector2 mouseLocation = new Vector2(mouseState.X, mouseState.Y);
            Vector2 spriteLocation = new Vector2(this.position.X, this.position.Y);
            Vector2 direction = spriteLocation - mouseLocation;

            // if the sprite is too close to the mouse, don't move the player anymore
            if (direction.Length() > 10)
            {
                // normalize the result and then move the player based on the input
                direction.Normalize();
                MovePlayer(mouseState, direction);
            }

            // update the elapsed time since a bullet was shot indicate to shoot one if necessary
            timeBulleElapsed += gameTime.ElapsedGameTime;
            if (timeBulleElapsed > timeNewBullet &&
                (mouseState.RightButton == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Space)))
            {
                timeBulleElapsed = TimeSpan.Zero;
                createBullet = true;
            }
            else
                createBullet = false;

            // base update must happen afterwards
            base.Update(gameTime);
        }

        // check to see what mouse button is pressed down and move in the respective direction
        private void MovePlayer(MouseState mouseState, Vector2 direction)
        {
            // set the angle (rotation) of the sprite
            this.rotation = (float)Math.Atan2(direction.Y, direction.X) - (float)(Math.PI / 2);

            // forward (toward the mouse)
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                this.position.X -= (direction.X * this.speed);
                this.position.Y -= (direction.Y * this.speed);
            }
        }

        // base drawing handles most the necessary drawing
        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }

        // base collision does most collision algorithms needed
        public override bool Collision(GameObject obj)
        {
            return base.Collision(obj);
        }

        // base out of bounds algorithm handles most of this
        public override bool OutofBounds()
        {
            return base.OutofBounds();
        }
    }
}
