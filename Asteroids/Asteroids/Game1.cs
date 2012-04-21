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
using Microsoft.Xna.Framework.Net;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    ///
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //testing testing
        public static Random randy = new Random();
        public static float Gravity = 50000f;
        public static int AstMinVel = 2;
        public static int AstMaxVel = 4;
        public static ParticleEngine engine;
        public static SoundEffectInstance playerdie;

        // let's us know what the current game state is
        public enum GameState { Menu, Pause, Play, End };
        public GameState currentGameState = GameState.Menu;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<GameObject> mygameobjects = new List<GameObject>();
        private List<guiitem> hud = new List<guiitem>();
        private Stack<Player> players = new Stack<Player>();
        private Texture2D rock1, rock2, bullet, spaceship, outerspace, hp_empty, hp_full, livesicon, blackhole, star, diamond;
        private SoundEffect tempSound;
        private SoundEffectInstance backgroundSound, engineSound, bulletSound, explosionSound, deathSound, newfiresound;
        private int number_asteroids = 10;
        private SpriteFont score, menu;
        private int screenHeight = 480;
        private int screenWidth = 800;

        private int min_asteroids_begin = 15;
        private int max_asteroids_begin = 26;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";

            // set the default resolution and make the game full screen
            DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            screenWidth = displayMode.Width;
            screenHeight = displayMode.Height;

            min_asteroids_begin *= (int)Math.Floor((float)screenWidth / (float)700);
            max_asteroids_begin *= (int)Math.Floor((float)screenWidth / (float)700);

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
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
            score = Content.Load<SpriteFont>("Score");
            menu = Content.Load<SpriteFont>("Menu");

            // load the images
            rock1 = Content.Load<Texture2D>("asteroid1");
            rock2 = Content.Load<Texture2D>("asteroid2");
            bullet = Content.Load<Texture2D>("bullet");
            spaceship = Content.Load<Texture2D>("Ship");
            outerspace = Content.Load<Texture2D>("space");
            hp_empty = Content.Load<Texture2D>("shield_empty");
            hp_full = Content.Load<Texture2D>("shield_full");
            livesicon = Content.Load<Texture2D>("Shipicon");
            blackhole = Content.Load<Texture2D>("Blackhole");
            star = Content.Load<Texture2D>("star");
            diamond = Content.Load<Texture2D>("diamond");

            // load the sounds
            tempSound = Content.Load<SoundEffect>("fire4");
            bulletSound = tempSound.CreateInstance();
            tempSound = Content.Load<SoundEffect>("explosion");
            explosionSound = tempSound.CreateInstance();
            tempSound = Content.Load<SoundEffect>("fail");
            deathSound = tempSound.CreateInstance();
            tempSound = Content.Load<SoundEffect>("woosh");
            engineSound = tempSound.CreateInstance();
            tempSound = Content.Load<SoundEffect>("doctorWho2");
            backgroundSound = tempSound.CreateInstance();
            tempSound = Content.Load<SoundEffect>("newfiresound");
            newfiresound = tempSound.CreateInstance();

            playerdie = explosionSound;

            // start the background music
            backgroundSound.Play();
            backgroundSound.Volume = 0.3f;
            newfiresound.Volume = .2f;
            explosionSound.Volume = 1f;
            float bx, by, bvx, bvy;
            bx = (float)randy.NextDouble() * 1000;
            by = (float)randy.NextDouble() * 1000;
            bvx = (float)randy.NextDouble() / 10;
            bvy = (float)randy.NextDouble() / 10;

            mygameobjects.Add(new BlackHole(this, blackhole, new Vector2(bx, by), new Vector2(bvx, bvy)));

            engine = new ParticleEngine(star, diamond, new Vector2(100));
            // add a player
            Player p1 = new Player(this, spaceship, new Vector2(screenWidth/2, screenHeight/2), new Vector2(0), engine);
            mygameobjects.Add(p1);

            number_asteroids = randy.Next(min_asteroids_begin, max_asteroids_begin);
            // create a fixed number of asteroids onto the screen
            int ast_x, ast_y, ast_vel_x, ast_vel_y;
            for (int i = 0; i < number_asteroids; i++)
            {
                ast_x = randy.Next(50, screenWidth);
                ast_y = randy.Next(50, screenHeight);
                ast_vel_x = randy.Next(AstMinVel, AstMaxVel);
                ast_vel_y = randy.Next(AstMinVel, AstMaxVel);
                if (Game1.randy.Next(0, 50) <= 24)
                    ast_vel_x *= -1;
                if (Game1.randy.Next(0, 50) <= 24)
                    ast_vel_y *= -1;
                mygameobjects.Add(new Asteroid(this, rock1, new Vector2(ast_x, ast_y), new Vector2(ast_vel_x, ast_vel_y)));

            }
            
            hud.Add(new lifebar(this, p1, 1, hp_full, hp_empty, score));
            hud.Add(new livesdisplay(this, p1, 1, livesicon));
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
            // allows game to pause
            if (keyboardState.IsKeyDown(Keys.P) && currentGameState != GameState.Menu && currentGameState != GameState.End)
                currentGameState = GameState.Pause;
            // allows game to play
            if (keyboardState.IsKeyDown(Keys.S) && currentGameState != GameState.End)
                currentGameState = GameState.Play;

            if (currentGameState == GameState.End && keyboardState.IsKeyDown(Keys.Enter))
                restartGame();

            // check to see if the background music is done playing and replay it
            if (backgroundSound.State != SoundState.Playing)
                backgroundSound.Play();

            // only update the all objects if we are in playing mode
            if (currentGameState != GameState.Pause)
            {
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
                        else if (obj is Asteroid)
                            obj.WrapAround();
                        else if (obj is BlackHole)
                            obj.WrapAround();
                    }
                    // now update the object
                    if (!(obj is Player))
                        obj.Update(gameTime);

                    // if it's a player, check if a bullet should be created or not and add it to the stack
                    if (obj is Player && (currentGameState == GameState.Play || currentGameState == GameState.End))
                    {
                        if (Player.createBullet)
                            players.Push((Player)obj);
                        if (Player.isMoving && engineSound.State != SoundState.Playing)
                            engineSound.Play();
                        if (!Player.isMoving)
                            engineSound.Stop();
                        if (((Player)obj).Lives <= 0)
                            GameOver();
                        obj.Update(gameTime);
                    }
                }

                // create a bullet for each player who fired one
                while (players.Count != 0)
                {
                    mygameobjects.Add(new Bullet(this, bullet, players.Pop()));
                    //bulletSound.Play();
                    newfiresound.Play();
                }

                // check for any collisions between objects
                foreach (GameObject obj1 in mygameobjects)
                {
                    foreach (GameObject obj2 in mygameobjects)
                    {
                        if (obj1 is BlackHole && obj1 != obj2 && obj2.Enabled)
                        {
                            obj2.SuckIn(obj1);
                        }
                        if (obj1 != obj2 && obj1.Enabled && obj2.Enabled && obj1.Collision(obj2))
                        {
                            obj1.OnCollide(obj2);

                            // if one object is a bullet and the other object is an asteroid and was disabled, it "exploded"
                            if (obj1 is Bullet || obj2 is Bullet)
                                if ((obj1 is Asteroid && !obj1.Enabled) || (obj2 is Asteroid && !obj2.Enabled))
                                    explosionSound.Play();
                        }
                    }
                }

                // now delete any game objects that are no longer 'alive'
                for (int i = mygameobjects.Count - 1; i >= 0; i--)
                    if (!mygameobjects[i].IsAlive)
                        mygameobjects.RemoveAt(i);
            }

            // update the base
            base.Update(gameTime);
        }

        private void restartGame()
        {
            mygameobjects.Clear();
            hud.Clear();
            LoadContent();
            currentGameState = GameState.Play;
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
            spriteBatch.Draw(outerspace, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

                // call the draw method for each object
            foreach (GameObject obj in mygameobjects)
                obj.Draw(gameTime, spriteBatch);

            if (currentGameState == GameState.Play || currentGameState == GameState.Pause)
            {
                // call the draw method for each gui item
                foreach (guiitem gui in hud)
                    gui.Draw(gameTime, spriteBatch);
            }

            if (currentGameState == GameState.End)
            {
                // draw our game objects if paused or playing
                if (currentGameState == GameState.End)
                {
                    // create the end game string
                    string stringToDraw =
                        String.Format(
                              "GAME OVER\n"
                            + "    Press Esc to Exit.\n"
                            + "    Press Enter to Restart.\n"
                        );
                    // get the score of the player
                    foreach (GameObject item in mygameobjects)
                    {
                        // find the player in the list of game objects
                        if (item is Player)
                        {
                            // put the score in the string to be drawn
                            Player p = (Player)item;
                            stringToDraw += "    Your score is: " + p.Score + "!!\n";
                            break;
                        }
                    }


                    // draw the created string
                    spriteBatch.DrawString(menu, stringToDraw, new Vector2(), Color.White);
                }

            }

            // draw the menu
            if (currentGameState == GameState.Menu)
            {
                // create the menu string
                string stringToDraw =
                    String.Format(
                          "Key Buttons:\n"
                        + "    Press \"ESC\" to exit the game!\n"
                        + "    Press \"S\" to start the game!\n"
                        + "    Press \"P\" to pause the game!\n\n"
                        + "Instructions:\n"
                        + "    Move around with left click and shoot with right click.\n"
                        + "    Destroy as many asteroids as possible before running out of lives!\n"
                        + "    GOOD LUCK!!!\n"
                    );

                // draw the created string
                spriteBatch.DrawString(menu, stringToDraw, new Vector2(), Color.White);
            }
            

            // end our spritebatch
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected virtual void GameOver()
        {
            currentGameState = GameState.End;
            
        }
    }
}
