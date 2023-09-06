using System;
using Microsoft.Xna.Framework;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class Camera {
    public Matrix Transform{get; private set;} 
    public Camera() {}

    public void Follow(Sprite target) {
        var position = Matrix.CreateTranslation(
            -target.Position.X - (target.Rectangle.Width / 2),
            -target.Position.Y - (target.Rectangle.Height / 2), 0);

        // var offset = Matrix.CreateTranslation(Game1.ScreenWidth/2 - randomNum()[0], Game1.ScreenHeight / 2 - randomNum()[1], 0); eartquake
        var offset = Matrix.CreateTranslation(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2, 0);
        Transform = position * offset; //* Matrix.CreateScale(0.4f, 0.4f, 1);
    }

    public void MakeStatic(Sprite target) {
        var position = Matrix.CreateTranslation(
            -target.Position.X - 150, // (target.Rectangle.Width / 2)
            -target.Position.Y ,  // (target.Rectangle.Height / 2)
            0);
        
        var offset = Matrix.CreateTranslation(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2, 0);
        Transform = position * offset; 
    }
    
    
}