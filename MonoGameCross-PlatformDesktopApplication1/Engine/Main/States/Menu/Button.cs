using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class Button : Component {

    private MouseState _currentMouseState;
    private SpriteFont _font;
    private bool _soundActivated = false;
    private bool _isHovering;
    private MouseState _previousMouseState;
    private Texture2D _texture;

    public event EventHandler Click;
    public bool Clicked { get; private set; }
    
    public event EventHandler Press;
    public bool Pressed { get; private set; }
    public Color PenColour { get; set; }
    public Vector2 Position { get; set; }

    public Rectangle Rectangle {
        get { return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); }
    }
    public string Text { get; set; }

    public Button(Texture2D texture, SpriteFont font) {
        _texture = texture;
        _font = font;
        PenColour = Color.Black;
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        var colour = Color.White;
        
        if (_isHovering) {
            colour = Color.Gray;
            spriteBatch.Draw(_texture, Rectangle, colour);
        }
        else {
            spriteBatch.Draw(_texture, Rectangle, colour * 0.5f);
        }

        if (!string.IsNullOrEmpty(Text)) {
            var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
            var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);
            
            spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
        }
    }

    public override void Update(GameTime gameTime) {
        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();

        var mouseRectangle = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 1, 1);
        _isHovering = false;

        if (mouseRectangle.Intersects(Rectangle)) {
            _isHovering = true;
            
            if (!_soundActivated) {
                Sound.UIHover();
                _soundActivated = true;
            }
            

            if (_currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed) {
                Click?.Invoke(this, new EventArgs());
            }

            if (_currentMouseState.LeftButton == ButtonState.Pressed) {
                Press?.Invoke(this, new EventArgs());
            }
        }
        else {
            _soundActivated = false;
        }
    }
}