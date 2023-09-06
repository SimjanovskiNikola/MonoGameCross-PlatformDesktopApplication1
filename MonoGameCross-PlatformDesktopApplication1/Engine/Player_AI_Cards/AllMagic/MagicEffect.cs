using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Particles;

namespace MonoGameCross_PlatformDesktopApplication1.Engine.Player_AI_Cards.AllMagic; 

public class MagicEffect : Sprite {
    
    public Particle particle;
    public int Type;
    public bool freeze = false;
    public int damage = 0;

    public int effectTimer = 0;
    public int effectTimerFinal = 8000;
    public int timer = 0;
    public int finalTimer = 0;
    public bool Finished = false;
    public bool Ready = false;
    public Vector2 positionCenter;
    public MagicEffect(Texture2D texture2D, Vector2 position, int type) 
        : base(texture2D, new Rectangle(0, 0,100, 100)) { // (int)(position.X - 50), (int)(position.Y - 50)
        Position = new Vector2(position.X - 50, position.Y - 50);
        positionCenter = position;
        Type = type;
        particle = new Particle(new Vector2(position.X + 100, position.Y + 100), 1900);
        TimeAndEffect(type);
    }

    public void TimeAndEffect(int type) {
        switch (type) { 
            case 1:
               particle.CreateFreeze(positionCenter);
               finalTimer = 2000;
               effectTimerFinal = 0;
               freeze = true;
               break;
            case 2:
               particle.CreateFire(positionCenter);
               finalTimer = 2000;
               effectTimerFinal = 650;
               damage = 6;
               break;
            case 3:
               particle.CreateEarth(positionCenter); // needs Fix
               finalTimer = 2000;
               damage = 7;
               effectTimerFinal = 500;
               break;
            case 4:
               particle.CreateWind(positionCenter);
               finalTimer = 2000;
               effectTimerFinal = 350;
               damage = 8;
               break;
        }
    }

    public override void Update(GameTime gameTime) {
        timer += gameTime.ElapsedGameTime.Milliseconds;
        effectTimer += gameTime.ElapsedGameTime.Milliseconds;
        
        if (effectTimer > effectTimerFinal) {
            effectTimer = 0;
            Ready = true;
        }
        
        if (timer > finalTimer) {
            Finished = true;
        }
        
        particle.ParticleUpdate(gameTime);

    }

    public void TurnOff() {
        Ready = false;
    }

    public void collisionDetected(Monsters monster = null) {
        switch (Type) { 
            case 1:
                monster.freeze = true;
                break;
            case 2:
                monster.health -= damage;
                break;
            case 3:
                monster.health -= damage;
                break;
            case 4:
                monster.health -= damage;
                break;
        }
    }
    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Draw(
            _texture,
            Position,
            Rectangle,
            // color * transperent,
            Color.WhiteSmoke * transperent,
            // new Color(255, 255, 255 , 255),
            0f,
            Vector2.Zero, // (256/2f, 128/2f)
            scale,
            Rotation ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
            0f
        );

        if (particle.activated) {
            spriteBatch.Draw(particle._particleEffect);
        }
        
        
    }
}