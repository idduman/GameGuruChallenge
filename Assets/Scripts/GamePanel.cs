using System.Collections;
using System.Collections.Generic;
using GameGuruChallenge;
using UnityEngine;

public abstract class GamePanel : MonoBehaviour , IGamePanel
{
    public abstract Vector3 GetNormalizedPosition();
    public abstract void SetNormalizedPosition(Vector3 position);

    public abstract Vector3 GetSize();

    public abstract void SetSize(Vector3 size);
    public abstract Vector3 GetNormalizedSize();
    public abstract void SetNormalizedSize(Vector3 size);
    public abstract void Stretch(float top, float bottom, float left, float right);
}
