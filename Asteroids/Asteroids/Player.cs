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
        public static bool isMoving = false;

        // variables to spread out the time between each bullet
        private TimeSpan timeNewBullet = TimeSpan.FromMilliseconds(550);
        private TimeSpan timeBulleElapsed = TimeSpan.Zero;
        private int shields, points;
        private static int maxshields = 3;
        private int lives = 10;
        //private TimeSpan respawnTimer = TimeSpan.FromMilliseconds(1000);
        //private TimeSpan respawnElapsed = TimeSpan.Zero;
        private Vector2 start_pos;
        //private TimeSpan mercyLength = TimeSpan.FromMilliseconds(1000);
        //private TimeSpan mercytime = TimeSpan.Zero;

        public int Shields { get { return shields; } set { ;} }
        public int Lives { get { return lives; } set { ;} }

        // constructor does most the initialization of the inherited variables
        public Player(Game game, Texture2D picture, Vector2 startposition, Vector2 velocity)
            : base(game, picture)
        {
            this.position = startposition;
            start_pos = startposition;
            this.velocity = velocity;
            this.scale = 0.5f;
            this.speed = 7f;
            this.bounds.Radius = 50 * this.scale;
            shields = maxshields;
            respawnTimer = TimeSpan.FromMilliseconds(1000);
            mercyLength = TimeSpan.FromMilliseconds(1000);
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
            if (this.Enabled)
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
                else
                    isMoving = false;

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
                mercytime += gameTime.ElapsedGameTime;
                if (mercytime > mercyLength)
                    this.color = Color.White;
                else
                    this.color = Color.CornflowerBlue;

            }
            else
            {
                respawnElapsed += gameTime.ElapsedGameTime;
                if (respawnElapsed >= respawnTimer)
                {
                    if (lives > 0)
                        this.Respawn();
                }
            }
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
                isMoving = true;
            }
            else
                isMoving = false;
        }

        public override void Die()
        {
            this.Enabled = false;
            respawnElapsed = TimeSpan.Zero;
            this.position = start_pos;
            isMoving = false;
            createBullet = false;
            this.lives--;
        }

        public override void Respawn()
        {
            this.Enabled = true;
            this.shields = maxshields;
            mercytime = TimeSpan.Zero;
            
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

        public override void OnCollide(GameObject obj)
        {
            if (mercytime > mercyLength)
            {
                if (obj is Asteroid)
                {
                    shields--;
                    if (shields <= 0)
                        this.Die();
                    mercytime = TimeSpan.Zero;
                }
                if (obj is Bullet)
                {
                    if (((Bullet)obj).Owner != this)
                    {
                        shields--;
                        obj.IsAlive = false;
                        if (shields <= 0)
                            this.Die();
                        mercytime = TimeSpan.Zero;
                    }
                }
            }
            base.OnCollide(obj);
        }

        public int Score { get { return points; } set { points = value; } }
    }
}
