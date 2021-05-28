using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reigns
{
	class Obstacle
	{
		public Vector2 Position;
		Texture2D texture;
		public int factor = 0;
		Random r = new Random();
		int speed;
		public Point size;
		private float rt;
		public Obstacle(Texture2D t, int f = 0)
		{
			factor = f;
			speed = r.Next(2) + 2;
			texture = t;
			size = new Point(texture.Width, texture.Height);
			Position = new Vector2(-100, factor >= 0 ? 800 - texture.Height : 0);
		}
		private float rtspeed = 0;
		public void Update()
		{
			if (!Game1.IsGameEnd)
			{
				if (factor > 0)
				{
					Position.Y -= speed;
				}
				else if (factor < -1)
				{
					Position.Y += speed;
				}
				Position.X += 5;
				rtspeed += rt;
			}
		}
		public void Draw(SpriteBatch ctx)
		{
			ctx.Draw(texture, new Rectangle((Position).ToPoint(), (size.ToVector2() * 2).ToPoint()), new Rectangle(0, 0, size.X, size.Y),
				Color.White, rtspeed, new Vector2(texture.Width, texture.Height) / 2, SpriteEffects.None, 0);
		}
	}
}
