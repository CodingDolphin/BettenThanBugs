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

namespace BetterThenBugs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        

        //Spieler

        Player Wolf;

        //Gegner

        //Spielwelt

        public const float Gravity = 750f;
        public const float Slowdown= 35f;
        Vector2 Cloudspeed = new Vector2(0.5f, 0);
        float DeltaTime;

        //Hintergrund Grafiken

        public Texture2D BackgroundTexture1;
        public Texture2D BackgroundTexture2;

        public Vector2 BackgroundPosition1;
        public Vector2 BackgroundPosition2;
        public  byte[] buffer = new byte[128];

        

        //Tastatur

        public KeyboardState currentKeyBoardState;

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
            //Player Initialisieren

            Wolf = new Player();
            Wolf.PlayerPosition.X = 100;
            Wolf.PlayerPosition.Y = 300;
            
            Wolf.PlayerAnimation.LoadContent(Content, "Wolf");
            Wolf.PlayerSpringAnimation.LoadContent(Content, "WolfSprung");

            Wolf.PlayerAnimation.Initialize(Wolf.PlayerPosition,0.15f,0,2,150,150);
            Wolf.PlayerSpringAnimation.Initialize(Wolf.PlayerPosition, 0.1f, 0, 1, 150, 150);
            

            //Spielwelt Initialisieren

            BackgroundPosition1 = new Vector2(0, -100);
            BackgroundPosition2 = new Vector2(0, 0);

            
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

            BackgroundTexture2 = Content.Load<Texture2D>("wolke");
            BackgroundTexture1 = Content.Load<Texture2D>("Background1");

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
        
            KeyboardAbfragen(gameTime);

            if (BackgroundPosition2.X > GraphicsDevice.Viewport.Width)
            {
                BackgroundPosition2.X = -100;
            }

            BackgroundPosition2.X += Cloudspeed.X;
          
          
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(BackgroundTexture1,BackgroundPosition1, Color.White);
            spriteBatch.Draw(BackgroundTexture2, BackgroundPosition2, Color.White);
            spriteBatch.End();

            Wolf.CurrentPlayerAnimation.AnimationsPosition = Wolf.PlayerPosition;
            Wolf.CurrentPlayerAnimation.Draw(spriteBatch);
            
            base.Draw(gameTime);
        }

        public void KeyboardAbfragen(GameTime gameTime)
        {

            currentKeyBoardState = Keyboard.GetState();

            if (currentKeyBoardState.IsKeyDown(Keys.D))
            {
                    Wolf.Velocity.X -= Wolf.PlayerSpeed;

                    if (Wolf.Velocity.X <= -Wolf.PlayerMaxSpeed)
                        Wolf.Velocity.X = -Wolf.PlayerMaxSpeed;

                    Wolf.currentDirection = BetterThenBugs.Player.walkDirection.WalkRight;
            }

            if (currentKeyBoardState.IsKeyDown(Keys.A))
            {
                    Wolf.Velocity.X += Wolf.PlayerSpeed;
     
                    if (Wolf.Velocity.X >= Wolf.PlayerMaxSpeed)
                       Wolf.Velocity.X =  Wolf.PlayerMaxSpeed;

                    Wolf.currentDirection = BetterThenBugs.Player.walkDirection.WalkLeft;
            }

            if (currentKeyBoardState.IsKeyDown(Keys.Space) & Wolf.isGrounded == true)
            {
                Wolf.isGrounded = false;
                Wolf.Velocity.Y += Wolf.JumpStrength;
                Wolf.currentDirection = BetterThenBugs.Player.walkDirection.Jumping;
            }

            if (currentKeyBoardState.IsKeyDown(Keys.LeftControl))
            {
            }

            Wolf.Bewegen(gameTime, Gravity, DeltaTime, Slowdown);
            Wolf.CurrentPlayerAnimation.AnimationsPosition = Wolf.PlayerPosition;
            Wolf.CurrentPlayerAnimation.Update(gameTime);
            DeltaTime = 0;
        }
    }
}
