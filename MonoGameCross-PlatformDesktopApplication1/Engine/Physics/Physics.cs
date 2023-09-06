using System;
using Microsoft.Xna.Framework;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class Physics {
    public Sprite spriteObj;

    public Physics(Sprite sprite) {
        spriteObj = sprite;
    }

    public Vector2 Resolve(Sprite spriteHelper) {
        if (Intersect(spriteHelper)) {
            var vX = spriteObj.coallisionRec.X + spriteObj.coallisionRec.Width / 2f - (spriteHelper.coallisionRec.X + spriteHelper.coallisionRec.Width / 2f);
            var vY = spriteObj.coallisionRec.Y + spriteObj.coallisionRec.Height / 2f - (spriteHelper.coallisionRec.Y + spriteHelper.coallisionRec.Height / 2f);
            var ww2 = spriteObj.coallisionRec.Width / 2f + spriteHelper.coallisionRec.Width / 2f;
            var hh2 = spriteObj.coallisionRec.Height / 2f + spriteHelper.coallisionRec.Height / 2f;

            var v = new Vector2();
            if (Math.Abs(vX) < ww2 && Math.Abs(vY) < hh2) {
                var oX = ww2 - Math.Abs(vX);
                var oY = hh2 - Math.Abs(vY);
                if (oX >= oY) {
                    if (vY > 0) {
                        v.Y += oY;
                    } else {
                        v.Y -= oY;
                    }
                } else {
                    if (vX > 0) {
                        v.X += oX;
                    } else {
                        v.X -= oX;
                    }
                }
            }
            
            // v += new Vector2(Mathematics.RandomNumber(-2, 2),Mathematics.RandomNumber(-2, 2) );
            return v;
            // Debug.WriteLine("Collision");
        }
        return Vector2.Zero;
    }
    
    public bool Intersect(Sprite spriteHelper) {
        Rectangle rec1 = new Rectangle(spriteObj.coallisionRec.X, spriteObj.coallisionRec.Y, 
            spriteObj.coallisionRec.Width, spriteObj.coallisionRec.Height);
            
        Rectangle rec2 = new Rectangle(spriteHelper.coallisionRec.X,
            spriteHelper.coallisionRec.Y, spriteHelper.coallisionRec.Width, spriteHelper.coallisionRec.Height);
        
        return rec1.Intersects(rec2);
    }

    public bool Intersect_FuturePos(Sprite spriteHelper, Vector2 futurePos) {
        Rectangle rec1 = new Rectangle((int)futurePos.X, (int)futurePos.Y, 
            spriteObj.coallisionRec.Width, spriteObj.coallisionRec.Height);
            
        Rectangle rec2 = new Rectangle(spriteHelper.coallisionRec.X,
            spriteHelper.coallisionRec.Y, spriteHelper.coallisionRec.Width, spriteHelper.coallisionRec.Height);
        
        return rec1.Intersects(rec2);
    }

}