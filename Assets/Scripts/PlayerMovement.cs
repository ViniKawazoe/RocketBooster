using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float upForce = 1500f;
    [SerializeField] private ParticleSystem MovementFX;
    [SerializeField] private ParticleSystem ThrustFX;

    private ParticleSystem.EmissionModule movementEmission;
    private ParticleSystem.EmissionModule thrustEmission;

    private Rigidbody2D rb;
    private float gravity;
    private int pointIncrease = 1;

    private PlayerDeath playerDeath;
    private PointCounter pointCounter;
    private AudioManager audioManager;

    // private string sceneName;
    private bool isMainMenu = true;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoading;
        FreezePositions();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoading;
    }

    void OnLevelLoading(Scene scene, LoadSceneMode mode)
    {
        // sceneName = scene.name;
        if (scene.name.Equals("MainMenu"))
        {
            isMainMenu = true;
        }
        else
        {
            isMainMenu = false;
        }
        GetData();
        FreezePositions();
    }

    private void GetData()
    {
        playerDeath = gameObject.GetComponent<PlayerDeath>();
        pointCounter = PointCounter.Instance;
        audioManager = AudioManager.Instance;
    }

    void Start()
    {
        // rb = gameObject.GetComponent<Rigidbody2D>();
        // playerDeath = gameObject.GetComponent<PlayerDeath>();
        // pointCounter = PointCounter.Instance;
        // audioManager = AudioManager.Instance;

        // sceneName = SceneManager.GetActiveScene().name;

        // gravity = rb.gravityScale;

        // FreezePositions();

        movementEmission = MovementFX.emission;
        thrustEmission = ThrustFX.emission;

        movementEmission.enabled = true;
    }

    private void FreezePositions()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
        // if (sceneName.Equals("MainMenu"))
        // if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        if (isMainMenu)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    void FixedUpdate()
    {
        // if (sceneName.Equals("MainMenu")) { return; }
        if (isMainMenu) { return; }
        if (playerDeath.IsDead()) { return; }
        Rotate();
        PushUp();
    }

    private void Rotate()
    {
        Vector2 velocity = rb.velocity;
        float ang = Mathf.Atan2(velocity.y, 10) * Mathf.Rad2Deg;
        // transform.root.rotation = Quaternion.Euler(new Vector3(0, 0, ang - 90));
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, ang - 90));
    }

    private void PushUp()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * gravity * upForce * Time.deltaTime, ForceMode2D.Force);
            thrustEmission.enabled = true;
            audioManager.PlayOneShot("ThrustSFX");
        }
        else
        {
            thrustEmission.enabled = false;
            audioManager.Stop("ThrustSFX");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pointCounter == null) { return; }
        if (playerDeath.IsDead()) { return; }
        
        if (other.gameObject.tag == "PointCounter")
        {
            pointCounter.AddPoint(pointIncrease);
        }
    }

    public void EnableEmission(bool enable)
    {
        movementEmission.enabled = enable;
        thrustEmission.enabled = enable;
    }
}
