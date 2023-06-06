using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryDisplay : MonoBehaviour
{
    bool displayInventory = false;
    Rect inventoryWindowRect;
    private Vector2 inventoryWindowSize = new Vector2(150, 150);
    Vector2 inventoryItemIconSize = new Vector2(130, 32);

    float offsetX = 6;
    float offsetY = 6;

    private void Awake() {
        inventoryWindowRect = new Rect(
            Screen.width - inventoryWindowSize.x,
            Screen.height - 40 - inventoryWindowSize.y,
            inventoryWindowSize.x,
            inventoryWindowSize.y);
    }

    private void OnGUI() {
        if(GUI.Button(
            new Rect(
                Screen.width - 40,
                Screen.height - 40,
                40,
                40),
                "INV"
        )) {
            displayInventory = !displayInventory;
        }
        if(displayInventory) {
            inventoryWindowRect = GUI.Window(
                0,
                inventoryWindowRect,
                DisplayInventoryWindow,
                "Inventory");

            inventoryWindowSize = new Vector2(
                inventoryWindowRect.width,
                inventoryWindowRect.height);
        }
    }

    private void DisplayInventoryWindow(int windowID)
    {
        var currentX = 0 + offsetX;
        var currentY = 18 + offsetY;
        foreach(var item in GameState.CurrentPlayer.Inventory) {
            Rect texcoords = item.sprite.textureRect;
            texcoords.x /= item.sprite.texture.width;
            texcoords.y /= item.sprite.texture.height;
            texcoords.width /= item.sprite.texture.width;
            texcoords.height /= item.sprite.texture.height;

            GUI.DrawTextureWithTexCoords(new Rect(
                currentX,
                currentY,
                item.sprite.textureRect.width,
                item.sprite.textureRect.height),
                item.sprite.texture,
                texcoords);

                currentX += inventoryItemIconSize.x;
                if(currentX + inventoryItemIconSize.x + offsetX > 
                    inventoryItemIconSize.x) {
                        currentX = offsetX;
                        currentY += inventoryItemIconSize.y;
                        if(currentY + inventoryItemIconSize.y + offsetY >
                            inventoryWindowSize.y) {
                                return;
                            }
                    }
        }
    }
}
