using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace VectorGame.Objects;

public abstract class GameObject
{
    public readonly GameObjectManager GameObjectManager;
    public readonly GraphicsDeviceManager Graphics;
    public int Id { get; set; }
    public Vector2 Position { get; set; }

    protected abstract string TextureName { get; }
    protected Texture2D? Texture { get; set; }
    protected Vector2 Scale { get; set; } = Vector2.One;
    protected Vector2 Origin { get; set; }
    protected float Rotation { get; set; }
    protected Color Color { get; set; } = Color.White;

    protected int FrameCount { get; set; } = 1;
    protected int FramesPerSecond { get; set; } = 0;

    protected float TimePerFrame { get; set; }
    protected int Frame { get; set; }

    protected float TotalElapsed { get; set; }

    protected GameObject(GameObjectManager gameObjectManager, GraphicsDeviceManager graphics)
    {
        Graphics = graphics;
        GameObjectManager = gameObjectManager;
    }

    public void LoadContent(ContentManager contentManager)
    {
        Texture = contentManager.Load<Texture2D>(TextureName);
        int frameWidth = Texture.Width / FrameCount;

        Origin = new Vector2(frameWidth / 2f, Texture.Height / 2f);
    }

    public virtual void Update(GameTime gameTime)
    {
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        UpdateFrame(elapsed);
    }

    private void UpdateFrame(float elapsed)
    {
        TotalElapsed += elapsed;
        if (TotalElapsed > TimePerFrame)
        {
            Frame++;
            // Keep the Frame between 0 and the total frames, minus one.
            Frame %= FrameCount;
            TotalElapsed -= TimePerFrame;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        int frameWidth = Texture!.Width / FrameCount;
        var sourceRectangle = new Rectangle(frameWidth * Frame, 0,
            frameWidth, Texture.Height);

        spriteBatch.Draw(
            texture: Texture,
            position: Position,
            sourceRectangle: sourceRectangle,
            color: Color,
            rotation: Rotation,
            origin: Origin,
            scale: Scale,
            effects: SpriteEffects.None,
            layerDepth: 0f);
    }
}
