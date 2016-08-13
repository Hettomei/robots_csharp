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
    class Souris
    {
        private PrimitiveBatch primitiveBatch;
        public Vector2 position;
        protected int size = 20;
        Color couleur = Color.OrangeRed;


        /// <summary>
        /// Initialise les variables du Sprite
        /// </summary>
        public virtual void Initialize()
        {
            Initialize(new Vector2(0, 0));
        }

        public virtual void Initialize(Vector2 position)
        {
            this.position = position;
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
            position.X = mouse.X;
            position.Y = mouse.Y;
        }

        /// <summary>
        /// Dessine le sprite en utilisant ses attributs et le spritebatch donné
        /// </summary>
        /// <param name="spriteBatch">Le spritebatch avec lequel dessiner</param>
        /// <param name="gameTime">Le GameTime de la frame</param>
        public virtual void Draw()
        {
            //position -> le centre
            // tell the primitive batch to start drawing lines
            primitiveBatch.Begin(PrimitiveType.LineList);
            Vector2 hautGauche = position + new Vector2(-(size / 2));
            Vector2 hautDroit = hautGauche + new Vector2(size, 0f);
            Vector2 basGauche = hautGauche + new Vector2(0f, size);
            Vector2 basdroit = basGauche + new Vector2(size, 0f);
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
      


    }
}
