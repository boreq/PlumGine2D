﻿using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PlumGine2D.Graphics
{
	public class DrawEngine : EngineExt
	{
		public Vector2 logicScreenResolution { get; private set; }
		public Vector2 realScreenResolution { get; private set; }

		public float scale { get; private set; }
		public Vector2 resScale { get; private set; }

		public Vector2 pos { get; private set; }

		private GraphicsDeviceManager graphics;

		private List<DrawEngineExt> extensions = new List<DrawEngineExt>();

		public DrawEngine(Engine engine, 
			Vector2 logicScreenResolution, Vector2 realScreenResolution,
			bool fullscreen, GraphicsDeviceManager graphics) : base(engine)
		{
			this.graphics = graphics;
			graphics.IsFullScreen = fullscreen;
			graphics.PreferredBackBufferWidth = (int)realScreenResolution.X;
			graphics.PreferredBackBufferHeight = (int)realScreenResolution.Y;

			this.logicScreenResolution = logicScreenResolution;
			this.realScreenResolution = realScreenResolution;

			this.resScale = new Vector2(realScreenResolution.X / logicScreenResolution.X,
				realScreenResolution.Y / logicScreenResolution.Y);
			this.scale = 1.0f;
		}

		public void AddExtension(DrawEngineExt ext)
		{
			extensions.Add(ext);
		}

		public void SetView(Vector2 newPos)
		{
			this.pos = newPos;
		}

		public void SetScale(float newScale)
		{
			this.scale = newScale;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			foreach (DrawEngineExt dee in extensions)
			{
				dee.Draw(spriteBatch, resScale * scale);
			}

			spriteBatch.End();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (DrawEngineExt dee in extensions)
			{
				dee.Update(gameTime);
			}
		}
	}
}

