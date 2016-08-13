using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuxDesRobots
{
	public interface ILoadAndDraw
	{
		void LoadContent(PrimitiveBatch primitive);
		void Draw();
	}
}
