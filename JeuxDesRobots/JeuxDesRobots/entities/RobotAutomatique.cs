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
					//pointAviser = new Vector2(100, 100);
					couleur = Color.White;

					break;
				case Phase.ennuie:
					pointAviser = new Vector2(Game1.ra.Next(0,1000), Game1.ra.Next(0,700));
			//		speedBase = 0.15f;
					phaseEnCour = Phase.PasDeTravail;
					break;
				case Phase.viseUneBrique:
				//	couleur = Color.Red;
					break;
				case Phase.transporteUneBrique:
				//	couleur = Color.Green;
					pointAviser = LaBrique.positionFinal;
					break;
			}
			if (Vector2.DistanceSquared(pointAviser, listePoints[numeroDuPointChaud]) > 10)
			{
				//D'abord on fait tourner le nez en visant la brique.:
				//rotation en degré
				int max = 0;
				int sens = AngleHelper.determinerSiTournePositifOuNegatifPourRejoindreAutrePoint(Position, listePoints[numeroDuPointChaud], pointAviser, out max);
				float rotation;

				//permet de ne pas trop tourner;
				if (vitesse_rotation < max)
					rotation = vitesse_rotation * sens;
				else
					rotation = max * sens;

				calculEmplacementPointsApresRotation(rotation);

				if (AngleHelper.peutAvancer(Position, listePoints[numeroDuPointChaud], pointAviser))
				{
					if (speed < speedBase)
						speed += 0.006f;
				}
				else
				{
					speed -= 0.006f;
					if (speed < 0.001)
					{
						//peut tourner en rond pas loind d'ici
						if (Vector2.DistanceSquared(Position, pointAviser) < distanceProbleme+100)
						{
							speed = speedBase;
							Console.WriteLine("NAAAAAAAAAAAANNNNNNNNNNNNNNNNNNNNNN");
						}
						else
							speed = 0.0005f;
					}
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
			{
				LaBrique = null;
			}

			if (phaseEnCour == Phase.PasDeTravail && Vector2.DistanceSquared( Position,pointAviser) < 1000)
			{
				phaseEnCour = Phase.ennuie;
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
