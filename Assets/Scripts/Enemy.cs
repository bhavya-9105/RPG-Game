using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    
    [SerializeField] private float RedColorDuration = 1; 
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        sr.color = Color.red;

        Invoke(nameof(TurnWhite), RedColorDuration);
    }


    private void TurnWhite()
    {
        sr.color = Color.white;
    }

}
