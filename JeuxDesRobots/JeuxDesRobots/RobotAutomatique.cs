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
    class RobotAutomatique : Robot
    {

        /// <summary>
        /// Met à jour les variables du sprite
        /// </summary>
        /// <param name="gameTime">Le GameTime associé à la frame</param>
        public override void Update(GameTime gameTime)
        {
          //  rotation += 1f;
         //   speed = 0.1f;
            direction = AngleHelper.getPointAfterRotate(rotation, -Vector2.UnitY, Vector2.Zero);
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

                /// <summary>
        /// Permet de gérer les entrées du joueur
        /// </summary>
        /// <param name="keyboardState">L'état du clavier à tester</param>
        /// <param name="mouseState">L'état de la souris à tester</param>
        public override void HandleInput(KeyboardState keyboardState)
        {
        }
    }
}
