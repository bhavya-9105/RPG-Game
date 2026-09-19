using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected string enemyName;


    public void Update()
    {
        MoveAround();

        if (Input.GetKeyDown(KeyCode.F))
            Attack();
    }
    private void MoveAround()
    {
        Debug.Log(enemyName + " moves at speed" + moveSpeed);
    }


    private void Attack()
    {
        Debug.Log(enemyName + "Attacks");

    }

    public void TakeDamage()
    {

    }
}

