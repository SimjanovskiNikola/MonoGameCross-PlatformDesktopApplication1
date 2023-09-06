using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace MonoGameCross_PlatformDesktopApplication1{

    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private static GamePlay _gamePlay;
        private static GamePlayArena _gameArena;
        private static Settings _settings;
        private Menu _menu;
        private static List<Song> _song;
        public static int ScreenHeight;
        public static int ScreenWidth;
        public static SaveLoadParameters parameters;
        
        public static Player _player;


        public static GameState state = GameState.GameMenu;
        public static GameState prevState;

        public static void ChangeState(GameState newState) {
            prevState = state;
            state = newState;
            Effects.BlackTransitionEffect();
            PrepareState();
        }

        public static void PrepareState() {
            switch (state) {
                case GameState.GameArena:
                    _gameArena.Prepare();
                    break;
                case GameState.GamePlay:
                    _gamePlay.Prepare();
                    break;
                default:
                    return;
            }
        }
        
        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;

            ScreenHeight = 1080;
            ScreenWidth = 1920;
            _graphics.ApplyChanges();
            base.Initialize();
            
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.AddNecessary(GraphicsDevice, Content);
            Effects.EffectsLoad(Content);
            Sound.LoadSounds();
            
            SaveLoadGame.SaveGame(new SaveLoadParameters());
            parameters = SaveLoadGame.LoadGame();
            
            _player = new Player(ContentLoader.LoadMainWizardAnimation()) { isUser = true, childTexture = ContentLoader.LoadTexturesGame()[7] };

            _gamePlay = new GamePlay(GraphicsDevice, Content);
            _gameArena = new GamePlayArena(GraphicsDevice, Content);
            _menu = new Menu(this, GraphicsDevice, Content);
            _settings = new Settings(this, GraphicsDevice, Content);
            
            _song = ContentLoader.LoadSongGame();
            MediaPlayer.Play(_song[0]);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0f;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            
        }
        
        public static void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e) {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0.5f;
            MediaPlayer.Play(_song[0]);
        }
        
        
        

        protected override void Update(GameTime gameTime) {
            if (!IsActive) {
                return;
            }
            
            switch (state) {
                case GameState.GameMenu:
                    _menu.Update(gameTime);
                    break;
                case GameState.GamePlay:
                    _gamePlay.Update(gameTime);
                    break;
                case GameState.GameArena:
                    _gameArena.Update(gameTime);
                    break;
                case GameState.GameSettings:
                    _settings.Update(gameTime); // Change !!!
                    break;
                case GameState.GameEnd:
                    Exit();
                    break;
                default:
                    return;
            }
            
            base.Update(gameTime);
        }
        
        

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (state) {
                case GameState.GameMenu:
                    _menu.Draw(gameTime, _spriteBatch);
                    break;
                case GameState.GamePlay:
                    _gamePlay.Draw(gameTime, _spriteBatch);
                    break;
                case GameState.GameArena:
                    _gameArena.Draw(gameTime, _spriteBatch);
                    break;
                case GameState.GameSettings:
                    _settings.Draw(gameTime, _spriteBatch); // Change
                    break;
                default:
                    return;
            }
            base.Draw(gameTime);
        }
        
    }
}
//dotnet mgcb-editor