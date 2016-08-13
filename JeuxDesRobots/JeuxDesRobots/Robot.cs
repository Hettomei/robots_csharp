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
    class Robot
    {
        private static PrimitiveBatch prim;
        private PrimitiveBatch primitiveBatch;
        private Vector2 position;
        private Vector2 direction;
        private float speed;
        private float speedAjout = 0.3f;
        private float rotation;
        private Color couleur;
        private float robotSize;

        /// <summary>
        /// Initialise les variables du Sprite
        /// </summary>
        public virtual void Initialize()
        {
            Initialize(new Vector2(500, 300), 0.2f, Color.Red, 0f, 50f);
        }

        public virtual void Initialize(Vector2 position, float speed, Color couleur, float rotation, float robotsize)
        {
            this.position = position;
            this.direction = new Vector2(0,0); //Commence le nez en haut // a recalculer avec angle
            this.speed = speed;
            this.couleur = couleur;
            this.rotation = rotation;
            this.robotSize = robotsize;

        }
        public virtual void LoadContent(GraphicsDevice graphic)
        {
            if (prim == null)
            {
                prim = new PrimitiveBatch(graphic);
            }
            primitiveBatch = prim;
        }

        /// <summary>
        /// Met à jour les variables du sprite
        /// </summary>
        /// <param name="gameTime">Le GameTime associé à la frame</param>
        public virtual void Update(GameTime gameTime)
        {
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Permet de gérer les entrées du joueur
        /// </summary>
        /// <param name="keyboardState">L'état du clavier à tester</param>
        /// <param name="mouseState">L'état de la souris à tester</param>
        public virtual void HandleInput(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                direction = getPointAfterRotate(rotation, -Vector2.UnitY, Vector2.Zero);
                speed = speedAjout;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                direction = getPointAfterRotate(rotation, Vector2.UnitY, Vector2.Zero);

                speed = speedAjout;
            }
            else
            {
                speed = 0;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                rotation -= 2f;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                rotation += 2f;
            }

            if (keyboardState.IsKeyDown(Keys.R))
            {
                rotation = 0f;
            }
            if (keyboardState.IsKeyDown(Keys.P))
            {
                speedAjout += 0.01f;
            }
            else if (keyboardState.IsKeyDown(Keys.M))
            {
                speedAjout -= 0.01f;
                if (speedAjout <= 0) 
                    speedAjout = 0.01f;
            }
        }

        /// <summary>
        /// Dessine le sprite en utilisant ses attributs et le spritebatch donné
        /// </summary>
        /// <param name="spriteBatch">Le spritebatch avec lequel dessiner</param>
        /// <param name="gameTime">Le GameTime de la frame</param>
        public virtual void Draw()
        {
            drawRobot();
        }

        /// <summary>
        /// Renvoie un vecteur transormé
        /// </summary>
        /// <param name="angle">en degré</param>
        /// <param name="pointInitial"></param>
        /// <param name="centreDeRotation"></param>
        /// <returns></returns>
        private Vector2 getPointAfterRotate(float angle, Vector2 pointInitial, Vector2 centreDeRotation)
        {
            float theta = MathHelper.ToRadians(angle);

            return new Vector2((float)(Math.Cos(theta) * (pointInitial.X - centreDeRotation.X) - Math.Sin(theta) * (pointInitial.Y - centreDeRotation.Y) + centreDeRotation.X),
                (float)(Math.Sin(theta) * (pointInitial.X - centreDeRotation.X) + Math.Cos(theta) * (pointInitial.Y - centreDeRotation.Y) + centreDeRotation.Y));
        }



        /// <summary>
        /// dessine un carré autour du centre
        /// </summary>
        /// <param name="centre">centre du carré</param>
        /// <param name="size"></param>
        /// <param name="angle">en degré, rotation sens horaires</param>
        /// <param name="color"></param>
        private void DrawRotateSquare()
        {
            //position -> le centre
            // tell the primitive batch to start drawing lines
            primitiveBatch.Begin(PrimitiveType.LineList);
            Vector2 hautGauche = position + new Vector2(-(robotSize / 2));
            Vector2 hautDroit = hautGauche + new Vector2(robotSize, 0f);
            Vector2 basGauche = hautGauche + new Vector2(0f, robotSize);
            Vector2 basdroit = basGauche + new Vector2(robotSize, 0f);

            hautGauche = getPointAfterRotate(rotation, hautGauche, position);
            hautDroit = getPointAfterRotate(rotation, hautDroit, position);
            basGauche = getPointAfterRotate(rotation, basGauche, position);
            basdroit = getPointAfterRotate(rotation, basdroit, position);

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
        private void DrawRotateNoze()
        {
            //position -> le centre
            // tell the primitive batch to start drawing lines
            primitiveBatch.Begin(PrimitiveType.LineList);
            Vector2 basGauche = position + new Vector2(-(robotSize / 2));
            Vector2 hautmilieu = basGauche + new Vector2(robotSize / 2, -robotSize);
            Vector2 basdroit = hautmilieu + new Vector2(robotSize / 2, robotSize);

            basGauche = getPointAfterRotate(rotation, basGauche, position);
            hautmilieu = getPointAfterRotate(rotation, hautmilieu, position);
            basdroit = getPointAfterRotate(rotation, basdroit, position);
            
            primitiveBatch.AddVertex(basGauche, couleur);
            primitiveBatch.AddVertex(hautmilieu, couleur);

            primitiveBatch.AddVertex(hautmilieu, couleur);
            primitiveBatch.AddVertex(basdroit, couleur);

            primitiveBatch.End();
        }

        private void drawRobot()
        {
            DrawRotateSquare();
            DrawRotateNoze();
        }

    }
}
