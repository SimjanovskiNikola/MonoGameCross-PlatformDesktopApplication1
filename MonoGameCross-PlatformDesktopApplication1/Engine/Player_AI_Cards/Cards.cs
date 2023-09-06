using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class Cards : Sprite{
    private Player _player;
    public List<Sprite> cardsHand = new List<Sprite>();
    public List<Texture2D> cardsTexture = new List<Texture2D>();
    public bool eIsDown = false;
    
    public int cardsCollected = 0;
    public int[] cardsNeededToUnlock = new int[] { 0, 1, 2, 3, 4};
    public int cardsInHand = 0;
    
    private List<Sprite> cards;
    public int card_num = -1;
    public Vector2[] card_position = new Vector2[5];
    public bool[] pressed = new bool[5];
    
    public bool[] activated = new bool[5];
    public int[] timer = new int[5];
    public int[] cooldown = new int[]{100, 2500, 2500, 2500, 2500};

    private static List<Texture2D> textures = ContentLoader.LoadTexturesGame();


    public List<Vector2> cardPosition = new List<Vector2>() {
        new Vector2(600, Game1.ScreenHeight - 252),
        new Vector2(738, Game1.ScreenHeight - 252),
        new Vector2(876, Game1.ScreenHeight - 252),
        new Vector2(1014, Game1.ScreenHeight - 252),
        new Vector2(1154, Game1.ScreenHeight - 252),
    };
    
    
    public Cards() : base(textures[10], new Rectangle(0,0, 10, 10)) {
        cardsTexture = ContentLoader.LoadCardTextures();
        Position = new Vector2(10000, 10000);
        

    }
    
    
    public void Update(GameTime gameTime) {
        MovementControls();
        UnlockCard();
        UpdateCards(gameTime);
    }

    public void UpdateCards(GameTime gameTime) {
        for (int i = 0; i < 5; i++) {
            if (activated[i]) {
                timer[i] += gameTime.ElapsedGameTime.Milliseconds;

                if (timer[i] >= cooldown[i]) {
                    timer[i] = 0;
                    activated[i] = false;
                }
                cardsHand[i].transperent = 0.5f;
            }
            else {
                if (cardsHand.Count > i) {
                    cardsHand[i].transperent = 1;
                }
            }
        }
    }

    public void UnlockCard() {
        if(cardsInHand > 4){ return;}

        if (cardsNeededToUnlock[cardsInHand] <= cardsCollected) {
            cardsInHand++;
            AddCardsHand();
        }
    }

    public void cardsPickedUp() {
        Sound.EquipCard();
        cardsCollected++;
    }

    public void usedCard() {
        if (card_num != -1) {
            activated[card_num] = true;
            timer[card_num] = 0;
        }
    }
    
    
    public void AddCardsHand() {
        if (cardsHand.Count >= cardPosition.Count) {
            return;
        }
        cardsHand.Add(new Sprite(cardsTexture[cardsInHand - 1], new Rectangle(0, 0, 131, 186))
            {Position = cardPosition[cardsHand.Count], collision = false, Rotation = true});
    }

    // public Keys[] a = new Keys[] { Keys.A, Keys.B };
    private void MovementControls() {

        if (Keyboard.GetState().IsKeyDown(Game1.parameters.PlayingKeysArray[5]) && !pressed[0] && cardsHand.Count > 0) {
            Check_Card(0);
            pressed[0] = true;
        }
        if (Keyboard.GetState().IsKeyDown(Game1.parameters.PlayingKeysArray[6]) && !pressed[1] && cardsHand.Count > 1) {
            Check_Card(1);
            pressed[1] = true;
        }
        if (Keyboard.GetState().IsKeyDown(Game1.parameters.PlayingKeysArray[7]) && !pressed[2] && cardsHand.Count > 2) {
            Check_Card(2);
            pressed[2] = true;
        }
        if (Keyboard.GetState().IsKeyDown(Game1.parameters.PlayingKeysArray[8]) && !pressed[3] && cardsHand.Count > 3) {
            Check_Card(3);
            pressed[3] = true;
        }
        if (Keyboard.GetState().IsKeyDown(Game1.parameters.PlayingKeysArray[9]) && !pressed[4] && cardsHand.Count > 4) {
            Check_Card(4);
            pressed[4] = true;
        }
        
        
        if (Keyboard.GetState().IsKeyUp(Game1.parameters.PlayingKeysArray[5])) {
            pressed[0] = false;
        }
        if (Keyboard.GetState().IsKeyUp(Game1.parameters.PlayingKeysArray[6])) {
            pressed[1] = false;
        }
        if (Keyboard.GetState().IsKeyUp(Game1.parameters.PlayingKeysArray[7])) {
            pressed[2] = false;
        }
        if (Keyboard.GetState().IsKeyUp(Game1.parameters.PlayingKeysArray[8])) {
            pressed[3] = false;
        }
        if (Keyboard.GetState().IsKeyUp(Game1.parameters.PlayingKeysArray[9])) {
            pressed[4] = false;
        }


    }

    public void Check_Card(int num) {
        if (card_num != -1) {
            Sound.UnEquipCard();
            cardsHand[card_num].Position = new Vector2( cardsHand[card_num].Position.X,  cardsHand[card_num].Position.Y + 90);
        }
        
        
        if (card_num == num) {
            card_num = -1;
        }
        else {
            Sound.UseCard();
            card_num = num;
            cardsHand[card_num].Position = new Vector2( cardsHand[card_num].Position.X,  cardsHand[card_num].Position.Y - 90);
        }
    }

    public void RestartCards() {
        cardsCollected = 0;

        for (int i = cardsHand.Count - 1; i >= 0; i--) {
            cardsHand.RemoveAt(i);
        }
        
        for (int i = 0; i < 5; i++) {
            pressed[i] = false;
        }

        cardsInHand = 0;
        card_num = -1;
    }

}