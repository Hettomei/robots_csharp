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
	class Brique : ILoadAndDraw
	{
		private PrimitiveBatch primitiveBatch;
		public Vector2 position;
		protected Vector2 direction;
		protected float speed;
		protected float rotation;
		protected Color couleur;
		protected float sizeX;
		protected float sizeY;

		private Vector2 hautGauche, hautDroit, basGauche, basdroit;

		protected bool hasMoved; // false -> l'objet n'a pas bougé

		/// <summary>
		/// Initialise les variables du Sprite
		/// </summary>
		public virtual void Initialize()
		{
			Initialize(new Vector2(500, 300), Color.Red, 0f, 50f, 25f);
		}

		public virtual void Initialize(Vector2 position, Color couleur, float rotation, float sizeX, float sizeY)
		{
			this.position = position;
			this.direction = new Vector2(0, 0); //Commence le nez en haut // a recalculer avec angle
			this.speed = 0;
			this.couleur = couleur;
			this.rotation = rotation;
			this.sizeX = sizeX;
			this.sizeY = sizeY;
			this.hasMoved = true;
			initPoint();
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
			hasMoved = false;
		}
		public void HandleInput(MouseState mouse)
		{
			//if (position.X != mouse.X || position.Y != mouse.Y)
			//{
			//    position.X = mouse.X;
			//    position.Y = mouse.Y;
			//    this.hasMoved = true;
			//}
			//else
			//{
			//    this.hasMoved = false;
			//}
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
			if (hasMoved)
			{
				initPoint();
			}
			primitiveBatch.Begin(PrimitiveType.LineList);

			primitiveBatch.AddVertex(hautGauche, couleur);
			primitiveBatch.AddVertex(hautDroit, couleur);

			primitiveBatch.AddVertex(hautDroit, couleur);
			primitiveBatch.AddVertex(basdroit, couleur);

			primitiveBatch.AddVertex(basdroit, couleur);
			primitiveBatch.AddVertex(basGauche, couleur);

			primitiveBatch.AddVertex(basGauche, couleur);
			primitiveBatch.AddVertex(hautGauche, couleur);

			primitiveBatch.End();
		}

		private void initPoint()
		{
			hautGauche = position + new Vector2(-(sizeX / 2), -(sizeY / 2));
			hautDroit = hautGauche + new Vector2(sizeX, 0f);
			basGauche = hautGauche + new Vector2(0f, sizeY);
			basdroit = basGauche + new Vector2(sizeX, 0f);

			hautGauche = AngleHelper.getPointAfterRotate(hautGauche, position, rotation);
			hautDroit = AngleHelper.getPointAfterRotate(hautDroit, position, rotation);
			basGauche = AngleHelper.getPointAfterRotate(basGauche, position, rotation);
			basdroit = AngleHelper.getPointAfterRotate(basdroit, position, rotation);
		}
	}
}
