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
		//Toujours utile
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private KeyboardState keyboardState;
		private MouseState mouseState;
		private PrimitiveBatch primitiveBatch;
		private SpriteFont _font;
		private Random ra = new Random();
		private Vector2 centreEcran;

		//Ajoute automatiquement le texte en dessous
		private Vector2 textAffiche;
		private Vector2 ajoutTextAffiche = new Vector2(0, 20);

		private List<ILoadAndDraw> listeAffichage;

		private Souris affichageSouris;

		private const int NBROBOTS = 0;
		private const int NBBRIQUES = 0;

		private Robot[] les_robots01;
		private Brique[] les_brique01;


		private LigneDebug axeX;
		private LigneDebug axeY;
		private LigneDebug centreToSouris;
		private Robot auto;
		private Brique brique;


		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 768;
			centreEcran = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			primitiveBatch = new PrimitiveBatch(GraphicsDevice);
			listeAffichage = new List<ILoadAndDraw>();

			affichageSouris = new Souris();
			affichageSouris.Initialize();
			listeAffichage.Add(affichageSouris);

			les_brique01 = new Brique[NBBRIQUES];
			for (int i = 0; i < NBBRIQUES; i++)
			{
				les_brique01[i] = new Brique();
				les_brique01[i].Initialize(new Vector2(ra.Next(10, 1000), ra.Next(10, 700)), Color.Black, ra.Next(0, 360), ra.Next(5, 80), ra.Next(5, 80));
				listeAffichage.Add(les_brique01[i]);
			}


			les_robots01 = new RobotAutomatique[NBROBOTS];
			for (int i = 0; i < NBROBOTS; i++)
			{
				les_robots01[i] = new RobotAutomatique();
				les_robots01[i].Initialize(new Vector2(ra.Next(10, 950), ra.Next(10, 760)), Color.Red, 0, ra.Next(10, 40));

				if (les_brique01.Length > i)
				{
					les_robots01[i].LaBrique = les_brique01[i];
				}
				listeAffichage.Add(les_robots01[i]);
			}


			axeX = new LigneDebug();
			axeY = new LigneDebug();
			centreToSouris = new LigneDebug();
			brique = new Brique();
			auto = new RobotAutomatique();

			axeX.Initialize(new Vector2(0, centreEcran.Y), new Vector2(centreEcran.X * 2, centreEcran.Y));
			axeY.Initialize(new Vector2(centreEcran.X, 0), new Vector2(centreEcran.X, centreEcran.Y * 2));
			centreToSouris.Initialize(centreEcran, Vector2.Zero, Color.White);
			brique.Initialize(Vector2.Add(centreEcran, new Vector2(0, -100)), Color.Black, 0, 50, 20);
			auto.Initialize(centreEcran, Color.Black, 0, 50);
			auto.LaBrique = brique;

			listeAffichage.Add(axeX);
			listeAffichage.Add(axeY);
			listeAffichage.Add(centreToSouris);
			listeAffichage.Add(brique);
			listeAffichage.Add(affichageSouris);
			listeAffichage.Add(auto);

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

			foreach (ILoadAndDraw ild in listeAffichage)
			{
				ild.LoadContent(primitiveBatch);
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
			keyboardState = Keyboard.GetState();
			mouseState = Mouse.GetState();
			Vector2 vSouris = new Vector2(mouseState.X, mouseState.Y);

			// Allows the game to exit
			if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				|| keyboardState.IsKeyDown(Keys.Escape))
				this.Exit();

			if (keyboardState.IsKeyDown(Keys.R))
				this.Initialize();

			// TODO: Add your update logic here
			affichageSouris.HandleInput(mouseState);

			foreach (Robot r in les_robots01)
			{
				r.Update(gameTime);
			}
			foreach (Brique b1 in les_brique01)
			{
				b1.Update(gameTime);
			}
			brique.position = vSouris;
			auto.Update(gameTime);

			centreToSouris.setPosition2(vSouris);


			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			textAffiche = new Vector2(10, -20);

			// TODO: Add your drawing code here
			foreach (ILoadAndDraw ild in listeAffichage)
			{
				ild.Draw();
			}

			spriteBatch.Begin();
			spriteBatch.DrawString(_font, "X : " + mouseState.X + " -- Y : " + mouseState.Y, textAffiche += ajoutTextAffiche, Color.White);

			spriteBatch.End();
			base.Draw(gameTime);
		}

		private Vector2 afficherLent = Vector2.Zero;
	}
}
