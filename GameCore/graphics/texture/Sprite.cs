using System.Drawing;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;

namespace GameCore.Graphics;

public class Sprite
{
    public Sprite(){}

    public Sprite(TextureRegion region)
    {
        SpriteRegion = region;
    }

    public void CenterOrigin()
    {
        Origin = new Vector2(SpriteRegion.Width, SpriteRegion.Height) * 0.5f;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        SpriteRegion.Draw(spriteBatch, position, SpriteColor, Rotation, Origin, Scale, Effects, layerDepth);
    }

    public TextureRegion SpriteRegion {get; set;}
    public Microsoft.Xna.Framework.Color SpriteColor {get; set;} = Microsoft.Xna.Framework.Color.White;
    public float Rotation {get; set;} = 0.0f;
    public Vector2 Scale {get; set;} = Vector2.One;
    public Vector2 Origin {get; set;} = Vector2.Zero;
    public SpriteEffects Effects {get; set;} = SpriteEffects.None;
    public float layerDepth {get; set;} = 0.0f;
    public float Width => SpriteRegion.Width * Scale.X;
    public float Height => SpriteRegion.Height * Scale.Y;
}