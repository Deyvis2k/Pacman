
namespace Pacman;

public static class Gui
{
  public static void Draw(SpriteBatch spriteBatch, SpriteFont font,Player player)
  {
    spriteBatch.DrawString(font, $"Player {player}", new Vector2(10, 10), Color.White);
  }
}
