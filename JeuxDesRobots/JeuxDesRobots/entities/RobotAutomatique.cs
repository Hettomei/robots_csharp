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
			switch (phaseEnCour)
			{
				case Phase.PasDeTravail:
					pointAviser = new Vector2(100, 100);
					break;
				case Phase.viseUneBrique:
					angleAviser = AngleHelper.AngleOfView(Position, listePoints[numeroDuPointChaud], LaBrique.Position);
					angleEnCours = 0;
					break;
				case Phase.transporteUneBrique:
					angleAviser = AngleHelper.AngleOfView(Position, listePoints[numeroDuPointChaud], LaBrique.positionFinal);
					angleEnCours = 0;
					pointAviser = LaBrique.positionFinal;
					break;
			}
			if (Vector2.DistanceSquared(pointAviser, listePoints[numeroDuPointChaud]) > 10)
			{
				//D'abord on fait tourner le nez en visant la brique.:
				////rotation en degré
				//int max = 0;
				//int sens = AngleHelper.determinerSiTournePositifOuNegatifPourRejoindreAutrePoint(Position, listePoints[numeroDuPointChaud], pointAviser, out max);
				float rotation = 0;
				rotation = vitesse_rotation_base;
				angleEnCours += vitesse_rotation_base;
				if (Math.Abs(angleAviser) < Math.Abs(angleEnCours)){
					rotation = vitesse_rotation_base / 2;
				}

				calculEmplacementPointsApresRotation(rotation);

				if (AngleHelper.peutAvancer(Position, listePoints[numeroDuPointChaud], pointAviser))
				{
					if (speed < speedBase)
						speed += 0.006f;
				}
				else
				{
					speed -= 0.006f;
					if (speed < 0)
						speed = 0.001f;
				}

				//On calcule la direction du robot, donc le vecteur du centre au nez puis on le fait avancer dans cette direction
				Vector2 v = Vector2.Normalize(Vector2.Subtract(listePoints[numeroDuPointChaud], Position)) * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
				Position += v;

				if (phaseEnCour == Phase.transporteUneBrique)
				{
					LaBrique.Position = listePoints[numeroDuPointChaud];
					LaBrique.calculEmplacementPointsApresRotation(rotation);
				}

			}

			if (phaseEnCour == Phase.viseUneBrique && Vector2.DistanceSquared(LaBrique.Position, listePoints[numeroDuPointChaud]) < 50)
				phaseEnCour = Phase.transporteUneBrique;

			if (phaseEnCour == Phase.transporteUneBrique && LaBrique.EstArriveAdestination())
				LaBrique = null;



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
