using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement inputs")]
    [SerializeField] private float upForce = 1500f;
    [SerializeField] private ParticleSystem movementFX;
    [SerializeField] private ParticleSystem thrustFX;
    [SerializeField] private Vector3 initialPosition = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 initialRotation = new Vector3(0, 0, -90);

    [Header("Death inputs")]
    [SerializeField] private float timeTillRestart = 2f;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private ParticleSystem explosionFX;

    private int pointIncrease = 1;

    private Rigidbody2D rb;
    private float gravity;

    private bool isMainMenu = true;
    private bool isDead = false;

    private PointCounter pointCounter;
    private AudioManager audioManager;

    private ParticleSystem.EmissionModule movementEmission;
    private ParticleSystem.EmissionModule thrustEmission;

    private const string MAIN_MENU = "MainMenu";
    private const string THRUST_SFX = "ThrustSFX";
    private const string EXPLOSION_SFX = "ExplosionSFX";
    private const string POINT_COUNTER_TAG = "PointCounter";
    private const string GROUND_TAG = "Ground";
    private const string OBSTACLE_TAG = "Obstacle";

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoading;
        FreezePositions();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoading;
    }

    private void OnLevelLoading(Scene scene, LoadSceneMode mode)
    {
        isMainMenu = IsMainMenu(scene);
        ReturnToInitialPosition();
        GetData();
        FreezePositions();
        EnableGameOverCanvas(false);
        RestartPoints();
        StartEmissions();
    }

    private bool IsMainMenu(Scene scene)
    {
        if (scene.name.Equals(MAIN_MENU))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ReturnToInitialPosition()
    {
        gameObject.transform.position = initialPosition;
        gameObject.transform.rotation = Quaternion.Euler(initialRotation);
    }

    private void GetData()
    {
        pointCounter = PointCounter.Instance;
        audioManager = AudioManager.Instance;
    }

    private void GetRigidbody()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
    }

    private void FreezePositions()
    {
        GetRigidbody();
        if (isMainMenu)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    private void EnableGameOverCanvas(bool enable)
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(enable);
        }
    }

    private void RestartPoints()
    {
        if (pointCounter != null)
        {
            pointCounter.RestartPoints();
        }
    }

    private void StartEmissions()
    {
        movementEmission = movementFX.emission;
        thrustEmission = thrustFX.emission;

        movementEmission.enabled = true;
    }

    void FixedUpdate()
    {
        if (isMainMenu || isDead) { return; }
        Rotate();
        PushUp();
    }

    private void Rotate()
    {
        Vector2 velocity = rb.velocity;
        float ang = Mathf.Atan2(velocity.y, 10) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, ang - 90));
    }

    private void PushUp()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * gravity * upForce * Time.deltaTime, ForceMode2D.Force);
            thrustEmission.enabled = true;
            audioManager.PlayOneShot(THRUST_SFX);
        }
        else
        {
            thrustEmission.enabled = false;
            audioManager.Stop(THRUST_SFX);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pointCounter == null || isDead) { return; }
        if (other.gameObject.tag == POINT_COUNTER_TAG)
        {
            pointCounter.AddPoint(pointIncrease);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == GROUND_TAG
        || other.gameObject.tag == OBSTACLE_TAG)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (isDead) { return; }
        isDead = true;
        
        EnableAllEmissions(false);
        Explode();
        StartCoroutine(Restart());
    }

    private void EnableAllEmissions(bool enable)
    {
        movementEmission.enabled = enable;
        thrustEmission.enabled = enable;
    }

    private void Explode()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        audioManager.PlayOneShot(EXPLOSION_SFX);
        audioManager.Stop(THRUST_SFX);
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(timeTillRestart);
        Time.timeScale = 0;

        EnableGameOverCanvas(true);

        isDead = false;
        EnableAllEmissions(true);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
