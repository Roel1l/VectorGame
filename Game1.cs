using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using VectorGame.Objects;
using VectorGame.Objects.BirdObject;
using VectorGame.Objects.FoodObject;

namespace VectorGame;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly GameObjectManager _gameObjectManager;

    private SpriteBatch _spriteBatch;

    private MouseState _previousMouseState;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _gameObjectManager = new GameObjectManager();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.99);
        _graphics.PreferredBackBufferHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.9);
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        for (int i = 0; i < 1; i++)
        {
            _gameObjectManager.Add(new Bird(_gameObjectManager, _graphics));
        }

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        foreach (var gameObject in _gameObjectManager.GetAll())
        {
            gameObject.LoadContent(Content);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        var currentKeyboardState = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        var currentMouseState = Mouse.GetState();
        if (currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
        {
            var newFood = new Food(_gameObjectManager, _graphics, new Vector2(currentMouseState.X, currentMouseState.Y));
            newFood.LoadContent(Content);
            _gameObjectManager.Add(newFood);
        }

        _previousMouseState = currentMouseState;

        foreach (var gameObject in _gameObjectManager.GetAll())
        {
            gameObject.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        foreach (var gameObject in _gameObjectManager.GetAll<Bird>())
        {
            gameObject.Draw(_spriteBatch);
        }

        foreach (var gameObject in _gameObjectManager.GetAll<Food>())
        {
            gameObject.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
