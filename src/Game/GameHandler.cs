using Microsoft.Xna.Framework.Content;

namespace Pacman;

public class GameHandler
{
    private SpriteBatch _spriteBatch;
    private Mapa _mapa;
    private Texture2D _default_texture;
    private Texture2D _playerTexture;
    private Texture2D _mapaTexture;
    private Texture2D _coinTexture;
    private Texture2D _ghostTexture;
    private Texture2D _fruitTexture;
    private SpriteFont _fonte;
    private Player _player;
    private List<Enemy> _ghosts = new List<Enemy>();
    private ContentManager _content;
    private Fruit _fruit;
    private float _gameStartTime = 0;
    public static bool _IsGameStarted = false;
    public static bool _RestartGame = false;

    public GameHandler(ContentManager content, SpriteBatch spriteBatch)
    {
        _content = content;
        _spriteBatch = spriteBatch;
    }

    public void Load()
    {           
        _default_texture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
        _default_texture.SetData(new[] { Color.White });

        _mapaTexture = _content.Load<Texture2D>("mapsprite");
        _coinTexture = _content.Load<Texture2D>("Map16");
        _fonte = _content.Load<SpriteFont>("Fonte");
        _mapa = new Mapa(_mapaTexture, _coinTexture);
        _ghostTexture = _content.Load<Texture2D>("pacmansheet");
        _playerTexture = _content.Load<Texture2D>("Pacman16");
        _player = new Player(_playerTexture);

        _fruitTexture = _content.Load<Texture2D>("fruits");

        _fruit = new(_fruitTexture);
     
        _ghosts.Add(new Pinky(_ghostTexture));
        _ghosts.Add(new Blinky(_ghostTexture));
        _ghosts.Add(new Clyde(_ghostTexture));
        _ghosts.Add(new Inky(_ghostTexture, _ghosts[1] as Blinky));
    }

    public void Initialize() {}

    private void Reset()
    {
        _mapa.ResetMap();
        _player.Reset();
        _ghosts.ForEach(ghost => ghost.Reset());
        _gameStartTime = 0;
        _IsGameStarted = false;
        Fruit._ready = false;
        Fruit._fruit.Clear();
        Fruit._timeToKill = 0;
        Fruit.timeToSpawn = 0;
        _RestartGame = false;
    }

    private void ResetGame()
    {
        if(_player.GameOver())
        {
            _player.Reset();
            _ghosts.ForEach(ghost => ghost.Reset());
            _gameStartTime = 0;
            _IsGameStarted = false;
            _RestartGame = true;
            Fruit._fruit.Clear();
            Fruit._ready = false;
            _player.Lives = 3;
            Player.MaxScore = Math.Max(Player.MaxScore, Player.Score);
            Player.Score = 0;
            Player.Level = 1;
        }
    }

    public void Draw()
    {
        _mapa.DrawMap(_spriteBatch);
        _fruit.Draw(_spriteBatch);
        _player.Draw(_spriteBatch);
        Gui.Draw(_spriteBatch, _fonte, _player);
        _ghosts.ForEach(ghost => ghost.Draw(_spriteBatch));

    }

    public void Update(GameTime gameTime, int screenX)
    {
        _gameStartTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(!_IsGameStarted && _gameStartTime > 2.0f) _IsGameStarted = true;
        
        
        ResetGame();
        if(Mapa.EmptyCoins())
        {
            Player.Level++;
            Reset(); 
        }
        if(_RestartGame) Reset();
        if(_IsGameStarted)
        {
            _player.Update(gameTime, _ghosts);
            _fruit.Update(gameTime);
            foreach (var ghost in _ghosts)
            {
                ghost.Update(gameTime);
            }
        }
    }
}

