namespace Pacman;

public enum WallType 
{
    CornerTopLeft,
    CornerTopRight,
    CornerBottomLeft,
    CornerBottomRight,
    Horizontal,
    Vertical,
    Intersection,
    Full,
    T,
    T_RIGHT, // 13
    T_LEFT, // 11
    RIGHT,
}

public class Mapa
{
    public static int map_width = 28;
    public static int map_height = 31;
    protected Texture2D _mapa_texture;
    public static int[,] _mapa;
    private int frameX = 0;
    private int frameY = 0;
    private int frameWidth;
    private int frameHeight;
    private int textureCount;
    private readonly int _tile_size = 24;
    private Dictionary<WallType, int> _walls = new Dictionary<WallType, int>()
    {
        {WallType.CornerTopLeft, 10},
        {WallType.CornerTopRight, 12},
        {WallType.CornerBottomLeft, 3},
        {WallType.CornerBottomRight, 5},
        {WallType.Horizontal, 6},
        {WallType.Vertical, 9},
        {WallType.Intersection, 15},
        {WallType.Full, 0},
        {WallType.T, 7},
        {WallType.T_RIGHT, 13},
        {WallType.T_LEFT, 11},
        {WallType.RIGHT, 2}
    };

    public Mapa(Texture2D texture)
    {
        const int map_width = 28;
        const int map_height = 31;
        textureCount = (int)texture.Width / 16;
        frameWidth = (int)texture.Width / textureCount;
        frameHeight = (int)texture.Height / 2;
        _mapa_texture = texture;
        _mapa = new int[map_height, map_width]{
        {1, 1, 1, 1, 1, 1, 1, 1 ,1, 1 ,1 ,1 ,1 ,1, 1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1, 1}, 
        {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1},
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1},
        {1 ,4, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 4, 1},
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1},
        {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1},
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 3, 1}, 
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 3, 1}, 
        {1, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 1}, 
        {1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1}, 
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 3, 1, 0, 0, 0, 0, 0}, 
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 1, 1, 3, 1, 0, 0, 0, 0, 0}, 
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 0, 1, 1, 1, 11,11, 1, 1, 1, 0, 1, 1, 3, 1, 0, 0, 0, 0,0}, 
        {1, 1, 1, 1, 1, 1, 3, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 3, 1, 1, 1, 1, 1, 1}, 
        {2, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 9, 0, 8, 0, 10, 1 ,0 ,0 ,0 ,3 ,0 ,0 ,0 ,0 ,0,2}, 
        {1, 1, 1, 1, 1, 1, 3, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 3, 1, 1, 1, 1, 1, 1}, 
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 3, 1, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 1, 1, 3, 1, 0, 0, 0, 0, 0}, 
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 3, 1, 0, 0, 0, 0, 0}, 
        {1, 1, 1, 1, 1, 1, 3, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 3, 1, 1, 1, 1, 1, 1}, 
        {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1}, 
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1}, 
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1}, 
        {1, 4, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 6, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 4, 1}, 
        {1, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 1}, 
        {1, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 1}, 
        {1, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 1}, 
        {1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1}, 
        {1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1}, 
        {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1}, 
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        }; 

    }
    public static bool EmptyCoins()
    {
      for(int i = 0; i < map_height; i++)
      {
        for(int j = 0; j < map_width; j++)
        {
          if(_mapa[i,j] == 3 || _mapa[i,j] == 4)
          {
            return false;
          }
        }
      }
      return true;
    }

    public void ResetMap()
    {
        const int map_height = 31;
        const int map_width = 28;
        int[,] newMap = new int[map_height, map_width]
        {
        {1, 1, 1, 1, 1, 1, 1, 1 ,1, 1 ,1 ,1 ,1 ,1, 1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1, 1}, 
        {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1},
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1},
        {1 ,4, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 4, 1},
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1},
        {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1},
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 3, 1}, 
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 3, 1}, 
        {1, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 1}, 
        {1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1}, 
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 3, 1, 0, 0, 0, 0, 0}, 
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 1, 1, 3, 1, 0, 0, 0, 0, 0}, 
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 0, 1, 1, 1, 11,11, 1, 1, 1, 0, 1, 1, 3, 1, 0, 0, 0, 0,0}, 
        {1, 1, 1, 1, 1, 1, 3, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 3, 1, 1, 1, 1, 1, 1}, 
        {2, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 9, 0, 8, 0, 10, 1 ,0 ,0 ,0 ,3 ,0 ,0 ,0 ,0 ,0,2}, 
        {1, 1, 1, 1, 1, 1, 3, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 3, 1, 1, 1, 1, 1, 1}, 
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 3, 1, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 1, 1, 3, 1, 0, 0, 0, 0, 0}, 
        {0, 0, 0, 0, 0, 1, 3, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 3, 1, 0, 0, 0, 0, 0}, 
        {1, 1, 1, 1, 1, 1, 3, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 3, 1, 1, 1, 1, 1, 1}, 
        {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1}, 
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1}, 
        {1, 3, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 3, 1}, 
        {1, 4, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 6, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 4, 1}, 
        {1, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 1}, 
        {1, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 3, 1, 1, 1}, 
        {1, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3, 3, 1}, 
        {1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1}, 
        {1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1}, 
        {1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1}, 
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };
        _mapa = newMap;
    }

    private WallType DetermineWallType(int x, int y)
    {
       //Deus me perdoe..
       bool isTopWall = y > 0 && _mapa[y - 1, x] == 1;
       bool isBottomWall = y < map_height - 1 && _mapa[y + 1, x] == 1;
       bool isLeftWall = x > 0 && _mapa[y, x - 1] == 1;
       bool isRightWall = x < map_width - 1 && _mapa[y, x + 1] == 1;

       bool isTopEmpty = y > 0 && _mapa[y - 1, x] != 1;
       bool isBottomEmpty = y < map_height - 1 && _mapa[y + 1, x] != 1;
       bool isLeftEmpty = x > 0 && _mapa[y, x - 1] != 1;
       bool isRightEmpty = x < map_width - 1 && _mapa[y, x + 1] != 1;

       
       if(isTopWall && isLeftWall && !isBottomWall && !isRightWall)
        return WallType.CornerTopLeft;
       if(isTopWall && isRightWall && !isBottomWall && !isLeftWall)
        return WallType.CornerTopRight;
       if(isBottomWall && isLeftWall && !isTopWall && !isRightWall)
        return WallType.CornerBottomLeft;
       if(isBottomWall && isRightWall && !isTopWall && !isLeftWall)
        return WallType.CornerBottomRight;
        

       if(isTopWall && isBottomWall && isLeftWall && isRightWall)
        return WallType.Intersection;

       if(isBottomWall && isLeftWall && isRightWall && !isTopWall)
        return WallType.T;
       if(isTopWall && isBottomWall && isRightWall && !isLeftWall)
        return WallType.T_RIGHT;
       if(isTopWall && isBottomWall && isLeftWall && !isRightWall)
        return WallType.T_LEFT;

       if(IsVerticalWall(isTopWall, isBottomWall))
        return WallType.Vertical;
       if(IsHorizontalWall(isLeftWall, isRightWall))
        return WallType.Horizontal;
       
       return WallType.Horizontal;
    }

    private bool IsVerticalWall(bool top, bool bottom)
    {
      return top && bottom;
    }

    private bool IsHorizontalWall(bool left, bool right)
    {
      return left && right;
    }

    
    public void DrawMap(SpriteBatch spritebatch)
    {
      for (int i = 0; i < map_height; i++)
      {
        for (int j = 0; j < map_width; j++)
        {
            if(_mapa[i,j] == 1)
            {   
                WallType wallType = DetermineWallType(j, i);
                int spriteIndex = _walls[wallType];
                Rectangle sourceRectangle = new Rectangle(spriteIndex * frameWidth, 0, frameWidth, frameHeight);
                Rectangle destinationRectangle = new Rectangle(j * _tile_size, i * _tile_size, _tile_size, _tile_size);
                spritebatch.Draw(_mapa_texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }

            if(_mapa[i,j] == 3)
            {
                frameX = 0;
                frameY = 1;
                Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameWidth, frameWidth, frameHeight);
                Rectangle destinationRectangle = new Rectangle(j * _tile_size, i * _tile_size, _tile_size,_tile_size);
                spritebatch.Draw(_mapa_texture, destinationRectangle, sourceRectangle, Color.Yellow, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }

            if(_mapa[i,j] == 4)
            {
                frameX = 1;
                frameY = 1;
                Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameWidth, frameWidth, frameHeight);
                Rectangle destinationRectangle = new Rectangle(j * _tile_size, i * _tile_size, _tile_size, _tile_size);
                spritebatch.Draw(_mapa_texture, destinationRectangle, sourceRectangle, Color.Yellow, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }

            if(_mapa[i,j] == 11)
            {
                frameX = 2;
                frameY = 1;
                Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameWidth, frameWidth, frameHeight);
                Rectangle destinationRectangle = new Rectangle(j * _tile_size, i * _tile_size, _tile_size, _tile_size);
                spritebatch.Draw(_mapa_texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }

        }
      }
   }
}
