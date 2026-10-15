using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Entity player;
    private void Awake()
    {
        player = GetComponentInParent<Entity>();
    }

    public void DamageEnemies() => player.DamageEnenmies();
    private void DisableMovementAndJump() => player.EnableMovementAndJump(false);

    private void EnableMovementAndJump() => player.EnableMovementAndJump(true); //another way to write a single line of function
}
