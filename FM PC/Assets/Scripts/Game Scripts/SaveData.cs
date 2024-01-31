using System;

[Serializable]
public class SaveData
{
    public float currentHealth;
    public float[] position;

    public SaveData(Player player)
    {
        currentHealth = PlayerHealthSystem.currentHealth;
        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
