using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameCore;
using MonoGameLibrary.Graphics;
using System;
using GameCore.Graphics;
using System.Reflection.PortableExecutable;

namespace TFG_Final;

public class Game1 : Core
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Tilemap _tilemap;

    private Rectangle _floorBounds;

    private Texture2D _logo;

    public Game1() : base("Mech_Game", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();

        /*Rectangle screenBounds = GraphicsDevice.PresentationParameters.Bounds;

       _floorBounds = new Rectangle(
            (int)_tilemap.TileWidth,
            (int)_tilemap.TileHeight,
            screenBounds.Width - (int)_tilemap.TileWidth * 2,
            screenBounds.Height - (int)_tilemap.TileHeight * 2
        );*/
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        
        Texture2D atlasTexture = Content.Load<Texture2D>("images/placeholder/atlas_placeholder");

        TextureRegion region = new TextureRegion(atlasTexture, 0, 0, atlasTexture.Width, atlasTexture.Height);

        Tileset tileset = new Tileset(region, 16, 16);

        Random rnd = new Random();
        Floor nFloor = new Floor(rnd.Next(), 128, 64, 1);

        _tilemap = new Tilemap(tileset, 128, 64, nFloor.GenerateTilemap());
        float mapWidth = 128 * 16; 
        float mapHeight = 64 * 16;

        // 4. Calcular y aplicar la escala
        float scalex = GraphicsDevice.Viewport.Width / mapWidth;
        float scaley = GraphicsDevice.Viewport.Height / mapHeight;
        float fitScale = Math.Min(scalex, scaley);

_tilemap.Scale = new Vector2(fitScale, fitScale);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        SpriteBatch.Begin();
        //SpriteBatch.Draw(_logo, Vector2.Zero, Color.White);
        _tilemap.Draw(SpriteBatch);
        SpriteBatch.End();


        base.Draw(gameTime);
    }
}
