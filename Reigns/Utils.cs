using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reigns
{
	public class Utils
	{
		static public GraphicsDevice graphicsDevice;
	}
	static public class Time
	{
		static public float deltaTime = 0;
		public static void Update(GameTime time)
		{
			deltaTime = (float)time.ElapsedGameTime.TotalSeconds;
		}
	}
}
