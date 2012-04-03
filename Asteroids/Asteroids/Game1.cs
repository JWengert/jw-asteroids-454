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
using System.IO;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<GameObject> mygameobjects = new List<GameObject>();
        private Stack<Player> players = new Stack<Player>();
        private Texture2D rock1, rock2, bullet, spaceship, outerspace, hp_empty, hp_full;
        private SoundEffect tempSound;
        private SoundEffectInstance engineSound, bulletSound, explosionSound, deathSound;
        private int number_asteroids;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // make mouse visible
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load the images
            rock1 = Content.Load<Texture2D>("asteroid1");
            rock2 = Content.Load<Texture2D>("asteroid2");
            bullet = Content.Load<Texture2D>("bullet");
            spaceship = Content.Load<Texture2D>("Ship");
            outerspace = Content.Load<Texture2D>("space");
            hp_empty = Content.Load<Texture2D>("shield_empty");
            hp_full = Content.Load<Texture2D>("shield_full");

            // load the sounds
            tempSound = Content.Load<SoundEffect>("fire");
            bulletSound = tempSound.CreateInstance();
            tempSound = Content.Load<SoundEffect>("explosion");
            explosionSound = tempSound.CreateInstance();
            tempSound = Content.Load<SoundEffect>("fail");
            deathSound = tempSound.CreateInstance();
            tempSound = Content.Load<SoundEffect>("woosh");
            engineSound = tempSound.CreateInstance();

            // add a player
            mygameobjects.Add(new Player(this, spaceship, new Vector2(100, 100), new Vector2(0)));

            // create a random number of asteroids onto the screen
            Random randy = new Random();
            number_asteroids = randy.Next(2, 10);
            int ast_x, ast_y, ast_vel_x, ast_vel_y;
            for (int i = 0; i < number_asteroids; i++)
            {
                ast_x = randy.Next(50, 700);
                ast_y = randy.Next(50, 400);
                ast_vel_x = randy.Next(-2, 2);
                ast_vel_y = randy.Next(-2, 2);
                mygameobjects.Add(new Asteroid(this, rock1, new Vector2(ast_x, ast_y), new Vector2(ast_vel_x, ast_vel_y)));

            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // gets current keyboard state
            KeyboardState keyboardState = Keyboard.GetState();

            // allows the game to exit
            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            // go through every game object created
            foreach (GameObject obj in mygameobjects)
            {
                // first check if it is out of bounds
                if (obj.OutofBounds())
                {
                    // bullets should be destroyed
                    if (obj is Bullet)
                        obj.IsAlive = false;
                    // the player will wrap around
                    else if (obj is Player)
                        obj.WrapAround();
                    // an asteroid will wrap around
                    else if(obj is Asteroid)
                        obj.WrapAround();
                }
                // now update the object
                obj.Update(gameTime);

                // if it's a player, check if a bullet should be created or not and add it to the stack
                if (obj is Player)
                    if (Player.createBullet)
                        players.Push((Player)obj);
            }
            
            // create a bullet for each player who fired one
            while (players.Count != 0)
                mygameobjects.Add(new Bullet(this, bullet, players.Pop()));

            // check for any collisions between objects
            foreach (GameObject obj1 in mygameobjects)
                foreach (GameObject obj2 in mygameobjects)
                    if (obj1 != obj2 && obj1.Collision(obj2))
                        obj1.OnCollide(obj2);

            // now delete any game objects that are no longer 'alive'
            for (int i = mygameobjects.Count - 1; i >= 0; i--)
                if (!mygameobjects[i].IsAlive)
                    mygameobjects.RemoveAt(i);

            // update the base
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // default background color
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // begin drawing all data
            spriteBatch.Begin();
            
            // draw the bacground with a height and width of the current resolution
            spriteBatch.Draw(outerspace, new Rectangle(0, 0, 800, 480), Color.White);

            // call the draw method for each object
            foreach (GameObject obj in mygameobjects)
            {
                obj.Draw(gameTime, spriteBatch);
                if (obj is Bullet)
                    bulletSound.Play();
            }

            // end our spritebatch
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
