using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] KeyCode[] toggleInventoryKeys;
    void Update()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);

                if (inventoryGameObject.activeSelf)
                {
                    // ShowMouseCursor();
                }
                else
                {
                    // HideMouseCursor();   
                }
                break;
            }
        }
    }


    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CloseInventoryButton()
    {
        inventoryGameObject.SetActive(false);
        // HideMouseCursor ();
    }

    public void ShowInventoryButton()
    {
        inventoryGameObject.SetActive(true);
        // ShowMouseCursor();
    }
}
