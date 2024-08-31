namespace Pacman;

public class Mapa
{
    public static int map_width = 28;
    public static int map_height = 31;
    protected Texture2D _mapa_texture;
    protected Texture2D _coins_texture;
    public static int[,] _mapa;
    private int frameX = 0;
    private int frameY = 0;
    private int frameWidth;
    private int frameHeight;
    private int textureCount;
    private readonly int _tile_size = 24;
    
    public Mapa(Texture2D texture1, Texture2D texture2)
    {
        textureCount = (int)texture2.Width / 16;
        frameWidth = (int)texture2.Width / textureCount;
        frameHeight = (int)texture2.Height / 2;
        _mapa_texture = texture1;
        _coins_texture = texture2;
        InitializeMap();
    }
    public void InitializeMap()
    {
        const int map_width = 28;
        const int map_height = 31;
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

    public void ResetMap() => InitializeMap();

    public void DrawMap(SpriteBatch spritebatch)
    {
      int targetWidth = 670;
      int targetHeight = 744;

      int gridTotalWidth = map_width * _tile_size;
      int gridTotalHeight = map_height * _tile_size;

      int offsetX = (targetWidth - gridTotalWidth) / 2;
      int offsetY = (targetHeight - gridTotalHeight) / 2;

      spritebatch.Draw(_mapa_texture, new Rectangle(offsetX, offsetY, gridTotalWidth, gridTotalHeight), Color.White);

      for (int i = 0; i < map_height; i++)
      {
        for (int j = 0; j < map_width; j++)
        {
            if(_mapa[i,j] == 3)
            {
                frameX = 0;
                frameY = 1;
                Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameWidth, frameWidth, frameHeight);
                Rectangle destinationRectangle = new Rectangle(j * _tile_size, i * _tile_size, _tile_size,_tile_size);
                spritebatch.Draw(_coins_texture, destinationRectangle, sourceRectangle, Color.Yellow, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
            if(_mapa[i,j] == 4)
            {
                frameX = 1;
                frameY = 1;
                Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameWidth, frameWidth, frameHeight);
                Rectangle destinationRectangle = new Rectangle(j * _tile_size, i * _tile_size, _tile_size, _tile_size);
                spritebatch.Draw(_coins_texture, destinationRectangle, sourceRectangle, Color.Yellow, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
         }
      }
   }
}
