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
	class RobotAutomatique : Robot
	{

		/// <summary>
		/// Met à jour les variables du sprite
		/// </summary>
		/// <param name="gameTime">Le GameTime associé à la frame</param>
		public override void Update(GameTime gameTime)
		{
			if (LaBrique != null)
			{

				//D'abord on fait tourner le nez en visant la brique.:
				//rotation en degré
				float rotation = AngleHelper.determinerSiTournePositifOuNegatifPourRejoindreAutrePoint(position, listePoints[(int)NomDesPoints._2nezHautMilieu], LaBrique.position) * vitesse_rotation;
				//La rotation prend 1 ou -1 degré * vitesse_rotation

				calculEmplacementPointsApresRotation(rotation);

				//On calcule la direction du robot, donc le vecteur du centre au nez puis on le fait avancer dans cette direction
				Vector2 v = Vector2.Normalize(Vector2.Subtract(listePoints[(int)NomDesPoints._2nezHautMilieu], position)) * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

				calculEmplacementPointApresDirection(v);
			}
		}

		/// <summary>
		/// Permet de gérer les entrées du joueur
		/// </summary>
		/// <param name="keyboardState">L'état du clavier à tester</param>
		/// <param name="mouseState">L'état de la souris à tester</param>
		public override void HandleInput(KeyboardState keyboardState)
		{
		}
	}
}
