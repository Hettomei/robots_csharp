using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JeuxDesRobots
{
	class RobotManuel : Robot
	{
		private Vector2 direction;


		/// <summary>
		/// Permet de gérer les entrées du joueur
		/// </summary>
		/// <param name="keyboardState">L'état du clavier à tester</param>
		/// <param name="mouseState">L'état de la souris à tester</param>
		public override void HandleInput(KeyboardState keyboardState)
		{
			if (keyboardState.IsKeyDown(Keys.Up))
			{
				direction = AngleHelper.getPointAfterRotate(-Vector2.UnitY, Vector2.Zero, vitesse_rotation);
				speed = 0.3f;
			}
			else if (keyboardState.IsKeyDown(Keys.Down))
			{
				direction = AngleHelper.getPointAfterRotate(Vector2.UnitY, Vector2.Zero, -vitesse_rotation);

				speed = 0.3f;
			}
			else
			{
				speed = 0;
			}

			if (keyboardState.IsKeyDown(Keys.Left))
			{
				vitesse_rotation = 4;
			}
			else if (keyboardState.IsKeyDown(Keys.Right))
			{
				vitesse_rotation = -4;
			}
		}

		/// <summary>
		/// Met à jour les variables du sprite
		/// </summary>
		/// <param name="gameTime">Le GameTime associé à la frame</param>
		public override void Update(GameTime gameTime)
		{
			Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
		}

	}
}
