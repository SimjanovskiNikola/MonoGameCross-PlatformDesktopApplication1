using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Input;

// using 

namespace MonoGameCross_PlatformDesktopApplication1; 

public static class SaveLoadGame {
    public const string PATH = "settings.json";
    // public 
    // public static int loadGameState { set; get; } // 0 na pocetok , 1 vtor fight, 2 final fight
    // public static int musicVolume { set; get; }
    // public static int soundVolume { set; get; }
    // public static Keys moveForward { set; get; }
    // public static Keys moveBackward { set; get; }
    // public static Keys moveLeft { set; get; }
    // public static Keys moveRight { set; get; }
    // public static Keys pickUpItems { set; get; }

    public static void SaveGame(SaveLoadParameters parameters) {
        if (File.Exists(PATH)) {
            return;
        }
        string serializedText = JsonSerializer.Serialize<SaveLoadParameters>(parameters);
        Trace.WriteLine(serializedText);
        File.WriteAllText(PATH, serializedText);
    }

    public static SaveLoadParameters LoadGame() {
        var fileContents = File.ReadAllText(PATH);
        var deserializeData = JsonSerializer.Deserialize<SaveLoadParameters>(fileContents);
        return deserializeData;
    }

    public static void SaveChanges(SaveLoadParameters parameters) {
        string serializedText = JsonSerializer.Serialize<SaveLoadParameters>(parameters);
        Trace.WriteLine(serializedText);
        File.WriteAllText(PATH, serializedText);
    }
    
    static SaveLoadGame() {
    }

    public static Dictionary<string, string> GameSaver = new Dictionary<string, string>();

    // public Save
}

