using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float MaxUseDist = 2f;
    [SerializeField] private float crouchingScale = 1.65f;

    public bool isCrouching = false;

    public GameObject keypadPanel;

    [SerializeField]
    private UI_Inventory uiInventory;
    private Inventory inventory;


    public void OnUse()
    {

    }

    public void Start()
    {
        inventory = new Inventory();
        uiInventory.setInventory(inventory);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, MaxUseDist);
            foreach (Collider collider in colliderArray)
            {

                if (collider.TryGetComponent(out Door door))
                {
                    Debug.Log(collider);
                    if (!door.isOpen)
                    {

                        door.Open(transform.position);
                    }
                    else
                    {

                        door.Close();
                    }
                    break;
                }
                else if (collider.tag == "Item")
                {
                    uiInventory.UpdateIventory(collider.name);
                    Destroy(collider);
                }


            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch(true);
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Crouch(false);
            isCrouching = false;
        }

    }


    void Crouch(bool crouch)
    {
        Player player = this.gameObject.GetComponent<Player>();
        Transform playerTransform = this.transform;
        float playerHeight = playerTransform.localScale.y;
        float playerPositionY = playerTransform.localPosition.y;

        if (crouch)
        {
            playerTransform.position = new Vector3(playerTransform.position.x, playerPositionY - playerHeight * (crouchingScale - 1) / crouchingScale, playerTransform.position.z);
            playerTransform.localScale = new Vector3(playerTransform.localScale.x, playerHeight / crouchingScale, playerTransform.localScale.z);
            player.setMovementSpeed(player.getMovementSpeed() / 2);
        }
        else
        {
            playerTransform.position = new Vector3(playerTransform.position.x, playerPositionY + playerHeight * (crouchingScale - 1) / crouchingScale, playerTransform.position.z);
            playerTransform.localScale = new Vector3(playerTransform.localScale.x, playerHeight * crouchingScale, playerTransform.localScale.z);
            player.setMovementSpeed(player.getMovementSpeed() * 2);
        }

    }


}
