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
    private readonly int tileSize = 16;

    private AnimatedSprite _player;

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
        //Suelo simple
        /*Texture2D atlasTexture = Content.Load<Texture2D>("images/placeholder/floor_placeholder");

        TextureRegion region = new TextureRegion(atlasTexture, 0, 0, atlasTexture.Width, atlasTexture.Height);

        Tileset tileset = new Tileset(region, 16, 16);

        Random rnd = new Random();
        Floor nFloor = new Floor(rnd.Next(), 128, 64, 1, 1);*/

        TextureAtlas playerAtlas = TextureAtlas.FromFile(Content, "images/placeholder/player_placeholder.xml");
        _player = playerAtlas.CreateAnimatedSprite("player_down_idle");

        //Con agua
        Texture2D floorAtlasTexture = Content.Load<Texture2D>("images/placeholder/floor_placeholder");
        Texture2D waterAtlasTexture = Content.Load<Texture2D>("images/placeholder/water_placeholder");
       
        
        TextureRegion floorRegion = new TextureRegion(floorAtlasTexture, 0, 0, floorAtlasTexture.Width, floorAtlasTexture.Height);
        TextureRegion waterRegion = new TextureRegion(waterAtlasTexture, 0, 0, waterAtlasTexture.Width, waterAtlasTexture.Height);
       

        Tileset floorTileset = new Tileset(floorRegion, tileSize, tileSize);
        Tileset waterTileset = new Tileset(waterRegion, tileSize, tileSize);

        
        Random rnd = new Random();
        Floor nFloor = new Floor(rnd.Next(), 128, 64, 0, 1);

        _tilemap = new Tilemap(floorTileset, waterTileset, 128, 64, nFloor.GenerateComplexTilemap());

        /*float mapWidth = 128 * tileSize; 
        float mapHeight = 64 * tileSize;

        // 4. Calcular y aplicar la escala
        float scalex = GraphicsDevice.Viewport.Width / mapWidth;
        float scaley = GraphicsDevice.Viewport.Height / mapHeight;
        float fitScale = Math.Min(scalex, scaley);

        _tilemap.Scale = new Vector2(fitScale, fitScale);*/

        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        _player.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        SpriteBatch.Begin();
        _tilemap.Draw(SpriteBatch);
        _player.Draw(SpriteBatch, Vector2.Zero);
        SpriteBatch.End();


        base.Draw(gameTime);
    }
}
