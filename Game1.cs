namespace Pacman
{
    public class Game1 : Game
    { 
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameHandler _gameHandler;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 672;
            _graphics.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gameHandler = new GameHandler(Content, _spriteBatch);
            _gameHandler.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _gameHandler.Load();
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _gameHandler.Update(gameTime, _graphics.PreferredBackBufferWidth);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(
            samplerState: SamplerState.PointClamp);
            _gameHandler.Draw();
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

