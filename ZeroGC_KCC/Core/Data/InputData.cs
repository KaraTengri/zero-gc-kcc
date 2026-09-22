using UnityEngine;
public struct InputData
{
    public Vector2 MoveInput;
    public bool JumpRequested;

    public void Reset()
    {
        MoveInput = Vector2.zero;
        JumpRequested = false;
    }
}