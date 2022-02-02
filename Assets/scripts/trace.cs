using System.Collections;
using UnityEngine;

public class trace : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem muzzleflash;
    public GameObject impacteffect;
    public float firerate = 5f;
    public Transform Target;
    public float Speed = 1f;
    public ParticleSystem rangebox;
    public bool turnalwd = true;
    private float nextshot = 5f;
    private Coroutine LookCoroutine;
    public GameObject LR;

    public void StartRotating()
    {
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        if (turnalwd == true)
            if (turnalwd == true)
            {
                Quaternion lookRotation = Quaternion.LookRotation(Target.position - transform.position);

                float time = 0;
                LR.SetActive(true);


                while (time < 1)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

                    time += Time.deltaTime * Speed;

                    yield return null;
                }

            }
    }

    IEnumerator Shooter()
    {
        yield return new WaitForSeconds(4);
        turnalwd = false;
        print("4 sec");
        if (Time.time >= nextshot)
            LR.SetActive(false);
        print("4 sec");
        if (Time.time >= nextshot)
        {
            nextshot = Time.time + 1f / firerate;
            shoot();
        }
        yield return new WaitForSeconds(3f);
        Debug.Log("2sec");
        turnalwd = true;
        StopAllCoroutines();

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            Debug.Log("a");
        }
    }
    void shoot()
    {
        turnalwd = false;
        Debug.Log("shhots fired");

        RaycastHit hit;
        if (UnityEngine.Physics.Raycast(rangebox.transform.position, rangebox.transform.forward, out hit, range))
        LR.SetActive(false);
        Debug.Log("shhots fired");

        RaycastHit hit1;
        if (Physics.Raycast(rangebox.transform.position, rangebox.transform.forward, out hit, range))
        {
            muzzleflash.Play();

            Debug.Log(hit.transform.name);

            GameObject impactGO = Instantiate(impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

        else
        {
            muzzleflash.Play();


            // Target target = hit.transform.GetComponent<Target>();



            // Target target = hit.transform.GetComponent<Target>();

            GameObject impactGO = Instantiate(impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("enterd");
            StartRotating();


        }
    }


    private void OnTriggerStay(Collider other)
    {
        StartCoroutine(Shooter());
        if (other.CompareTag("Player"))
        {

            StartCoroutine(Shooter());
            if (other.CompareTag("Player"))
            {
                StartRotating();
            }
        }

    }
}