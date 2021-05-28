using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reigns
{
	public class Food
	{
		public Vector2 Position = Vector2.Zero;
		public Texture2D texture;
		Point size;
		public int speed = 5;
		private float rt;
		public int cost = 0;
		public Food(Texture2D t, int c)
		{
			cost = c;
			texture = t;
			size = new Point(texture.Width * 10, texture.Height * 10);
			Random r = new Random();
			rt = (float)r.NextDouble() / 10;
			Position = new Vector2(-10, r.Next(700) + 50);
		}
		private float rtspeed = 0;
		public void Update()
		{
			if (!Game1.IsGameEnd)
			{
				Position.X += speed;
				rtspeed += rt;
			}
		}
		public void Draw(SpriteBatch ctx)
		{
			ctx.Draw(texture, new Rectangle(Position.ToPoint(), size), new Rectangle(0, 0, size.X, size.Y),
				Color.White, rtspeed, new Vector2(texture.Width, texture.Height) / 2, SpriteEffects.None, 0);
		}
	}
}
