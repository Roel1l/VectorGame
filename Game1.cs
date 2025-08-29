using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using VectorGame.Objects;

namespace VectorGame;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private readonly List<GameObject> _gameObjects = [];

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.99);
        _graphics.PreferredBackBufferHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.9);
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        for (int i = 0; i < 100; i++)
        {
            _gameObjects.Add(new Bird());
        }

        _gameObjects.ForEach(obj => obj.Initialize(_graphics));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _gameObjects.ForEach(obj => obj.LoadContent(Content));
    }

    protected override void Update(GameTime gameTime)
    {
        var currentKeyboardState = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        _gameObjects.ForEach(obj => obj.Update(gameTime));

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        foreach (var obj in _gameObjects)
        {
            obj.Draw(_spriteBatch);
        }
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
