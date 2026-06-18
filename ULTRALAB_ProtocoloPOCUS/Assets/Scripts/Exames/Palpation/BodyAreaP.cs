using TMPro;
using UnityEngine;
using FMODUnity;

public class BodyAreaP : MonoBehaviour
{
    public enum BodyRegion
    {
        Hands,
        Elbows,
        Legs
    }

    public BodyRegion region;

    [TextArea]
    public string info;

    [Header("FMOD")]
    public EventReference clickSound;

    private Collider2D areaCollider;

    private void Awake()
    {
        areaCollider = GetComponent<Collider2D>();
    }

    public bool ContainsPoint(Vector2 worldPosition)
    {
        return areaCollider.OverlapPoint(worldPosition);
    }
}