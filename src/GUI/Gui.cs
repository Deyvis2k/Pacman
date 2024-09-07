namespace Pacman;

public static class Gui
{
    public static string[] CollectedFruit = new string[6]
    {
        "Cherry",
        "Orange",
        "Apple",
        "Pear",
        "Strawberry",
        "Grape"
    };

  public static void Draw(SpriteBatch spriteBatch, SpriteFont font,Player player)
  {
    spriteBatch.DrawString(font, $"Score: {Player.Score}", new Vector2(150, 750), Color.White);
    spriteBatch.DrawString(font, $"Max Score: {Player.MaxScore}", new Vector2(150, 770), Color.White);

    DrawPlayer(spriteBatch, player);
  }


  public static void DrawFruits(SpriteBatch spritebatch, Texture2D fruitTexture)
  {
      int frame_countX = fruitTexture.Width / 16;
      int frame_countY = fruitTexture.Height / 16;
      int frameWidth = (int)fruitTexture.Width / frame_countX;
      int frameHeight = (int)fruitTexture.Height / frame_countY;
      float scale = 1.5f;
      int count = Player.Level > 6 ? 6 : Player.Level;
      for(int i = 0; i < count; i++)
      {
          string fruta = CollectedFruit[i];
          int frameY = Fruit._fruits[fruta];
          Rectangle sourceRectangle = new Rectangle(0, frameY * frameHeight, frameWidth, frameHeight);
          Rectangle destinationRectangle = new Rectangle(400 + (frameWidth + 7)* i, 750, (int)(frameWidth * scale), (int)(frameHeight * scale));
          spritebatch.Draw(fruitTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      }
      
      
  }

  private static void DrawPlayer(SpriteBatch spriteBatch, Player player)
  {
    Texture2D playerTexture = player.Texture;
    int frameWidth = (int)playerTexture.Width / 6;
    int frameHeight = (int)playerTexture.Height / 4;
    int frameX = 2;
    int frameY = 0;
    float scale = 1.4f;
    for(int i = 0; i < player.Lives; i++)
    {
      Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameHeight,frameWidth,frameHeight);
      Rectangle destinationRectangle = new Rectangle(10 + (frameWidth + 7)* i, 765, (int)(frameWidth * scale), (int)(frameHeight * scale));
      spriteBatch.Draw(playerTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
    }
  }
}
