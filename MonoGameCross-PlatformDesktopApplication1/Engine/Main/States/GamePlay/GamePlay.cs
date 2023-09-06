using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class GamePlay {
    
    private List<Texture2D> textures = new List<Texture2D>();
    private SpriteFont _spriteFont;
    private Camera _camera;
    private Sprite user;
    private Player _player;
    private List<Component> _components;
    private Scene _scene;
    public static Texture2D reta;
    public Stopwatch stopwatch;
    

    public GamePlay(GraphicsDevice graphicsDevice,ContentManager Content) {
        
        reta = new Texture2D(graphicsDevice, 1, 1);
        reta.SetData(new[] { Color.White });
        
        _camera = new Camera();
        _spriteFont = ContentLoader.LoadSpriteFont();


        textures = ContentLoader.LoadTexturesGame();
        
        _scene = new Scene(textures);
        // _player = new Player(ContentLoader.LoadMainWizardAnimation()) { isUser = true, childTexture = textures[7] };
        // _scene.Aplayer = _player;
        // _scene.AddPlayerToTheScene(_player);
        _player = Game1._player;
        _scene.Aplayer = _player;
        _scene.AddPlayerToTheScene( _player);
        _scene.MakingScene();
        _scene.AddDecorationGamePlay();

        stopwatch = new Stopwatch();
        stopwatch.Start();

        List<Texture2D> textureHealthBar = ContentLoader.LoadHealthBar();

        _components = new List<Component>() {
            new HealthBar(textureHealthBar[1], textureHealthBar[0], 20, new Vector2(100, 100))
        };
    }

    public void Prepare() {
        if (Level.restart[0]) {
            Level.restart[0] = false;
            _player.Position = new Vector2(0, 0);
            _player.Cards.RestartCards();
            _scene.difficulty = 1;
        }
        _scene.Prepare();
        _scene.difficulty++;
        stopwatch.Restart();
    }
    public void Update(GameTime gameTime) {
        Level.GamePlayFieldStats(_player, gameTime);
        if (Level.gameEnd) {
            stopwatch.Stop();
            return;
        }
        ((HealthBar)_components[0]).value = _player.health;
        _scene.Update(gameTime);
        _camera.Follow(_player);
        Effects.UpdateEffects();
        foreach (var component in _components) {
            component.Update(gameTime);
        }
        
        if (_scene.mode == 1) {
            _scene.EditorModeUpdate();
        }

        if (stopwatch.Elapsed.Seconds >= Level.SecondsPerRound[Level.levelNum]) { // 1000 Level.SecondsPerRound[Level.levelNum]
            stopwatch.Stop();
            Game1.ChangeState(GameState.GameArena);
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        
        
        spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend, transformMatrix:_camera.Transform);

        _scene.Draw(gameTime, spriteBatch);
        
        
        foreach (var effect in Effects.effects) {
            if (effect != null) {
                effect.Draw(gameTime, spriteBatch);
            }
        }
        spriteBatch.End();
        
        //EditMode
        spriteBatch.Begin();
        foreach (var component in _scene._editSceneComponents) {
            component.Draw(gameTime, spriteBatch);
            foreach (var child in component.childs) {
                child.Draw(gameTime, spriteBatch);
            }
        }

        foreach (var card in _player.Cards.cardsHand) {
            card.Draw(gameTime, spriteBatch);
        }
        
        foreach (var component in _components) {
            component.Draw(gameTime, spriteBatch);
        }
        
        spriteBatch.DrawString(_spriteFont, "Time : " + stopwatch.Elapsed.ToString(@"ss") + "/" + Level.SecondsPerRound[Level.levelNum], new Vector2(1000, 100),Color.Black);
        
        foreach (var effect in Effects.effects) {
            if (effect != null) {
                effect.Draw(gameTime, spriteBatch);
            }
        }
        
        spriteBatch.End();
        //EditMode
        // base.Draw(gameTime);
    }
}