using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reigns
{
	public class Background
	{
		Texture2D texture;
		int factor = 0;
		public Background(Texture2D t, int f)
		{
			texture = t;
			factor = f;
		}
		int i = 0;
		public void Update()
		{
			if (!Game1.IsGameEnd)
			{
				i = i > 1400 ? 0 : i + factor;
			}
		}
		public void Draw(SpriteBatch ctx)
		{
			ctx.Draw(texture, new Rectangle(i, 0, 1400, 800), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
			ctx.Draw(texture, new Rectangle(i - 1400, 0, 1400, 800), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
		}
	}
}
