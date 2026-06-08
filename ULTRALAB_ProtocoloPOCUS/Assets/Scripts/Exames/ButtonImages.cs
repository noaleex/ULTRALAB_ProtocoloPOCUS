using UnityEngine;
using UnityEngine.UI;

public class ButtonImages : MonoBehaviour
{
    void Start()
    {
        // 0.1f significa que partes com mais de 10% de opacidade aceitam o clique
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f; 
    }
}
