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
        private Texture2D rock1, rock2, bullet, spaceship, outerspace;
        private Rectangle screen;
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
            // TODO: Add your initialization logic here

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

            rock1 = Content.Load<Texture2D>("asteroid1");
            rock2 = Content.Load<Texture2D>("asteroid2");
            bullet = Content.Load<Texture2D>("bullet");
            spaceship = Content.Load<Texture2D>("Ship");
            outerspace = Content.Load<Texture2D>("space");
            //Might need to be changed!!!
            screen = new Rectangle(0, 0, 800, 480);

            mygameobjects.Add(new Player(this, spaceship, new Vector2(100, 100), new Vector2(0)));

            Random randy = new Random();
            number_asteroids = randy.Next(2, 10);
            int ast_x, ast_y, ast_vel;
            for (int i = 0; i < number_asteroids; i++)
            {
                ast_x = randy.Next(50, 700);
                ast_y = randy.Next(50, 400);
                ast_vel = randy.Next(-2, 2);
                mygameobjects.Add(new Asteroid(this, rock1, new Vector2(ast_x, ast_y), new Vector2(0)));

            }

            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            foreach (GameObject obj in mygameobjects)
            {
                if (obj.OutofBounds())
                {
                    if (obj is Bullet)
                        obj.IsAlive = false;
                    else if (obj is Player)
                        obj.WrapAround();
                    else if(obj is Asteroid)
                        obj.WrapAround();
                }
                obj.Update(gameTime);
                if (obj is Player)
                    if (Player.createBullet)
                        players.Push((Player)obj);
            }
            while (players.Count != 0)
                mygameobjects.Add(new Bullet(this, bullet, players.Pop()));

            foreach (GameObject obj1 in mygameobjects)
            {
                foreach (GameObject obj2 in mygameobjects)
                {
                    if (obj1 != obj2 && obj1.Collision(obj2))
                    {
                        obj1.OnCollide(obj2);
                    }
                }
            }

            for (int i = mygameobjects.Count - 1; i >= 0; i--)
            {
                if (!mygameobjects[i].IsAlive)
                {
                    mygameobjects.RemoveAt(i);
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(outerspace, new Rectangle(0, 0, 800, 480), Color.White);
            foreach (GameObject obj in mygameobjects)
            {
                obj.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
