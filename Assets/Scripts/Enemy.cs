using Unity.Collections.Tests.CoreCLR.TestJobs;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected string enemyName;
    [SerializeField] protected float moveSpeed;


    public void Update()
    {
     //   MoveAround();

        if (Input.GetKeyDown(KeyCode.F))
            Attack();
    }
    private void MoveAround()
    {
        Debug.Log(enemyName + " moves at speed" + moveSpeed);
    }


    protected virtual void Attack()
    {
        Debug.Log(enemyName + "Attacks");

    }

    public void TakeDamage()
    {

    }

    public string GetEnemyName()
    {
        return enemyName;
    }
}

