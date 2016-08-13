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
    abstract class Robot
    {
        private PrimitiveBatch primitiveBatch;
        public Vector2 position;
        protected Vector2 direction;
        protected float speed;
        protected float rotation;
        protected float vitesse_rotation;
        protected Color couleur;
        protected float robotSize;
        private Vector2 positionNez;

        private Brique laBrique;

        public Brique LaBrique
        {
            get { return laBrique; }
            set { laBrique = value; }
        }

        public Vector2 PositionNez
        {
            get {
                Vector2 hautmilieu = position + new Vector2(-(robotSize / 2)) + new Vector2(robotSize / 2, -robotSize);
                return AngleHelper.getPointAfterRotate(rotation, hautmilieu, position);
            }
            set { positionNez = value; }
        }

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
            vitesse_rotation = 7f;
            this.rotation = rotation;
            this.robotSize = robotsize;
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
        /// Dessine le sprite en utilisant ses attributs et le spritebatch donné
        /// </summary>
        /// <param name="spriteBatch">Le spritebatch avec lequel dessiner</param>
        /// <param name="gameTime">Le GameTime de la frame</param>
        public virtual void Draw()
        {
            drawRobot();
        }

        private void drawRobot()
        {
            DrawRotateSquare();
            DrawRotateNoze();
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

            hautGauche = AngleHelper.getPointAfterRotate(rotation, hautGauche, position);
            hautDroit = AngleHelper.getPointAfterRotate(rotation, hautDroit, position);
            basGauche = AngleHelper.getPointAfterRotate(rotation, basGauche, position);
            basdroit = AngleHelper.getPointAfterRotate(rotation, basdroit, position);

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
            primitiveBatch.Begin(PrimitiveType.LineList);
            Vector2 basGauche = position + new Vector2(-(robotSize / 2));
            Vector2 hautmilieu = basGauche + new Vector2(robotSize / 2, -robotSize);
            Vector2 basdroit = hautmilieu + new Vector2(robotSize / 2, robotSize);

            basGauche = AngleHelper.getPointAfterRotate(rotation, basGauche, position);
            hautmilieu = AngleHelper.getPointAfterRotate(rotation, hautmilieu, position);
            basdroit = AngleHelper.getPointAfterRotate(rotation, basdroit, position);
            
            primitiveBatch.AddVertex(basGauche, couleur);
            primitiveBatch.AddVertex(hautmilieu, couleur);

            primitiveBatch.AddVertex(hautmilieu, couleur);
            primitiveBatch.AddVertex(basdroit, couleur);

            primitiveBatch.End();
        }


    }
}
