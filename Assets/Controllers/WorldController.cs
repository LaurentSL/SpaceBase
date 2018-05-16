using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance { get; protected set; }


    /// <summary>
    /// The only tile sprite we have right now, so this
    /// it a pretty simple way to handle it.
    /// </summary>
    public Sprite floorSprite;

    /// <summary>
    /// The world and tile data.
    /// </summary>
    public World World { get; protected set; }

    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("There should never be two world controllers.");
        }
        Instance = this;

        // Create a world with Empty tiles
        World = new World();

        // Create a GameObject for each of our tiles, so they show visually. (and redunt reduntantly).
        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                Tile tile = World.GetTileAt(x, y);

                GameObject gameObject = new GameObject();
                gameObject.name = "Tile_" + x + "_" + y;
                gameObject.transform.position = new Vector3(tile.X, tile.Y, 0);
                gameObject.transform.SetParent(transform, true); //.parent = transform;
                gameObject.AddComponent<SpriteRenderer>();

                tile.RegisterTileTypeChangedCallback((my_tile) => { OnTileTypeChanged(my_tile, gameObject); });
            }
        }

        World.RandomizeTiles();
    }

    void Update()
    {
    }

    void OnTileTypeChanged(Tile tile, GameObject gameObject)
    {
        if (tile.Type == Tile.TileType.Floor)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if (tile.Type == Tile.TileType.Empty)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile type " + tile.Type);
            throw new TileTypeException("OnTileTypeChanged - Unrecognized tile type: " + tile.Type);
        }
    }
}
