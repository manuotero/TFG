using System;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameCore;

public class Core : Game
{

    public Core(string title, int width, int height, bool fullscreen)
    {
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single Core Instance");
        }

        s_instance = this;

        Graphics = new GraphicsDeviceManager(this);

        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullscreen;

        Graphics.ApplyChanges();

        Window.Title = title;

        Content = base.Content;

        Content.RootDirectory = "Content";

        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        GraphicsDevice = base.GraphicsDevice;
        base.Initialize();
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }
    
    internal static Core s_instance;

    public static Core Instance => s_instance;

    public static GraphicsDeviceManager Graphics {get; private set;}

    public static new GraphicsDevice GraphicsDevice {get; private set;}

    public static SpriteBatch SpriteBatch {get; private set;}

    public static new ContentManager Content {get; private set;}

}