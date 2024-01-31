using UnityEngine;

public interface IDamagable
{
    public void OnHit(int damage, Vector2 knockBack);
}