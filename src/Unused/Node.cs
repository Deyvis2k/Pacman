namespace Pacman;

public class Node
{
    public Vector2 Position { get; set; }
    public Node Parent { get; set; }
    public float GCost { get; set; } 
    public float HCost { get; set; }
    public float FCost => GCost + HCost;

    public Node(Vector2 position)
    {
        this.Position = position;
        this.Parent = null;
        this.GCost = 0;
        this.HCost = 0;
    }
}
