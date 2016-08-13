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
		public static Random ra = new Random();

		//Ajoute automatiquement le texte en dessous
		private Vector2 textAffiche;
		private Vector2 ajoutTextAffiche = new Vector2(0, 20);

		private List<ILoadAndDraw> listeAffichage;

		private Souris affichageSouris;

		private const int NBROBOTS = 10;
		private const int NBBRIQUES = 1000;

		private Robot[] les_robots01;
		private Brique[] les_brique01;
		private List<Brique> les_brique_restantes;

		private float speed_en_cours = 0.6f;
		private float rotation_en_cours = 7f;
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
			primitiveBatch = new PrimitiveBatch(GraphicsDevice);
			listeAffichage = new List<ILoadAndDraw>();
			les_brique_restantes = new List<Brique>();

			affichageSouris = new Souris();
			affichageSouris.Initialize();
			listeAffichage.Add(affichageSouris);

			les_brique01 = new Brique[NBBRIQUES];
			for (int i = 0; i < NBBRIQUES; i++)
			{
				les_brique01[i] = new Brique();
				//les_brique01[i].Initialize(new Vector2(ra.Next(10, 1000), ra.Next(10, 700)), Color.Black, ra.Next(0, 360), ra.Next(5, 10)); //tout ecran
				//les_brique01[i].Initialize(new Vector2(ra.Next(400, 600), ra.Next(350, 400)), Color.Black, ra.Next(0, 360), ra.Next(5, 10)); //oncentré au centre
				//les_brique01[i].Initialize(new Vector2(ra.Next(1000, 1000), ra.Next(700, 700)), Color.Black, ra.Next(0, 360), ra.Next(5, 10)); //concentré bas droite
				//les_brique01[i].Initialize(new Vector2(ra.Next(500, 1000), ra.Next(10, 500)), Color.Black, ra.Next(0, 360), ra.Next(2, 10));
				les_brique01[i].Initialize(new Vector2(ra.Next(10, 1000), ra.Next(600, 750)), Color.Black, ra.Next(0, 360), ra.Next(1, 2)); //toute la zone du bas

				listeAffichage.Add(les_brique01[i]);
			}
			les_brique_restantes = les_brique01.ToList();


			les_robots01 = new RobotAutomatique[NBROBOTS];
			for (int i = 0; i < NBROBOTS; i++)
			{
				les_robots01[i] = new RobotAutomatique();
				les_robots01[i].Initialize(new Vector2(ra.Next(10, 950), ra.Next(10, 760)), Color.Red, 0, ra.Next(10,15));
			//	les_robots01[i].Initialize(new Vector2(i * 10 + 10, 500), Color.Red, i * 3 + 60, 5);
			//	les_robots01[i].Initialize(Vector2.Zero, Color.Red,90, 5);

				if (les_brique01.Length > i)
				{
					les_robots01[i].LaBrique = les_brique01[i];
					les_brique_restantes.Remove(les_brique01[i]);
				}
				listeAffichage.Add(les_robots01[i]);
			}
			afficherText.salutNakib1000(les_brique01);
			//afficherText.dessinSoleilMaisonGuguss(les_brique01);
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
		bool modif = false;
		protected override void Update(GameTime gameTime)
		{
			keyboardState = Keyboard.GetState();
			mouseState = Mouse.GetState();

			// Allows the game to exit
			if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				|| keyboardState.IsKeyDown(Keys.Escape))
				this.Exit();

			if (keyboardState.IsKeyDown(Keys.R))
				this.Initialize();

			if (keyboardState.IsKeyDown(Keys.Up))
			{
				speed_en_cours += 0.05f;
				modif = true;
			}
			else if (keyboardState.IsKeyDown(Keys.Down))
			{
				speed_en_cours -= 0.05f;
				modif = true;
			}

			if (keyboardState.IsKeyDown(Keys.Left))
			{
				rotation_en_cours -= 0.01f;
				modif = true;
			}
			else if (keyboardState.IsKeyDown(Keys.Right))
			{
				rotation_en_cours += 0.01f;
				modif = true;
			}

			// TODO: Add your update logic here
			//brique01.HandleInput(mouseState);

			foreach (Robot r in les_robots01)
			{
				if (modif){
					r.vitesse_rotation = rotation_en_cours;
					r.speedBase = speed_en_cours;
				}
				if (r.phaseEnCour == Robot.Phase.PasDeTravail && les_brique_restantes.Count > 0)
				{
					r.LaBrique = les_brique_restantes[0];
					les_brique_restantes.Remove(les_brique_restantes[0]);
				}

				r.Update(gameTime);
			}

			if (modif)
			{
				modif = false;
			}
			//foreach (Brique b1 in les_brique01)
			//{
			//    b1.Update(gameTime);
			//}

			affichageSouris.HandleInput(mouseState);

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
			spriteBatch.DrawString(_font, "Vitesse : " + speed_en_cours, textAffiche += ajoutTextAffiche, Color.White);
			spriteBatch.DrawString(_font, "rotation : " + rotation_en_cours, textAffiche += ajoutTextAffiche, Color.White);
			spriteBatch.DrawString(_font, "Haut, bas -> vitesse -- gauche, droite -> rotation ", textAffiche += ajoutTextAffiche, Color.White);


			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
