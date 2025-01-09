using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject neightboor = null;
    public bool isOpen = false;
    [SerializeField]
    private bool isRotatingDoor = true;
    [SerializeField]
    private float speed = 1f;
    [Header("Rotation Config")]
    [SerializeField]
    private float rotationAmount = 90f;
    [SerializeField]
    private Vector3 inwardDirection = Vector3.forward;
    private Vector3 startRotation;

    private Coroutine animationCoroutine;

    private void Awake()
    {
        startRotation = transform.rotation.eulerAngles;
        Transform parent = transform.parent;

        if (parent != null)
        {
            
            foreach (Transform sibling in parent)
            {
                
                if (sibling != transform && !sibling.gameObject.name.ToLower().EndsWith("frame") )
                {
                    neightboor = sibling.gameObject;

                }
            }
        }


    }

    public void Open(Vector3 userPosition)
    {
        if (!isOpen)
        {
            if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);

            if (isRotatingDoor)
            {
                // Determine if the user is inside or outside
                Vector3 worldInteriorDirection = transform.TransformDirection(inwardDirection);
                Vector3 userDirection = (userPosition - transform.position).normalized;
                float dot = Vector3.Dot(worldInteriorDirection, userDirection);

                animationCoroutine = StartCoroutine(DoRotationOpen(dot > 0)); 
            }
            if (neightboor != null)
                neightboor.GetComponent<Door>().Open(userPosition);
        }
    }

    private IEnumerator DoRotationOpen(bool openInward)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (openInward)
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.eulerAngles.y - rotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.eulerAngles.y + rotationAmount, 0));
        }

        isOpen = true;
        float time = 0f;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null; // Wait for the next frame
            time += Time.deltaTime * speed;
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);
            if (isRotatingDoor)
            {
                animationCoroutine = StartCoroutine(DoRotationClose());
            }
            if (neightboor != null)
                neightboor.GetComponent<Door>().Close();
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(this.startRotation);

        isOpen = false;
        float time = 0f;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}
