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
	class Souris : ILoadAndDraw
	{
		private PrimitiveBatch primitiveBatch;
		public Vector2 position;
		private int size;
		private Color couleur;
		protected bool hasMoved; // false -> l'objet n'a pas bougé

		private Vector2 hautGauche, hautDroit, basGauche, basdroit;

		/// <summary>
		/// Initialise les variables du Sprite
		/// </summary>
		public virtual void Initialize()
		{
			this.size = 20;
			this.position = new Vector2(0, 0);
			this.couleur = Color.OrangeRed;
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
		}
		public void HandleInput(MouseState mouse)
		{
			if (position.X != mouse.X || position.Y != mouse.Y)
			{
				position.X = mouse.X;
				position.Y = mouse.Y;
				this.hasMoved = true;
			}
			else
			{
				this.hasMoved = false;
			}
		}

		/// <summary>
		/// Dessine le sprite en utilisant ses attributs et le spritebatch donné
		/// </summary>
		/// <param name="spriteBatch">Le spritebatch avec lequel dessiner</param>
		/// <param name="gameTime">Le GameTime de la frame</param>
		public virtual void Draw()
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

			primitiveBatch.AddVertex(hautGauche, couleur);
			primitiveBatch.AddVertex(basdroit, couleur);

			primitiveBatch.AddVertex(hautDroit, couleur);
			primitiveBatch.AddVertex(basGauche, couleur);

			primitiveBatch.End();

		}

		private void initPoint()
		{
			hautGauche = position + new Vector2(-(size / 2));
			hautDroit = hautGauche + new Vector2(size, 0f);
			basGauche = hautGauche + new Vector2(0f, size);
			basdroit = basGauche + new Vector2(size, 0f);
		}
	}
}
