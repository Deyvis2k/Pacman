namespace Pacman;

public sealed class Player : Sprite
{
      public static new Vector2 Position { get; private set; }
      public int Lives {get; set;} = 3;
      private readonly List<Rectangle> _walls = new();
      public static int MaxScore = 0;
      private List<Rectangle> _coins = new();
      private List<Rectangle> _coinsCollected = new();
      private const int Speed = 2;
      private string _lastDirection { get; set; } = "Right";
      public static string _Direction { get; set; } = "Right";
      public static int Score { get; set;} = 0;
      public static int Level { get; set;} = 1;
      public static Dictionary<string, Vector2> _Path = new Dictionary<string, Vector2>()
      {
        {"Right", new Vector2(1, 0)},
        {"Left", new Vector2(-1, 0)},
        {"Up", new Vector2(0, -1)},
        {"Down", new Vector2(0, 1)},
        {"Stop", new Vector2(0, 0)}
      };

      public Player(Texture2D texture) : base(texture)
      {
        LoadWalls();
        LoadCoins();
        InitializePosition();
        frameWidth = (int)Texture.Width / 6;
        frameHeight = (int)Texture.Height / 4;
        frameCount = 0;
        frameX = 0;
        frameY = 0;
      }

      public override void Draw(SpriteBatch spriteBatch)
      {
          float scale = 1.65f;
          Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameHeight, frameWidth, frameHeight);
          Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)(frameWidth * scale), (int)(frameHeight * scale));
          spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Tint, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      }

      private void InitializePosition()
      {
          for(int i = 0; i < Mapa.map_height; i++)
          {
              for(int j = 0; j < Mapa.map_width; j++)
              {
                  if(Mapa._mapa[i, j] == 6)
                  {
                      Position = new Vector2(j * 24, i * 24);
                  }
              }
          }
      }

      public void LoadWalls()
      {
        for (int i = 0; i < Mapa.map_height; i++)
        {
          for (int j = 0; j < Mapa.map_width; j++)
          {
            if (Mapa._mapa[i, j] == 1 || Mapa._mapa[i,j] == 11)
            {
              _walls.Add(new Rectangle(j * 24, i * 24, 24, 24));
            }
          }
        }
      }

      private void ConsumeFruit()
      {
          Rectangle playerBounds = new Rectangle((int)Position.X, (int)Position.Y, 23, 23);
            
          if(Fruit._ready && Fruit._fruit.Count > 0)
            if(playerBounds.Intersects(Fruit._fruit[0])) 
            {
              Fruit._ready = false;
              Fruit._fruit.Remove(Fruit._fruit[0]);
              Score += 100;
              Fruit.timeToSpawn = 0f;
            }
      }

      private bool PositionCollides(Vector2 Velocity)
      {
        Rectangle playerBounds = new Rectangle(
            (int)(Position.X + Velocity.X * Speed),
            (int)(Position.Y + Velocity.Y * Speed),
            23,
            23);

        foreach(var wall in _walls)
        {
          if(playerBounds.Intersects(wall)) return true;
        }

        return false;
      }
     
      public void Update(GameTime gametime, List<Enemy> ghosts)
      {
        InputHandler();
        bool isCollidingWithWall = PositionCollides(_Path[_lastDirection]);
        Dead(ghosts);
        ConsumeFruit();

        if (isCollidingWithWall)
        {
            frameX = 1;
            return;
        }
        
        Position += _Path[_lastDirection] * Speed;
        GetCoins(ghosts);
        ReversePosition(672);

        if (!_Direction.Equals("Stop")) UpdateTexture(gametime);

        switch (_lastDirection)
        {
            case "Right":
                frameY = 0;
                break;
            case "Left":
                frameY = 2;
                break;
            case "Up":
                frameY = 1;
                break;
            case "Down":
                frameY = 3;
                break;
        };
      }

      private void InputHandler()
      {
        if(Keyboard.GetState().IsKeyDown(Keys.Up)) _Direction = "Up";
        if(Keyboard.GetState().IsKeyDown(Keys.Down)) _Direction = "Down";
        if(Keyboard.GetState().IsKeyDown(Keys.Left)) _Direction = "Left";
        if(Keyboard.GetState().IsKeyDown(Keys.Right)) _Direction = "Right";

        if(!PositionCollides(_Path[_Direction]) && Position.X < 672 && Position.X > 0 && Position.Y < 744 && Position.Y > 0)
        {
          _lastDirection = _Direction;
        }
      }

        
      public void Dead(List<Enemy> ghosts)
      {
         Rectangle PlayerRec = new Rectangle((int)Position.X, (int)Position.Y, 15, 15);
         foreach(var ghost in ghosts)
         {
           Rectangle ghostBounds = new Rectangle((int)ghost.Position.X, (int)ghost.Position.Y, 15, 15);

           if(!ghost.IsEaten && !ghost.IsScared && ghostBounds.Intersects(PlayerRec))
            {
                Lives -= 1;
                GameHandler._RestartGame = true;
                break;
            }
         }
      }

      public bool GameOver() => Lives <= 0;
       

      private void GetCoins(List<Enemy> ghosts)
      {
        Rectangle PlayerRec = new Rectangle((int)Position.X, (int)Position.Y, 24, 24);

        _coinsCollected.Clear();
        
        foreach(var coin in _coins)
        {
          if(PlayerRec.Intersects(coin))
          {
            if(Mapa._mapa[(int)coin.Y / 24, (int)coin.X / 24] == 4)
            {
               foreach(var ghost in ghosts)
               {
                 ghost.IsScared = ghost.isInPrison ? false : true;
                 ghost.scaredTime = 7.0f;
               }
            }
            _coinsCollected.Add(coin);
          }
        }
        
        foreach(var coin in _coinsCollected)
        {
          Mapa._mapa[(int)coin.Y / 24, (int)coin.X / 24] = 0;
        }
        
        Score += _coinsCollected.Count * 5;

        _coins.RemoveAll(x => _coinsCollected.Contains(x));
      }

      private void LoadCoins()
      {
        for(int i = 0; i < Mapa.map_height; i++)
        {
          for(int j = 0; j < Mapa.map_width; j++)
          {
            if(Mapa._mapa[i,j] == 3 || Mapa._mapa[i,j] == 4)
            {
              Rectangle coinBounds = new Rectangle(j * 24, i * 24, 10, 10);
              _coins.Add(coinBounds);
            }
          }
        }
      }
      private void ReversePosition(int ScreenX)
      {
          if(Position.X < 0) Position = new Vector2(ScreenX, Position.Y);
          if(Position.X > ScreenX) Position = new Vector2(0, Position.Y);
      }

      public void Reset()
      {
          _coinsCollected.Clear();
          _coins.Clear();
          InitializePosition();
          LoadWalls();
          LoadCoins();
          frameX = 0;
          frameX = 0;
          _lastDirection = "Right";
          _Direction = "Right";
      }
}
