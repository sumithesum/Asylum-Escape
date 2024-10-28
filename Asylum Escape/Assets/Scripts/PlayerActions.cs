using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private float MaxUseDist = 2f;

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
                
                if (collider.TryGetComponent(out Door dor))
                {
                    Debug.Log(collider);
                    if (!dor.isOpended)
                    {
                       
                        dor.open(transform.position);
                    }
                    else {
                      
                        dor.close();
                    }
                }
              
            }
        }
    }
}
