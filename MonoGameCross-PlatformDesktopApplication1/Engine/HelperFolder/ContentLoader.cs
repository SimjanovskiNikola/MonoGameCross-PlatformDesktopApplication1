using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace MonoGameCross_PlatformDesktopApplication1; 

public static class ContentLoader {
    private static GraphicsDevice _graphicsDevice;
    private static ContentManager _content;
    public static List<List<Texture2D>> monsters = new List<List<Texture2D>>();

    public static void AddNecessary(GraphicsDevice graphicsDevice, ContentManager Content) {
        _content = Content;
        _graphicsDevice = graphicsDevice;
        
        addMonsterTexture();
    }

    public static List<Texture2D> LoadTexturesGame() {
        Texture2D reta = new Texture2D(_graphicsDevice, 1, 1);
        reta.SetData(new[] { Color.Red * 0.4f}); // id = 9

        List<Texture2D> textures = new List<Texture2D>();
        textures.Add(_content.Load<Texture2D>("Images/SceneDesign/TX Plant")); // id = 0
        textures.Add(_content.Load<Texture2D>("Images/SceneDesign/TX Player")); // id = 1
        textures.Add(_content.Load<Texture2D>("Images/SceneDesign/TX Props")); // id = 2
        textures.Add(_content.Load<Texture2D>("Images/SceneDesign/TX Shadow")); // id = 3
        textures.Add(_content.Load<Texture2D>("Images/SceneDesign/TX Shadow Plant")); // id = 4
        textures.Add(_content.Load<Texture2D>("Images/SceneDesign/TX Struct")); // id = 5
        textures.Add(_content.Load<Texture2D>("Images/SceneDesign/TX Tileset Grass")); // id = 6
        textures.Add(_content.Load<Texture2D>("Images/SceneDesign/TX Tileset Stone Ground")); // id = 7
        textures.Add(_content.Load<Texture2D>("Images/SceneDesign/TX Tileset Wall")); // id = 8
        textures.Add(reta); // id = 9
        textures.Add(_content.Load<Texture2D>("Cards/cardsOnMap")); // id = 10
        textures.Add(_content.Load<Texture2D>("Cards/basic")); // id = 11

        return textures;
    }

    public static List<Song> LoadSongGame() {
        List<Song> songs = new List<Song>();
        songs.Add(_content.Load<Song>("Songs/Number One (Vocal Version)"));
        return songs;
    }

    public static SpriteFont LoadSpriteFont() {
        SpriteFont spriteFont = _content.Load<SpriteFont>("Font/Font");
        return spriteFont;

    }
    public static SpriteFont LoadEffectSpriteFont() {
        SpriteFont spriteFont = _content.Load<SpriteFont>("Font/EffectFont");
        return spriteFont;
    }

    public static List<Texture2D> LoadHealthBar() {
        List<Texture2D> tex = new List<Texture2D>();
        tex.Add(_content.Load<Texture2D>("HealthBar/front"));
        tex.Add(_content.Load<Texture2D>("HealthBar/back"));
        return tex;
    }

    public static Texture2D LoadDamage() {
        Texture2D texture2D = _content.Load<Texture2D>("Effects/ScreenDamage");
        return texture2D;
    }

    public static Texture2D LoadPointer() {
        Texture2D texture2D = _content.Load<Texture2D>("Button/pointer_60x60");
        return texture2D;
    }

    public static List<SoundEffect> UISoundEffects() {
        List<SoundEffect> soundEffects = new List<SoundEffect>();
        soundEffects.Add(_content.Load<SoundEffect>("SoundEffects/UI/001_Hover_01"));
        soundEffects.Add(_content.Load<SoundEffect>("SoundEffects/UI/013_Confirm_03"));
        soundEffects.Add(_content.Load<SoundEffect>("SoundEffects/UI/029_Decline_09"));
        soundEffects.Add(_content.Load<SoundEffect>("SoundEffects/UI/070_Equip_10"));
        soundEffects.Add(_content.Load<SoundEffect>("SoundEffects/UI/071_Unequip_01"));
        soundEffects.Add(_content.Load<SoundEffect>("SoundEffects/UI/051_use_item_01"));
        return soundEffects;
    }
    
    public static List<SoundEffectInstance> AttackSoundEffects() {
        List<SoundEffectInstance> soundEffects = new List<SoundEffectInstance>();
        soundEffects.Add(_content.Load<SoundEffect>("SoundEffects/UI/001_Hover_01").CreateInstance());
        soundEffects.Add(_content.Load<SoundEffect>("SoundEffects/UI/013_Confirm_03").CreateInstance());
        soundEffects.Add(_content.Load<SoundEffect>("SoundEffects/UI/029_Decline_09").CreateInstance());
        return soundEffects;
    }
    
    public static List<SoundEffectInstance> MovementSoundEffects() {
        List<SoundEffectInstance> soundEffects = new List<SoundEffectInstance>();
        soundEffects.Add(_content.Load<SoundEffect>("SoundEffects/Movement/03_Step_grass_03").CreateInstance());
        return soundEffects;
    }

    public static Dictionary<string, Animation> LoadMainWizardAnimation() {
        Dictionary<string, Animation> dict = new Dictionary<string, Animation>() {
            { "Idle", new Animation(_content.Load<Texture2D>("Wizzard/Idle"), 6) },
            { "Run", new Animation(_content.Load<Texture2D>("Wizzard/Run"), 8) }
        };

        return dict;
    }
    
    public static Dictionary<string, Animation> LoadEvilWizardAnimation() {
        Dictionary<string, Animation> dict = new Dictionary<string, Animation>() {
            { "Idle", new Animation(_content.Load<Texture2D>("Characters/EvilWiz2/Idle"), 8) },
            { "Run", new Animation(_content.Load<Texture2D>("Characters/EvilWiz2/Run"), 8) }
        };

        return dict;
    }
    
    
    public static List<Texture2D> LoadSkeletonList() {
        List<Texture2D> temp = new List<Texture2D>();
        temp.Add(_content.Load<Texture2D>("Characters/Skeleton/Idle"));
        temp.Add(_content.Load<Texture2D>("Characters/Skeleton/Run"));
        temp.Add(_content.Load<Texture2D>("Characters/Skeleton/Attack"));

        return temp;
    }
    public static Dictionary<string, Animation> LoadSkeletonAnimation(List<Texture2D> tex) {
        Dictionary<string, Animation> dict = new Dictionary<string, Animation>() {
            { "Idle", new Animation(tex[0], 4) },
            { "Run", new Animation(tex[1], 4) },
            { "Attack", new Animation(tex[2], 8) }
        };

        return dict;
    }
    
    public static List<Texture2D> LoadWormList() {
        List<Texture2D> temp = new List<Texture2D>();
        temp.Add(_content.Load<Texture2D>("Characters/Worm/Idle"));
        temp.Add(_content.Load<Texture2D>("Characters/Worm/Walk"));
        temp.Add(_content.Load<Texture2D>("Characters/Worm/Attack"));

        return temp;
    }
    
    public static Dictionary<string, Animation> LoadWormAnimation(List<Texture2D> tex) {
        Dictionary<string, Animation> dict = new Dictionary<string, Animation>() {
            { "Idle", new Animation(tex[0], 9) },
            { "Run", new Animation(tex[1], 9) },
            { "Attack", new Animation(tex[2], 16) }
        };

        return dict;
    }
    
    
    public static List<Texture2D> LoadMushroomList() {
        List<Texture2D> temp = new List<Texture2D>();
        temp.Add(_content.Load<Texture2D>("Characters/Mushroom/Idle"));
        temp.Add(_content.Load<Texture2D>("Characters/Mushroom/Run"));
        temp.Add(_content.Load<Texture2D>("Characters/Mushroom/Attack"));

        return temp;
    }
    public static Dictionary<string, Animation> LoadMushroomAnimation(List<Texture2D> tex) {
        Dictionary<string, Animation> dict = new Dictionary<string, Animation>() {
            { "Idle", new Animation(tex[0], 4) },
            { "Run", new Animation(tex[1], 8) },
            { "Attack", new Animation(tex[2], 8) }
        };

        return dict;
    }
    
    public static List<Texture2D> LoadGoblinList() {
        List<Texture2D> temp = new List<Texture2D>();
        temp.Add(_content.Load<Texture2D>("Characters/Goblin/Idle"));
        temp.Add(_content.Load<Texture2D>("Characters/Goblin/Run"));
        temp.Add(_content.Load<Texture2D>("Characters/Goblin/Attack"));

        return temp;
    }
    public static Dictionary<string, Animation> LoadGoblinAnimation(List<Texture2D> tex) {
        Dictionary<string, Animation> dict = new Dictionary<string, Animation>() {
            { "Idle", new Animation(tex[0], 4) },
            { "Run", new Animation(tex[1], 8) },
            { "Attack", new Animation(tex[2], 8) }
        };

        return dict;
    }
    
    public static List<Texture2D> LoadFlying_EyeList() {
        List<Texture2D> temp = new List<Texture2D>();
        temp.Add(_content.Load<Texture2D>("Characters/Flying_Eye/Flight")); 
        temp.Add(_content.Load<Texture2D>("Characters/Flying_Eye/Flight"));
        temp.Add(_content.Load<Texture2D>("Characters/Flying_Eye/Attack"));

        return temp;
    }
    
    public static Dictionary<string, Animation> LoadFlying_EyeAnimation(List<Texture2D> tex) {
        Dictionary<string, Animation> dict = new Dictionary<string, Animation>() {
            { "Idle", new Animation(tex[0], 8) },
            { "Run", new Animation(tex[1], 8) },
            { "Attack", new Animation(tex[2], 8) }
        };

        return dict;
    }

    public static Dictionary<string, Animation> LoadMonster(int num) {
        switch (num) {
            case 0:
                return LoadWormAnimation(monsters[0]);
                break;
            case 1:
                return LoadSkeletonAnimation(monsters[1]);
                break;
            case 2:
                return LoadMushroomAnimation(monsters[2]);
                break;
            case 3:
                return LoadGoblinAnimation(monsters[3]);
                break;
            case 4:
                return LoadFlying_EyeAnimation(monsters[4]);
                break;
        }

        return null;
    }

    public static void addMonsterTexture() {
        monsters.Add(LoadWormList());
        monsters.Add(LoadSkeletonList());
        monsters.Add(LoadMushroomList());
        monsters.Add(LoadGoblinList());
        monsters.Add(LoadFlying_EyeList());
    }

    public static List<Texture2D> LoadCardTextures() {
        List<Texture2D> tex = new List<Texture2D>();
        tex.Add(_content.Load<Texture2D>("newCards/LIGTENING"));
        tex.Add(_content.Load<Texture2D>("newCards/FREEZE"));
        tex.Add(_content.Load<Texture2D>("newCards/FIRE"));
        tex.Add(_content.Load<Texture2D>("newCards/MAGIC_EARTH"));
        tex.Add(_content.Load<Texture2D>("newCards/WIND"));
        return tex;
    }
    


}