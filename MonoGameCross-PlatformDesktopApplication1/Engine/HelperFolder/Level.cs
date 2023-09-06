using System;
using Microsoft.Xna.Framework;

namespace MonoGameCross_PlatformDesktopApplication1; 

public static class Level {
    //Game - Field /  Game - Arena (mix)
    public static int levelNum = 0;
    
    // Game - Field
    public static int[] SecondsPerRound = new int[]{50, 50, 50, 50, 50, 50, 50, 50, 50, 50};
    public static float[] VelocityPerRound = new float[] { 2f, 2.5f, 3f, 3f, 3f, 3f};
    
    // Game - Arena 
    
    public static int[] numberOfRounds = new int[]{3,3,3,3,3,3,3,3,3,3};
    public static int counterRounds = 0;

    public static bool gameEnd = false;
    public static int waitEnd = 0;
    public static bool[] restart = new bool[]{false, false};

    static Level() {}

    public static void GamePlayArenaStats(Player player, AIPlayer aiPlayer, GameTime gameTime) {
        
        if (player.health <= 0 && waitEnd == 0) {
            Effects.TextCentarEffect("Defeat");
            gameEnd = true;
        }
        else if (aiPlayer.health <= 0 && waitEnd == 0) {
            Effects.TextCentarEffect("Victory");
            gameEnd = true;
        }

        if (gameEnd) {
            waitEnd += gameTime.ElapsedGameTime.Milliseconds;
        }
        
        
        if (waitEnd >= 3000) {
            waitEnd = 0;
            restart[0] = true;
            restart[1] = true;
            player.health = 20;
            aiPlayer.health = 10;
            
            gameEnd = false; 
            Game1.ChangeState(GameState.GameMenu);
        }
    }

    public static void GamePlayFieldStats(Player player, GameTime gameTime) {
        if (player.health <= 0 && waitEnd == 0) {
            Effects.TextCentarEffect("Defeat");
            gameEnd = true;
        }

        if (gameEnd) {
            waitEnd += gameTime.ElapsedGameTime.Milliseconds;
        }
        
        if (waitEnd >= 3000) {
            waitEnd = 0;
            restart[0] = true;
            restart[1] = true;
            player.health = 20;
            
            gameEnd = false; 
            Game1.ChangeState(GameState.GameMenu);
        }

    }

    public static void GameArenaStats(Player player, AIPlayer aiPlayer) {
        counterRounds++;
        CheckDamage(player, aiPlayer);
        if (counterRounds >= numberOfRounds[levelNum]) {
            levelNum++;
            counterRounds = 0;
            
            if (levelNum == 10) {
                Game1.ChangeState(GameState.GameEnd);
            }
            else {
                Game1.ChangeState(GameState.GamePlay);
            }
        }
    }

    public static void CheckDamage(Player player, AIPlayer aiPlayer) {
        if (player.shootPos == aiPlayer.currentArenaPos) {
            aiPlayer.health--;
            CheckForEndOfGame(aiPlayer.health, aiPlayer.isUser);
        }
        if (aiPlayer.shootPos == player.currentArenaPos) {
            player.health--;
            CheckForEndOfGame(player.health, player.isUser);
        }
    }

    public static void CheckForEndOfGame(int health, bool isUser) {
        if (health > 0) {
            return;
        }

        if (isUser) {
            Console.WriteLine("Nikola, You Lost !!!");
        }else {
            Console.WriteLine("AI player, You Lost !!!");
        }
    }
    
    
}