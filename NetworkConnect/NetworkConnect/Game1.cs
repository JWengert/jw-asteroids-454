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
using Microsoft.Xna.Framework.Net;
namespace NetworkConnect
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public NetworkSession networkSession;
        AvailableNetworkSessionCollection availableSessions;
        int selectedSessionIndex;
        PacketReader packetReader = new PacketReader();
        PacketWriter packetWriter = new PacketWriter();
        test form;
        public IAsyncResult result;
        public IAsyncResult finder;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // Add Gamer Services
            Components.Add(new GamerServicesComponent(this));
            form = new test(this);
            form.Show();
            //Components[0].Initialize();
            // Respond to the SignedInGamer event
            SignedInGamer.SignedIn +=
                new EventHandler<SignedInEventArgs>(SignedInGamer_SignedIn);
        }

        void SignedInGamer_SignedIn(object sender, SignedInEventArgs e)
        {
         //   e.Gamer.Tag = new Player();
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
            // gets current keyboard state
            KeyboardState keyboardState = Keyboard.GetState();

            // allows the game to exit
            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            base.Update(gameTime);
            /*if (!Guide.IsVisible)
                {
                    foreach (SignedInGamer signedInGamer in
                        SignedInGamer.SignedInGamers)
                    {
                        Asteroids.Player player = signedInGamer.Tag as Asteroids.Player;


                                           if (networkSession != null)
                                            {
                                                if (networkSession.SessionState ==
                                                    NetworkSessionState.Lobby)
                                                    HandleLobbyInput();
                                                else
                                                    HandleGameplayInput(player, gameTime);
                                            }
                                            else if (availableSessions != null)
                                            {
                                                HandleAvailableSessionsInput();
                                            }
                                            else
                                            {
                                                HandleTitleScreenInput();
                                            }
                                            player.lastState = currentState;
                       
                    }
                
     

*/
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void HostGame()
        {
            NetworkSessionProperties props = new NetworkSessionProperties();
            result = NetworkSession.BeginCreate(NetworkSessionType.SystemLink, 1, 3, 0, props, new AsyncCallback(GotResult), null);

        }

        public void GotResult(IAsyncResult result)
        {
            form.CreateSuccess();
        }
        public void FindGames()
        {
            NetworkSessionProperties props = new NetworkSessionProperties();
            finder = NetworkSession.BeginFind(NetworkSessionType.SystemLink, 1, props, new AsyncCallback(FoundSessions), null);

        }
        public void FoundSessions(IAsyncResult result)
        {
            form.PopulateList(result);
        }
    }
}
