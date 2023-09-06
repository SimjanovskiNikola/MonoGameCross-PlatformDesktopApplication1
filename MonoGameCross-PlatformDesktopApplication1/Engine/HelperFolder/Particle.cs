using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;
using MonoGameCross_PlatformDesktopApplication1;

public class Particle {
    public ParticleEffect _particleEffect;
    public Texture2D _particleTexture;
    public TextureRegion2D textureRegion;
    public bool activated = true;
    public double timer = 0f; 
    public Vector2 axis = new Vector2(0 ,0);
    public int finalTimer;

    public Particle(Vector2 pos, int Temptimer) {
        axis = pos;
        _particleTexture = ContentLoader.LoadTexturesGame()[9];
        _particleTexture.SetData(new[] { Color.White });
        textureRegion = new TextureRegion2D(_particleTexture);
        CreateEarth(pos);
        finalTimer = Temptimer;
    }

    public void ParticleUpdate(GameTime gameTime) {
        
        if (activated) {
            _particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;
            //finalTimer
            if (timer > finalTimer) {
                timer = 0f;
                activated = false;
                _particleEffect.Dispose();
            }
        }
    }

    public void CreateEarth(Vector2 pos) {

        _particleEffect = new ParticleEffect(autoTrigger: false)
        {
            Position = pos,
            Emitters = new List<ParticleEmitter> 
            {
                new ParticleEmitter(textureRegion, 200, TimeSpan.FromSeconds(0.8),
                    Profile.BoxFill(100, 100))
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(5f, 8f),
                        Quantity = 5,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(2.0f, 5.0f),
                        Color = Color.Gray.ToHsl()
                    },
                    Modifiers = {
                        new AgeModifier {
                            Interpolators = {
                                new ColorInterpolator {
                                    // StartValue = new HslColor(0.33f, 0.5f, 0.5f),
                                    // EndValue = new HslColor(0.5f, 0.9f, 1.0f)
                                    StartValue = Color.Chocolate.ToHsl(),
                                    EndValue = Color.Black.ToHsl()
                                }
                            }
                        },
                        new RotationModifier {RotationRate = -2.1f},
                        new RectangleContainerModifier {Width = 100, Height = 100},
                        // new CircleContainerModifier{Radius = 10},
                        // new LinearGravityModifier {Direction = -Vector2.UnitY, Strength = 20f},
                    }
                }
            }
        };
    }
    
    public void CreateFreeze(Vector2 pos) {

        _particleEffect = new ParticleEffect(autoTrigger: false)
        {
            Position = pos,
            Emitters = new List<ParticleEmitter> 
            {
                new ParticleEmitter(textureRegion, 1000, TimeSpan.FromSeconds(1),
                    Profile.Circle(100, Profile.CircleRadiation.Out))
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(100f, 300f),
                        Quantity = 20,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(3.0f, 4.0f)
                    },
                    Modifiers = {
                        // new AgeModifier {
                        //     Interpolators = {
                        //         new ColorInterpolator {
                        //             // StartValue = new HslColor(0.33f, 0.5f, 0.5f),
                        //             // EndValue = new HslColor(0.5f, 0.9f, 1.0f)
                        //             StartValue = Color.White.ToHsl(),
                        //             EndValue = Color.DarkBlue.ToHsl()
                        //         }
                        //     }
                        // },
                        new VelocityModifier()
                        {
                            Interpolators =
                            {
                                new ColorInterpolator
                                {
                                    StartValue = Color.WhiteSmoke.ToHsl(),
                                    EndValue = Color.Black.ToHsl()
                                }
                            },
                            VelocityThreshold = 100f
                        },
                        new RotationModifier {RotationRate = -2.1f},
                        new RectangleContainerModifier {Width = 150, Height = 150},
                        // new CircleContainerModifier{Radius = 100},
                        new LinearGravityModifier {Direction = -Vector2.UnitY, Strength = 20f},
                    }
                }
            }
        };
    }
    public void CreateFire(Vector2 pos) {

        _particleEffect = new ParticleEffect(autoTrigger: false)
        {
            Position = pos,
            Emitters = new List<ParticleEmitter> 
            {
                new ParticleEmitter(textureRegion, 1000, TimeSpan.FromSeconds(1),
                    Profile.Box(100, 100))
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(20f, 30f),
                        Quantity = 15,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(3.0f, 8.0f),
                        Mass = 1f
                    },
                    Modifiers = {
                        new AgeModifier {
                            Interpolators = {
                                new ColorInterpolator {
                                    // StartValue = new HslColor(0.33f, 0.5f, 0.5f),
                                    // EndValue = new HslColor(0.5f, 0.9f, 1.0f)
                                    StartValue = Color.Red.ToHsl(),
                                    EndValue = Color.Yellow.ToHsl()
                                }
                            }
                        },
                        new RotationModifier {RotationRate = -2.1f},
                        new RectangleContainerModifier {Width = 100, Height = 100},
                        // new CircleContainerModifier{Radius = 100},
                        new LinearGravityModifier {Direction = -Vector2.UnitY, Strength = 20f},
                        new VortexModifier()
                        {
                            Mass = 4f,
                            MaxSpeed = 20f,
                            Position = pos
                        }
                    }
                }
            }
        };
    }
    public void CreateWind(Vector2 pos) {

        _particleEffect = new ParticleEffect(autoTrigger: false)
        {
            Position = pos,
            Emitters = new List<ParticleEmitter>
            {
                new ParticleEmitter(textureRegion, 250, TimeSpan.FromSeconds(0.5),
                    Profile.Point())
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(25f, 50f),
                        Quantity = 5,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(2f, 3f),
                        Mass = 10f
                    },
                    Modifiers =
                    {
                        new AgeModifier
                        {
                            Interpolators =
                            {
                                new ColorInterpolator
                                {
                                    StartValue = Color.White.ToHsl(),
                                    EndValue = Color.WhiteSmoke.ToHsl()
                                }
                            }
                        },
                        new RotationModifier {RotationRate = -2.1f},
                        new LinearGravityModifier {Direction = -Vector2.UnitY, Strength = 30f},
                        new LinearGravityModifier {Direction = -Vector2.UnitX, Strength = 30f},
                        new VortexModifier()
                        {
                        Mass = 40f,
                        MaxSpeed = 80f,
                        Position = new Vector2(0,40)
                        },
                        new VortexModifier()
                        {
                            Mass = 40f,
                            MaxSpeed = 80f,
                            Position = new Vector2(40,0)
                        }
                    }
                }, // Dolu desno
                new ParticleEmitter(textureRegion, 250, TimeSpan.FromSeconds(0.5),
                    Profile.Point())
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(25f, 50f),
                        Quantity = 5,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(1f, 3f),
                        Mass = 10f
                    },
                    Modifiers =
                    {
                        new AgeModifier
                        {
                            Interpolators =
                            {
                                new ColorInterpolator
                                {
                                    StartValue = Color.White.ToHsl(),
                                    EndValue = Color.WhiteSmoke.ToHsl()
                                }
                            }
                        },
                        new RotationModifier {RotationRate = -2.1f},
                        // new RectangleContainerModifier {Width = 100, Height = 100},
                        new LinearGravityModifier {Direction = Vector2.UnitY, Strength = 30f},
                        new LinearGravityModifier {Direction = -Vector2.UnitX, Strength = 30f},
                        new VortexModifier()
                        {
                        Mass = 40f,
                        MaxSpeed = 200f,
                        Position = new Vector2(-40, 0)
                        },
                        new VortexModifier()
                        {
                            Mass = 40f,
                            MaxSpeed = 200f,
                            Position = new Vector2(0, -40)
                        }
                    }
                }, // Gore levo
                new ParticleEmitter(textureRegion, 250, TimeSpan.FromSeconds(0.5),
                    Profile.Point())
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(25f, 50f),
                        Quantity = 5,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(1f, 3f),
                        Mass = 10f
                    },
                    Modifiers =
                    {
                        new AgeModifier
                        {
                            Interpolators =
                            {
                                new ColorInterpolator
                                {
                                    StartValue = Color.White.ToHsl(),
                                    EndValue = Color.WhiteSmoke.ToHsl()
                                }
                            }
                        },
                        new RotationModifier {RotationRate = -2.1f},
                        // new RectangleContainerModifier {Width = 100, Height = 100},
                        new LinearGravityModifier {Direction = Vector2.UnitY, Strength = 30f},
                        new LinearGravityModifier {Direction = -Vector2.UnitX, Strength = 30f},
                        new VortexModifier()
                        {
                            Mass = 40f,
                            MaxSpeed = 200f,
                            Position = new Vector2(40, 0)
                        },
                        new VortexModifier()
                        {
                            Mass = 40f,
                            MaxSpeed = 200f,
                            Position = new Vector2(0, -40)
                        }
                    }
                }, // Gore desno
                new ParticleEmitter(textureRegion, 250, TimeSpan.FromSeconds(0.5),
                    Profile.Point())
                {
                    Parameters = new ParticleReleaseParameters
                    {
                        Speed = new Range<float>(25f, 50f),
                        Quantity = 5,
                        Rotation = new Range<float>(-1f, 1f),
                        Scale = new Range<float>(1f, 3f),
                        Mass = 10f
                    },
                    Modifiers =
                    {
                        new AgeModifier
                        {
                            Interpolators =
                            {
                                new ColorInterpolator
                                {
                                    StartValue = Color.White.ToHsl(),
                                    EndValue = Color.WhiteSmoke.ToHsl()
                                }
                            }
                        },
                        new RotationModifier {RotationRate = -2.1f},
                        // new RectangleContainerModifier {Width = 100, Height = 100},
                        new LinearGravityModifier {Direction = Vector2.UnitY, Strength = 30f},
                        new LinearGravityModifier {Direction = -Vector2.UnitX, Strength = 30f},
                        new VortexModifier()
                        {
                            Mass = 40f,
                            MaxSpeed = 200f,
                            Position = new Vector2(-40, 0)
                        },
                        new VortexModifier()
                        {
                            Mass = 40f,
                            MaxSpeed = 200f,
                            Position = new Vector2(0, 40)
                        } // Doly levo
                    }
                }
            }
        };
    }
}

