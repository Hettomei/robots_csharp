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

		private const int NBROBOTS = 200;
		private const int NBBRIQUES = 1000;

		private Robot[] les_robots01;
		private Brique[] les_brique01;
		private List<Brique> les_brique_restantes;

		private int clique = 0;
		private float affiche_vitesse_robots = 0;
		private int ralentit = 0;

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
				//les_brique01[i].Initialize(new Vector2(ra.Next(10, 1000), ra.Next(600, 750)), Color.Black, ra.Next(0, 360), ra.Next(1, 2)); //toute la zone du bas
				les_brique01[i].Initialize(new Vector2(100, 600), Color.Black, 45, 4); //tout au meme point
				listeAffichage.Add(les_brique01[i]);
			}
			les_brique_restantes = les_brique01.ToList();


			les_robots01 = new RobotAutomatique[NBROBOTS];
			for (int i = 0; i < NBROBOTS; i++)
			{
				les_robots01[i] = new RobotAutomatique();
				les_robots01[i].Initialize(new Vector2(ra.Next(10, 950), ra.Next(10, 760)), Color.Red, 0, ra.Next(2, 5));
				//	les_robots01[i].Initialize(new Vector2(i * 10 + 10, 500), Color.Red, i * 3 + 60, 5);
				//	les_robots01[i].Initialize(Vector2.Zero, Color.Red,90, 5);

				if (les_brique01.Length > i)
				{
					les_robots01[i].LaBrique = les_brique01[i];
					les_brique_restantes.Remove(les_brique01[i]);
				}
				listeAffichage.Add(les_robots01[i]);
			}

			//afficherText.afficherTim(les_brique01);
			//afficherText.afficherTimLeR500(les_brique01);
			//afficherText.dessinSoleilMaisonGuguss(les_brique01);
			//afficherText.bonjourLudo1000PetiteBrique(les_brique01);

			afficherText.AngieMaCherie(les_brique01);
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

			// Allows the game to exit
			if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				|| keyboardState.IsKeyDown(Keys.Escape))
				this.Exit();

			if (keyboardState.IsKeyDown(Keys.R))
				this.Initialize();

			//ralentit les robots
			if (keyboardState.IsKeyDown(Keys.Down)){
				foreach (Robot r in les_robots01)
				{

						r.speedBase -= 0.01f;
						if (r.speedBase < 0.0001f)
						{
							r.speedBase = 0.0002f;
						}
					
				}
				if (les_robots01.Length > 0)
				{
					affiche_vitesse_robots = les_robots01[0].speedBase;		 
				}
			}
			//accelere les robots
			if (keyboardState.IsKeyDown(Keys.Up))
			{
				foreach (Robot r in les_robots01)
				{

						r.speedBase += 0.01f;
					
				}
				if (les_robots01.Length > 0)
				{
					affiche_vitesse_robots = les_robots01[0].speedBase;
				}
			}

			//augmente la taille des robots
			if (keyboardState.IsKeyDown(Keys.T))
			{
				foreach (Robot r in les_robots01)
				{
					r.robotSize += 0.1f;
					r.changeTailleRobot();
				}
			}

			//reduit la taille des robots
			if (keyboardState.IsKeyDown(Keys.G))
			{
				foreach (Robot r in les_robots01)
				{
					r.robotSize -= 0.1f;
					r.changeTailleRobot();
				}
			}

			//reduit la taille des brique
			if (keyboardState.IsKeyDown(Keys.H))
			{
				foreach (Brique b in les_brique01)
				{
					b.size -= 0.1f;
					b.changeTailleBrique();
				}
			}

			//augmente la taille des brique
			if (keyboardState.IsKeyDown(Keys.Y))
			{
				foreach (Brique b in les_brique01)
				{
					b.size += 0.1f;
					b.changeTailleBrique();
				}
			}

			if (keyboardState.IsKeyDown(Keys.Space))
			{
				ralentit++;
				if (ralentit % 5 == 0)
				{
					if (clique < les_brique01.Length)
					{
						les_brique01[clique].Position = new Vector2(mouseState.X, mouseState.Y);
						Console.WriteLine("les_brique01[" + clique + "].positionFinal = new Vector2(" + mouseState.X + ", " + mouseState.Y + ");");
						clique++;
						ralentit = 0;
					}
				}
			}

			if (keyboardState.IsKeyDown(Keys.N))
			{
				ralentit++;
				if (ralentit % 5 == 0)
				{
					clique += (clique > 0 ? -1 : 0);
					Console.WriteLine(clique + " a annuler");
				}
			}
			// TODO: Add your update logic here
			//brique01.HandleInput(mouseState);

			foreach (Robot r in les_robots01)
			{
				if (r.phaseEnCour == Robot.Phase.PasDeTravail && les_brique_restantes.Count > 0)
				{
					r.LaBrique = les_brique_restantes[0];
					les_brique_restantes.Remove(les_brique_restantes[0]);
				}

				r.Update(gameTime);
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
			textAffiche = new Vector2(10, 600);

			// TODO: Add your drawing code here
			foreach (ILoadAndDraw ild in listeAffichage)
			{
				ild.Draw();
			}

			spriteBatch.Begin();
			//spriteBatch.DrawString(_font, "X : " + mouseState.X + " -- Y : " + mouseState.Y, textAffiche += ajoutTextAffiche, Color.White);
			spriteBatch.DrawString(_font, "Espace : deplace les briques", textAffiche += ajoutTextAffiche, Color.White);
			spriteBatch.DrawString(_font, "N :annule le déplacement briques", textAffiche += ajoutTextAffiche, Color.White);
			spriteBatch.DrawString(_font, "Bas/Haut : reduit/augmente la vitesse max", textAffiche += ajoutTextAffiche, Color.White);
			spriteBatch.DrawString(_font, "T/G : augmente/reduit la taille des robots", textAffiche += ajoutTextAffiche, Color.White);
			spriteBatch.DrawString(_font, "Y/H : augmente/reduit la taille des briques", textAffiche += ajoutTextAffiche, Color.White);
			spriteBatch.DrawString(_font, "Vitesse : " + affiche_vitesse_robots, textAffiche += ajoutTextAffiche, Color.White);
			//spriteBatch.DrawString(_font, "" + clique, textAffiche += ajoutTextAffiche, Color.White);
			

			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
