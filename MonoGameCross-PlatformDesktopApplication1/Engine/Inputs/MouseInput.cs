using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameCross_PlatformDesktopApplication1.Engine; 

public class MouseInput {
    public  MouseState CurmouseState;
    public  MouseState PrevmouseState;
    public  Rectangle MouseRectangle;
    public  Vector2 Position;
    public  Sprite Player;
    

    public MouseInput(Sprite player) {
        Player = player;
    }

    public  void MouseUpdate() {
        PrevmouseState = CurmouseState;
        CurmouseState = Mouse.GetState();
        Position = MousePositionOnCanvas();
        
        
    }

    public  Vector2 MousePositionOnCanvas() {
        // return new Vector2(CurmouseState.X, CurmouseState.Y); // needs a fix
        return new Vector2(CurmouseState.X +  Player.Rectangle.Width / 2 + Player.Position.X - Game1.ScreenWidth/2f,
            CurmouseState.Y +  Player.Rectangle.Height / 2 + Player.Position.Y - Game1.ScreenHeight/2f);
    }

    public bool MouseSinglePress() {
        // if (CurmouseState.LeftButton == ButtonState.Released && PrevmouseState.LeftButton == ButtonState.Pressed) {
        //     return true;
        // }
        // Particle.CreateParticle(Position);
        return (CurmouseState.LeftButton == ButtonState.Released && PrevmouseState.LeftButton == ButtonState.Pressed);
    }
}