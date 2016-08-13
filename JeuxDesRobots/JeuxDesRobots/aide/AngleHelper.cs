﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JeuxDesRobots
{
	static class AngleHelper
	{

		/// <summary>
		/// Renvoie un vecteur transormé
		/// </summary>
		/// <param name="pointToRotate"></param>
		/// <param name="centreDeRotation"></param>
		/// <param name="angle">en radian</param>
		/// <returns></returns>
		public static Vector2 getPointAfterRotate(Vector2 pointToRotate, Vector2 centreDeRotation, float theta)
		{
			if (theta == 0) // utile ?
				return pointToRotate;

			return new Vector2(
				(float)(Math.Cos(theta) * (pointToRotate.X - centreDeRotation.X) - Math.Sin(theta) * (pointToRotate.Y - centreDeRotation.Y) + centreDeRotation.X),
				(float)(Math.Sin(theta) * (pointToRotate.X - centreDeRotation.X) + Math.Cos(theta) * (pointToRotate.Y - centreDeRotation.Y) + centreDeRotation.Y)
				);
		}

		public static int determinerSiTournePositifOuNegatifPourRejoindreAutrePoint(Vector2 pointDeRotation, Vector2 pointABouger, Vector2 pointAViser)
		{
			Vector2 point1 = Vector2.Subtract(pointDeRotation, pointABouger);
			Vector2 point2 = Vector2.Subtract(pointDeRotation, pointAViser);
			point1.Normalize();
			point2.Normalize();
			float test = Vector2.Dot(point1, point2);
			if (test > 0.999 && test < 1.001)
				return 0;

			if ((point1.X * point2.Y) > (point1.Y * point2.X))
				return (+1);

			if ((point1.X * point2.Y) < (point1.Y * point2.X))
				return (-1);

			if (((point1.X * point2.X) < 0.0) || ((point1.Y * point2.Y) < 0.0))
				return (-1);

			if ((point1.X * point1.X + point1.Y * point1.Y) < (point2.X * point2.X + point2.Y * point2.Y))
				return (+1);

			return (0);
		}

		#region Inutile

		/// <summary>
		/// Renvoie l'angle en degré 
		/// </summary>
		/// <param name="pointDeRotation"></param>
		/// <param name="pointABouger"></param>
		/// <param name="pointAViser"></param>
		/// <returns></returns>
		public static float AngleOfView(Vector2 pointDeRotation, Vector2 pointABouger, Vector2 pointAViser)
		{
			if (pointABouger.Equals(pointAViser))
				return 0;

			//pointABouger = Vector2.Subtract(pointDeRotation, pointABouger);
			pointABouger = Vector2.Subtract(pointABouger, pointDeRotation);
			pointAViser = Vector2.Subtract(pointAViser, pointDeRotation);
			//	pointAViser = Vector2.Subtract(pointDeRotation, pointAViser);

			pointABouger.Normalize();
			pointAViser.Normalize();
			return AngleHelper.CounterClockWise2(pointABouger, pointAViser) * MathHelper.ToDegrees((float)Math.Acos(Vector2.Dot(pointABouger, pointAViser)));
		}

		/// <summary>
		/// Permet de savoir si Point1 est "au dessus" de point2.
		/// Les vecteur doivent etre normalisés avant d'appeler cette fonction
		/// </summary>
		/// <param name="point1"></param>
		/// <param name="point2"></param>
		/// <returns></returns>
		public static int CounterClockWise2(Vector2 point1, Vector2 point2)
		{
			if ((point1.X * point2.Y) > (point1.Y * point2.X))
				return (+1);

			if ((point1.X * point2.Y) < (point1.Y * point2.X))
				return (-1);

			if (((point1.X * point2.X) < 0.0) || ((point1.Y * point2.Y) < 0.0))
				return (-1);

			if ((point1.X * point1.X + point1.Y * point1.Y) < (point2.X * point2.X + point2.Y * point2.Y))
				return (+1);

			return (0);
		}
		#endregion

	

	}
}
