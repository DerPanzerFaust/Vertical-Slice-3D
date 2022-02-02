using System.Collections;
using UnityEngine;

public class Lazerdingus : MonoBehaviour
{
    public LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();

    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
              

            if (hit.collider.tag == "Player")
            {
                lr.SetPosition(1, new Vector3(0, 0, hit.distance));
                Debug.Log("triggger 2");

            }
            else
            {
                lr.SetPosition(1, new Vector3(0, 0, 3000));
            }



        }

    }

}



