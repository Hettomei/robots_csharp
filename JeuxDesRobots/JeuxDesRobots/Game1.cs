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

namespace JeuxDesRobots
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private KeyboardState _keyboardState;
        private TestLignes testLigne01;
        private Robot r1;
        private List<Robot> lr1 = new List<Robot>();
        //private Robot[] lr1;
        private SpriteFont _font;
        private    int max = 100;
         
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
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
            Console.WriteLine("programme long a charger. Utiliser les fleche pour avancer");

            testLigne01 = new TestLignes();
            testLigne01.Initialize();
            r1 = new Robot();
            r1.Initialize();

            Robot temp;
            Random rand = new Random();
            for (int i = 0; i < max; i++)
            {
                temp = new Robot();
                temp.Initialize(new Vector2(rand.Next(100, 904), rand.Next(100, 668)), (float)rand.NextDouble() * 4, new Color(rand.Next(1, 254), rand.Next(1, 254), rand.Next(1, 254)), rand.Next(0, 359), rand.Next(5, 200));
                lr1.Add(temp);
                Console.WriteLine(i + "/" + max + " de chargé");
            }

            //lr1 = new  Robot[max];
            //for (int i = 0; i < max; i++)
            //{
            //    lr1[i] = new Robot();
            //    lr1[i].Initialize(new Vector2(rand.Next(100, 904), rand.Next(100, 668)), 2f, Color.Black, rand.Next(0, 359), 20);
            //        Console.WriteLine(i + "/" + max + " de chargé");
            //}
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
            _font = Content.Load<SpriteFont>("SpriteFont1");

            testLigne01.LoadContent(GraphicsDevice);
            r1.LoadContent(GraphicsDevice);
            int i = 1;
            foreach (Robot r in lr1)
            {
                r.LoadContent(GraphicsDevice);
                Console.WriteLine(i++ + "/" + max + " de chargé");
            }
            //for (int j = 0; j < max; j++)
            //{
            //    lr1[j].LoadContent(GraphicsDevice);
            //    Console.WriteLine(j+1 + "/" + max + " de chargé");
            //}
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
            _keyboardState = Keyboard.GetState();

            // Allows the game to exit
           if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
               || _keyboardState.IsKeyDown(Keys.Escape))
                    this.Exit();
                


            // TODO: Add your update logic here
           testLigne01.Update(gameTime);
           r1.HandleInput(_keyboardState);
           r1.Update(gameTime);
           foreach (Robot r in lr1)
           {
               r.HandleInput(_keyboardState);

               r.Update(gameTime);
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
        
            // TODO: Add your drawing code here
            testLigne01.Draw();
            r1.Draw();
            foreach (Robot r in lr1)
            {
                r.Draw();
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(_font, "Appuyer sur les fleches...", new Vector2(10, 20), Color.White);
            spriteBatch.DrawString(_font, "R reinitialise le sens", new Vector2(10, 40), Color.White);
            spriteBatch.DrawString(_font, "P Accelere M ralentit", new Vector2(10, 60), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
