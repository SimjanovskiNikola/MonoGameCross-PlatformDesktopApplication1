using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class Monsters : Sprite {
    
    // private int health = 1;
    public bool attack = false;
    public double attackDelay = 0f;
    public Vector2 centerPosition;
    public Player player;
    public Vector2 chasingPoint;
    public bool useRandomnes = false;
    public double timer = 0f;
    public int health = 1;
    public bool Finished = false;
    public bool freeze = false;
    public int damage = 0;
    public int moveVelocity = 2;

    public Monsters(Dictionary<string, Animation> animations, Player player, Vector2 chasePos) : base(animations) {
        this.player = player;
        isMonster = true;
        chasingPoint = chasePos;
    }

    public void Update(GameTime gameTime) {
        if (health <= 0) {
            Finished = true;
        }
        
        // coallisionRec = new Rectangle((int)Position.X + 50, (int)Position.Y + 50, _animations["Idle"].FrameWidth - 110,
        //     _animations["Idle"].FrameHeight - 95); // Goblin
        // Console.WriteLine(coallisionRec);
        // coallisionRec = new Rectangle((int)Position.X + 20, (int)Position.Y , _animations["Idle"].FrameWidth - 30,
        //     _animations["Idle"].FrameHeight - 20); // Warm
        
        // coallisionRec = new Rectangle((int)Position.X + 50, (int)Position.Y + 50 , _animations["Idle"].FrameWidth - 110,
        //     _animations["Idle"].FrameHeight - 95); // Warm
        
        coallisionRec = new Rectangle((int)Position.X + 50, (int)Position.Y + 50 , _animations["Idle"].FrameWidth - 110,
            _animations["Idle"].FrameHeight - 95); // Warm
        // UseRandomnes(gameTime);
        SetAnimations();
        _animationMenager.Update(gameTime);
        followMainWizard(gameTime);
        freeze = false;
    }

    // public void UseRandomnes(GameTime gameTime) {
    //     timer += gameTime.ElapsedGameTime.Milliseconds;
    //
    //     if (useRandomnes || timer > 1000) {
    //         if (Mathematics.RandomNumber(0, 100) > 50) {
    //             chasingPoint = player.centerPosition() + new Vector2(0, Mathematics.RandomNumber(-1000, 1000));
    //
    //         }
    //         else {
    //             chasingPoint = player.centerPosition() + new Vector2(Mathematics.RandomNumber(-1000, 1000), 0);
    //
    //         }
    //         
    //         useRandomnes = false;
    //         timer = 0f;
    //     }
    //
    //     if (Mathematics.DistanceBetween(new Vector2(Position.X + 75, Position.Y + 75), player.centerPosition()) > 200) {
    //         chasingPoint = player.centerPosition();
    //     }
    //     
    //     
    // }
    
    
    protected virtual void SetAnimations() {
        if (attack) {
            _animationMenager.Play(_animations["Attack"]);    
        }
        else if (Velocity == Vector2.Zero) {
            _animationMenager.Play(_animations["Idle"]);    
        }
        else {
            _animationMenager.Play(_animations["Run"]);
        }
    }

    public void followMainWizard(GameTime gameTime) {
        if (freeze) {
            return;
        }
        if (!attack && Mathematics.DistanceBetween(new Vector2(Position.X + 75, Position.Y + 75), player.centerPosition()) > 200) {
            Velocity = Vector2.One;
            // Position = Mathematics.PointOnLine(Position, player.centerPosition() + chasingPoint , 1.5/Mathematics.DistanceBetween(Position, player.centerPosition() + chasingPoint));
            Position += Move(player.centerPosition() + chasingPoint, gameTime);
            // Position = Mathematics.PointOnLine(Position, Position + new Vector2(2, 0) , 1);
            ChangeSide(player);
        }
        else if (!attack && Mathematics.DistanceBetween(new Vector2(Position.X + 75, Position.Y + 75), player.centerPosition()) > 70) {
            Velocity = Vector2.One;
            // Position = Mathematics.PointOnLine(Position, player.centerPosition() , 1.5/Mathematics.DistanceBetween(Position, player.centerPosition()));
            Position += Move(player.centerPosition(), gameTime);
            // Position = Mathematics.PointOnLine(Position, Position + new Vector2(2, 0) , 1);
            ChangeSide(player);
        }
        else {
            attack = true;
            Velocity = Vector2.Zero;
        }
        
        AttackDelay(gameTime);
    }

    public Vector2 Move(Vector2 targetedPos, GameTime gameTime) {
        // float angle = targetedPos.Dot(Position);
        Vector2 Direction = targetedPos - new Vector2(Position.X + 75, Position.Y + 75);
        
        Direction.Normalize();
        return Direction / (moveVelocity * (float)0.8); //(float)gameTime.ElapsedGameTime.TotalSeconds;
        
    }

    // public Vector2 MovingDirections(List<Sprite> sprites) {
    //     if (freeze) {
    //         Console.WriteLine("Freezed");
    //         return Position;
    //     }
    //     int index = 0;
    //     double rez = double.MaxValue;
    //     Vector2[] bla = new Vector2[8];
    //     bla[0] = Position + new Vector2(2, 0);
    //     bla[1] = Position + new Vector2(-2, 0);
    //     bla[2] = Position + new Vector2(0, 2);
    //     bla[3] = Position + new Vector2(0, -2);
    //     bla[4] = Position + new Vector2(1, 1);
    //     bla[5] = Position + new Vector2(-1, 1);
    //     bla[6] = Position + new Vector2(1, -1);
    //     bla[7] = Position + new Vector2(-1, -1);
    //
    //     
    //     Vector2[] future = new Vector2[8];
    //     future[0] = Position + new Vector2(5, 0);
    //     future[1] = Position + new Vector2(-5, 0);
    //     future[2] = Position + new Vector2(0, 5);
    //     future[3] = Position + new Vector2(0, -5);
    //     future[4] = Position + new Vector2(5, 5);
    //     future[5] = Position + new Vector2(-5, 5);
    //     future[6] = Position + new Vector2(5, -5);
    //     future[7] = Position + new Vector2(-5, -5);
    //
    //     for (int i = 0; i < 8; i++) {
    //         double temprez = double.MaxValue;
    //         if (isMoveValid(future[i], sprites)) {
    //             temprez = Mathematics.DistanceBetween(bla[i], player.centerPosition());
    //         }
    //
    //         if (temprez < rez) {
    //             rez = temprez;
    //             index = i;
    //         }
    //     }
    //     
    //     float angle = bla[index].Dot(player.Position);
    //     // bla[index] = new Vector2((int)(bla[index].X * Math.Cos(angle)), (int)(bla[index].Y * Math.Cos(angle)));
    //     return Mathematics.PointOnLine(Position, bla[index] , 1);
    //     // return bla[index];
    // }
    //
    // public bool isMoveValid(Vector2 bla, List<Sprite> sprites) {
    //
    //     foreach (var sprite in sprites) {
    //         if (!sprite.collision) {
    //             continue;
    //         }
    //
    //         if (this.physics.Intersect_FuturePos(sprite, bla)) {
    //             return false;
    //         }
    //     }
    //
    //     return true;
    // }

    public void AttackDelay(GameTime gameTime) {
        if (attack) {
            attackDelay += gameTime.ElapsedGameTime.Milliseconds;
            if (attackDelay > 2000) {
                attack = false;
                attackDelay = 0;
                player.health -= 1;
            }
        }
    }

    public void ChangeSide(Sprite player) {
        if (player.Position.X > Position.X) {
            Rotation = false;
            return;
        }

        Rotation = true;
    }

}