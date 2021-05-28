using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Reigns
{
	
	public class Game1 : Game
	{
		public static void GameOver()
		{
			IsGameEnd = true;
		}

		static public bool IsGameEnd = false;
		List<Food> Foods = new List<Food>();
		List<DescendingText> Texts = new List<DescendingText>();
		List<Obstacle> Obstacles = new List<Obstacle>();
		List<Boost> Boosts = new List<Boost>();
		public int Hunger = 100;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		Song bg_song;
		Song jager;
		SoundEffect bottlesfx;
		SoundEffect bitesfx;
		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			//_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			//IntPtr hWnd = Window.Handle;
			//System.Windows.Forms.Control ctrl = System.Windows.Forms.Control.FromHandle(hWnd);
			//System.Windows.Forms.Form form = ctrl.FindForm();
			//form.TransparencyKey = System.Drawing.Color.Black;
		}
		int Score = 0;
		Player snake;
		Background bg4;
		Background bg3;
		Background bg2;
		Background bg1;
		static public SpriteFont font;
		Texture2D Vignette;
		Texture2D Panel;
		Texture2D texture;
		int temp_f = 0;
		Random r = new Random();

		protected override void Initialize()
		{
			_graphics.PreferredBackBufferWidth = 1400;
			_graphics.PreferredBackBufferHeight = 800;
			_graphics.ApplyChanges();
			Utils.graphicsDevice = _graphics.GraphicsDevice;
			font = Content.Load<SpriteFont>("font");
			snake = new Player(
				Content.Load<Texture2D>("head"),
				Content.Load<Texture2D>("tail")
				);
			bg4 = new Background(Content.Load<Texture2D>("Background-4"), 1);
			bg3 = new Background(Content.Load<Texture2D>("Background-3"), 2);
			bg2 = new Background(Content.Load<Texture2D>("Background-2"), 3);
			bg1 = new Background(Content.Load<Texture2D>("Background-1"), 4);
			Vignette = Content.Load<Texture2D>("vignette");
			Panel = Content.Load<Texture2D>("panel");
			jager = Content.Load<Song>("jager");
			bg_song = Content.Load<Song>("bg");

			bottlesfx = Content.Load<SoundEffect>("bottlesfx");
			bitesfx = Content.Load<SoundEffect>("bitesfx");
			MediaPlayer.Volume = .25f;
			MediaPlayer.Play(bg_song);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			
		}
		int mouth = 0;
		bool mouth_open = false;
		int inv = 0;
		protected override void Update(GameTime gameTime)
		{
			if (IsGameEnd && Keyboard.GetState().IsKeyDown(Keys.R))
			{
				IsGameEnd = false;
				Obstacles.Clear();
				Foods.Clear();
				Texts.Clear();
				Score = 0;
				Hunger = 100;
			}
			temp_f++;
			if (temp_f % 15 == 0 && !IsGameEnd)
				Hunger--;
			if (temp_f % 50 == 0)
			{
				int temp = r.Next(4) + 1;
				Foods.Add(new Food(Content.Load<Texture2D>("food" + temp), temp));
			}
			if (temp_f % 300 == 0)
			{
				int temp = r.Next(3) + 1;
				Boosts.Add(new Boost(Content.Load<Texture2D>("boost" + temp), temp));
			}
			if (temp_f % (r.Next(50) + 50) == 0)
			{
				int temp = r.Next(3) + 1;
				int temp_c = 0;
				if (temp == 1)
				{
					temp_c = 0;
				}
				else if (temp == 4 || temp == 2)
				{
					temp_c = -2;
				} else if (temp == 3)
				{
					temp_c = -1;
				}
				Obstacles.Add(new Obstacle(Content.Load<Texture2D>("Obstacle" + temp), temp_c));
			}
			Time.Update(gameTime);
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			snake.Update();
			bg4.Update();
			bg3.Update();
			bg2.Update();
			bg1.Update();

			if (snake.Invinsible)
			{
				inv++;
			}
			if (inv == 350)
			{
				inv = 0;
				snake.Invinsible = false;
			}
			if (mouth_open)
			{
				snake.textureHead = Content.Load<Texture2D>("head1");
				mouth++;
			}
			if (mouth == 50)
			{
				snake.textureHead = Content.Load<Texture2D>("head");
				mouth = 0;
				mouth_open = false;
			}
			for (int i = 0; i < Foods.Count; i++)
			{
				Vector2 temp_head = snake.Parts[snake.Parts.Count - 1] - new Vector2(snake.size.X / 2 + 0, snake.size.Y / 2 + 0) + Camera.Position;
				if (Vector2.Distance(temp_head, Foods[i].Position) < 40)
				{
					bitesfx.Play();
					Texts.Add(new DescendingText(font, "+" + Foods[i].cost, temp_head + new Vector2(-50, -20)));
					Score += Foods[i].cost;
					Hunger += Foods[i].cost;
					mouth_open = true;
					Foods.RemoveAt(i);
				}
				else if (Foods[i].Position.X < 1450)
				{
					Foods[i].Update();
				}
				else
				{
					Foods.RemoveAt(i);
				}
			}
			for (int i = 0; i < Boosts.Count; i++)
			{
				if (Vector2.Distance(snake.Position + Camera.Position, Boosts[i].Position) < 25)
				{
					Texts.Add(new DescendingText(font, "+" + (Boosts[i].cost * 5), snake.Position + Camera.Position));
					Score += Boosts[i].cost * 5;
					if (Boosts[i].cost == 1)
					{
						bottlesfx.Play();
						snake.Invinsible = true;
					}
					if (Boosts[i].cost == 2)
					{
						bottlesfx.Play();
						Hunger = 100;
					}
					if (Boosts[i].cost == 3)
					{
						MediaPlayer.Volume = 20f;
						MediaPlayer.Play(jager);
						GameOver();
					}
					Boosts.RemoveAt(i);
				}
				else if (Boosts[i].Position.X < 1450)
				{
					Boosts[i].Update();
				}
				else
				{
					Boosts.RemoveAt(i);
				}
			}
			for (int i = 0; i < Texts.Count; i++)
			{
				if (Texts[i].alpha < 50)
				{
					Texts.RemoveAt(i);
				}
				else
				{
					Texts[i].Update();
				}
			}
			if (snake.Position.X + Camera.Position.X > 1400 ||
				snake.Position.Y > 800 || snake.Position.Y < 0)
			{
				GameOver();
			}
			for (int i = 0; i < Obstacles.Count; i++)
			{
				var temp_pos = snake.Position + Camera.Position;
				var tempo_pos = Obstacles[i].Position;
				if (temp_pos.Y > tempo_pos.Y - Obstacles[i].size.Y &&
					temp_pos.Y < tempo_pos.Y + Obstacles[i].size.Y &&
					temp_pos.X > tempo_pos.X &&
					temp_pos.X < tempo_pos.X + Obstacles[i].size.X / 2 && !snake.Invinsible)
				{
					GameOver();
				}
				if (Obstacles[i].factor > 0 && Obstacles[i].Position.Y < -100 ||
					Obstacles[i].factor < 0 && Obstacles[i].Position.Y > 900 ||
					Obstacles[i].factor == 0 && Obstacles[i].Position.X > 1500)
				{
					Obstacles.RemoveAt(i);
				}
				else
				{
					Obstacles[i].Update();
				}
			}
			if (Hunger <= 0)
			{
				GameOver();
			}
			base.Update(gameTime);
		}
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);
			GraphicsDevice.SetRenderTarget(null);
			_spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null);
			bg4.Draw(_spriteBatch);
			bg3.Draw(_spriteBatch);
			bg2.Draw(_spriteBatch);
			bg1.Draw(_spriteBatch);
			snake.Draw(_spriteBatch);
			foreach (var item in Foods)
			{
				item.Draw(_spriteBatch);
			}
			foreach (var item in Texts)
			{
				item.Draw(_spriteBatch);
			}
			foreach (var item in Obstacles)
			{
				item.Draw(_spriteBatch);
			}
			foreach (var item in Boosts)
			{
				item.Draw(_spriteBatch);
			}
			_spriteBatch.Draw(Panel, new Rectangle(0, 0, 240, 80), Color.White);
			_spriteBatch.Draw(Panel, new Rectangle(0, 60, 240, 80), Color.White);
			_spriteBatch.Draw(Vignette, new Rectangle(0, 0, 1400, 800), new Color(255, 255, 255, IsGameEnd ? 255 : 100));
			_spriteBatch.DrawString(Game1.font, "Score: " + Score, new Vector2(30, 27), Color.Wheat);
			_spriteBatch.DrawString(Game1.font, "Hunger: " + Hunger, new Vector2(30, 87), Color.Wheat);
			if (IsGameEnd)
			{
				_spriteBatch.DrawString(Game1.font, "Game Over", new Vector2(620, 350), Color.Wheat);
				_spriteBatch.DrawString(Game1.font, "Press [R] to restart", new Vector2(540, 400), Color.Wheat);
			}
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
