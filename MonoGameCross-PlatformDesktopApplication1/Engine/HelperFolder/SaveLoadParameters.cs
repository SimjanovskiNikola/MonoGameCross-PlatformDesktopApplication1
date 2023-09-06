using Microsoft.Xna.Framework.Input;

namespace MonoGameCross_PlatformDesktopApplication1; 

public class SaveLoadParameters {
    protected Keys[] _playingKeysArray = new Keys[10];
    public int loadGameState { set; get; } // 0 na pocetok , 1 vtor fight, 2 final fight
    public int musicVolume { set; get; }
    public int soundVolume { set; get; }

    public Keys[] PlayingKeysArray {
        get { return _playingKeysArray; }
        set {
            _playingKeysArray = value;
        }
    }
    // public Keys moveForward { set; get; }
    // public Keys moveBackward { set; get; }
    // public Keys moveLeft { set; get; }
    // public Keys moveRight { set; get; }
    // public Keys pickUpItems { set; get; }

    public SaveLoadParameters() {
        loadGameState = 0;
        musicVolume = 50;
        soundVolume = 50;
        PlayingKeysArray[0] = Keys.W;
        PlayingKeysArray[1] = Keys.S;
        PlayingKeysArray[2] = Keys.A;
        PlayingKeysArray[3] = Keys.D;
        PlayingKeysArray[4] = Keys.E;
        PlayingKeysArray[5] = Keys.D1;
        PlayingKeysArray[6] = Keys.D2;
        PlayingKeysArray[7] = Keys.D3;
        PlayingKeysArray[8] = Keys.D4;
        PlayingKeysArray[9] = Keys.D5;
    }
}