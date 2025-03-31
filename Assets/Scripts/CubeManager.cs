using UnityEngine;
using System.Collections;
using Mirror;

public class CubeManager : NetworkBehaviour
{
    private bool isActive;
    private MeshRenderer MR;
    private BoxCollider box;
    void Start()
    {
        isActive = true;
        MR = GetComponent<MeshRenderer>();
        box = GetComponent<BoxCollider>();
    }
    public void boxUpdate()
    {
        if(isActive)
        {
            StartCoroutine(Restart());
        }
    }
    [Command]
    private void setActive(bool valeu)
    {
        isActive= true;
    }
    IEnumerator Restart()
    {
        setActive(false);
        setEnable(false);
        float RandomNumber = Random.Range(15,30);
        yield return new WaitForSeconds(RandomNumber);
        setEnable(true);
        setActive(true);
        yield return null;
    }
    private void setEnable(bool enabled)
    {
        MR.enabled = enabled;
        box.enabled = enabled;
    }
}
