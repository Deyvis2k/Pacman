
namespace Pacman;

public static class Gui
{
  public static void Draw(SpriteBatch spriteBatch, SpriteFont font,Player player)
  {
    spriteBatch.DrawString(font, $"Player {player}", new Vector2(10, 10), Color.White);

    DrawPlayer(spriteBatch, player);
  }


  public static void DrawPlayer(SpriteBatch spriteBatch, Player player)
  {
    Texture2D playerTexture = player.Texture;
    int frameWidth = (int)playerTexture.Width / 6;
    int frameHeight = (int)playerTexture.Height / 4;
    int frameX = 1;
    int frameY = 0;
    for(int i = 0; i < player.Lives; i++)
    {
      Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameHeight, frameWidth, frameHeight);
      Rectangle destinationRectangle = new Rectangle(10 + (frameWidth + 5)* i, 750, frameWidth, frameHeight);
      spriteBatch.Draw(playerTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
    }
  }
}
