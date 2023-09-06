using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class HealthBar : Component{
    protected readonly Texture2D background;
    protected readonly Texture2D foreground;
    protected readonly Vector2 position;
    protected readonly float maxValue;
    protected float currentValue;
    protected Rectangle part;

    public float value;

    public HealthBar(Texture2D bg, Texture2D fg, float max, Vector2 pos)
    {
        background = bg;
        foreground = fg;
        maxValue = max;
        currentValue = max;
        position = pos;
        part = new(0, 0, foreground.Width, foreground.Height);
    }
    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Draw(background, position, Color.White);
        spriteBatch.Draw(foreground, position, part, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
    }

    public override void Update(GameTime gameTime) {
        currentValue = value;
        part.Width = (int)(currentValue / maxValue * foreground.Width);
    }
}