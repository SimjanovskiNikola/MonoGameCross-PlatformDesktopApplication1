using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameCross_PlatformDesktopApplication1.AllMagic;
using MonoGameCross_PlatformDesktopApplication1.Engine;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class Player : Sprite {
    
    // Cards _cards
    public Vector2[] arenaPos = new Vector2[] { new Vector2(-300, 100), new Vector2(-100, 100), new Vector2(100, 100)};
    public Vector2[] arenaShoot = new Vector2[] { new Vector2(-300, -300), new Vector2(-100, -300), new Vector2(100, -300)};
    
    public int health = 20; // 20
    public bool arenaMoveTime = false;
    public bool arenaShootTime = false;
    public int shootPos = 1;
    
    public int prevArenaPos = -1;
    public int currentArenaPos = 1;

    public bool getTheCard = false;
    public bool eIsDown = false;
    public MouseInput MouseInput;
    public Cards Cards = new Cards();
    public List<Magic> Magics = new List<Magic>();


    public Player(Dictionary<string, Animation> animations) : base(animations) {
        MouseInput = new MouseInput(this);
    }

    public void MovePlayerArena() {
        prevArenaPos = currentArenaPos;
        Position = arenaPos[currentArenaPos];
    }

    public void Update(GameTime gameTime) {
        MouseInput.MouseUpdate();
        // Particle.ParticleUpdate(gameTime);
        UpdateMagic(gameTime);
        Move();
        Cards.Update(gameTime);

        if (_animationMenager != null) {
            SetAnimations();
            _animationMenager.Update(gameTime);
        }

        coallisionRec = new Rectangle((int)Position.X + 70, (int)Position.Y + 50, _animations["Idle"].FrameWidth - 130,
            _animations["Idle"].FrameHeight - 95);
        Position += Velocity;
        Velocity = Vector2.Zero;
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
    public void ShootPlayerArena() {
        // childs.Add(new Sprite(childTexture, new Rectangle(35,50, 10,10), mousePoint) {
        //     type = 5
        // });
        Magics.Add(new LightningMagic(childTexture, new Rectangle(35,50, 10,10), 
            centerPosition(), arenaShoot[shootPos] + new Vector2(150, -550), 0));
    }
    public void AddPointerToArena() {
        childs.Add(new Sprite(ContentLoader.LoadPointer(), new Rectangle(0,0, 60 ,60), arenaPos[currentArenaPos])
            {transperent = 0f});
    }

    public void AddPointer(Vector2 pos) {
        childs[0].transperent = 1f;
        pos.Y += 180;
        pos.X += 90;
        childs[0].Position = pos;
    }

    public void RemovePointer() {
        childs[0].transperent = 0f;
    }

    public virtual void Move() {
        if (Game1.state == GameState.GameArena) {
            if (arenaMoveTime) {
                if (Keyboard.GetState().IsKeyDown(Keys.A) && prevArenaPos != 2) {
                    currentArenaPos = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                    currentArenaPos = 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) && prevArenaPos != 0) {
                    currentArenaPos = 2;
                }
                AddPointer(arenaPos[currentArenaPos]);
            }
            else if (arenaShootTime){
                if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                    shootPos = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                    shootPos = 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                    shootPos = 2;
                }
                AddPointer(arenaShoot[shootPos]);
            }
            
        }
        else if (Game1.state == GameState.GamePlay) {
            getTheCard = false;
            if (Keyboard.GetState().IsKeyDown(Game1.parameters.PlayingKeysArray[0])) {
                Velocity.Y -= Level.VelocityPerRound[Level.levelNum];
                Sound.WalkEffect();
            }
            if (Keyboard.GetState().IsKeyDown(Game1.parameters.PlayingKeysArray[1])) {
                Velocity.Y += Level.VelocityPerRound[Level.levelNum];
                Sound.WalkEffect();
            }
            if (Keyboard.GetState().IsKeyDown(Game1.parameters.PlayingKeysArray[2])) {
                Velocity.X -= Level.VelocityPerRound[Level.levelNum];
                Sound.WalkEffect();
                Rotation = true;
            }
            if (Keyboard.GetState().IsKeyDown(Game1.parameters.PlayingKeysArray[3])) {
                Velocity.X += Level.VelocityPerRound[Level.levelNum];
                Sound.WalkEffect();
                Rotation = false;
            }
            if (MouseInput.MouseSinglePress() && Cards.card_num != -1 && !Cards.activated[Cards.card_num]) {
                Cards.usedCard();
                playerShootingMagic(Cards.card_num);
                // Console.WriteLine(Mouse.GetState().Position);
            }
            if (Keyboard.GetState().IsKeyDown(Game1.parameters.PlayingKeysArray[4])) {
                eIsDown = true;
            }
            if (eIsDown && Keyboard.GetState().IsKeyUp(Game1.parameters.PlayingKeysArray[4])) {
                eIsDown = false;
                getTheCard = true;
                // CardDistance();
            }
        }
    }

    public Vector2 centerPosition() {
        return Position + new Vector2(120, 100);
    }

    public void AddCardsHand() {
        Cards.cardsPickedUp();
        // Cards.AddCardsHand();
    }
    
    protected virtual void SetAnimations() {
        if (Velocity == Vector2.Zero) {
            _animationMenager.Play(_animations["Idle"]);    
        }
        else {
            _animationMenager.Play(_animations["Run"]);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        _animationMenager.Draw(spriteBatch);

        // spriteBatch.Draw(ContentLoader.LoadTexturesGame()[9], coallisionRec, Color.Wheat * 0.7f);
        foreach (var magic in Magics) {
            magic.Draw(gameTime, spriteBatch);
        }
    }

    public void playerShootingMagic(int type) {

        if (Magics.Count > 0) {
            for (int i = 0; i < Magics.Count; i++) {
                if (Magics[i].Type == type) {
                    return;
                }
            }
        }
        
        Magics.Add(new LightningMagic(childTexture, new Rectangle(35,50, 10,10), 
            centerPosition(), MouseInput.MousePositionOnCanvas(), type)); 
    }
    
}