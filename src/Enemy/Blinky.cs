namespace Pacman;


//1.5 sprite scale
public sealed class Blinky: Enemy
{
    private Vector2 lastMoveDirection;
    public Blinky(Texture2D texture) : base(texture)
    {
        Texture = texture;
        Position = GetPosition(7);
        lastMoveDirection = Vector2.Zero;
        LoadWalls();
        textureCountX = Texture.Width / 16;
        textureCountY = Texture.Height / 16;
        frameWidth = Texture.Width / textureCountX;
        frameHeight = Texture.Height / textureCountY;
        frameX = 0;
        frameY = 2;
        isInPrison = false;
    }

    public override void Update(GameTime gametime)
    {

        MinimumDistance();
        UpdateConditions(gametime);
        GetEaten();
        reversePosition(671);
    }

    public override void UpdateTextures(Vector2 BestMove)
    {
        if(BestMove == new Vector2(0, -1)) // up
        {
            frameX = 0;
            frameY = 1;
        }
        if(BestMove == new Vector2(0, 1)) //down
        {
            frameX = 0;
            frameY = 0;
        }
        if(BestMove == new Vector2(-1, 0)) //left
        {
            frameX = 0;
            frameY = 2;
        }
        if(BestMove == new Vector2(1, 0)) //right
        {
            frameX = 0;
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
    public override void UpdateConditions(GameTime gametime)
    {
        time+= (float)gametime.ElapsedGameTime.TotalSeconds;
        if(HunterMode && time > 20)
        {
            time = 0;
            HunterMode = false;
        }
        else if(!HunterMode && time > 14)
        {
            time = 0;
            HunterMode = true;
        }
        if(IsScared) HunterMode = false;
        scaredTime -= (float)gametime.ElapsedGameTime.TotalSeconds;
        if(IsScared && scaredTime < 0.0f) IsScared = false;

        if(isInPrison)
            if(HasReached(GetPosition(7))) isInPrison = false;

        if(IsEaten)
        {
            isInPrison = true;
            if(HasReached(GetPosition(9)))
            {
                IsEaten = false;
                IsScared = false;
            }
        }
    }
    public override Vector2 GetNewDirection()
    {
        if(IsEaten) return GetPosition(9);
        List<Vector2> positions = new List<Vector2>()
        {
            new Vector2(24,24),
            new Vector2(646,696),
            new Vector2(24,696),
            new Vector2(646,24)
        };
        if(IsScared) 
        {
            foreach(Vector2 position in positions)
            {
                if(Vector2.Distance(Player.Position, position) > 500) return position;
            }
        }


        if(isInPrison) return GetPosition(7);

        return HunterMode ? Player.Position : new Vector2(24,24);
    }

    private void MinimumDistance()
    {
        Vector2 target = Player.Position - Position;
        float minDistance = float.MaxValue;
        Vector2 bestMove = Vector2.Zero;
            
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
                    bestMove = direction;
                }
            }
        }
        if(bestMove == Vector2.Zero)
        {
            foreach(Vector2 direction in _path)
            {
                if(!GhostCollide(direction) && !UpdateGates(direction))
                {
                    bestMove = direction;
                    break;
                }
            }
        }
        UpdateTextures(bestMove);
        Position += bestMove * Speed;
        lastMoveDirection = bestMove;
    }

    public override void Reset()
    {
        Position = GetPosition(7);
        LoadWalls();
        IsEaten = false;
        IsScared = false;
        isInPrison = false;
        HunterMode = false;
        frameX = 0;
        frameY = 2;
    }


    public override void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameHeight, frameWidth, frameHeight);
        Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)(frameWidth * scale), (int)(frameHeight * scale));
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
    }
}
