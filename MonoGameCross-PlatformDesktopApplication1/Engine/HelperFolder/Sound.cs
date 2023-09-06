using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace MonoGameCross_PlatformDesktopApplication1; 

public static class Sound {
    
    public static List<SoundEffect> soundEffectsAttack; // ContentLoader.SoundEffects();
    public static List<SoundEffectInstance> soundEffectsMovement; // ContentLoader.SoundEffects();
    public static List<SoundEffect> soundEffectsUI; // ContentLoader.SoundEffects();

    public static void LoadSounds() {
        soundEffectsUI = ContentLoader.UISoundEffects();
        soundEffectsMovement = ContentLoader.MovementSoundEffects();
    }

    public static void EquipCard() {
        soundEffectsUI[3].Play();
    }
    
    public static void UnEquipCard() {
        soundEffectsUI[4].Play();
    }
    
    public static void UseCard() {
        soundEffectsUI[5].Play();
    }
    
    public static void UIHover() {
        ChangeVolume(soundEffectsUI[0]);
    }
    
    public static void UIOk() {
        ChangeVolume(soundEffectsUI[1]);
    }
    
    public static void UICancel() {
        ChangeVolume(soundEffectsUI[2]);
    }
    

    public static void WalkEffect() {
        soundEffectsMovement[0].Play(); //Place own number
        
    }

    public static void ChangeVolume(SoundEffect soundEffect) {
        soundEffect.Play(((float)Game1.parameters.soundVolume / 100), 0, 0);
    }
    public static void ChangeVolume(SoundEffectInstance soundEffect) {
        soundEffect.Volume = (((float)Game1.parameters.soundVolume / 100));
    }
    //
    // public static void ShootEffect() {
    //     soundEffects[0].Play(); //Place own number
    // }
    //
    // public static void AnimalRoarEffect() {
    //     soundEffects[0].Play(); //Place own number
    // }
    
    
}