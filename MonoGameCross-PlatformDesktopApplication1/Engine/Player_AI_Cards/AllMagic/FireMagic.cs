using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameCross_PlatformDesktopApplication1.AllMagic; 

public class FireMagic : Magic{
    public FireMagic(Texture2D texture2D, Rectangle rectangle, Vector2 pos, Vector2 des, int type) 
        : base(texture2D, rectangle, pos, des, type) {

        velocityMagic = 5;
        Damage = 3;
    }


    public override void collisionDetected(Monsters monster = null) {
        if (monster != null) {
            DealDmg(monster);
        }
    }

    public void DealDmg(Monsters monster) {
        monster.health = -Damage;
    }
    
    public override void Update(GameTime gameTime) {
        Move();
    }

    public void Move() {
        double time = distance/Mathematics.DistanceBetween(DestinationPos, InitialPos) ;
        if (time > 1) {
            Finished = true;
            return;
        }
        Position = Mathematics.PointOnLine(InitialPos, DestinationPos , time);
        distance += velocityMagic;
    }
}