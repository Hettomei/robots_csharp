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
	class LigneDebug : ILoadAndDraw
	{
		private PrimitiveBatch primitiveBatch;
		protected Vector2 position1;
		protected Vector2 position2;
		protected Color couleur;


		/// <summary>
		/// Initialise les variables du Sprite
		/// </summary>
		public virtual void Initialize()
		{
			Initialize(new Vector2(10, 10), new Vector2(500, 500));
		}
		public virtual void Initialize(Vector2 position1, Vector2 position2)
		{
			Initialize(position1, position2, Color.Red);

		}
		public virtual void Initialize(Vector2 position1, Vector2 position2, Color c)
		{
			this.position1 = position1;
			this.position2 = position2;
			this.couleur = c;
		}

		public virtual void LoadContent(PrimitiveBatch primitive)
		{
			primitiveBatch = primitive;
		}
		public void setPosition2(Vector2 p)
		{
			position2 = p;
		}

		public void HandleInput(KeyboardState keyboardState) { }

		/// <summary>
		/// Dessine le sprite en utilisant ses attributs et le spritebatch donné
		/// </summary>
		/// <param name="spriteBatch">Le spritebatch avec lequel dessiner</param>
		/// <param name="gameTime">Le GameTime de la frame</param>
		public virtual void Draw()
		{
			primitiveBatch.Begin(PrimitiveType.LineList);

			primitiveBatch.AddVertex(position1, couleur);
			primitiveBatch.AddVertex(position2, couleur);

			primitiveBatch.End();
		}

	}
}
