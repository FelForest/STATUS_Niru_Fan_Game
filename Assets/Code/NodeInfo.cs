using UnityEngine;
[System.Serializable]
public struct NodeInfo
{
    public NodeDirection direction;
    public float time;
}

public enum NodeDirection
{
    Up,
    Down,
    Left,
    Right
}