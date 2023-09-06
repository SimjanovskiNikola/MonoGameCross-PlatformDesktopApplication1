using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameCross_PlatformDesktopApplication1; 
//Ako si dovolno Pameten ke mozis da naprajs da ne raboti ova so tempParameters tuku samo so ona od Game1
public class Settings {

    public Texture2D background;
    private List<Component> _components;
    private SpriteFont buttonFont;

    private SaveLoadParameters tempParameters;
    // public int musicValue = Game1.parameters.musicVolume;
    // public int soundValue = Game1.parameters.soundVolume;
    public bool waiting = false;
    public int waitingInt = -1;
    public bool closingSettings = true;

    public Button[] inputButtons = new Button[10];
    // public Keys[] keys
    
    public Settings(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) {
        background = content.Load<Texture2D>("Button/settingBack");
        var buttonTexture = content.Load<Texture2D>("Button/newBtn");
        buttonFont = content.Load<SpriteFont>("Font/Font");

        #region increaseVolumeSound
        var increaseVolumeSound = new Button(buttonTexture, buttonFont) {
            Position = new Vector2(100, 400),
            Text = "+", 
        };
        increaseVolumeSound.Press += IncreaseVolumeSound_Press;

        #endregion
        
        #region decreaseVolumeSound

        var decreaseVolumeSound = new Button(buttonTexture, buttonFont) {
            Position = new Vector2(400, 400),
            Text = "-", 
        };
        decreaseVolumeSound.Press += DecreaseVolumeSound_Press;

        #endregion
        
        #region increaseVolumeMusic
        var increaseVolumeMusic = new Button(buttonTexture, buttonFont) {
            Position = new Vector2(100, 600),
            Text = "+", 
        };
        increaseVolumeMusic.Press += IncreaseVolume_Press;
        

        #endregion

        #region decreaseVolumeMusic

        var decreaseVolumeMusic = new Button(buttonTexture, buttonFont) {
            Position = new Vector2(400, 600),
            Text = "-", 
        };
        decreaseVolumeMusic.Press += DecreaseVolume_Press;

        #endregion

        if (closingSettings) {
            closingSettings = false;
            tempParameters = SaveLoadGame.LoadGame();
        }

        for (int i = 0; i < inputButtons.Length; i++) {
            if (i < 5) {
                inputButtons[i] = new Button(buttonTexture, buttonFont) {
                    Position = new Vector2(Game1.ScreenWidth*3.5f/9f + 200,Game1.ScreenHeight/2 - 160 + (i * 100)), // Popravka !!!
                    Text = tempParameters.PlayingKeysArray[i].ToString(),
                };
            }
            else {
                inputButtons[i] = new Button(buttonTexture, buttonFont) {
                    Position = new Vector2(Game1.ScreenWidth*6f/9f + 300,Game1.ScreenHeight/2 - 160 + ((i-5) * 100)), // Popravka !!!
                    Text = tempParameters.PlayingKeysArray[i].ToString(),
                };
            }
            inputButtons[i].Press += Input_Click;
        }



        #region OkButton
        var okSettingsButton = new Button(buttonTexture, buttonFont) {
            Position = new Vector2(300, Game1.ScreenHeight - 100),
            Text = "Ok", 
        };
        okSettingsButton.Click += Ok_Click;
        

        #endregion

        #region cancelButton

        var cancelSettingsButton = new Button(buttonTexture, buttonFont) {
            Position = new Vector2(1600, Game1.ScreenHeight - 100),
            Text = "Cancel", 
        };
        cancelSettingsButton.Click += Cancel_Click;

        #endregion
        
        //
        _components = new List<Component>() {
            increaseVolumeSound,
            decreaseVolumeSound,
            increaseVolumeMusic,
            decreaseVolumeMusic,
            okSettingsButton,
            cancelSettingsButton,
            inputButtons[0],
            inputButtons[1],
            inputButtons[2],
            inputButtons[3],
            inputButtons[4],
            
            inputButtons[5],
            inputButtons[6],
            inputButtons[7],
            inputButtons[8],
            inputButtons[9],
        };
    }
    
    private async void IncreaseVolumeSound_Press(object sender, EventArgs e) {
        // Game1.ChangeState(GameState.GamePlay);
        if (tempParameters.soundVolume  >= 100) {
            tempParameters.soundVolume = 100;
            return;
        }
        await Task.Delay(50);
        tempParameters.soundVolume += 1;

    }
    
    private async void DecreaseVolumeSound_Press(object sender, EventArgs e) {
        // Game1.ChangeState(GameState.GameSettings);
        if (tempParameters.soundVolume  <= 0) {
            tempParameters.soundVolume = 0;
            return;
        }
        await Task.Delay(50);
        tempParameters.soundVolume -= 1;
    }

    private async void IncreaseVolume_Press(object sender, EventArgs e) {
        // Game1.ChangeState(GameState.GamePlay);
        if (tempParameters.musicVolume  >= 100) {
            tempParameters.musicVolume  = 100;
            return;
        }
        await Task.Delay(50);
        tempParameters.musicVolume  += 1;

    }
    
    private async void DecreaseVolume_Press(object sender, EventArgs e) {
        // Game1.ChangeState(GameState.GameSettings);
        if (tempParameters.musicVolume   <= 0) {
            tempParameters.musicVolume  = 0;
            return;
        }
        await Task.Delay(50);
        tempParameters.musicVolume  -= 1;
    }
    
    private void Ok_Click(object sender, EventArgs e) {
        waiting = false;
        closingSettings = true;
        Sound.UIOk();
        Game1.parameters = tempParameters;
        SaveLoadGame.SaveChanges(tempParameters);
        Game1.ChangeState(GameState.GameMenu);
    }
    
    private void Cancel_Click(object sender, EventArgs e) {
        waiting = false;
        closingSettings = true;
        Sound.UICancel();
        Game1.ChangeState(GameState.GameMenu);
    }

    private void Input_Click(object sender, EventArgs e) {
        waitingInt = SeeWhichButton(((Button)sender).Position);
        waiting = true;
    }

    public int SeeWhichButton(Vector2 pos) {

        if (pos == inputButtons[0].Position) {
            return 0;
        }else if (pos == inputButtons[1].Position) {
            return 1;
        }else if (pos == inputButtons[2].Position) {
            return 2;
        }else if (pos == inputButtons[3].Position) {
            return 3;
        }else if (pos == inputButtons[4].Position) {
            return 4;
        }else if (pos == inputButtons[5].Position) {
            return 5;
        }else if (pos == inputButtons[6].Position) {
            return 6;
        }else if (pos == inputButtons[7].Position) {
            return 7;
        }else if (pos == inputButtons[8].Position) {
            return 8;
        }else if (pos == inputButtons[9].Position) {
            return 9;
        }

        return -1;
    }
    
    
    
    public void CreateTempParam() {
        tempParameters = SaveLoadGame.LoadGame();
        for (int i = 0; i < inputButtons.Length; i++) {
            inputButtons[i].Text = tempParameters.PlayingKeysArray[i].ToString();
        }
        // tempParameters = new SaveLoadParameters() {
        //     loadGameState = Game1.parameters.loadGameState,
        //     musicVolume = Game1.parameters.musicVolume,
        //     soundVolume = Game1.parameters.soundVolume,
        //     PlayingKeysArray = new Keys[] {
        //         Game1.parameters.PlayingKeysArray[0],
        //         Game1.parameters.PlayingKeysArray[1],
        //         Game1.parameters.PlayingKeysArray[2],
        //         Game1.parameters.PlayingKeysArray[3],
        //         Game1.parameters.PlayingKeysArray[4],
        //     },
        // };
    }
    public  void Update(GameTime gameTime) {
        if (closingSettings) {
            closingSettings = false;
            CreateTempParam();
        }
        
        foreach (var component in _components) {
            component.Update(gameTime);
        }
        
        if (waiting && Keyboard.GetState().GetPressedKeyCount() > 0) {
            waiting = false;
            tempParameters.PlayingKeysArray[waitingInt] = Keyboard.GetState().GetPressedKeys()[0];
            inputButtons[waitingInt].Text = tempParameters.PlayingKeysArray[waitingInt].ToString();
        }

        
    }

    public  void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Begin();
        spriteBatch.Draw(background, new Rectangle(0,0,Game1.ScreenWidth, Game1.ScreenHeight), Color.White * 1.0f);

        #region Titles
        spriteBatch.DrawString(buttonFont, "Settings", 
            new Vector2(Game1.ScreenWidth/2f,Game1.ScreenHeight/2 - 400), Color.Black);
        spriteBatch.DrawString(buttonFont, "Audio Settings", 
            new Vector2(Game1.ScreenWidth/9f,Game1.ScreenHeight/2 - 300), Color.Black);
        spriteBatch.DrawString(buttonFont, "Movement Settings", 
            new Vector2(Game1.ScreenWidth*3.5f/9f,Game1.ScreenHeight/2 - 300), Color.Black);
        spriteBatch.DrawString(buttonFont, "Card Selection Settings", 
            new Vector2(Game1.ScreenWidth*6/9f,Game1.ScreenHeight/2 - 300), Color.Black);
        

        #endregion

        #region Sound
        spriteBatch.DrawString(buttonFont, "Sound Volume", 
            new Vector2(225,350), Color.Black);
        spriteBatch.DrawString(buttonFont, tempParameters.soundVolume.ToString(), 
            new Vector2(300,410), Color.Black);
        

        #endregion

        #region Music
        spriteBatch.DrawString(buttonFont, "Music Volume", 
            new Vector2(225,550), Color.Black);
        spriteBatch.DrawString(buttonFont, tempParameters.musicVolume.ToString(), 
            new Vector2(300,610), Color.Black);
        

        #endregion

        #region Movement Input Settings

        spriteBatch.DrawString(buttonFont, "Move Forward", 
            new Vector2(Game1.ScreenWidth*3.5f/9f,Game1.ScreenHeight/2 - 150), Color.Black);
        
        spriteBatch.DrawString(buttonFont, "Move Backward", 
            new Vector2(Game1.ScreenWidth*3.5f/9f,Game1.ScreenHeight/2 - 50), Color.Black);
        
        spriteBatch.DrawString(buttonFont, "Move Left", 
            new Vector2(Game1.ScreenWidth*3.5f/9f,Game1.ScreenHeight/2 + 50), Color.Black);
        
        spriteBatch.DrawString(buttonFont, "Move Right", 
            new Vector2(Game1.ScreenWidth*3.5f/9f,Game1.ScreenHeight/2 + 150), Color.Black);
        
        spriteBatch.DrawString(buttonFont, "Pick Up Items", 
            new Vector2(Game1.ScreenWidth*3.5f/9f,Game1.ScreenHeight/2 + 250), Color.Black);
        

        #endregion
        
        #region CardSelection Settings

        spriteBatch.DrawString(buttonFont, "Select Card Lightning", 
            new Vector2(Game1.ScreenWidth*6f/9f,Game1.ScreenHeight/2 - 150), Color.Black);
        
        spriteBatch.DrawString(buttonFont, "Select Card Freeze", 
            new Vector2(Game1.ScreenWidth*6f/9f,Game1.ScreenHeight/2 - 50), Color.Black);
        
        spriteBatch.DrawString(buttonFont, "Select Card Fire", 
            new Vector2(Game1.ScreenWidth*6f/9f,Game1.ScreenHeight/2 + 50), Color.Black);
        
        spriteBatch.DrawString(buttonFont, "Select Card Magic Earth", 
            new Vector2(Game1.ScreenWidth*6f/9f,Game1.ScreenHeight/2 + 150), Color.Black);
        
        spriteBatch.DrawString(buttonFont, "Select Card Wind", 
            new Vector2(Game1.ScreenWidth*6f/9f,Game1.ScreenHeight/2 + 250), Color.Black);
        

        #endregion
        
       
        
       
        foreach (var component in _components) {
            component.Draw(gameTime, spriteBatch);
        }
        // spriteBatch.Draw(ContentLoader.LoadDamage(), new Rectangle(0,0,Game1.ScreenWidth, Game1.ScreenHeight), Color.White * 0.0f);
        spriteBatch.End();
    }
}
