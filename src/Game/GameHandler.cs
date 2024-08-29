using Microsoft.Xna.Framework.Content;

namespace Pacman;

public class GameHandler
{
    private SpriteBatch _spriteBatch;
    private Mapa _mapa;
    private Texture2D _default_texture;
    private Texture2D _playerTexture;
    private Texture2D _mapaTexture;
    private Texture2D _ghostTexture;
    private SpriteFont _fonte;
    private Player _player;
    private List<Enemy> _ghosts = new List<Enemy>();
    private ContentManager _content;
    private float _gameStartTime = 0;
    private bool _IsGameStarted = false;
    public static bool _IsGameOver = false;

    public GameHandler(ContentManager content, SpriteBatch spriteBatch)
    {
        _content = content;
        _spriteBatch = spriteBatch;
    }

    public void Load()
    {           
        _default_texture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
        _default_texture.SetData(new[] { Color.White });

        _mapaTexture = _content.Load<Texture2D>("Map16");
        _fonte = _content.Load<SpriteFont>("Fonte");
        _mapa = new Mapa(_mapaTexture);
        _ghostTexture = _content.Load<Texture2D>("pacmansheet");
        _playerTexture = _content.Load<Texture2D>("Pacman16");
        _player = new Player(_playerTexture);
     
        _ghosts.Add(new Pinky(_ghostTexture));
        _ghosts.Add(new Blinky(_ghostTexture));
        _ghosts.Add(new Clyde(_ghostTexture));
        _ghosts.Add(new Inky(_ghostTexture, _ghosts[1].Position));
    }

    public void Initialize() {}

    private void Reset()
    {
        _mapa.ResetMap();
        _player.Reset();
        _ghosts.ForEach(ghost => ghost.Reset());
        _gameStartTime = 0;
        _IsGameStarted = false;
        _IsGameOver = false;
    }

    public void Draw()
    {
        _mapa.DrawMap(_spriteBatch);
        _player.Draw(_spriteBatch);
        Gui.Draw(_spriteBatch, _fonte, _player);
        _ghosts.ForEach(ghost => ghost.Draw(_spriteBatch));
    }

    public void Update(GameTime gameTime, int screenX)
    {
        _gameStartTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if(!_IsGameStarted && _gameStartTime > 2.0f) _IsGameStarted = true;
        

        if(Mapa.EmptyCoins()) Reset(); 
        if(_IsGameOver) Reset();
        if(_IsGameStarted)
        {
            _player.Update(gameTime, _ghosts);
            foreach (var ghost in _ghosts)
            {
                ghost.Update(gameTime);
            }

        }


    }
}

