using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpended = false;
    [SerializeField]
    private bool isRotatingDoor = true;
    [SerializeField]
    private float speed = 1f;
    [Header("Rotation Config")]
    [SerializeField]
    private float rotationAmount = 90f;
    [SerializeField]
    private float fowardDirection = 0f;

    private Vector3 startRotation;
    private Vector3 foward;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        startRotation = transform.rotation.eulerAngles;
        // "foward" e defatp spre frame , deci alegem noi un foward (directie)
        foward = transform.right;
    }
    public void open(Vector3 UserPositon)
    {
        if (!isOpended)
        {
            if (AnimationCoroutine != null)
                StopCoroutine(AnimationCoroutine);
            if (isRotatingDoor)
            {
                float dot = Vector3.Dot(foward, (UserPositon - transform.position).normalized);
                Debug.Log($"Dot: {dot.ToString("N3")}");
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }


        }
    }
    private IEnumerator DoRotationOpen(float forawrdAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;
        if (forawrdAmount >= fowardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y - rotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y + rotationAmount, 0));
        }

        isOpended = true;
        float time = 0f;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null; //wait next frame
            time += Time.deltaTime * speed;
        }
    }

    public void close()
    {
        if (isOpended)
        {
            if (AnimationCoroutine != null)
                StopCoroutine(AnimationCoroutine);
            if (isRotatingDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startrotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation);

        isOpended = false;
        float time = 0f;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startrotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}