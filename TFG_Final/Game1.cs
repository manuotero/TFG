using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameCore;
using MonoGameLibrary.Graphics;
using System;
using GameCore.Graphics;
using System.Reflection.PortableExecutable;
using System.Collections.Generic;

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
    private (float x, float y) _playerPos;

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

        List<int> tiles = new List<int>();
        tiles.Add(1);

        Random rnd = new Random();
        Floor nFloor = new Floor(rnd.Next(), 128, 64, 0, tiles);
        GameEntity player = new GameEntity("Player", "images/placeholder/player_placeholder.xml");
        player.SetInitPos(nFloor.GenerateSpawnPoint());
        
        _playerPos = player.SpriteLocation(tileSize);
        
        TextureLoader PlayerLoader = new TextureLoader(Content, player.SpriteFile);
        _player = PlayerLoader.LoadEntity("player_down_idle");

        List<string> filenames = new List<string>();
        filenames.Add("images/placeholder/floor_placeholder");
        filenames.Add("images/placeholder/water_placeholder");

        TextureLoader MapLoader = new TextureLoader(Content, filenames);

        _tilemap = MapLoader.LoadTileMap(tileSize, 128, 64, nFloor.GenerateComplexTilemap());
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
        _player.Draw(SpriteBatch, new Vector2(_playerPos.x, _playerPos.y));
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
