using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteSheet {
    protected Texture2D sprite;
    protected int sheetIndex;
    protected int sheetColumns;
    protected int sheetRows;
    protected bool mirror;

    public SpriteSheet(string assetname, int sheetIndex = 0) {
        sprite = GameEnvironment.AssetManager.GetSprite(assetname);
        this.sheetIndex = sheetIndex;
        sheetColumns = 1;
        sheetRows = 1;

        // see if we can extract the number of sheet elements from the assetname
        var assetSplit = assetname.Split('@');
        if (assetSplit.Length <= 1)
            return;

        var sheetNrData = assetSplit[assetSplit.Length - 1];
        var colrow = sheetNrData.Split('x');
        sheetColumns = int.Parse(colrow[0]);
        if (colrow.Length == 2)
            sheetRows = int.Parse(colrow[1]);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin, float scale) {
        var columnIndex = sheetIndex % sheetColumns;
        var rowIndex = sheetIndex / sheetColumns % sheetRows;
        var spritePart = new Rectangle(columnIndex * Width, rowIndex * Height, Width, Height);
        var spriteEffects = SpriteEffects.None;
        if (mirror)
            spriteEffects = SpriteEffects.FlipHorizontally;
        spriteBatch.Draw(sprite, position, spritePart, Color.White,
            0.0f, origin, scale, spriteEffects, 0.0f);
    }

    public Color GetPixelColor(int x, int y) {
        var columnIndex = sheetIndex % sheetColumns;
        var rowIndex = sheetIndex / sheetColumns % sheetRows;
        var sourceRectangle = new Rectangle(columnIndex * Width + x, rowIndex * Height + y, 1, 1);
        var retrievedColor = new Color[1];
        sprite.GetData(0, sourceRectangle, retrievedColor, 0, 1);
        return retrievedColor[0];
    }

    public Texture2D Sprite
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
            return sprite.Width / sheetColumns;
        }
    }

    public int Height
    {
        get
        {
            return sprite.Height / sheetRows;
        }
    }

    public bool Mirror
    {
        get { return mirror; }
        set { mirror = value; }
    }

    public int SheetIndex
    {
        get
        {
            return sheetIndex;
        }
        set
        {
            if (value < sheetColumns * sheetRows && value >= 0)
                sheetIndex = value;
        }
    }

    public int NumberSheetElements
    {
        get { return sheetColumns * sheetRows; }
    }
}