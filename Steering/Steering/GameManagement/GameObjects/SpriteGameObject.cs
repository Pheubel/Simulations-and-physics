using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteGameObject : GameObject {
    protected SpriteSheet sprite;
    protected Vector2 origin;
    protected float scale;

    public SpriteGameObject(string assetname, int layer = 0, string id = "", int sheetIndex = 0, float scale = 1f)
        : base(layer, id) {
        sprite = assetname != "" ? new SpriteSheet(assetname, sheetIndex) : null;
        this.scale = scale;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        if (!visible || sprite == null)
            return;
        sprite.Draw(spriteBatch, GlobalPosition, origin, scale);
    }

    public SpriteSheet Sprite
    {
        get { return sprite; }
    }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get
        {
            return sprite.Width;
        }
    }

    public int Height
    {
        get
        {
            return sprite.Height;
        }
    }

    public bool Mirror
    {
        get { return sprite.Mirror; }
        set { sprite.Mirror = value; }
    }

    public Vector2 Origin
    {
        get { return origin; }
        set { origin = value; }
    }

    public float Scale
    {
        get { return scale; }
    }

    public override Rectangle BoundingBox
    {
        get
        {
            var left = (int)(GlobalPosition.X - origin.X);
            var top = (int)(GlobalPosition.Y - origin.Y);
            return new Rectangle(left, top, Width, Height);
        }
    }

    public bool CollidesWith(SpriteGameObject obj) {
        return Visible && obj.Visible && BoundingBox.Intersects(obj.BoundingBox);
        //Per pixel collision detection removed
    }
}

