namespace Pacman;

public sealed class Fruit : Sprite
{
    public static float timeToSpawn = 0f;
    public static float _timeToKill = 0f;
    public static bool _ready = false;
    public static List<Rectangle> _fruit = new ();
    private int _frameCountX;
    private int _frameCountY;
    private Random _rand = new();
    private readonly Dictionary<string, int> _fruits = new()
    {
        {"Cherry", 1},
        {"Orange", 2},
        {"Apple", 3},
        {"Pear", 4},
        {"Strawberry", 0},
        {"Grape", 5}
    };

    public Fruit(Texture2D texture) : base(texture)
    {
        Texture  = texture;
        IntializeFruits();
        _frameCountX = Texture.Width / 16;
        _frameCountY = Texture.Height / 16;
        frameWidth = (int)Texture.Width / _frameCountX;
        frameHeight = (int)Texture.Height / _frameCountY;
        frameX = 0;
        frameY = 0;
    }

    public void IntializeFruits()
    {
        for (int i = 0; i < Mapa.map_height; i++)
        {
            for (int j = 0; j < Mapa.map_width; j++)
            {
                if (Mapa._mapa[i, j] == 5)
                {
                    Position = new Vector2(j * 24, i * 24);
                }
            }
        }
    }

    public void Update(GameTime gametime)
    {
        SpawnFruit(gametime);
        DestroyFruit(gametime);
        UpdateTexture();
    }

    private void SpawnFruit(GameTime gametime)
    {
        timeToSpawn += (float)gametime.ElapsedGameTime.TotalSeconds;
        if (timeToSpawn > 15.5f && !_ready && _fruit.Count == 0)
        {
            _ready = true;
            _fruit.Add(new Rectangle((int)Position.X, (int)Position.Y, (int)(frameWidth * 1.5f), (int)(frameHeight * 1.5f)));
            timeToSpawn = 0;
        }
    }

    private void DestroyFruit(GameTime gametime)
    {
        
        if(_ready && _fruit.Count > 0)
        {
            _timeToKill += (float)gametime.ElapsedGameTime.TotalSeconds;
            if(_timeToKill > 6f)
            {
                _timeToKill = 0;
                timeToSpawn = 0;
                _ready = false;
                _fruit.Clear();
            }
        }
        if(GameHandler._RestartGame)
        {
            timeToSpawn = 0;
            _timeToKill = 0;
            _fruit.Clear();
            _ready = false;
        }
    }

    private void UpdateTexture()
    {
        switch(Player.Level)
        {
            case 1:
                frameY = _fruits["Cherry"];
                break;
            case 2:
                frameY = _fruits["Orange"];
                break;
            case 3:
                frameY = _fruits["Apple"];
                break;
            case 4:
                frameY = _fruits["Pear"];
                break;
            case 5:
                frameY = _fruits["Strawberry"];
                break;
            case 6:
                frameY = _fruits["Grape"];
                break;
        }

        if(Player.Level > 6 && _fruit.Count < 1) frameY = _fruits[RandomFruit()];
    }

    private string RandomFruit()
    {
        List<string> frutas = new List<string>{"Cherry", "Orange", "Apple", "Pear", "Strawberry", "Grape"};
        return frutas[_rand.Next(0,6)];
    }

    public override void Draw(SpriteBatch spritebatch)
    {
        float scale = 2f;
        Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameHeight, frameWidth, frameHeight);
        Rectangle destinationRectangle = new Rectangle((int)Position.X + 5, (int)Position.Y - 5, (int)(frameWidth * scale), (int)(frameHeight * scale));
        if(_ready) spritebatch.Draw(Texture, destinationRectangle, sourceRectangle, Tint, 0f, Vector2.Zero, SpriteEffects.None, 0f);
    }
}
