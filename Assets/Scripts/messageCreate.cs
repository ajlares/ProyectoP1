using UnityEngine;
using TMPro;
using Mirror;

public class messageCreate : NetworkBehaviour
{
    public TextMeshPro info;
    public float Speed =0.05f;
    public float random;
    public float lifeTime = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this,lifeTime);
    }

void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x - Speed,transform.position.y,transform.position.z);
    }

    public void instalize(string msj, Color color)
    {
        info.text = msj;
        transform.position = new Vector3(8,3,0);
        Debug.Log("create");
    }
}
