namespace Pacman;

//notas para os ghosts:
//clyde corner position: (646,24)
//pinky corner position: (24,24)
//blinky corner position: (24,696)
//inky corner position: (646,696)
public abstract class Enemy : Sprite
{
      protected int Speed = 2;
      protected readonly List<Rectangle> _walls = new List<Rectangle>();
      protected int textureCountX;
      protected int textureCountY;
      protected const float scale = 1.5f;
      protected bool HunterMode { get; set;} = false;
      protected float time { get; set;} = 0;
      public bool IsEaten { get; set; } = false;
      public float scaredTime { get; set;} = 7;
      public bool isInPrison { get; set;}
      public bool IsScared { get; set; } = false;
      protected List<Vector2> _path = new List<Vector2>()
      {
          new Vector2(1,0),
          new Vector2(0,1),
          new Vector2(-1,0),
          new Vector2(0,-1),
      };
      
      public Enemy(Texture2D texture) : base(texture)
      {
        Texture = texture;
      }
      public virtual void Update(GameTime gametime) {}

      public virtual void UpdateConditions(GameTime gametime) {}

      public virtual Vector2 GetNewDirection() { return new Vector2(0,0); }

      public virtual void UpdateTextures(Vector2 BestMove) {}

      public virtual void Reset()
      {
          Position = GetPosition(8);
          LoadWalls();
          IsEaten = false;
          IsScared = false;
          isInPrison = false;
          HunterMode = false;
      }

      protected void LoadWalls()
        {
            for(int i = 0; i < Mapa.map_height; i++)
            {
                for(int j = 0; j < Mapa.map_width; j++)
                {
                    if(Mapa._mapa[i, j] == 1)
                    {
                        _walls.Add(new Rectangle(j * 24, i * 24, 24, 24));
                    }
                }
            }
        }

      protected bool UpdateGates(Vector2 BestMove)
      {
          Rectangle ghostBounds = new Rectangle(
              (int)(Position.X + BestMove.X * Speed),
              (int)(Position.Y + BestMove.Y * Speed),
              24,
              24);
          for(int i = 0; i < Mapa.map_height; i++)
          {
              for(int j = 0; j < Mapa.map_width; j++)
              {
                  if(Mapa._mapa[i, j] == 11)
                  {
                       Rectangle wallBounds = new Rectangle(j * 24, i * 24, 24, 24);
                       if(!isInPrison && ghostBounds.Intersects(wallBounds))
                       {
                            return true;
                       }
                  }
              }
          }
          return false;
      }

    protected bool GhostCollide(Vector2 Velocity)
    {
        Rectangle ghostBounds = new Rectangle(
            (int)Position.X + (int)Velocity.X * Speed,
            (int)Position.Y + (int)Velocity.Y * Speed,
            23,
            23);

        foreach(var wall in _walls)
        {
            if(ghostBounds.Intersects(wall)) return true;
        }

        return false;
    }


      protected void GetEaten()
      {
        Rectangle ghostBounds = new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            23,
            23);
        Rectangle PlayerBounds = new Rectangle(
            (int)Player.Position.X,
            (int)Player.Position.Y,
            23,
            23);

        if(IsScared && ghostBounds.Intersects(PlayerBounds)) IsEaten = true;
     }
      protected bool HasReached(Vector2 position) { return Vector2.Distance(Position, position) < 1f; }

      protected virtual Vector2 GetPosition(int pos) 
      { 
        for(int i = 0; i < Mapa.map_height; i++)
        {
            for(int j = 0; j < Mapa.map_width; j++)
            {
                if(Mapa._mapa[i,j] == pos) return new Vector2(j * 24, i * 24);
            }
        }
        return new Vector2(0, 0);
      }
     protected void reversePosition(int ScreenX)
     {
          if (Position.X < 0) Position = new Vector2(ScreenX, Position.Y);
          if (Position.X > ScreenX) Position = new Vector2(0, Position.Y);
     }
}
