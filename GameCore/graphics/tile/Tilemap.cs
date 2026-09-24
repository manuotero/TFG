using System;
using System.Collections.Generic;
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
    public Tilemap(List<Tileset> tilesets, int columns, int rows, (int, int)[] tilemap)
    {
        _tilesets = tilesets;
        Rows = rows;
        Columns = columns;
        Count = Columns * Rows;
        Scale = Vector2.One;
        _tiles = tilemap;

        TileWidths = new List<float>();
        TileHeight = new List<float>();

        foreach(Tileset tileset in tilesets)
        {
            TileWidths.Add(tileset.TileWidth * Scale.X);
            TileHeight.Add(tileset.TileHeight * Scale.Y);
        }
    }

    public void SetTile(int index, int tilesetID)
    {
        _tiles[index].Item2 = tilesetID;
    }

    public void SetTile(int column, int row, int tilesetID)
    {
        int index = row * Columns + column;
        SetTile(index, tilesetID);
    }

    public TextureRegion GetTile(int atlasIndex, int index)
    {
        return _tilesets[atlasIndex].GetTile(index);
    }

    public TextureRegion GetTile(int atlasIndex, int column, int row)
    {
        int index = row * Columns + column;
        return GetTile(atlasIndex, index);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for(int i = 0; i < Count; i++)
        {
            int atlasIndex = _tiles[i].Item1;
            int tilesetIndex = _tiles[i].Item2;
            TextureRegion tile = _tilesets[atlasIndex].GetTile(tilesetIndex);

            int x = i % Columns;
            int y = i / Columns;

            Vector2 position = new Vector2(x * TileWidths[atlasIndex], y * TileHeight[atlasIndex]);
            tile.Draw(spriteBatch, position, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 1.0f);
        }
    }

    private readonly List<Tileset> _tilesets;
    private readonly (int, int)[] _tiles;
    public int Rows {get;}

    public int Columns {get;}

    public int Count {get;}

    public Vector2 Scale {get; set;}

    public List<float> TileWidths;

    public List<float> TileHeight;
}