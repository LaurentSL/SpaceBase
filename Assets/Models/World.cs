using UnityEngine;

public class World
{
    private Tile[,] tiles;
    private int width;
    private int height;

    public int Width { get { return width; } }
    public int Height { get { return height; } }


    public World(int width = 100, int height = 100)
    {
        this.width = width;
        this.height = height;
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }
    }

    public Tile GetTileAt(int x, int y)
    {
        try
        {
            return tiles[x, y];
        }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.LogError("Tile (" + x + ", " + y + ") is out of range!");
            throw e;
        }
    }

    public void RandomizeTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Random.Range(0,2)==0)
                {
                    tiles[x, y].Type = Tile.TileType.Empty;
                }
                else
                {
                    tiles[x, y].Type = Tile.TileType.Floor;
                }
            }
        }
    }

}
