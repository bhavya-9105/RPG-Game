using UnityEngine;

public class ObjectToProtect : Entity
{
    protected override void Update()
    {
        HandleFlip();
    }

    protected override void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && facingRight == false)
            Flip();
        else if (rb.linearVelocity.x < 0 && facingRight == true)
            Flip();
    }
}
