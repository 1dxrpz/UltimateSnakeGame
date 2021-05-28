using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reigns
{
	public class Player
	{
		public Vector2 Position = new Vector2(32, 32);
		public List<Vector2> Parts = new List<Vector2>();

		public bool Invinsible = false;
		public Texture2D texture;
		public Texture2D textureHead;
		private bool init = false;
		private int maxsize = 20;
		public Point size = new Point(32, 32);
		private float Angle = 0;
		int co = 0;
		public Player(Texture2D head, Texture2D tail)
		{
			texture = tail;
			textureHead = head;
		}
		public void Update()
		{
			
			if (!Game1.IsGameEnd)
			{
				Angle = (float)((Math.Atan2(
					Mouse.GetState().Y - Position.Y,
					Mouse.GetState().X - Position.X - Camera.Position.X
					)));
				co++;
				if (Mouse.GetState().LeftButton == ButtonState.Pressed)
				{

				}
				if (Mouse.GetState().LeftButton == ButtonState.Pressed || co % 1 == 0)
				{
					Vector2 newpos = new Vector2(8, 0);
					double x = newpos.X * Math.Cos(Angle) - newpos.Y * Math.Sin(Angle);
					double y = newpos.X * Math.Sin(Angle) + newpos.Y * Math.Cos(Angle);

					Position += new Vector2((float)x, (float)y);
					//Position = new Vector2((float)x, (float)y);
					//Parts.Add(new Vector2((float)x, (float)y));
					Parts.Add(Position);
				}
				if (Parts.Count > maxsize)
				{
					Parts.RemoveAt(0);
				}
				Camera.Position.X += 5;
			}
		}
		public void Draw(SpriteBatch ctx)
		{
			
			for (int i = 0; i < Parts.Count - 1; i += 2)
			{
				ctx.Draw(texture, new Rectangle(Camera.Position.ToPoint() + Parts[i].ToPoint() - new Point(size.X / 2 + 20, size.Y / 2 + 20), size),
					Invinsible ? new Color(255, 255, 255, 100) : Color.White);
			}
			ctx.Draw(textureHead, 
				new Rectangle(Camera.Position.ToPoint() + Parts[Parts.Count - 1].ToPoint() - new Point(size.X / 2 + 0, size.Y / 2 + 0), new Point(48, 48)),
				new Rectangle(0, 0, textureHead.Width, textureHead.Height),
				Invinsible ? new Color(255, 255, 255, 100) : Color.White, Angle, new Vector2(size.X / 2 - 16, size.Y / 2 - 8), SpriteEffects.FlipVertically, 1);
		}
	}
}
