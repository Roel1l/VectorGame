using Microsoft.Xna.Framework;
using System; 

namespace VectorGame.Objects;

public class Bird : GameObject
{
    private const float PixelsMovedPerSecond = 100f;
    protected override string TextureName => "bird";

    public Vector2 Direction { get; set; }

    public override void Initialize(GraphicsDeviceManager graphics)
    {
        Position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
        FrameCount = 2;
        FramesPerSecond = 2;
        TimePerFrame = (float)1 / FramesPerSecond;

        Direction = new Vector2(1, 1);

        if (Direction != Vector2.Zero)
        {
            Direction.Normalize();
        }
    }

    public override void Update(GameTime gameTime)
    {
        float pixelsToMove = PixelsMovedPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Position += Direction * pixelsToMove;

        if (Direction != Vector2.Zero)
        {
            float angle = (float)Math.Atan2(Direction.Y, Direction.X);
            Rotation = angle + MathHelper.PiOver2;
        }

        base.Update(gameTime);
    }
}