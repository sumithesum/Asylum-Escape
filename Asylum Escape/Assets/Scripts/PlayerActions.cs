using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float MaxUseDist = 2f;
    [SerializeField] private float crouchingScale = 1.65f;

    private bool isCrouching = false;

    public GameObject keypadPanel;
    public void OnUse()
    {

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

            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Crouch(false);
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
