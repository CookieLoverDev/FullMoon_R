using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    [SerializeField] GameObject ShopGameObject;
    [SerializeField] KeyCode[] toggleInventoryKeys;
    void Update()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                ShopGameObject.SetActive(!ShopGameObject.activeSelf);

                if (ShopGameObject.activeSelf)
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
        ShopGameObject.SetActive(false);
        // HideMouseCursor ();
    }

    public void CloseAndOpenShop()
    {
        if (!ShopGameObject.activeSelf)
        {
            ShopGameObject.SetActive(true);
        }
        else
        {
            ShopGameObject.SetActive(false) ;
        }
    }
    public void ShowInventoryButton()
    {
        ShopGameObject.SetActive(true);
        // ShowMouseCursor();
    }
}
