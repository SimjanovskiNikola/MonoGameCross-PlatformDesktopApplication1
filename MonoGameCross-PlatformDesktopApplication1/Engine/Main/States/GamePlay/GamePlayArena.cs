using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class GamePlayArena {
    private List<Texture2D> textures = new List<Texture2D>();
    private SpriteFont _spriteFont;
    private Camera _camera;
    private Sprite user;
    private Player _player;
    private AIPlayer _playerAI;
    private Cards _cards;
    private List<Component> _components;
    private Scene _scene;
    public static Texture2D reta;
    public Stopwatch stopwatch;
    public int timeState = 0; // Move 0, Shoot 1, Moving 2, Shooting  3, Chilling 4

    public string str = "Waiting...";
    // Please Move !!! 3
    // Please Shoot !!! 0
    // Player Moving !!! 1
    // Player Shooting !!! 2
    public GamePlayArena(GraphicsDevice graphicsDevice,ContentManager Content) {
        reta = new Texture2D(graphicsDevice, 1, 1);
        reta.SetData(new[] { Color.White });
        
        _camera = new Camera();
        _spriteFont = ContentLoader.LoadSpriteFont();

        var animations = new Dictionary<string, Animation>() {
            { "Idle", new Animation(Content.Load<Texture2D>("Wizzard/Idle"), 6) },
            { "Run", new Animation(Content.Load<Texture2D>("Wizzard/Run"), 8) }
        };
        
        textures = ContentLoader.LoadTexturesGame();
        
        _scene = new Scene(textures);
        // user = new Sprite(animations){isUser = true, childTexture = textures[7]};
        _player = Game1._player;//new Player(animations){isUser = true,childTexture = textures[7]};
        _player.Position = _player.arenaPos[1];
         _playerAI = new AIPlayer(ContentLoader.LoadEvilWizardAnimation()){childTexture = textures[7]};
         _playerAI.Position = _playerAI.arenaPos[1];
        // _cards = new Cards(_player, textures);
        _camera.MakeStatic(_player);
        
        _scene.Aplayer = _player;
        _scene.AddPlayerToTheScene(_player);
        _scene.AddAIPlayerToTheScene(_playerAI);
        _scene.MakingScene();
        _scene.AddDecorationGameArena();
        // _scene.AddSpriteToTheScene(_cards);
        _scene.AddSpriteToTheScene(_playerAI);
        _player.AddPointerToArena();

        stopwatch = new Stopwatch();
        
        List<Texture2D> textureHealthBar = ContentLoader.LoadHealthBar();
        _components = new List<Component>() {
            
            new HealthBar(textureHealthBar[1], textureHealthBar[0], 20, new Vector2(575, 850)),
            new HealthBar(textureHealthBar[1], textureHealthBar[0], 10, new Vector2(575, 50))
        };

    }

    public void Time() {
        
        if (stopwatch.Elapsed.Seconds >= 5) {
            switch (timeState) {
                case 0:
                    timeState = 1;
                    str = "Time to Move: ";
                    // whatishappening = "Please Move";
                    Effects.TextCentarEffect("Move!");
                    _player.arenaMoveTime = true;
                    break;
                case 1:
                    timeState = 2;
                    str = "Time to Shoot: ";
                    Effects.TextCentarEffect("Shoot!");
                    // whatishappening = "Please Shoot";
                    _player.arenaMoveTime = false;
                    _player.arenaShootTime = true;
                    break;
                case 2:
                    timeState = 3;
                    str = "Players Moving: ";
                    Effects.TextCentarEffect("Players Moving");
                    _player.arenaShootTime = false;
                    _player.RemovePointer();
                    _player.MovePlayerArena();
                    _playerAI.MovePlayerArena();
                    break;
                case 3:
                    timeState = 4;
                    str = "Players Shooting: ";
                    _player.ShootPlayerArena();
                    _playerAI.ShootPlayerArena();
                    break;
                case 4:
                    timeState = 0;
                    str = "Waiting... : ";
                    Level.GameArenaStats(_player, _playerAI);
                    
                    break;
            }
            stopwatch.Restart();
        }

    }

    public void Prepare() {
        if (Level.restart[1]) {
            Level.restart[1] = false;
            _playerAI.health = 2;
        }
        // _scene.MakingScene();
        // _scene.AddSpriteToTheScene(_cards);
        // _scene.AddSpriteToTheScene(_player);
        _player.Position = _player.arenaPos[1];
        _playerAI.Position = _playerAI.arenaPos[1];
        timeState = 0;
        stopwatch.Start();
    }
    
    public  void Update(GameTime gameTime) {
        
        ((HealthBar)_components[0]).value = _player.health;
        ((HealthBar)_components[1]).value = _playerAI.health;
        foreach (var component in _components) {
            component.Update(gameTime);
        }
        
        Level.GamePlayArenaStats(_player, _playerAI, gameTime);
        if (Level.gameEnd) {
            Effects.UpdateEffects();
            stopwatch.Stop();
            return;
        }
        
        _scene.Update(gameTime);
        // _cards.Update();
        if (_scene.mode == 1) {
            _scene.EditorModeUpdate();
        }
        
        Time();
        Effects.UpdateEffects();

        // if (Stopwatch.Elapsed.Seconds > 10) {
        //     Stopwatch.Stop();
        //     Game1.ChangeState(GameState.GameArena);
        // }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        
        
        spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,transformMatrix:_camera.Transform); // 
        
        _scene.Draw(gameTime, spriteBatch);
            
        // foreach (var component in _scene._sceneComponents) {
        //     component.Draw(gameTime, spriteBatch);
        //     foreach (var child in component.childs) {
        //         child.Draw(gameTime, spriteBatch);
        //     }
        // }
        
        
        spriteBatch.End();
        
        //EditMode
        spriteBatch.Begin();
        
        foreach (var component in _scene._editSceneComponents) {
            component.Draw(gameTime, spriteBatch);
            foreach (var child in component.childs) {
                child.Draw(gameTime, spriteBatch);
            }
        }

        foreach (var component in _components) {
            component.Draw(gameTime, spriteBatch);
        }

        // foreach (var card in _cards.cardsHand) {
        //     card.Draw(gameTime, spriteBatch);
        // }

        foreach (var effect in Effects.effects) {
            if (effect != null) {
                effect.Draw(gameTime, spriteBatch);
            }
        }
        
        spriteBatch.DrawString(_spriteFont, str + stopwatch.Elapsed.ToString(@"ss")[1] + "/5", new Vector2(1600, 100),Color.Black);
        spriteBatch.DrawString(_spriteFont, "State: " + timeState, new Vector2(1600, 200),Color.Black);
        // spriteBatch.DrawString(_spriteFont, whatishappening, new Vector2(1600, 300),Color.Black);
        

        // foreach (var card in _scene.) {
        //     
        // }
        
        
        spriteBatch.End();
        //EditMode
        // base.Draw(gameTime);
    }
}