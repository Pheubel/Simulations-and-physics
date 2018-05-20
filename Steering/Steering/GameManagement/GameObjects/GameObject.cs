using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class GameObject : IGameLoopObject {
    protected GameObject parent;
    protected Vector2 position, velocity;
    protected int layer;
    protected string id;
    protected bool visible;

    protected GameObject(int layer = 0, string id = "") {
        this.layer = layer;
        this.id = id;
        position = Vector2.Zero;
        velocity = Vector2.Zero;
        visible = true;
    }

    public virtual void HandleInput(InputHelper inputHelper) {
    }

    public virtual void Update(GameTime gameTime) {
        position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
    }

    public virtual void Reset() {
        visible = true;
    }

    public virtual Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public virtual Vector2 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    public virtual Vector2 GlobalPosition
    {
        get {
            if (parent != null)
                return parent.GlobalPosition + Position;
            return Position;
        }
    }

    public GameObject Root
    {
        get {
            return parent != null ? parent.Root : this;
        }
    }

    public GameObjectList GameWorld
    {
        get
        {
            return Root as GameObjectList;
        }
    }

    public virtual int Layer
    {
        get { return layer; }
        set { layer = value; }
    }

    public virtual GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    public string ID
    {
        get { return id; }
    }

    public bool Visible
    {
        get { return visible; }
        set { visible = value; }
    }

    public virtual Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, 0, 0);
        }
    }
}