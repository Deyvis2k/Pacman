namespace Pacman;


public sealed class Inky : Enemy
{
    private Vector2 lastMoveDirection;
    private Blinky _blinky;
    public Inky(Texture2D texture, Blinky blinky) : base (texture)
    {
        Texture = texture;
        Position = GetPosition(9);
        lastMoveDirection = Vector2.Zero;
        LoadWalls();
        textureCountX = Texture.Width / 16;
        textureCountY = Texture.Height / 16;
        frameWidth = Texture.Width / textureCountX;
        frameHeight = Texture.Height / textureCountY;
        isInPrison = true;
        HunterMode = true;
        _blinky = blinky;
        frameX = 2;
        frameY = 1;
    }

    public override void Update(GameTime gametime)
    {
        MinimumDistance();
        GetEaten();
        UpdateConditions(gametime);
        reversePosition(671);
    }

    public override void UpdateConditions(GameTime gametime)
    {
        time+= (float)gametime.ElapsedGameTime.TotalSeconds;
        if(HunterMode && time > 20)
        {
            time = 0;
            HunterMode = false;
        }
        else if (!HunterMode && time < 14)
        {
            time = 0;
            HunterMode = true;
        }
        scaredTime -= (float)gametime.ElapsedGameTime.TotalSeconds;
        if(IsScared && scaredTime < 0) IsScared = false;
        if(IsScared) HunterMode = false;
        if(isInPrison && HasReached(GetPosition(7) - new Vector2(24,0))) isInPrison = false;
        if(IsEaten)
        {
            isInPrison = true;
            if(HasReached(GetPosition(8)))
            {
                IsScared = false;
                IsEaten = false;
            }
        }
    }

    public override void UpdateTextures(Vector2 BestMove)
    {
        if(BestMove == new Vector2(0, -1)) // up
        {
            frameX = 2;
            frameY = 1;
        }
        if(BestMove == new Vector2(0, 1)) //down
        {
            frameX = 2;
            frameY = 0;
        }
        if(BestMove == new Vector2(-1, 0)) //left
        {
            frameX = 2;
            frameY = 2;
        }
        if(BestMove == new Vector2(1, 0)) //right
        {
            frameX = 2;
            frameY = 3;
        }

        if(IsScared)
        {
            frameX = 4;
            frameY = 0;
        }

        if(IsEaten)
        {
            if(BestMove == new Vector2(0, -1)) // up
            {
                frameX = 5;
                frameY = 1;
            }
            if(BestMove == new Vector2(0, 1)) //down
            {
                frameX = 5;
                frameY = 0;
            }
            if(BestMove == new Vector2(-1, 0)) //left
            {
                frameX = 5;
                frameY = 2;
            }
            if(BestMove == new Vector2(1, 0)) //right
            {
                frameX = 5;
                frameY = 3;
            }
        }
    }

    public override Vector2 GetNewDirection()
    {
         if(IsEaten) return GetPosition(8);
         if(isInPrison) return GetPosition(7) - new Vector2(24,0);

         List<Vector2> positions = new List<Vector2>()
         {
            new Vector2(24,24),
            new Vector2(24,696),
            new Vector2(646,24),
            new Vector2(646,696)
         };

         if(IsScared)
         {
             foreach(Vector2 position in positions) 
                 if(Vector2.Distance(Player.Position, position) > 500) return position;
         }

         if(HunterMode)
         {
             Vector2 PlayerTarget = Player.Position + Player._Path[Player._Direction] * 48;
             Vector2 desiredTarget = PlayerTarget + (PlayerTarget - _blinky.Position);
             return desiredTarget;
         }
         return new Vector2(646,696);
    }

    private void MinimumDistance()
    {
        float minDistance = float.MaxValue;
        Vector2 BestMove = Vector2.Zero;

        foreach(Vector2 direction in _path)
        {
            if(!GhostCollide(direction) && !UpdateGates(direction))
            {
                Vector2 desiredLocation = GetNewDirection();
                Vector2 potentialPostion = Position + direction * Speed;
                float distance = Vector2.Distance(potentialPostion, desiredLocation);
                if(distance < minDistance && direction != - lastMoveDirection)
                {
                    minDistance = distance;
                    BestMove = direction;
                }
            }
        }
        if(BestMove == Vector2.Zero)
        {
            foreach(Vector2 direction in _path)
            {
                if(!GhostCollide(direction) && !UpdateGates(direction))
                {
                    BestMove = direction;
                    break;
                }
            }
        }
        UpdateTextures(BestMove);
        Position += BestMove * Speed;
        lastMoveDirection = BestMove;
    }

    public override void Reset()
    {
        Position = GetPosition(9);
        LoadWalls();
        IsScared = false;
        IsEaten = false;
        isInPrison = true;
        HunterMode = true;
        frameX = 2;
        frameY = 1;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameHeight, frameWidth, frameHeight);
        Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)(frameWidth * scale), (int)(frameHeight * scale));
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
    }
}
