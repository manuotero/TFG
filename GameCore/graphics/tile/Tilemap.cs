using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using GameCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Graphics;

public class Tilemap
{
    public Tilemap(Tileset tileset, int columns, int rows, int[] tilemap)
    {
        _tileset = tileset;
        Rows = rows;
        Columns = columns;
        Count = Columns * Rows;
        Scale = Vector2.One;
        _tiles = tilemap;
    }

    public void SetTile(int index, int tilesetID)
    {
        _tiles[index] = tilesetID;
    }

    public void SetTile(int column, int row, int tilesetID)
    {
        int index = row * Columns + column;
        SetTile(index, tilesetID);
    }

    public TextureRegion GetTile(int index)
    {
        return _tileset.GetTile(_tiles[index]);
    }

    public TextureRegion GetTile(int column, int row)
    {
        int index = row * Columns + column;
        return GetTile(index);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < Count; i++)
        {
            int tilesetIndex = _tiles[i];
            TextureRegion tile = _tileset.GetTile(tilesetIndex);

            int x = i % Columns;
            int y = i / Columns;

            Vector2 position = new Vector2(x * TileWidth, y * TileHeight);
            tile.Draw(spriteBatch, position, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 1.0f);
        }
    }

    private readonly Tileset _tileset;
    private readonly int[] _tiles;

    public int Rows {get;}

    public int Columns {get;}

    public int Count {get;}

    public Vector2 Scale {get; set;}

    public float TileWidth => _tileset.TileWidth * Scale.X;

    public float TileHeight => _tileset.TileHeight * Scale.Y;
}