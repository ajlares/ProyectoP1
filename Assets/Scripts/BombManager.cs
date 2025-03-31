using UnityEngine;
using Mirror;
using System.Collections;
public class BombManager : NetworkBehaviour
{
    public ParticleSystem CharcheSistem, ExploiteSistem;
     public BoxCollider box;
    public void StartBomb()
    {
        StartCoroutine(BombSystem());
    }
    IEnumerator BombSystem()
    {
        CharcheSistem.Play();
        yield return new WaitForSeconds(3);
        CharcheSistem.Stop();
        box.enabled = true;
        yield return new WaitForSeconds(.1f);
        Instantiate(ExploiteSistem, transform.position, transform.rotation);
        Destroy(gameObject);
        yield return null;
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Box"))
        {
            Debug.Log("collide con box");
            other.GetComponent<CubeManager>().boxUpdate();
        }
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().getDamage();
        }
    }
}
