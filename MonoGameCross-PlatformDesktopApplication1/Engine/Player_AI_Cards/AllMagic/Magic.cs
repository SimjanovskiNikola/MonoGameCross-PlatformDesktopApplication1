using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class Magic : Sprite{
    public double timer = 0f;
    public double timerParticle = 0f;
    public int distance = 0;
    public int velocityMagic = 1;
    public int Damage = 1;
    private Rectangle _rectangle;
    public Vector2 InitialPos;
    public Vector2 DestinationPos;
    private Texture2D _texture2D;
    public int Type;
    private bool activate = false;
    public bool Finished = false;

    public Magic(Texture2D texture2D, Rectangle rectangle, Vector2 pos, Vector2 des, int type):base(texture2D, rectangle) {
        _texture2D = texture2D;
        _rectangle = rectangle;
        InitialPos = pos;
        Position = pos;
        DestinationPos = des;
        Type = type;
    }

    public virtual void collisionDetected(Monsters monster = null) {}
    

    // public override void Update(GameTime gameTime) {
    //
    //     switch (_type) {
    //         case 1: break;
    //         case 2: break;
    //         case 3: break;
    //         case 4: break;
    //         case 5: break;
    //     }
    //     
    // }
    
    // 1. Lightning - very fast velocity; Only one target; low dmg; cooldown 1 sec 
    // 2. Freezing -  medium velocity; Doesn't deal damage; Freezes Enemies 2sec; Gives Speed Boost 2sec;  cooldown 3 sec;
    // 3. Fire  - slow velocity; Deals dmg in area for 1 sec; coldown 3 sec;
    // 4. Earth - very fast; Deals dmg to all, minimal; stuns withing area instantly; coldown 8 sec; 
    // 5. Wind - medium velocity; Deals dmg in an area only 3 times 



}