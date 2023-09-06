using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Particles;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class Sprite : Component {
    protected AnimationMenager _animationMenager;
    protected Dictionary<string, Animation> _animations;
    protected Vector2 _position;
    protected bool _rotation;
    protected Rectangle _coallisionRec;
    
    // public Particle Particle = new Particle(); 

    

    public List<Sprite> childs = new List<Sprite>();
    public Physics physics;

    public Rectangle coallisionRec {
        get {
            return (isUser || isMonster) ? _coallisionRec : new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);
        }
        set {
            _coallisionRec = value;
        }
    }

    public bool isUser = false;
    public bool isMonster = false;
    
    public bool collision = false;
    public Texture2D _texture;
    public Texture2D childTexture;
    public int distanceBullet = 1;
    public Vector2 mousePoint;
    public Vector2 OriginalPos;
    public float transperent = 1f;
    
    public SpriteFont _spriteFont;
    public string Text;
    public Color color = Color.Black;
    public bool grid = false;

    

    public Vector2 Position {
        get { return _position; }
        set {
            _position = value;
            if (_animationMenager != null) {
                _animationMenager.Position = _position;
            }
        }
    }
    
    public bool Rotation {
        get { return _rotation; }
        set {
            _rotation = value;
            if (_animationMenager != null) {
                _animationMenager.Rotation = _rotation;
            }
        }
    }
    
    public Rectangle Rectangle;
    public Vector2 scale = Vector2.One;
    public Vector2 Velocity = Vector2.Zero;
    
    public int type; 
    // 1-Static object with coallision 
    // 2-Just the ground 
    // 3-Player
    // 4-Shooting
    // 5-Cards

    

    public Sprite(Dictionary<string, Animation> animations) {
        _animations = animations;
        _animationMenager = new AnimationMenager(_animations.First().Value);
        physics = new Physics(this);
        Rectangle = new Rectangle((int)Position.X, (int)Position.Y, _animations["Idle"].FrameWidth,
            _animations["Idle"].FrameHeight);
    }
    
    
    public Sprite(Texture2D texture2D, Rectangle rectangle) {
        _texture = texture2D;
        Rectangle = rectangle;
        physics = new Physics(this);
    }
    
    public Sprite(Texture2D texture2D, Rectangle rectangle, Vector2 position) {
        _texture = texture2D;
        Rectangle = rectangle;
        Position = position;
        OriginalPos = position;
        physics = new Physics(this);
    }
    
    public Sprite(SpriteFont spriteFont, string text,Vector2 position, float transperentVariable) {
        _spriteFont  = spriteFont;
        transperent = transperentVariable;
        Text = text;
        // Rectangle = rectangle;
        Position = position;
        OriginalPos = position;
    }
    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        if (_texture != null) {
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
            // if (grid) { // collision 
            // spriteBatch.Draw(ContentLoader.LoadTexturesGame()[9], coallisionRec, color * transperent); //Color.Wheat * 0.7f
            // }
            
        }else if (_spriteFont != null) {
            // spriteBatch.DrawString(_spriteFont, Text, Position, Color.Black * transperent);
            spriteBatch.DrawString(_spriteFont, Text, Position, color * transperent, 0f, 
                _spriteFont.MeasureString(Text) / 2, 1.0f, SpriteEffects.None,0.5f);
        } 
        else if (_animationMenager != null) {
            _animationMenager.Draw(spriteBatch);
            // if (Particle.activated) {
            //     // Particle.activated = false;
            //     spriteBatch.Draw(Particle._particleEffect);
            // }
            // spriteBatch.Draw(ContentLoader.LoadTexturesGame()[9], coallisionRec, Color.Wheat * 0.7f);
        }
        else {
            throw new Exception("This ain't right..");
        }
    }
    

    public override void Update(GameTime gameTime) { }
}