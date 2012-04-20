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
        private TimeSpan timeNewBullet = TimeSpan.FromMilliseconds(250);
        private TimeSpan timeBulleElapsed = TimeSpan.Zero;
        private int shields, points;
        private static int maxshields = 3;
        private int lives = 3;
        private Vector2 start_pos;
        int blink = 0;
        public int Shields { get { return shields; } set { ;} }
        public int Lives { get { return lives; } set { ;} }
        public int MaxShields { get { return maxshields; } set { ;} }
        private ParticleEngine engine;

        // constructor does most the initialization of the inherited variables
        public Player(Game game, Texture2D picture, Vector2 startposition, Vector2 velocity, ParticleEngine engine)
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
            this.engine = engine;
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
                    MovePlayer(mouseState, keyboardState, direction);
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
                {
                    if (blink++ % 8 > 3)
                        this.color = Color.Transparent;
                    else
                        this.color = Color.White;
                }
                engine.EmitterLocation = this.position;
                engine.Update(isMoving);
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
        private void MovePlayer(MouseState mouseState,KeyboardState keyboardState, Vector2 direction)
        {
            // set the angle (rotation) of the sprite
            this.rotation = (float)Math.Atan2(direction.Y, direction.X) - (float)(Math.PI / 2);

            // forward (toward the mouse)
            if (keyboardState.IsKeyDown(Keys.W) || mouseState.LeftButton == ButtonState.Pressed)
            {
                position.X -= (direction.X * speed);
                position.Y -= (direction.Y * speed);
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
            this.velocity = new Vector2(0);
            this.force = new Vector2(0);
        }

        // base drawing handles most the necessary drawing
        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            engine.Draw(sb);
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
                if (obj is BlackHole)
                    this.Die();
            }
            base.OnCollide(obj);
        }

        public int Score { get { return points; } set { points = value; } }
    }
}
