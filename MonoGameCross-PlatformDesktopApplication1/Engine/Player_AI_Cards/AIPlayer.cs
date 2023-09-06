using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameCross_PlatformDesktopApplication1.AllMagic;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class AIPlayer : Sprite{
    
    public Vector2[] arenaShoot = new Vector2[] { new Vector2(-300, 100), new Vector2(-100, 100), new Vector2(100, 100)};
    public Vector2[] arenaPos = new Vector2[] { new Vector2(-300, -300), new Vector2(-100, -300), new Vector2(100, -300)};

    public int health = 10; // 20
    public bool arenaMoveTime = false;
    public bool arenaShootTime = false;
    public int shootPos = 1;
    
    public int prevArenaPos = -1;
    public int currentArenaPos = 1;
    public List<Magic> Magics = new List<Magic>();
        
    public AIPlayer(Dictionary<string, Animation> animations) : base(animations) {
        
    }
    
    public void MovePlayerArena() {
        prevArenaPos = currentArenaPos;
        currentArenaPos = Mathematics.RandomNumber(0, 3);
        Position = arenaPos[currentArenaPos];
    }
    
    public void ShootPlayerArena() {
        // childs.Add(new Sprite(childTexture, new Rectangle(35,50, 10,10), arenaPos[currentArenaPos]) {
        //     type = 5
        // });
        
        Magics.Add(new LightningMagic(childTexture, new Rectangle(35,50, 10,10), 
            centerPosition(), arenaShoot[shootPos], 0));
    }
    
    public Vector2 centerPosition() {
        return Position + new Vector2(120, 100);
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        _animationMenager.Draw(spriteBatch);
        
        foreach (var magic in Magics) {
            magic.Draw(gameTime, spriteBatch);
        }
    }

    public override void Update(GameTime gameTime) {
            Move();
            // BulletMoving();
            UpdateMagic(gameTime);
            
            SetAnimations();
            _animationMenager.Update(gameTime);
            
            
    }
    
    public void UpdateMagic(GameTime gameTime) {

        for (int i = 0; i < Magics.Count; i++) {
            Magics[i].Update(gameTime);
            if (Magics[i].Finished) {
                Magics.Remove(Magics[i]);
                i--;
            }
        }
    }

    public virtual void Move() {
        if (arenaMoveTime) {
            Position = arenaPos[Mathematics.RandomNumber(0, 3)];
        }
        if (arenaShootTime) {
            shootPos = Mathematics.RandomNumber(0, 3);
        }
    }
    
    public void BulletMoving() {
        for (int i = 0; i < childs.Count; i++) {
            if (childs[i].type == 5) {
                double time = distanceBullet/Mathematics.DistanceBetween(arenaShoot[shootPos], childs[i].OriginalPos) ;
                if (time > 1) {
                    RemoveBoolet();
                    return;
                }
                childs[i].Position = Mathematics.PointOnLine(childs[i].OriginalPos, arenaShoot[shootPos] , time);
                distanceBullet += 3;
                return;
            }
        }
    }
    
    public void RemoveBoolet() {
        for (int i = 0; i < childs.Count; i++) {
            if (childs[i].type == 5) {
                childs.RemoveAt(i);
                distanceBullet = 0;
            }
        }
    }
    
    protected virtual void SetAnimations() {
        if (Velocity == Vector2.Zero) {
            _animationMenager.Play(_animations["Idle"]);    
        }
        else {
            _animationMenager.Play(_animations["Run"]);
        }
    }
}