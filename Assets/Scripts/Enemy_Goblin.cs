using UnityEngine;

public class Enemy_Goblin : Enemy
{
    private void Awake()
    {
        moveSpeed = 10;
    }
    [ContextMenu("Steal Gold!")]
    private void StealMoney()
    {
        Debug.Log(enemyName + " Steal the money.");    
    }
}
