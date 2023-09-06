using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class Menu {

    public Texture2D background;
    private List<Component> _components;
    private SpriteFont buttonFont;

    public Menu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) {
        background = content.Load<Texture2D>("Button/background");
        var buttonTexture = content.Load<Texture2D>("Button/newBtn");
        buttonFont = content.Load<SpriteFont>("Font/Font");
        
        // var sliderBackground =
        // var sliderTexture = 

        var newGameButton = new Button(buttonTexture, buttonFont) {
            Position = new Vector2(Game1.ScreenWidth / 2f, Game1.ScreenHeight/ 2f - 60),
            Text = "Start Game", 
        };
        newGameButton.Click += NewGameButton_Click;
        
        // var loadGameButton = new Button(buttonTexture, buttonFont) {
        //     Position = new Vector2(Game1.ScreenWidth / 2f, Game1.ScreenHeight/ 2f - 60),
        //     Text = "Load Game", 
        // };
        // loadGameButton.Click += LoadGameButton_Click;
        
        var settingsGameButton = new Button(buttonTexture, buttonFont) {
            Position = new Vector2(Game1.ScreenWidth / 2f, Game1.ScreenHeight/ 2f ),
            Text = "Settings", 
        };
        settingsGameButton.Click += SettingsGameButton_Click;
        
        var quitGameButton = new Button(buttonTexture, buttonFont) {
            Position = new Vector2(Game1.ScreenWidth / 2f, Game1.ScreenHeight/ 2f + 60),
            Text = "Quit", 
        };
        quitGameButton.Click += QuitGameButton_Click;

        _components = new List<Component>() {
            newGameButton,
            settingsGameButton,
            quitGameButton,
        };
    }

    private void NewGameButton_Click(object sender, EventArgs e) {
        Game1.ChangeState(GameState.GamePlay);
    }
    
    private void SettingsGameButton_Click(object sender, EventArgs e) {
        Game1.ChangeState(GameState.GameSettings);
    }
    
    private void QuitGameButton_Click(object sender, EventArgs e) {
        Game1.ChangeState(GameState.GameEnd);
    }
    
    // public void GameMenuLoader() {
    //     
    // }
    public  void Update(GameTime gameTime) {
        foreach (var component in _components) {
            component.Update(gameTime);
        }
    }

    public  void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        spriteBatch.Begin();
        spriteBatch.Draw(background, new Rectangle(0,0,Game1.ScreenWidth, Game1.ScreenHeight), Color.White);
        // spriteBatch.DrawString(buttonFont, "Outsmart Me If you can !!!", new Vector2(Game1.ScreenWidth/2 + 70,Game1.ScreenHeight/2 - 300), Color.Black, 0f, 
        //     buttonFont.MeasureString("Outsmart Me If you can !!!") / 2, 1.0f, SpriteEffects.None,0.5f);
        foreach (var component in _components) {
            component.Draw(gameTime, spriteBatch);
        }
        // spriteBatch.Draw(ContentLoader.LoadDamage(), new Rectangle(0,0,Game1.ScreenWidth, Game1.ScreenHeight), Color.White * 0.0f);
        spriteBatch.End();
    }
}