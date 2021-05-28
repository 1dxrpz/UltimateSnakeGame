using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reigns
{
	public class DescendingText
	{
		string Text = "";
		SpriteFont font;
		public Vector2 Position;
		public int alpha = 255;
		public DescendingText(SpriteFont f, string t, Vector2 pos)
		{
			font = f;
			Text = t;
			Position = pos;
		}
		public void Update()
		{
			Position.Y -= 2;
			alpha -= 10;
		}
		public void Draw(SpriteBatch ctx)
		{
			ctx.DrawString(font, Text, Position, new Color(255, 215, 0, alpha));
		}
	}
}
