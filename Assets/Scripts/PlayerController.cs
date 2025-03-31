using UnityEngine;
using Mirror;
using System.Collections;
public class PlayerController : NetworkBehaviour
{
    public Rigidbody rb;
    private float Horizontal;
    private float Vertical;
    public float speed;
    public GameObject bombPrefab;
    public GameObject messagePrefab;
    public Animator anim;
    private bool canuseBomb;

    [SyncVar(hook = nameof(setColor))]
    public Color NetColor;
    public Material BaseMath;

    void Start()
    {
        canuseBomb = true;
        rb = gameObject.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        ComandSendMesage("entro: "+ this.name);
    }
    void Update()
    {
        if (!isLocalPlayer) return;
        if(Input.GetKeyDown(KeyCode.A)) newAnim(1);
        else if(Input.GetKeyDown(KeyCode.D)) newAnim(1);
        else if(Input.GetKeyDown(KeyCode.S)) newAnim(1);
        else if(Input.GetKeyDown(KeyCode.W)) newAnim(1);
        else newAnim(0);
        if(Input.GetKey(KeyCode.Space)) CommandSpawnbomb();
    }
    void FixedUpdate()
    {
        if (!isLocalPlayer) return;
         Vector3 direccion = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) direccion += Vector3.left;
        if (Input.GetKey(KeyCode.S)) direccion += Vector3.right;
        if (Input.GetKey(KeyCode.A)) direccion += Vector3.back;
        if (Input.GetKey(KeyCode.D)) direccion += Vector3.forward;
         if (direccion != Vector3.zero)
        {
            rb.AddForce(direccion.normalized * speed, ForceMode.Impulse);

            // Girar hacia la dirección del movimiento
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            rb.rotation = Quaternion.Slerp(rb.rotation, rotacionObjetivo, speed * Time.fixedDeltaTime);
        }
    }
    #region server
    [Command]
    private void CommandSetColor(Color newColor)
    {
        NetColor = newColor;
    }
    private void setColor(Color oldColor, Color newColor)
    {
        BaseMath.SetColor("MainColor",newColor);
    }
    public override void OnStartClient() 
    {
       //CommandSetColor(GameObject.FindObjectsByType<PlayerInfo>().color); 
       CommandSetColor(GameObject.FindFirstObjectByType<PlayerInfo>().color);
    }
    private void newAnim(int newInteger)
    {
        anim.SetInteger("Tansicion",newInteger);
    }

    [Command]
    private void CommandSpawnbomb()
    {
        if(!canuseBomb) return;
        canuseBomb = false;
        Vector3 bombPos = transform.position;
        GameObject Bomb = Instantiate(bombPrefab,transform.position,transform.rotation);
        NetworkServer.Spawn(Bomb);
        clientSetBomb(Bomb.GetComponent<BombManager>());
        StartCoroutine(bombDelay());
    }
    [ClientRpc]
    private void clientSetBomb(BombManager BM)
    {
        BM.StartBomb();
    }

    [Command] // esto es para servidor
    private void ComandSendMesage(string msj)
    {
        GameObject Message = Instantiate(messagePrefab);
        NetworkServer.Spawn(Message);
        clientSendMesage(msj, Message.GetComponent<messageCreate>());
    }

    [ClientRpc] // esto es para clientes
    private void clientSendMesage(string msj,messageCreate message)
    {
        message.instalize(msj,NetColor);
    }

    IEnumerator bombDelay()
    {
        yield return new WaitForSeconds(4);
        canuseBomb = true;
        yield return null;
    }
    public void getDamage()
    {
        
    }
    #endregion
}
