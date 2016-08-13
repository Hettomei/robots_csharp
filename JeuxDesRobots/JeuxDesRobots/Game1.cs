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
		private KeyboardState keyboardState;
		private MouseState mouseState;
		private PrimitiveBatch primitiveBatch;
		private Robot r1;
		private Robot auto1;
		private LigneDebug ld1;
		private LigneDebug zeroToSouris;
		private LigneDebug zeroToBrique;

		private LigneDebug rotationAutoQuaternion;
		private LigneDebug centreToSouris;

		private Vector2 centreEcran;

		private List<Robot> lr1 = new List<Robot>();
		private Brique b1;
		private Souris affichageSouris;
		private SpriteFont _font;

		private Vector2 textAffiche = new Vector2(10, 0);
		private Vector2 ajoutTextAffiche = new Vector2(0, 20);

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
			r1 = new RobotManuel();
			auto1 = new RobotAutomatique();
			b1 = new Brique();
			ld1 = new LigneDebug();
			zeroToBrique = new LigneDebug();
			zeroToSouris = new LigneDebug();
			affichageSouris = new Souris();

			rotationAutoQuaternion = new LigneDebug();
			centreToSouris = new LigneDebug();

			Vector2 centreRobotAuto = new Vector2(100, 600);
			Vector2 centreBrique = new Vector2(500, 500);


			r1.Initialize();
			auto1.Initialize(centreRobotAuto, 0f, Color.Red, 0, 70);
			b1.Initialize(centreEcran, 0, Color.Black, 23f, 70, 20);
			ld1.Initialize(centreRobotAuto, centreEcran);
			zeroToBrique.Initialize(Vector2.Zero, centreEcran);
			zeroToSouris.Initialize(Vector2.Zero, Vector2.Zero);

			rotationAutoQuaternion.Initialize(centreEcran, centreEcran / 2, Color.Black);
			centreToSouris.Initialize(centreEcran, Vector2.Zero, Color.White);

			affichageSouris.Initialize();

			Console.WriteLine(auto1.getPointNoze());

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

			r1.LoadContent(primitiveBatch);
			b1.LoadContent(primitiveBatch);
			auto1.LoadContent(primitiveBatch);
			ld1.LoadContent(primitiveBatch);
			affichageSouris.LoadContent(primitiveBatch);
			zeroToSouris.LoadContent(primitiveBatch);
			zeroToBrique.LoadContent(primitiveBatch);

			rotationAutoQuaternion.LoadContent(primitiveBatch);
			centreToSouris.LoadContent(primitiveBatch);
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
			keyboardState = Keyboard.GetState();
			mouseState = Mouse.GetState();
			Vector2 vSouris = new Vector2(mouseState.X, mouseState.Y);
			// Allows the game to exit
			if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				|| keyboardState.IsKeyDown(Keys.Escape))
				this.Exit();


			// TODO: Add your update logic here
			r1.HandleInput(keyboardState);
			r1.Update(gameTime);
			b1.Update(gameTime);
			auto1.Update(gameTime);
			zeroToSouris.setPosition2(vSouris);
			affichageSouris.HandleInput(mouseState);


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

			// TODO: Add your drawing code here
			r1.Draw();
			b1.Draw();
			auto1.Draw();
			ld1.Draw();
			affichageSouris.Draw();
			zeroToSouris.Draw();
			zeroToBrique.Draw();

			rotationAutoQuaternion.Draw();
			//	centreToSouris.Draw();

			spriteBatch.Begin();
			textAffiche = new Vector2(500, -20);
			spriteBatch.DrawString(_font, "X : " + mouseState.X + " -- Y : " + mouseState.Y, textAffiche += ajoutTextAffiche, Color.White);
			spriteBatch.DrawString(_font, "" + AngleHelper.AngleOfView(auto1.position, affichageSouris.position, b1.position), textAffiche += ajoutTextAffiche, Color.White);
			spriteBatch.DrawString(_font, "" + AngleHelper.CounterClockWise(auto1.position, affichageSouris.position, b1.position), textAffiche += ajoutTextAffiche, Color.White);

			spriteBatch.DrawString(_font, "---------------", textAffiche += ajoutTextAffiche, Color.White);

			Vector2 pointSouris = affichageSouris.position;
			Vector2 pointAngle = auto1.position;
			Vector2 pointFinal = b1.position;
			float resultDotRadian;
			float angleDegre;

			spriteBatch.DrawString(_font, "Angle ancienne methode : " + AngleHelper.AngleOfView(Vector2.Zero, pointSouris, pointFinal), textAffiche += ajoutTextAffiche, Color.White);
			//	spriteBatch.DrawString(_font, "" + AngleHelper.CounterClockWise(Vector2.Zero, pointSouris, pointFinal), textAffiche += ajoutTextAffiche, Color.White);



			pointSouris.Normalize();
			pointFinal.Normalize();
			Vector2.Dot(ref pointSouris, ref pointFinal, out resultDotRadian);

			spriteBatch.DrawString(_font, "scalaire : " + resultDotRadian, textAffiche += ajoutTextAffiche, Color.White);
			angleDegre = MathHelper.ToDegrees((float)Math.Acos(resultDotRadian));

			spriteBatch.DrawString(_font, "Angle acos : " + angleDegre, textAffiche += ajoutTextAffiche, Color.White);

			int signeAngle = AngleHelper.CounterClockWise(Vector2.Zero, pointSouris, pointFinal);
			spriteBatch.DrawString(_font, "Angle avec signe : " + signeAngle + "->" + signeAngle*angleDegre, textAffiche += ajoutTextAffiche, Color.White);

			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
