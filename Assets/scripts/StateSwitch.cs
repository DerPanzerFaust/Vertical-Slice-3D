using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _walkState;
    [SerializeField] private GameObject _surfState;

    private GameObject oldPos;
    private GameObject newPos;


    // True = Walking // False = Surfing 
    private bool _switch = true;

    private void Start()
    {
        _surfState.SetActive(false);
        _walkState.SetActive(true);
    }

    private void Update()
    {
        SwitchState();

        if (oldPos == _surfState)
        {
            oldPos.transform.position = new Vector3(newPos.transform.position.x, newPos.transform.position.y + -0.52f, newPos.transform.position.z);
            oldPos.transform.eulerAngles = newPos.transform.eulerAngles;
        }
        if (oldPos == _walkState)
        {
            oldPos.transform.position = new Vector3(newPos.transform.position.x, newPos.transform.position.y + 0.92f, newPos.transform.position.z);
        }
    }

    public void SwitchState()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            if (_switch == true) // Walking --> Surfing
            {
                GiveNewPos(_walkState, _surfState);
                _surfState.SetActive(true);
                _walkState.SetActive(false);

                _switch = false;
            }
            else if (_switch == false) // Surfing --> Walking
            {
                GiveNewPos(_surfState, _walkState);
                _surfState.SetActive(false);
                _walkState.SetActive(true);

                _surfState.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

                _switch = true;
            }
        }
    }

    public void GiveNewPos(GameObject AoldPos, GameObject AnewPos)
    {
        //Debug.Log(oldPos.transform.position); //-0.82
        //Debug.Log(newPos.transform.position);
        oldPos = AoldPos;
        newPos = AnewPos;
    }
}
