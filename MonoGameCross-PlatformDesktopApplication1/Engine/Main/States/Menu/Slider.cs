using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class Slider : Component {

    private MouseState _currentMouseState;
    private SpriteFont _font;
    private bool _soundActivated = false;
    private bool _isHovering;
    private MouseState _previousMouseState;
    private Texture2D _background;
    private Texture2D _sliderTexture;

    public event EventHandler Click;
    public bool Clicked { get; private set; }
    
    public event EventHandler Press;
    public bool Pressed { get; private set; }
    public Color PenColour { get; set; }
    public Vector2 Position { get; set; }

    public Rectangle RectangleBackground {
        get { return new Rectangle((int)Position.X, (int)Position.Y, _background.Width, _background.Height); }
    }
    
    public Rectangle RectangleSlider {
        get { return new Rectangle((int)Position.X, (int)Position.Y, _sliderTexture.Width, _sliderTexture.Height); }
    }

    public Slider(Texture2D background, Texture2D sliderTexture) {
        _background = background;
        _sliderTexture = sliderTexture;
        PenColour = Color.Black;
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        var colour = Color.White;
        
        
        spriteBatch.Draw(_background, RectangleBackground, colour);
        
        if (_isHovering) {
            colour = Color.Gray;
            spriteBatch.Draw(_sliderTexture, RectangleSlider, colour);
        }
        else {
            spriteBatch.Draw(_sliderTexture, RectangleSlider, colour);
        }
        
    }

    public override void Update(GameTime gameTime) {
        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();

        var mouseRectangle = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 1, 1);
        _isHovering = false;

        if (mouseRectangle.Intersects(RectangleSlider)) {
            _isHovering = true;
            
            if (!_soundActivated) {
                Sound.UIHover();
                _soundActivated = true;
            }
            
            // if (_currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed) {
            //     Click?.Invoke(this, new EventArgs());
            // }

            if (_currentMouseState.LeftButton == ButtonState.Pressed) {
                Press?.Invoke(this, new EventArgs());
            }
        }
        else {
            _soundActivated = false;
        }
    }
}