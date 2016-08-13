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
    class TestLignes
    {
        private int screenWidth ;
        private int screenHeight;
        private float rotation_carre = 10f;

        // how big is the ship?
        const float ShipSizeX = 40f;
        const float ShipSizeY = 45f;
        const float ShipCutoutSize = 10f;

        const float CarreSize = 50f; //50px;

        private PrimitiveBatch primitiveBatch;
        /// <summary>
        /// Récupère ou définit la position du Sprite
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private Vector2 _position;

        /// <summary>
        /// Récupère ou définit la direction du sprite. Lorsque la direction est modifiée, elle est automatiquement normalisée.
        /// </summary>
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = Vector2.Normalize(value); }
        }
        private Vector2 _direction;

        /// <summary>
        /// Récupère ou définit la vitesse de déplacement du sprite.
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        private float _speed;

        /// <summary>
        /// Initialise les variables du Sprite
        /// </summary>
        public void Initialize()
        {
            _position = new Vector2(500, 300);
            _direction = Vector2.Zero;
            _speed = 0;
        }

        public void LoadContent(GraphicsDevice graphic)
        {
            screenWidth = graphic.Viewport.Width;
            screenHeight = graphic.Viewport.Height;
            primitiveBatch = new PrimitiveBatch(graphic);

        }

        /// <summary>
        /// Met à jour les variables du sprite
        /// </summary>
        /// <param name="gameTime">Le GameTime associé à la frame</param>
        public void Update(GameTime gameTime)
        {
            _position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Permet de gérer les entrées du joueur
        /// </summary>
        /// <param name="keyboardState">L'état du clavier à tester</param>
        /// <param name="mouseState">L'état de la souris à tester</param>
        /// <param name="joueurNum">Le numéro du joueur qui doit être surveillé</param>
        public void HandleInput(KeyboardState keyboardState, MouseState mouseState)
        {
        }

        /// <summary>
        /// Dessine le sprite en utilisant ses attributs et le spritebatch donné
        /// </summary>
        /// <param name="spriteBatch">Le spritebatch avec lequel dessiner</param>
        /// <param name="gameTime">Le GameTime de la frame</param>
        public void Draw()
        {
            // draw the left hand ship
            DrawShip(new Vector2(100, screenHeight / 2));
            Vector2 centre = new Vector2(screenWidth / 2, screenHeight / 2);
            DrawCarre(centre, CarreSize, Color.Black);
            DrawCarre(centre, 20f, Color.Red);
            DrawCarre(centre, 10f, Color.Yellow);
            DrawRobot(centre + new Vector2(100, 0), 50f, Color.Red);
            TestAngle90(centre + new Vector2(200, 0), 50f, Color.Red);

            rotation_carre += 0.5f;
            DrawRotateSquare(centre + new Vector2(-200, 0), 100f, rotation_carre, Color.Blue);
        }

        // called to draw the spacewars ship at a point on the screen.
        private void DrawShip(Vector2 where)
        {
            // tell the primitive batch to start drawing lines
            primitiveBatch.Begin(PrimitiveType.LineList);

            // from the nose, down the left hand side
            primitiveBatch.AddVertex(
                where + new Vector2(0f, -ShipSizeY), Color.White);
            primitiveBatch.AddVertex(
                where + new Vector2(-ShipSizeX, ShipSizeY), Color.White);

            // to the right and up, into the cutout
            primitiveBatch.AddVertex(
                where + new Vector2(-ShipSizeX, ShipSizeY), Color.White);
            primitiveBatch.AddVertex(
                where + new Vector2(0f, ShipSizeY - ShipCutoutSize), Color.White);

            // to the right and down, out of the cutout
            primitiveBatch.AddVertex(
                where + new Vector2(0f, ShipSizeY - ShipCutoutSize), Color.White);
            primitiveBatch.AddVertex(
                where + new Vector2(ShipSizeX, ShipSizeY), Color.White);

            // and back up to the nose, where we started.
            primitiveBatch.AddVertex(
                where + new Vector2(ShipSizeX, ShipSizeY), Color.White);
            primitiveBatch.AddVertex(
                where + new Vector2(0f, -ShipSizeY), Color.White);

            // and we're done.
            primitiveBatch.End();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where">le centre du carré</param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        private void DrawCarre(Vector2 where, float size, Color color) //Le centre est le point "Where"
        {
            //where -> le centre
            // tell the primitive batch to start drawing lines
            primitiveBatch.Begin(PrimitiveType.LineList);
            Vector2 lastPos;
            where = where + new Vector2(-(size / 2));
            // from the nose, down the left hand side
            primitiveBatch.AddVertex(where, color);
            primitiveBatch.AddVertex(where + new Vector2(size, 0f), color);

            lastPos = where + new Vector2(size, 0f);
            primitiveBatch.AddVertex(lastPos, color);
            primitiveBatch.AddVertex(lastPos + new Vector2(0f, size), color);

            lastPos = lastPos + new Vector2(0f, size);
            primitiveBatch.AddVertex(lastPos, color);
            primitiveBatch.AddVertex(lastPos + new Vector2(-size, 0f), color);

            lastPos = lastPos + new Vector2(-size, 0f);
            primitiveBatch.AddVertex(lastPos, color);
            primitiveBatch.AddVertex(lastPos + new Vector2(0f, -size), color);

            
            primitiveBatch.End();
        }

        private void DrawRobot(Vector2 where, float size, Color color) //Le centre est le point "Where"
        {
            DrawCarre(where, size, color);
            where = where + new Vector2(-(size / 2));
            Vector2 lastPos;

            primitiveBatch.Begin(PrimitiveType.LineList);

            primitiveBatch.AddVertex(where , Color.Red);
            primitiveBatch.AddVertex(where + new Vector2(size / 2, -size), Color.Red);

            lastPos = where + new Vector2(size / 2, -size);

            primitiveBatch.AddVertex(lastPos, Color.Red);
            primitiveBatch.AddVertex(lastPos + new Vector2(size / 2, size), Color.Red);

            primitiveBatch.End();


        }

        private void TestAngle90(Vector2 where, float size, Color color) //Le centre est le point "Where"
        {


            Vector2 centre = where;

            //where -> le centre
            // tell the primitive batch to start drawing lines
            primitiveBatch.Begin(PrimitiveType.LineList);
            Vector2 lastPos;
            where = where + new Vector2(-(size / 2));
            // from the nose, down the left hand side
            primitiveBatch.AddVertex(where, color);
            primitiveBatch.AddVertex(where + new Vector2(size, 0f), color);

            lastPos = where + new Vector2(size, 0f);
            primitiveBatch.AddVertex(lastPos, color);
            primitiveBatch.AddVertex(lastPos + new Vector2(0f, size), color);

            lastPos = lastPos + new Vector2(0f, size);
            primitiveBatch.AddVertex(lastPos, color);
            primitiveBatch.AddVertex(lastPos + new Vector2(-size, 0f), color);

            lastPos = lastPos + new Vector2(-size, 0f);
            primitiveBatch.AddVertex(lastPos, color);
            primitiveBatch.AddVertex(lastPos + new Vector2(0f, -size), color);


            primitiveBatch.End();

         

            primitiveBatch.Begin(PrimitiveType.LineList);

            primitiveBatch.AddVertex(where, Color.Red);
            primitiveBatch.AddVertex(where + new Vector2(size / 2, -size), Color.Red); //position du nez

            lastPos = where + new Vector2(size / 2, -size);

            primitiveBatch.AddVertex(lastPos, Color.Red);
            primitiveBatch.AddVertex(lastPos + new Vector2(size / 2, size), Color.Red);

            primitiveBatch.End();

            Vector2 positionNez = where + new Vector2(size / 2, -size);
            //if you rotate point (px, py) around point (ox, oy) by angle theta you'll get:

            // p'x = cos(theta) * (px-ox) - sin(theta) * (py-oy) + ox
            // p'y = sin(theta) * (px-ox) + cos(theta) * (py-oy) + oy

            float angle = 90f;
            float theta = MathHelper.ToRadians(angle);
            float xx = (float)(Math.Cos(theta) * (positionNez.X - centre.X) - Math.Sin(theta) * (positionNez.Y - centre.Y) + centre.X);
            float yy = (float)(Math.Sin(theta) * (positionNez.X - centre.X) + Math.Cos(theta) * (positionNez.Y - centre.Y) + centre.Y);
            DrawStars(positionNez);
            DrawStars(centre);
            DrawStars(new Vector2(xx, yy));
            DrawStars(getPointAfterRotate(90, new Vector2(xx, yy), centre));

            for (float i = 0; i < 205; i+=0.1f )
            {

                DrawStars(getPointAfterRotate(i, positionNez, centre), Color.Black);

            }
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

        private void DrawStars(Vector2 where)
        {
            DrawStars(where, Color.Black);
        }
        private void DrawStars(Vector2 where, Color color)
        {
            // stars are drawn as a list of points, so begin the primitiveBatch.
            primitiveBatch.Begin(PrimitiveType.TriangleList);

            primitiveBatch.AddVertex(where, color);
            primitiveBatch.AddVertex(where + Vector2.UnitX, color);
            primitiveBatch.AddVertex(where + Vector2.UnitY, color);


            // and then tell it that we're done.
            primitiveBatch.End();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centre"></param>
        /// <param name="size"></param>
        /// <param name="angle">en degré, rotation sens horaires</param>
        /// <param name="color"></param>
        private void DrawRotateSquare(Vector2 centre, float size, float angle, Color color)
        {
            //where -> le centre
            // tell the primitive batch to start drawing lines
            primitiveBatch.Begin(PrimitiveType.LineList);
            Vector2 hautGauche = centre     + new Vector2(-(size / 2));
            Vector2 hautDroit  = hautGauche + new Vector2(size, 0f);
            Vector2 basGauche  = hautGauche + new Vector2(0f, size);
            Vector2 basdroit   = basGauche  + new Vector2(size, 0f);

            hautGauche = getPointAfterRotate(angle, hautGauche, centre);
            hautDroit = getPointAfterRotate(angle, hautDroit, centre);
            basGauche = getPointAfterRotate(angle, basGauche, centre);
            basdroit = getPointAfterRotate(angle, basdroit, centre);

            primitiveBatch.AddVertex(hautGauche, color);
            primitiveBatch.AddVertex(hautDroit, color);

            primitiveBatch.AddVertex(hautDroit, color);
            primitiveBatch.AddVertex(basdroit, color);

            primitiveBatch.AddVertex(basdroit, color);
            primitiveBatch.AddVertex(basGauche, color);

            primitiveBatch.AddVertex(basGauche, color);
            primitiveBatch.AddVertex(hautGauche, color);

            primitiveBatch.End();
        }

        private void drawRobot(Vector2 centre, float size, float angle, Color color) //Le centre est le point "Where"
        {



            //where -> le centre
            // tell the primitive batch to start drawing lines
            primitiveBatch.Begin(PrimitiveType.LineList);
            Vector2 lastPos;
            centre = centre + new Vector2(-(size / 2));
            // from the nose, down the left hand side
            primitiveBatch.AddVertex(centre, color);
            primitiveBatch.AddVertex(centre + new Vector2(size, 0f), color);

            lastPos = centre + new Vector2(size, 0f);
            primitiveBatch.AddVertex(lastPos, color);
            primitiveBatch.AddVertex(lastPos + new Vector2(0f, size), color);

            lastPos = lastPos + new Vector2(0f, size);
            primitiveBatch.AddVertex(lastPos, color);
            primitiveBatch.AddVertex(lastPos + new Vector2(-size, 0f), color);

            lastPos = lastPos + new Vector2(-size, 0f);
            primitiveBatch.AddVertex(lastPos, color);
            primitiveBatch.AddVertex(lastPos + new Vector2(0f, -size), color);


            primitiveBatch.End();



            primitiveBatch.Begin(PrimitiveType.LineList);

            primitiveBatch.AddVertex(centre, Color.Red);
            primitiveBatch.AddVertex(centre + new Vector2(size / 2, -size), Color.Red); //position du nez

            lastPos = centre + new Vector2(size / 2, -size);

            primitiveBatch.AddVertex(lastPos, Color.Red);
            primitiveBatch.AddVertex(lastPos + new Vector2(size / 2, size), Color.Red);

            primitiveBatch.End();

            Vector2 positionNez = centre + new Vector2(size / 2, -size);
            //if you rotate point (px, py) around point (ox, oy) by angle theta you'll get:

            // p'x = cos(theta) * (px-ox) - sin(theta) * (py-oy) + ox
            // p'y = sin(theta) * (px-ox) + cos(theta) * (py-oy) + oy

            float theta = MathHelper.ToRadians(angle);
            float xx = (float)(Math.Cos(theta) * (positionNez.X - centre.X) - Math.Sin(theta) * (positionNez.Y - centre.Y) + centre.X);
            float yy = (float)(Math.Sin(theta) * (positionNez.X - centre.X) + Math.Cos(theta) * (positionNez.Y - centre.Y) + centre.Y);
            DrawStars(positionNez);
            DrawStars(centre);
            DrawStars(new Vector2(xx, yy));
            DrawStars(getPointAfterRotate(90, new Vector2(xx, yy), centre));

            for (float i = 0; i < 205; i += 0.1f)
            {

                DrawStars(getPointAfterRotate(i, positionNez, centre), Color.Black);

            }
        }

    }
}
