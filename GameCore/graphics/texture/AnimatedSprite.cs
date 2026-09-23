using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;

namespace GameCore.Graphics;

public class AnimatedSprite : Sprite
{
    
    public AnimatedSprite(){}
    public AnimatedSprite(Animation animation)
    {
        Animation = animation;
    }

    public void Update(GameTime gameTime)
    {
        _elapsed += gameTime.ElapsedGameTime;

        if (_elapsed >= _animation.Delay)
        {
            _elapsed -= _animation.Delay;
            _currentFrame++;

            if (_currentFrame >= _animation.Frames.Count)
            {
                _currentFrame = 0;
            }

            SpriteRegion = _animation.Frames[_currentFrame];
        }
    }
    
    public Animation Animation
    {
        get => _animation;
        set
        {
            _animation = value;
            SpriteRegion = _animation.Frames[0];
        }
    }
    private int _currentFrame;
    private TimeSpan _elapsed;
    private Animation _animation;
}