using System.Linq;

namespace Pacman;


public class PriorityQueue<T> 
{
    public SortedList<float, Queue<T>> elements = new SortedList<float, Queue<T>>();
    public int Count { get; private set; } = 0;

    public void Enqueue(T item, float priority)
    {
        if(!elements.ContainsKey(priority))
        {
            elements[priority] = new Queue<T>();
        }
        elements[priority].Enqueue(item);
        Count++;
    }

    public T Dequeue()
    {
        var firstElement = elements.First();
        var item = firstElement.Value.Dequeue();
        if(firstElement.Value.Count == 0)
        {
            elements.Remove(firstElement.Key);
        }
        Count--;
        return item;
    }

}

public sealed class PathFinder
{

    private HashSet<Rectangle> _walls = new HashSet<Rectangle>();

    public PathFinder()
    {
        LoadWalls();
    }
    public List<Vector2> AStar(Vector2 start, Vector2 end)
    {
        PriorityQueue<Node> openSet = new PriorityQueue<Node>();
        HashSet<Vector2> openSetHash = new HashSet<Vector2>();
        HashSet<Vector2> closedSet = new HashSet<Vector2>();

        Node startNode = new(start);
        Node endNode = new(end);

        openSet.Enqueue(startNode, 0);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet.Dequeue();
            openSetHash.Remove(currentNode.Position);

            if(currentNode.Position == end)
            {
                return ReconstructPath(currentNode);
            }

            closedSet.Add(currentNode.Position);

            foreach(Vector2 neighbor in GetNeighbors(currentNode.Position))
            {
                if(closedSet.Contains(neighbor)) continue;

                float tentativeGCost = currentNode.GCost + 1;
                Node neighborNode = new(neighbor)
                {
                    GCost = tentativeGCost,
                    HCost = Heuristic(neighbor, end),
                    Parent = currentNode
                };

                if(!openSetHash.Contains(neighbor))
                {
                    openSetHash.Add(neighbor);
                    openSet.Enqueue(neighborNode, neighborNode.FCost);
                }
                else if(tentativeGCost < neighborNode.GCost)
                {
                    neighborNode.GCost = tentativeGCost;
                    neighborNode.Parent = currentNode;
                    openSetHash.Remove(neighbor);
                    openSet.Enqueue(neighborNode, neighborNode.FCost);
                }
            }
        }

        return null;
    }

    private List<Vector2> ReconstructPath(Node node)
    {
        List<Vector2> path = new();

        while(node != null)
        {
            path.Add(node.Position);
            node = node.Parent;
        }
        path.Reverse();
        return path;
    }

    public float Heuristic(Vector2 start, Vector2 end)
    {
        return Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
    }


    private List<Vector2> GetNeighbors(Vector2 position)
    {
        List<Vector2> neighbors = new();

        Vector2[] directions = new[]
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(0, -1) 
        };

        foreach(Vector2 direction in directions)
        {
            Vector2 neighborPos = position  + direction;

            if(!GhostCollision(neighborPos))
            {
                neighbors.Add(neighborPos);
            }
        }

        return neighbors;
    }

    private void LoadWalls()
    {
        for(int i = 0; i < Mapa.map_height; i++)
        {
            for(int j = 0; j < Mapa.map_width; j++)
            {
                if(Mapa._mapa[i,j] == 1)
                {
                    Rectangle wallBounds = new Rectangle(j * 24, i * 24, 24, 24);
                    _walls.Add(wallBounds);
                }
            }
        }
    }

    private bool GhostCollision(Vector2 position)
    {
        int x = (int)position.X;
        int y = (int)position.Y;
        Rectangle ghostBounds = new Rectangle(x, y, 24, 24);

        if(x > 0 && x <= 670 && y > 0 && y <= 720)
        {
            foreach(Rectangle wall in _walls)
            {
                if(ghostBounds.Intersects(wall)) return true;
            }
        }

        return false;
    }
}
