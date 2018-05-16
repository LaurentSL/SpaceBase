using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile
{
    /// <summary>
    /// TileType is the base type of the tile.
    /// In some tile-based games, that might be the terrain type. 
    /// For us, we only need to differentiate between empty space and floor 
    /// (a.k.a. the station structure/scaffold). Walls/Doors/etc... will be
    /// InstalledObjects sittong on top of the floor.
    /// </summary>
    public enum TileType { Empty, Floor };

    /// <summary>
    /// The type of the Tile.
    /// </summary>
    private TileType type = TileType.Empty;
    public TileType Type
    {
        get { return type; }
        set
        {
            oldType = type;
            type = value;
            if (tileTypeChangedCallback != null)
            {
                if (oldType != type)
                {
                    tileTypeChangedCallback(this);
                }
            }
            else
            {
                Debug.LogWarning("TileType.set - No callback defined.");
            }
        }
    }


    /// <summary>
    /// LooseObject is something like a drill or a stack of metal sitting on the floor.
    /// </summary>
    private LooseObject looseObject;
    /// <summary>
    /// InstalledObject is something like a wall, door, or sofa.
    /// </summary>
    private InstalledObject installedObject;

    // We need to knox the context in which we exist. Probably. Maybe.
    private World world;
    public int X { get; protected set; }
    public int Y { get; protected set; }

    /// <summary>
    /// The function we callback any time our type changes
    /// </summary>
    System.Action<Tile> tileTypeChangedCallback;

    // Use in TileType.Type.set (Garbage collection optimization)
    private TileType oldType;


    /// <summary>
    /// Initializes a new instance of the <see cref="Tile"/> class.
    /// </summary>
    /// <param name="world">A World instance.</param>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// Register a function to be called back when our tile type changes.
    /// </summary>
    /// <param name="callback">The callback function.</param>
    public void RegisterTileTypeChangedCallback(System.Action<Tile> callback)
    {
        tileTypeChangedCallback += callback;
    }

    /// <summary>
    /// Unregiser a callback.
    /// </summary>
    /// <param name="callback">The callback function.</param>
    public void UnregisterTileTypeChangedCallback(System.Action<Tile> callback)
    {
        tileTypeChangedCallback -= callback;
    }
}