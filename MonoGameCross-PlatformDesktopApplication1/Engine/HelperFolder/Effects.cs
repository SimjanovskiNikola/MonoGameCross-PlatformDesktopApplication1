using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameCross_PlatformDesktopApplication1; 

public static class Effects {
    public static SpriteFont SpriteFont;
    private static int numberOfEffects = 2;
    public static Sprite[] effects = new Sprite[numberOfEffects];
    public static Stopwatch[] stopwatches = new Stopwatch[numberOfEffects];
    public static bool[] activateEffect = new bool[numberOfEffects];
    public static ContentManager Content; 

    public static void EffectsLoad(ContentManager content) {
        Content = content;
        SpriteFont = ContentLoader.LoadEffectSpriteFont();
        for (int i = 0; i < numberOfEffects; i++) {
            stopwatches[i] = new Stopwatch();
            activateEffect[i] = false;
        }        
    }

    public static void UpdateEffects() {
        for (int i = 0; i < numberOfEffects; i++) {
            if (activateEffect[i]) {
                callFunction(i);
            }
        }
    }
    
    //0. Damage Taken Effect
    public static void BlackTransitionEffect() {
        if (!activateEffect[0]) {
            
            StartOfEffect(0);
            
            Texture2D texture2D = ContentLoader.LoadDamage();


            effects[0] = new Sprite(texture2D, new Rectangle(0,0,5000, 5000), 
                new Vector2(-2500,-2500)){color = Color.Black};
            return;
        }
        
        double transperent = (3000 - stopwatches[0].Elapsed.TotalMilliseconds) / 3000;
        if (transperent <= 0.1) {
            EndOfEffect(0);
            return;
        }
        effects[0].transperent =(float) transperent;
    }

    public static void TextCentarEffect(string str = "") {
        if (!activateEffect[1]) {
            StartOfEffect(1);
            effects[1] = new Sprite(SpriteFont, str, 
                new Vector2(Game1.ScreenWidth/2f, Game1.ScreenHeight/2f), 1.0f){color = Color.Red};
            return;
        }

        double transperent = (3000 - stopwatches[1].Elapsed.TotalMilliseconds) / 3000;
        if (transperent <= 0.2) {
            EndOfEffect(1);
            return;
        }
        effects[1].transperent =(float) transperent;
    }
    public static void callFunction(int num) {
        switch (num) {
            case 0:
                BlackTransitionEffect();
                break;
            case 1:
                TextCentarEffect();
                break;

        }
    }
    
    public static void StartOfEffect(int num) {
        activateEffect[num] = true;
        stopwatches[num].Start();
        stopwatches[num].Restart();
    }

    public static void EndOfEffect(int num) {
        effects[num] = null;
        activateEffect[num] = false;
        stopwatches[num].Stop();
    }

}