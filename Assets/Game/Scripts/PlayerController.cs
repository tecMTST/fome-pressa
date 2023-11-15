using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMovingRight = false;
    private bool isMovingLeft = false;
    private int roomIndex = 7;
      

    [Header("Vagoes"), Tooltip("Aqui vao os coliders dos respectivos vagoes")]
    public Collider2D[] vagoes = new Collider2D[15];

    public CameraController cameraController;
    public MapController mapController;

    //�udio:
    private AudioSource audioSource;

    //Lista de SFX:
    [SerializeField] private AudioClip[] sfxPassos;

    //Vari�veis �udio:
    [SerializeField] private float moveSFX = 0.1f;
    private float timerPassos = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isMovingRight)
        {
            Vector3 newPosition = transform.position + Vector3.right * moveSpeed * Time.deltaTime;
            transform.position = newPosition;
            transform.rotation = Quaternion.identity;

            //SFX Passos:
            if (timerPassos <= 0f)
            {
                audioSource.PlayOneShot(sfxPassos[UnityEngine.Random.Range(0, sfxPassos.Length)]);
                timerPassos = moveSpeed * moveSFX;
            }
            timerPassos = timerPassos - Time.deltaTime;
        }
        else if (isMovingLeft) 
        {
            Vector3 newPosition = transform.position + Vector3.left * moveSpeed * Time.deltaTime;
            transform.position = newPosition;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            //SFX Passos:
            if (timerPassos <= 0f)
            {
                audioSource.PlayOneShot(sfxPassos[UnityEngine.Random.Range(0, sfxPassos.Length)]);
                timerPassos = moveSpeed * moveSFX;
            }
            timerPassos = timerPassos - Time.deltaTime;
        }
        else
        {
            //SFX Passos:
            timerPassos = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "metro")
        {
            roomIndex = other.GetComponent<RoomTrigger>().roomIndex;
            cameraController.SwitchRoom(roomIndex);
            mapController.SetPlayerMapPosition(roomIndex);
            mapController.RevealMap();
        }
    }

    public int GetRoomIndex()
    {
        return roomIndex;
    }

    public void StartMovingRight()
    {
        isMovingRight = true;
        isMovingLeft = false;
    }

    public void StopMovingRight()
    {
        isMovingRight = false;
    }

    public void StartMovingLeft()
    {
        isMovingLeft = true;
        isMovingRight = false;
    }

    public void StopMovingLeft()
    {
        isMovingLeft = false;
    }
}
