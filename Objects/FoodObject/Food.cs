using Microsoft.Xna.Framework;

namespace VectorGame.Objects.FoodObject;

public class Food : GameObject
{
    protected override string TextureName => "food";

    public Food(GameObjectManager gameObjectManager, GraphicsDeviceManager graphics, Vector2 position) : base(gameObjectManager, graphics)
    {
        Color = Color.Yellow;
        Position = position;
    }
}
