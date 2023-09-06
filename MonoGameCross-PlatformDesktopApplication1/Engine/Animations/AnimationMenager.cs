using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class AnimationMenager {
    private Animation _animation;
    private float _timer;
    public Vector2 Position{ get; set; }
    public bool Rotation { get; set; }

    public AnimationMenager(Animation animation) {
        _animation = animation;
    }

    public void Draw(SpriteBatch spriteBatch) {
        // spriteBatch.Draw(_animation.Texture, Position,
        //     new Rectangle(_animation.CurrentFrame * _animation.FrameWidth, 0, _animation.FrameWidth,
        //         _animation.FrameHeight),
        //     Color.White);
        
        spriteBatch.Draw(
            _animation.Texture,
            Position,
            new Rectangle(_animation.CurrentFrame * _animation.FrameWidth, 0, _animation.FrameWidth, _animation.FrameHeight),
            Color.White,
            0f,
            Vector2.Zero, // (256/2f, 128/2f)
            Vector2.One, 
            Rotation ? SpriteEffects.FlipHorizontally: SpriteEffects.None ,
            0f
        );
    }

    public void Play(Animation animation) {
        if (_animation == animation) {
            return;
        }

        _animation = animation;
        _animation.CurrentFrame = 0;
        _timer = 0;
    }

    public void Stop() {
        _timer = 0f;
        _animation.CurrentFrame = 0;
    }

    public void Update(GameTime gameTime) {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_timer > _animation.FrameSpeed) {
            _timer = 0f;
            _animation.CurrentFrame++;
            if (_animation.CurrentFrame >= _animation.FrameCount) {
                _animation.CurrentFrame = 0;
            }
        }
    }
}