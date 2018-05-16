using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileTypeException : System.Exception
{
    public TileTypeException() { }
    public TileTypeException(string message) : base(message) { }
    public TileTypeException(string message, System.Exception inner) : base(message, inner) { }
    protected TileTypeException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}