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
    class LigneDebug
    {
        private PrimitiveBatch primitiveBatch;
        protected Vector2 position1;
        protected Vector2 position2;

        

        /// <summary>
        /// Initialise les variables du Sprite
        /// </summary>
        public virtual void Initialize()
        {
            Initialize(new Vector2(10,10), new Vector2(500,500));
        }

        public virtual void Initialize(Vector2 position1,Vector2 position2)
        {
            this.position1 = position1;
            this.position2 = position2;
            Console.WriteLine("centre robot " + position1);
            Console.WriteLine("centre brique " + position1);

           
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
            //direction = AngleHelper.getPointAfterRotate(rotation, -Vector2.UnitY, Vector2.Zero);
            //position += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
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
            
            primitiveBatch.AddVertex(position1, Color.Black);
            primitiveBatch.AddVertex(position2, Color.Red);

            primitiveBatch.End();
        }
    }
}
