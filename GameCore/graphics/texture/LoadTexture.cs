

using System.Collections.Generic;
using System.IO;
using GameCore.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;

public class TextureLoader
{
    public TextureLoader(ContentManager content, string filename)
    {
        _entityAtlas = TextureAtlas.FromFile(content, filename);
    }

    public TextureLoader(ContentManager content, List<string> filenames)
    {
        _textures = new List<Texture2D>();
        foreach(string file in filenames)
        {
            _textures.Add(content.Load<Texture2D>(file));
        }
    }

    public AnimatedSprite LoadEntity(string animation)
    {
        if (_entityAtlas != null)
        {
            return _entityAtlas.CreateAnimatedSprite(animation);
        }
        else
        {
            return null;
        }
    }

    public Tilemap LoadTileMap(int tileSize, int mapWidth, int mapHeight, (int, int)[] tilemap)
    {
        if (_textures.Count > 0)
        {
            List<Tileset> tilesetList = new List<Tileset>();
            foreach(Texture2D texture in _textures)
            {
                TextureRegion region = new TextureRegion(texture, 0, 0, texture.Width, texture.Height);
                Tileset tileset = new Tileset(region, tileSize, tileSize);
                tilesetList.Add(tileset);
            }
            
            return new Tilemap(tilesetList, mapWidth, mapHeight, tilemap);
        }
        else
        {
            return null;
        }
    }

    private readonly TextureAtlas _entityAtlas;
    private readonly List<Texture2D> _textures;
}