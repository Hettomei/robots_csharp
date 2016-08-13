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
    class RobotManuel : Robot
    {

        /// <summary>
        /// Permet de gérer les entrées du joueur
        /// </summary>
        /// <param name="keyboardState">L'état du clavier à tester</param>
        /// <param name="mouseState">L'état de la souris à tester</param>
        public override void HandleInput(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                direction = AngleHelper.getPointAfterRotate(rotation, -Vector2.UnitY, Vector2.Zero);
                speed = 0.3f;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                direction = AngleHelper.getPointAfterRotate(rotation, Vector2.UnitY, Vector2.Zero);

                speed = 0.3f;
            }
            else
            {
                speed = 0;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                rotation -= vitesse_rotation;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                rotation += vitesse_rotation;
            }
        }

        /// <summary>
        /// Met à jour les variables du sprite
        /// </summary>
        /// <param name="gameTime">Le GameTime associé à la frame</param>
        public override void Update(GameTime gameTime)
        {
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

    }
}
