﻿using System;
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
	class Dechet : ILoadAndDraw
	{
		private static Random ra = new Random();
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
		protected float speed;
		protected float rotation;
		protected Color couleur;
		protected float size;
		public Vector2 positionFinal;


		#region Tous pour les points

		public List<Vector2> listePoints;
		//L'ordre DOIT correspondre à enum NomDesPoints !
		protected Vector2[] modeleDiforme;

		#endregion

		/// <summary>
		/// Initialise les variables du Sprite
		/// </summary>
		public virtual void Initialize()
		{
			Initialize(new Vector2(500, 300), Color.Red, 0f, 25);
		}

		public virtual void Initialize(Vector2 position, Color couleur, float rotation, float size)
		{
			//
			this.speed = 0;
			this.listePoints = new List<Vector2>(4); //My research shows that capacity can improve performance by nearly two times for adding elements
			this.positionFinal = new Vector2(500, 380);
			
			this.size = size;

			this.Position = position;
			this.couleur = couleur;
			this.rotation = rotation;

			genererModeleDiforme();
			initialiseLaBaseDesPoints(rotation);

		}

		private void genererModeleDiforme()
		{
			int nbDePoint = ra.Next(2, 10);
			this.modeleDiforme = new Vector2[nbDePoint];
			float theta;

			Vector2 pointPrecedant = new Vector2(0,1);
			int i = 0;
			while (i < nbDePoint)
			{
				theta = MathHelper.ToRadians(ra.Next(0, 45));
				modeleDiforme[i] = AngleHelper.getPointAfterRotate(pointPrecedant,Vector2.Zero,theta);
				pointPrecedant = modeleDiforme[i];
				i++;
			}
		}

		/// <summary>
		/// Calcule l'emplacement des points apres une rotation
		/// Si la rotation vaut 0, on ne touche pas les points
		/// </summary>
		/// <param name="angleEnDegre"></param>
		public void calculEmplacementPointsApresRotation(float angleEnDegre)
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

		//Enregistre tous les vecteurs à la bonne position de l'écran à la bonne taille en fonction du modele
		private void initialiseLaBaseDesPoints(float rotation)
		{
			foreach (Vector2 v in modeleDiforme)
			{
				//Multiplie de cette facon car le modele est basé sur un axe classique
				//C# inverse le Y
				listePoints.Add(Vector2.Multiply(v, new Vector2(size, -size)) + this.position);
			}
			calculEmplacementPointsApresRotation(rotation);
		}

		public virtual void LoadContent(PrimitiveBatch primitive)
		{
			primitiveBatch = primitive;
		}

		/// <summary>
		/// Met à jour les variables du sprite
		/// </summary>
		/// <param name="gameTime">Le GameTime associé à la frame</param>
		public void Update(GameTime gameTime)
		{
		}
		public void HandleInput(MouseState mouse)
		{
		}

		/// <summary>
		/// Dessine le sprite en utilisant ses attributs et le spritebatch donné
		/// </summary>
		/// <param name="spriteBatch">Le spritebatch avec lequel dessiner</param>
		/// <param name="gameTime">Le GameTime de la frame</param>
		public virtual void Draw()
		{
			DrawRotateBrique();
		}

		/// <summary>
		/// dessine un carré autour du centre
		/// </summary>
		/// <param name="centre">centre du carré</param>
		/// <param name="size"></param>
		/// <param name="angle">en degré, rotation sens horaires</param>
		/// <param name="color"></param>
		private void DrawRotateBrique()
		{
			primitiveBatch.dessinerDessinComplet(listePoints, couleur, true);

		}

		public bool EstArriveAdestination()
		{
			return Vector2.DistanceSquared(position, positionFinal) < 300;
		}
	}
}
