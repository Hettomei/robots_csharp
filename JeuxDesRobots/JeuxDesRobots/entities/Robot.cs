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
	abstract class Robot : ILoadAndDraw
	{
		private PrimitiveBatch primitiveBatch;

		private Vector2 position;

		public Vector2 Position
		{
			get { return position; }
			set
			{

				int i = 0;
				Vector2 nouvelleEmplacement = value - position;
				while (i < listePoints.Count)
				{
					listePoints[i] += nouvelleEmplacement;
					i++;
				}
				position = value;
			}
		}

		public Vector2 pointAviser;

		protected float speed;
		public float speedBase;
		protected float vitesse_rotation;
		protected float vitesse_rotation_base;
		protected Color couleur;
		public float robotSize;
		protected float distanceProbleme;

		#region Tous pour les points

		public List<Vector2> listePoints;
		//L'ordre DOIT correspondre à enum NomDesPoints !
		//protected Vector2[] modeleRobotPoints = new Vector2[5]{
		//    new Vector2(-1, 1), 
		//    new Vector2(0, 3),
		//    new Vector2(1, 1),
		//    new Vector2(1, -1),
		//    new Vector2(-1, -1)
		//};
		protected Vector2[] modeleRobotPoints = new Vector2[10]{
			new Vector2(-1, 3), 
			new Vector2(-1, -1),
			new Vector2(1, -1),
			new Vector2(1, 3),
			new Vector2(1, 1),
			new Vector2(-1, 1),
			new Vector2(0, 1),
			new Vector2(0, 2),
			new Vector2(0, 1),
			new Vector2(-1, 1)
		};
		protected int numeroDuPointChaud = 7; // ici le nez donc modeleRobotPoints[1]

		#endregion

		public enum Phase
		{
			PasDeTravail, viseUneBrique, transporteUneBrique, ennuie
		}
		public Phase phaseEnCour;

		private Brique _laBrique;

		public Brique LaBrique
		{
			get { return _laBrique; }
			set
			{
				if (value != null)
				{
					pointAviser = value.Position;
					phaseEnCour = Phase.viseUneBrique;
				}
				else
				{
					phaseEnCour = Phase.PasDeTravail;
				}
				_laBrique = value;
			}
		}


		/// <summary>
		/// Initialise les variables du Sprite
		/// </summary>
		public virtual void Initialize()
		{
			Initialize(new Vector2(500, 300), Color.Red, 0f, 50f);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="position"></param>
		/// <param name="couleur"></param>
		/// <param name="rotation">En degré, de 0 à 360</param>
		/// <param name="robotsize"></param>
		public virtual void Initialize(Vector2 position, Color couleur, float rotation, float robotsize)
		{
			this.position = position;
			this.speed = 0.3f;
			this.speedBase = speed;
			this.vitesse_rotation = 7f;
			this.vitesse_rotation_base = this.vitesse_rotation;
			this.couleur = couleur;
			this.robotSize = robotsize;
			this.phaseEnCour = Phase.PasDeTravail;

			initialiseLaBaseDesPoints(rotation);

			this.distanceProbleme = Vector2.DistanceSquared(listePoints[numeroDuPointChaud], position);
		}

		//Enregistre tous les vecteurs à la bonne position de l'écran à la bonne taille en fonction du modele
		private void initialiseLaBaseDesPoints(float rotation)
		{
			this.listePoints = new List<Vector2>(7); //(from a blog) My research shows that capacity can improve performance by nearly two times for adding elements
			foreach (Vector2 v in modeleRobotPoints)
			{
				//Multiplie de cette facon car le modele est basé sur un axe classique
				//C# inverse le Y
				listePoints.Add(Vector2.Multiply(v, new Vector2(robotSize, -robotSize)) + this.position);
			}
			calculEmplacementPointsApresRotation(rotation);
		}

				//Enregistre tous les vecteurs à la bonne position de l'écran à la bonne taille en fonction du modele
		public void changeTailleRobot()
		{
			this.listePoints = new List<Vector2>(7); //(from a blog) My research shows that capacity can improve performance by nearly two times for adding elements
			foreach (Vector2 v in modeleRobotPoints)
			{
				//Multiplie de cette facon car le modele est basé sur un axe classique
				//C# inverse le Y
				listePoints.Add(Vector2.Multiply(v, new Vector2(robotSize, -robotSize)) + this.position);
			}
		}
		/// <summary>
		/// Calcule l'emplacement des points apres une rotation
		/// Si la rotation vaut 0, on ne touche pas les points
		/// </summary>
		/// <param name="angleEnDegre"></param>
		protected void calculEmplacementPointsApresRotation(float angleEnDegre)
		{
			if (angleEnDegre != 0)
			{
				float theta = MathHelper.ToRadians(angleEnDegre);

				int i = 0;
				while (i < listePoints.Count)
				{
					listePoints[i] = AngleHelper.getPointAfterRotate(listePoints[i], position, theta);
					i++;
				}
			}
		}

		public virtual void LoadContent(PrimitiveBatch primitive)
		{
			primitiveBatch = primitive;
		}

		/// <summary>
		/// Met à jour les variables du sprite
		/// </summary>
		/// <param name="gameTime">Le GameTime associé à la frame</param>
		public abstract void Update(GameTime gameTime);
		public abstract void HandleInput(KeyboardState keyboardState);

		/// <summary>
		/// Dessine le sprite
		/// </summary>
		public virtual void Draw()
		{
			DrawRotateSquareAndNose();
		}

		/// <summary>
		/// dessine un carré autour du centre
		/// </summary>
		private void DrawRotateSquareAndNose()
		{
			// tell the primitive batch to start drawing lines
			primitiveBatch.dessinerDessinComplet(listePoints, couleur, true);
		}

	}
}
