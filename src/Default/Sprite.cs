namespace Pacman;


public abstract class Sprite
{
  protected Texture2D Texture;
  public Vector2 Position { get; set; }
  protected Color Tint { get; set; } = Color.White;
  protected int frameWidth;
  protected int frameHeight; 
  protected float frameCount;
  protected int frameX;
  protected int frameY;

  public Sprite(Texture2D texture)
  {
    Texture = texture;
  }

  public virtual void Draw(SpriteBatch spriteBatch)
  {
    spriteBatch.Draw(Texture, Position, Tint);
  }

  public virtual void UpdateTexture(GameTime gameTime)
  {
    frameCount += (float)gameTime.ElapsedGameTime.TotalSeconds;
    

    if(frameCount >= 0.02f) 
    {
      frameX++;
      frameCount = 0;
      if(frameX >= 5)
      {
        frameX = 0;
      }
    }
  }
}
