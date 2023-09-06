using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameCross_PlatformDesktopApplication1;
using MonoGameCross_PlatformDesktopApplication1.Engine.Player_AI_Cards.AllMagic;

public class Scene {
    
    // public Cards cards;
    public Player Aplayer;
    public AIPlayer AiPlayer;
    public List<Sprite> _sceneComponents = new List<Sprite>();
    public List<Monsters> _monsters = new List<Monsters>();
    public List<Sprite> _cards = new List<Sprite>();
    public bool activateAdding = false;
    public List<Sprite> _editSceneComponents = new List<Sprite>();
    private List<Texture2D> _textures;
    public List<Dictionary<string, Animation>> monsterTexture;
    private int[ , ] tile = new int[4 , 2] { {0, 0}, {0, 128}, {128, 0}, {128, 128}};
    private int[] tileNumber = new int[] {3, 2, 1, 3, 0, 1, 1, 0, 2, 3, 2, 3, 3, 2, 0, 1, 3, 2, 0, 3, 3, 0, 0, 1, 2, 2, 0, 3, 3, 0, 3, 1, 3, 3, 1, 1, 0, 1, 3, 0, 3, 0, 3, 3, 3, 1, 3, 1, 0, 1, 0, 3, 0, 1, 2, 2, 3, 1, 3, 0, 0, 1, 1, 0, 3, 2, 3, 3, 2, 2, 2, 0, 1, 0, 2, 3, 3, 1, 3, 0, 0, 1, 3, 0, 1, 1, 0, 2, 1, 1, 3, 0, 0, 3, 1, 2, 2, 1, 0, 1, 0, 0, 0, 2, 3, 1, 0, 2, 0, 2, 3, 0, 2, 1, 1, 0, 2, 1, 1, 3, 3, 1, 1, 3, 3, 0, 2, 1, 2, 1, 1, 3, 3, 1, 0, 1, 0, 3, 2, 0, 2, 0, 2, 0, 0, 0, 2, 3, 0, 2, 2, 0, 3, 0, 1, 3, 0, 1, 0, 3, 2, 2, 0, 1, 1, 2, 2, 2, 3, 3, 2, 3, 2, 3, 2, 3, 3, 0, 0, 0, 2, 2, 0, 2, 3, 2, 3, 3, 1, 2, 1, 3, 3, 3, 0, 0, 0, 1, 3, 0, 0, 3, 3, 0, 3, 0, 0, 3, 0, 2, 3, 3, 3, 3, 1, 2, 3, 1, 3, 1, 2, 3, 1, 3, 2, 3, 2, 1, 0, 1, 3, 2, 1, 2, 0, 3, 3, 0, 1, 3, 3, 3, 3, 1, 1, 3, 3, 2, 0, 2, 3, 2, 1, 2, 0, 0, 3, 0, 0, 2, 2, 2, 0, 1, 2, 0, 1, 2, 2, 1, 3, 1, 1, 1, 3, 0, 3, 2, 1, 1, 0, 2, 0, 2, 3, 2, 3, 2, 0, 0, 3, 3, 0, 1, 3, 1, 1, 3, 0, 3, 2, 2, 3, 2, 2, 0, 0, 0, 3, 2, 0, 0, 1, 0, 2, 0, 2, 2, 3, 3, 0, 2, 1, 0, 2, 2, 2, 0, 0, 1, 2, 1, 3, 3, 2, 3, 1, 3, 3, 1, 0, 3, 0, 3, 2, 0, 1, 1, 3, 1, 2, 3, 2, 1, 2, 3, 3, 2, 0, 1, 0, 2, 2, 0, 0, 2, 1, 3, 0, 1, 0, 2, 0, 2, 1, 2, 2, 2, 0, 2, 2, 0, 3, 0, 0, 3, 2, 1, 1, 1, 2, 3, 0, 1, 0, 2, 1, 0, 1, 2, 3, 3, 3, 3, 0, 2, 2, 2, 0, 0, 3, 2, 1, 0, 1, 2, 2, 2, 1, 0, 0, 1, 0, 0, 3, 2, 3, 3, 2, 3, 1, 2, 3, 3, 0, 0, 1, 2, 3, 2, 0, 1, 0, 2, 2, 3, 2, 0, 2, 1, 0, 0, 1, 1, 1, 1, 2, 0, 0, 3, 1, 3, 2, 2, 3, 2, 0, 0, 0, 0, 3, 1, 1, 1, 0, 0, 2, 1, 2, 0, 2, 1, 0, 1, 1, 0, 1, 3, 3, 0, 0, 0, 3, 3, 2, 2, 1, 2, 0, 3, 0, 2, 1, 2, 0, 1, 2, 2, 2, 3, 3, 0, 1, 1, 1, 3, 1, 3, 3, 1, 2, 0, 1, 0, 3, 2, 0, 0, 3, 0, 0, 1, 3, 3, 0, 1, 0, 0, 3, 1, 3, 3, 2, 0, 0, 0, 0, 2, 0, 3, 3, 2, 0, 1, 0, 2, 2, 0, 1, 0, 2, 2, 0, 0, 0, 2, 1, 3, 1, 2, 0, 2, 3, 1, 3, 2, 3, 1, 0, 1, 3, 1, 2, 1, 2, 0, 1, 3, 3, 2, 0, 3, 1, 1, 2, 1, 2, 3, 0, 3, 1, 1, 2, 2, 0, 0, 0, 3, 1, 3, 1, 2, 2, 2, 3, 0, 1, 1, 2, 1, 1, 1, 2, 0, 1, 0, 0, 1, 0, 2, 2, 0, 1, 2, 0, 3, 1, 1, 1, 3, 3, 2, 0, 2, 3, 2, 3, 2, 0, 3, 1, 2, 0, 2, 3, 1, 1, 0, 3, 2, 2, 1, 1, 2, 0, 2, 1, 3, 2, 2, 1, 3, 0, 1, 3, 2, 1, 3, 1, 2, 0, 0, 1, 1, 2, 2, 2, 2, 2, 0, 1, 3, 0, 0, 3, 1, 3, 2, 3, 3, 2, 3, 0, 1, 0, 1, 2, 3, 0, 0, 3, 3, 0, 3, 2, 1, 2, 2, 2, 0, 2, 1, 3, 2, 3, 3, 3, 0, 0, 0, 1, 2, 1, 0, 3, 3, 1, 3, 0, 3, 2, 2, 3, 0, 1, 1, 1, 0, 2, 0, 3, 3, 3, 3, 3, 1, 1, 0, 1, 0, 2, 3, 2, 3, 2, 1, 2, 2, 2, 1, 2, 1, 1, 3, 1, 2, 3, 0, 3, 0, 3, 1, 2, 3, 3, 2, 3, 3, 1, 1, 2, 3, 2, 3, 1, 3, 0, 0, 0, 0, 2, 1, 2, 3, 0, 0, 0, 1, 1, 0, 0, 2, 1, 3, 2, 2, 1, 0, 0, 3, 0, 1, 3, 0, 0, 0, 2, 2, 2, 0, 0, 3, 0, 3, 1, 2, 1, 1, 1, 0, 2, 2, 0, 3, 1, 3, 0, 1, 3, 0, 2, 1, 3, 2, 0, 1, 0, 2, 1, 2, 2, 1, 0, 1, 3, 0, 0, 2, 1, 2, 1, 3, 3, 1, 3, 2, 2, 0, 3, 1, 2, 2, 1, 2, 2, 2, 2, 3, 2, 1, 3, 0, 1, 1, 3, 2, 0, 2, 0, 0, 0, 0, 1, 3, 3, 0, 0, 3, 0, 1, 0, 0, 3, 0, 0, 2, 1, 2, 3, 1, 0, 3, 2, 2, 2, 3, 1, 0, 3, 0, 1, 0, 0, 0, 0, 2, 0, 0, 2, 1, 1, 0, 3, 3, 0, 1, 2, 0, 0, 2, 2, 1, 3, 2, 3, 2, 0, 0, 1, 0, 3, 1, 0, 2, 1, 3, 1, 0, 2, 2, 0, 1, 3, 3, 0, 3, 3, 2, 3, 3, 0, 3, 3, 1, 1, 3, 3, 2, 3, 3, 1, 1, 1, 3, 1, 2, 3, 2, 1, 3, 0, 3, 0, 0, 1, 0, 1, 2, 3, 2, 1, 2, 3, 3, 3, 3, 0, 2, 0, 3, 0, 2, 0, 0};
    private Vector2[] SpawnPos = new Vector2[] {
        new Vector2(900, 0),
        new Vector2(-900, 0),
        new Vector2(0, 900),
        new Vector2(0, -900),
        
        new Vector2(900, 900),
        new Vector2(-900, 900),
        new Vector2(900, -900),
        new Vector2(-900, -900),
        
        new Vector2(900, 450),
        new Vector2(900, -450),
        new Vector2(-900, 450),
        new Vector2(-900, -450),
        new Vector2(450, 900),
        new Vector2(-450, 900),
        new Vector2(450, -900),
        new Vector2(-450, -900),
    };
    
    private Vector2[] ChasePos = new Vector2[] {
        new Vector2(150, 0),
        new Vector2(-150, 0),
        new Vector2(0, 150),
        new Vector2(0, -150),
        
        new Vector2(150, 150),
        new Vector2(-150, 150),
        new Vector2(150, -150),
        new Vector2(-150, -150),
    };
    
    private List<Sprite>[,] grid = new List<Sprite>[12, 12];
    private int rectangleSize = 200;
    private List<Tuple<int, int>> nextRectangle = new List<Tuple<int, int>>();

    private List<MagicEffect> Magic = new List<MagicEffect>();

    #region Grozen Grid Code
public void placeObjects() {

        for (int i = 0; i < 12; i++) {
            for (int j = 0; j < 12; j++) {
                grid[i, j] = new List<Sprite>();
            }
        }
        foreach (var sprite in _sceneComponents) {
            if (!sprite.collision) {
                continue;
            }
            for (int i = 0; i < 12; i++) {
                for (int j = 0; j < 12; j++) {
                    
                    bool temp = sprite.physics.Intersect(new Sprite(_textures[9],
                        new Rectangle(-1200 + i * rectangleSize, - 1200 + j * rectangleSize, rectangleSize, rectangleSize)) {
                        Position = new Vector2(-1200 + i * rectangleSize, - 1200 + j * rectangleSize)
                    });
                    if (temp) {
                        grid[i, j].Add(sprite);
                    }
                }
            }
        }

        nextRectangle.Add(Tuple.Create<int, int>(0, 0));
        nextRectangle.Add(Tuple.Create<int, int>(0, 1));
        nextRectangle.Add(Tuple.Create<int, int>(0, -1));
        nextRectangle.Add(Tuple.Create<int, int>(1, 0));
        nextRectangle.Add(Tuple.Create<int, int>(-1, 0));
        nextRectangle.Add(Tuple.Create<int, int>(1, 1));
        nextRectangle.Add(Tuple.Create<int, int>(1, -1));
        nextRectangle.Add(Tuple.Create<int, int>(-1, 1));
        nextRectangle.Add(Tuple.Create<int, int>(-1, -1));
        
    }

    public void checkCoallisionGrid(Sprite sprite) {
        int x = (int)Math.Floor(Math.Abs(-1200 - sprite.Position.X) / rectangleSize);
        int y = (int)Math.Floor(Math.Abs(-1200 - sprite.Position.Y) / rectangleSize);
        
        if (x >= 12 || y >= 12) {
            return;
        }
        
        for (int i = 0; i < 9; i++) {
            if (!CheckValidgridRectangle(x, y, i)) {
                resolveCollision(sprite, grid[x, y]);
            }
        }
    }

    public void PrintGrid() {
        bool nemaNisto = true;
        for (int i = 0; i < 12; i++) {
            for (int j = 0; j < 12; j++) {
                for (int k = 0; k < grid[i, j].Count; k++) {
                    nemaNisto = false;
                }
            }
        }

        if (nemaNisto) {
        }
        
    }
    
    public bool CheckValidgridRectangle(int x, int y, int num) {
        return ((x + nextRectangle[num].Item1) >= 12) ||
               ((x + nextRectangle[num].Item1) < 0) ||
               ((y + nextRectangle[num].Item1) >= 12) ||
               ((y + nextRectangle[num].Item1) < 0);
    }

    public void resolveCollision(Sprite mainSprite, List<Sprite> sprites) {
        foreach (var sprite in sprites) {
            mainSprite.Position += mainSprite.physics.Resolve(sprite);
            mainSprite.Position = new Vector2((int)mainSprite.Position.X, (int)mainSprite.Position.Y);
        }
    }
    

    #endregion
    
    #region cardRectangle
        private Rectangle[] cardRectangle = new Rectangle[] {
            Mathematics.MakeRectangle(11, 12, 27,34), // Karta 1 (Index: 0)
            Mathematics.MakeRectangle(29, 12, 45,34), // Karta 2 (Index: 0)
            Mathematics.MakeRectangle(47, 12, 62,34), // Karta 3 (Index: 0)
            Mathematics.MakeRectangle(66, 12, 80,34), // Karta 4 (Index: 0)
            Mathematics.MakeRectangle(85, 12, 99,34), // Karta 5 (Index: 0)
            Mathematics.MakeRectangle(11, 44, 27,66), // Karta 6 (Index: 0)
            Mathematics.MakeRectangle(29, 44, 45,66), // Karta 7 (Index: 0)
            Mathematics.MakeRectangle(47, 44, 63,66), // Karta 8 (Index: 0)
            Mathematics.MakeRectangle(66, 44, 80,66), // Karta 9 (Index: 0)
            Mathematics.MakeRectangle(85, 44, 99,66), // Karta 10 (Index: 0)
        };
        
    #endregion

    #region objectsTexture
        private Rectangle[] objektiTekstura2 = new Rectangle[] {
            Mathematics.MakeRectangle(160, 10, 190,65), // Kocka box (Index: 0)
            Mathematics.MakeRectangle(160, 80, 190,130), // Kocka pomala box (Index: 1)
            Mathematics.MakeRectangle(160, 150, 190,190), // bure (Index: 2)
            Mathematics.MakeRectangle(160, 215, 190,250), // vazna (Index: 3)
            Mathematics.MakeRectangle(160, 280, 190,320), // pot (Index: 4)
            Mathematics.MakeRectangle(160, 340, 190,380), // pomala vazna (Index: 5)
            Mathematics.MakeRectangle(220, 10, 220,60), // Stub sreden (Index: 6)
            Mathematics.MakeRectangle(220, 90, 220,160), // Stub golem (Index: 7)
            Mathematics.MakeRectangle(350, 170, 380,250), // Stub najgolem (Index: 8)
            Mathematics.MakeRectangle(290, 10, 350,60), // Stub mal (Index: 9)
            Mathematics.MakeRectangle(380, 0, 410,70), // Klupa (Index: 10)
            Mathematics.MakeRectangle(380, 90, 420,160), // Klupa (Index: 11)
            Mathematics.MakeRectangle(290, 10, 350,60), // Klupa (Index: 12)
            Mathematics.MakeRectangle(450, 20, 480,100), // Statua (Index: 13)
            Mathematics.MakeRectangle(420, 360, 470,410), // rusevina (Index: 14)
            Mathematics.MakeRectangle(0, 430, 60,470), // Kamenja masiven (Index: 15)
            Mathematics.MakeRectangle(10, 490, 20,500), // Kamenja mal (Index: 16)
            Mathematics.MakeRectangle(40, 490, 60,500), // Kamenja pogolem (Index: 17)
            Mathematics.MakeRectangle(70, 480, 90,510), // Kamenja sreden 1 (Index: 18)
            Mathematics.MakeRectangle(100, 480, 120,510), // Kamenja sreden 2 (Index: 19)
            Mathematics.MakeRectangle(130, 480, 160,510), // Kamenja sreden 3 (Index: 20)
            Mathematics.MakeRectangle(160, 480, 190,510), // Kamenja najgolem (Index: 21)
        };
    

    #endregion

    public Scene(List<Texture2D> textures) {
        _textures = textures;
    }

    

    public void Prepare() {
        if (Game1.state == GameState.GamePlay) {
            RemoveAllCards();
            RemoveAllMonsters();
            AddCards();    
        }
        
    }

    public void CheckShooting() {
        foreach (var magic in Aplayer.Magics) {
            certainMagic(magic);
        }
    }

    public void certainMagic(Magic magic) {
        foreach (var monster in _monsters) {
            if (magic.physics.Intersect(monster)) {

                magic.collisionDetected(monster);
                if (magic.Type != 0 && CheckIfMagicAreaExists(magic.Type)) {
                    Magic.Add(new MagicEffect(_textures[9], magic.Position, magic.Type){transperent = 0f});    
                }
                
                if (magic.Finished) {
                    return;
                }
            }
        }
    }
    
    public void CheckMagicArea() {
        foreach (MagicEffect magic in Magic) {
            if (magic.Ready) {
                certainMagicArea(magic);
            }
            magic.TurnOff();
        }
    }

    public void certainMagicArea(MagicEffect magic) {
        foreach (var monster in _monsters) {
            if (magic.physics.Intersect(monster)) {
                magic.collisionDetected(monster);
                
            }
        }
    }

    public bool CheckIfMagicAreaExists(int type) {
        if (Magic.Count > 0) {
            for (int i = 0; i < Magic.Count; i++) {
                if (Magic[i].Type == type) {
                    return false;
                }
            }
        }

        return true;
    }
    
    public void Update(GameTime gameTime) {

        Aplayer.Update(gameTime);

        if (Game1.state == GameState.GameArena) {
            AiPlayer.Update(gameTime);
        }
        
        
        for (int i = 0; i < Magic.Count; i++) {
            Magic[i].Update(gameTime);
            if (Magic[i].Finished) {
                Magic.Remove(Magic[i]);
                i--;
            }
        }

        CardDistance();
        AddMonsters(gameTime);

        CheckShooting();
        CheckMagicArea();
        
        
        
        // foreach (var monster in _monsters) {
        //     monster.Position = monster.MovingDirections(_sceneComponents);
        // }
        
        foreach (var monster in _monsters) {
            checkCoallisionGrid(monster);
        }
        
        foreach (var monster in _monsters) {
            foreach (var monster2 in _monsters) {
                if (monster == monster2) {
                    continue;
                }
                monster.Position += monster.physics.Resolve(monster2);
                // monster.Position = new Vector2((int)monster.Position.X, (int)monster.Position.Y);

            }
        }
        
        for (int i = 0; i < _monsters.Count; i++) {
            _monsters[i].Update(gameTime);
            if (_monsters[i].Finished) {
                _monsters.Remove(_monsters[i]);
                i--;
            }
        }
        
        foreach (var sprite in _sceneComponents) {
            if (!sprite.collision) {
                continue;
            }
            Aplayer.Position += Aplayer.physics.Resolve(sprite);
            Aplayer.Position = new Vector2((int)Aplayer.Position.X, (int)Aplayer.Position.Y);
        }
    }
    
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        foreach (var sprite in _sceneComponents) { sprite.Draw(gameTime, spriteBatch); }
        foreach (var card in _cards) { card.Draw(gameTime, spriteBatch); }
        foreach (var monster in _monsters) { monster.Draw(gameTime, spriteBatch); }
        Aplayer.Draw(gameTime, spriteBatch);
        
        if (Game1.state == GameState.GameArena) {
            AiPlayer.Draw(gameTime, spriteBatch);
            foreach (var child in Aplayer.childs) {
                child.Draw(gameTime, spriteBatch);
            }
        }
        foreach (var magic in Magic) { magic.Draw(gameTime, spriteBatch); }
    }
    //

    #region cardsRegion

    // ***************************************** All About Cards ****************************************//
    public void AddCard(int card, Vector2 pos) {
        _cards.Add(new Sprite(_textures[10], cardRectangle[card]) {Position = pos, collision = false});
    }
    
    public void RemoveCard(int num) {
        _cards.RemoveAt(num);
    }

    public void RemoveAllCards() {
        for (int i = _cards.Count - 1; i >= 0; i--) {
            _cards.RemoveAt(i);
        }
    }
    
    public void RemoveAllMonsters() {
        for (int i = _monsters.Count - 1; i >= 0; i--) {
            _monsters.RemoveAt(i);
        }
    }
    // ***************************************** All About Cards ****************************************//

    #endregion
    
    

    public void MakingScene() {
        
        int counter = 0;
        for (int i = -1024; i <= 1024; i += 128) {
            for (int j = -1024; j <= 1024; j += 128) {
                _sceneComponents.Add(new Sprite(_textures[6], new Rectangle(tile[tileNumber[counter], 0],tile[tileNumber[counter], 1] , 128, 128)){Position = new Vector2(i, j)});
                counter++;
            }
        }
        
        for (int i = -1024; i <= 1119; i += 95) {
            _sceneComponents.Add(new Sprite(_textures[8], new Rectangle(30,190, 130, 70)){Position = new Vector2(i, -1034), collision = true});
            _sceneComponents.Add(new Sprite(_textures[8], new Rectangle(385,30, 95, 10)){Position = new Vector2(i, 1034+95+20), collision = true});
            _sceneComponents.Add(new Sprite(_textures[8], new Rectangle(285,30, 10, 95)){Position = new Vector2(-1024, i), collision = true});
            _sceneComponents.Add(new Sprite(_textures[8], new Rectangle(285,30, 10, 95)){Position = new Vector2(1034+95+20, i), collision = true});
        
        }
        
        if (mode == 1) {
            EditorMode();
            return;
        }
        
    }

    public double timerMonster = 0f;
    public int difficulty = 1;
    public void AddMonsters(GameTime gameTime) {
        if (!activateAdding || _monsters.Count > 10 * difficulty) {
            return;
        }
        timerMonster += gameTime.ElapsedGameTime.Milliseconds;
    
        if (timerMonster > 200) {
            AddOneMonster(Mathematics.RandomNumber(1, 5));
            timerMonster = 0f;
        }
    }

    public void AddOneMonster(int num) {
        if (num == 1) {
            _monsters.Add(new Monsters(
                ContentLoader.LoadMonster(num), Aplayer, ChasePos[Mathematics.RandomNumber(0, 8)]) {
                Position = SpawnPos[Mathematics.RandomNumber(0, 16)],
                moveVelocity = 1,
                damage = 2,
                health = 3
            });
        }
        else if (num == 2) {
            _monsters.Add(new Monsters(
                ContentLoader.LoadMonster(num), Aplayer, ChasePos[Mathematics.RandomNumber(0, 8)]) {
                Position = SpawnPos[Mathematics.RandomNumber(0, 16)],
                moveVelocity = 1,
                damage = 5,
                health = 10
            });
        }
        else if (num == 3) {
            _monsters.Add(new Monsters(
                ContentLoader.LoadMonster(num), Aplayer, ChasePos[Mathematics.RandomNumber(0, 8)]) {
                Position = SpawnPos[Mathematics.RandomNumber(0, 16)],
                moveVelocity = 1,
                damage = 2,
                health = 5
            });
        }
        else if (num == 4) {
            _monsters.Add(new Monsters(
                ContentLoader.LoadMonster(num), Aplayer, ChasePos[Mathematics.RandomNumber(0, 8)]) {
                Position = SpawnPos[Mathematics.RandomNumber(0, 16)],
                moveVelocity = 1,
                damage = 1,
                health = 1
            });
        }
    }
    
    
    public void AddCards() {

        for (int i = 0; i < 20; i++) {
            AddCard(Mathematics.RandomNumber(0,9), new Vector2(Mathematics.RandomNumber(-950, 950), Mathematics.RandomNumber(-950, 950)));
        }
    }
    
    public void CardDistance() {
        
        if (!Aplayer.getTheCard) { return; }
        
        for (int i = 0; i < _cards.Count; i++) {
            if (Mathematics.DistanceBetween(Aplayer.centerPosition(), _cards[i].Position) < 50) {
                RemoveCard(i);
                Aplayer.AddCardsHand();
                return;
            }
        }
    }


    public void AddPlayerToTheScene(Player player) {
        Aplayer = player;
    }
    
    public void AddAIPlayerToTheScene(AIPlayer aiPlayer) {
        AiPlayer = aiPlayer;
    }
    public void AddSpriteToTheScene(Sprite sprite) {
        _sceneComponents.Add(sprite);
    }
    
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************
    //*****************************************************************************************************************************

    #region AddDecorationGamePlay
    
    
    public void AddDecorationGamePlay() {
        
    //  _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[9]){Position = new Vector2(-460,-838), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[10]){Position = new Vector2(-520,-791), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[10]){Position = new Vector2(-519,-716), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[11]){Position = new Vector2(-367,-805), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[0]){Position = new Vector2(-452,-775), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[0]){Position = new Vector2(-452,-748), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[0]){Position = new Vector2(-422,-772), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[0]){Position = new Vector2(-421,-746), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(-861,-913), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(-125,-932), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(522,-610), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(-463,-335), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(498,-44), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(-147,170), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(327,330), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(904,281), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(1002,-140), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(946,722), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(478,921), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(-278,800), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(-800,599), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(-883,208), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(-874,-73), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[21]){Position = new Vector2(126,-14), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[20]){Position = new Vector2(-771,-387), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[20]){Position = new Vector2(-544,-582), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[20]){Position = new Vector2(-879,-818), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[20]){Position = new Vector2(-257,-900), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[20]){Position = new Vector2(456,-714), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[18]){Position = new Vector2(68,-338), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[18]){Position = new Vector2(640,-339), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[18]){Position = new Vector2(748,-583), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[18]){Position = new Vector2(948,-589), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[18]){Position = new Vector2(895,238), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[18]){Position = new Vector2(-249,414), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[18]){Position = new Vector2(-664,124), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[17]){Position = new Vector2(-536,-454), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[17]){Position = new Vector2(-29,-178), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[17]){Position = new Vector2(-402,-132), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[17]){Position = new Vector2(-948,-485), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[17]){Position = new Vector2(-712,-721), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[17]){Position = new Vector2(-929,-918), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(13,-669), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(450,-418), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(290,-10), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(70,-43), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(422,47), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(187,147), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(279,66), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(193,46), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(326,30), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(390,-63), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(579,-363), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(566,-420), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(602,-427), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(605,-371), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(985,-356), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(972,-287), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(986,-246), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[16]){Position = new Vector2(1064,-335), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[15]){Position = new Vector2(560,-850), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[15]){Position = new Vector2(719,320), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[14]){Position = new Vector2(-546,176), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[14]){Position = new Vector2(-720,-701), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[13]){Position = new Vector2(63,-556), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[13]){Position = new Vector2(-897,76), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[13]){Position = new Vector2(810,162), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8]){Position = new Vector2(236,196), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8]){Position = new Vector2(-120,188), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8]){Position = new Vector2(247,443), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8]){Position = new Vector2(-111,438), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8]){Position = new Vector2(508,466), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8]){Position = new Vector2(-447,359), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8]){Position = new Vector2(-345,633), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[2]){Position = new Vector2(-707,-658), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[3]){Position = new Vector2(-706,-678), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[3]){Position = new Vector2(-448,-780), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[3]){Position = new Vector2(-420,-777), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[3]){Position = new Vector2(-446,-756), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[4]){Position = new Vector2(-417,-751), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[4]){Position = new Vector2(-373,-721), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[0]){Position = new Vector2(-721,836), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[0]){Position = new Vector2(356,649), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[0]){Position = new Vector2(831,471), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[1]){Position = new Vector2(732,1063), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[1]){Position = new Vector2(592,-445), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[0]){Position = new Vector2(958,-644), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[11]){Position = new Vector2(815,737), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[10]){Position = new Vector2(671,751), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[9]){Position = new Vector2(725,687), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[2]){Position = new Vector2(743,766), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[2]){Position = new Vector2(-323,1081), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[1]){Position = new Vector2(-210,-393), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8]){Position = new Vector2(-115,-613), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8]){Position = new Vector2(222,-598), collision = true});
    _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[2]){Position = new Vector2(-413,-220), collision = true});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[19]){Position = new Vector2(977,-326), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[19]){Position = new Vector2(1032,-555), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[19]){Position = new Vector2(944,-394), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[19]){Position = new Vector2(57,204), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[19]){Position = new Vector2(-372,-124), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[19]){Position = new Vector2(-726,-41), collision = false});
    // _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[19]){Position = new Vector2(-906,-346), collision = false});
    placeObjects();
    activateAdding = true;
    }
    #endregion

    #region AddDecorationGameArena

    public void AddDecorationGameArena() {
        
        for (int i = 0; i <= 10; i++) {
            _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8])
                {Position = new Vector2(-400 + i*(80), -400), collision = true});
            
            _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8])
                {Position = new Vector2(400, -400 + (i * 80)), collision = true});
            
            _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8])
                {Position = new Vector2(-400, -400 + (i * 80)), collision = true});
            
            _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[8])
                {Position = new Vector2(-400 + i*(80), 400), collision = true});
        }
    }

    #endregion
    
    #region EditMode
 
    //TODO
    //**********************************************************EDIT MODE BEGIN*****************************************************
    //TODO
    public int num = 0;
    public int mode = 0;
    private Sprite player;
    public int componentsAdded = 0;
    public MouseState curCursorState;
    public MouseState prevCursorState;
    public List<int> numNumber = new List<int>();
    public bool qIsDown;
    public bool eIsDown;
    public bool pIsDown;
    public void EditorMode() {
        _editSceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[num]){Position = new Vector2(50,50), collision = num <= 15});
    }
    public void EditorModeUpdate() {
        prevCursorState = curCursorState;
        curCursorState = Mouse.GetState();
        
        if (curCursorState.LeftButton == ButtonState.Released && prevCursorState.LeftButton == ButtonState.Pressed) {
            PutObject();
        }
        
        if (curCursorState.RightButton == ButtonState.Released && prevCursorState.RightButton == ButtonState.Pressed) {
            DeleteObject();
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Q)) {
            qIsDown = true;
        }
        if (qIsDown && Keyboard.GetState().IsKeyUp(Keys.Q)) {
            SwitchObjectBackwards();
            qIsDown = false;
        }
        
        if (Keyboard.GetState().IsKeyDown(Keys.E)) {
            eIsDown = true;
        }
        if (eIsDown && Keyboard.GetState().IsKeyUp(Keys.E)) {
            SwitchObjectForward();
            eIsDown = false;
        }
        
        if (Keyboard.GetState().IsKeyDown(Keys.P)) {
            pIsDown = true;
        }
        if (pIsDown && Keyboard.GetState().IsKeyUp(Keys.P)) {
            Printing();
            pIsDown = false;
        }
    }
    public void PutObject(){
        foreach (var component in _sceneComponents) {
            if (component.isUser) {
                player = component;
                break;
            }
        }
        Vector2 bulshit = new Vector2(curCursorState.X +  player.Rectangle.Width / 2 + player.Position.X - Game1.ScreenWidth/2f,
            curCursorState.Y +  player.Rectangle.Height / 2 + player.Position.Y - Game1.ScreenHeight/2f);
        _sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[num]){Position = new Vector2((int)bulshit.X, (int)bulshit.Y), collision = num <= 15});
        numNumber.Add(num);
        componentsAdded++;
        // (componentsAdded);
    }
    
    public void DeleteObject(){
        if (componentsAdded > 0) {
            _sceneComponents.RemoveAt(_sceneComponents.Count - 1);
            numNumber.RemoveAt(numNumber.Count - 1);
            componentsAdded--;
        }
    }
    
    public void SwitchObjectForward() {
        num = (num + 1) % objektiTekstura2.Length;
        _editSceneComponents.RemoveAt(_editSceneComponents.Count - 1);
        EditorMode();
    }
    
    public void SwitchObjectBackwards() {
        if (num - 1 < 0) {
            num = objektiTekstura2.Length - 1;
        }
        else {
            num = (num - 1) % objektiTekstura2.Length;
        }
        
        _editSceneComponents.RemoveAt(_editSceneComponents.Count - 1);
        EditorMode();
    }
    
    private void Printing() {
        Console.WriteLine("------------------------------------------------------------------------");
        for (int i = _sceneComponents.Count - componentsAdded; i < _sceneComponents.Count ; i++) {
            Console.WriteLine(        
                "_sceneComponents.Add(new Sprite(_textures[2], objektiTekstura2[" + numNumber[i + componentsAdded - _sceneComponents.Count] +"])" +
                "{Position = new Vector2(" + _sceneComponents[i].Position.X +"," + _sceneComponents[i].Position.Y +")" +
                ", collision = " + _sceneComponents[i].collision + "});"
            );
        }
        Console.WriteLine("------------------------------------------------------------------------");
    }
    //TODO
    //**********************************************************EDIT MODE END*******************************************************
    //TODO
    

    #endregion
    
   
}