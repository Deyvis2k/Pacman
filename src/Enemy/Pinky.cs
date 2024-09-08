namespace Pacman;

public sealed class Pinky : Enemy
{
    private Vector2 lastMoveDirection;
    public Pinky(Texture2D texture) : base(texture)
    {
        Texture = texture;
        Position = GetPosition(10);
        lastMoveDirection = Vector2.Zero;
        LoadWalls();
        textureCountX = Texture.Width / 16;
        textureCountY = Texture.Height / 16;
        frameWidth = Texture.Width / textureCountX;
        frameHeight = Texture.Height / textureCountY;
        isInPrison = true;
        HunterMode = true;
        frameX = 1;
        frameY = 1;
    }

    public override void Update(GameTime gametime)
    {
        MinimumDistance();
        GetEaten();
        UpdateConditions(gametime);
        reversePosition(671);
    }

    public override void UpdateTextures(Vector2 BestMove)
    {
        if(BestMove == new Vector2(0, -1)) // up
        {
            frameX = 1;
            frameY = 1;
        }
        if(BestMove == new Vector2(0, 1)) //down
        {
            frameX = 1;
            frameY = 0;
        }
        if(BestMove == new Vector2(-1, 0)) //left
        {
            frameX = 1;
            frameY = 2;
        }
        if(BestMove == new Vector2(1, 0)) //right
        {
            frameX = 1;
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

        time += (float)gametime.ElapsedGameTime.TotalSeconds;
        if(HunterMode && time > 18)
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
            if(HasReached(GetPosition(7) - new Vector2(24,0))) isInPrison = false;

        if(IsEaten)
        {
            isInPrison = true;
            if(HasReached(GetPosition(10)))
            {
                IsScared = false;
                IsEaten = false;
            }
        }
    }

    public override Vector2 GetNewDirection()
    {
        if(IsEaten) return GetPosition(10);
        if(isInPrison) return GetPosition(7) - new Vector2(24,0);

        if(HunterMode)
        {
            Vector2 target = Player.Position + (Player._Path[Player._Direction] * 24 * 4);
            return target;
        }
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
                if(Vector2.Distance(Player.Position, position) > 500) return position;
        }


        return new Vector2(646,24);
    }

    private void MinimumDistance()
    {
        float minDistance = float.MaxValue;
        Vector2 bestMove = Vector2.Zero;
            
        foreach(Vector2 direction in _path)
        {
            if(!GhostCollide(direction) && !UpdateGates(direction))
            {
                Vector2 potentialPostion = Position + direction * Speed;
                Vector2 desiredPosition = GetNewDirection();
                float distance = Vector2.Distance(potentialPostion, desiredPosition);

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
        Position += bestMove * Speed;
        UpdateTextures(bestMove);
        lastMoveDirection = bestMove;
    }

    public override void Reset()
    {
        Position = GetPosition(10);
        LoadWalls();
        IsEaten = false;
        IsScared = false;
        isInPrison = true;
        HunterMode = true;
        time = 0;
        frameX = 1;
        frameY = 1;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = new Rectangle(frameX * frameWidth, frameY * frameHeight, frameWidth, frameHeight);
        Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)(frameWidth * scale), (int)(frameHeight * scale));
        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
    }

}
