using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region Singleton
    public static PlayerMovement Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        pointCounter = FindObjectOfType<PointCounter>();
    }
    #endregion

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;

        rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        playerDeath = PlayerDeath.Instance;

        movementEmission = MovementFX.emission;
        thrustEmission = ThrustFX.emission;
    }

    void FixedUpdate()
    {
        if (PlayerDeath.Instance.IsDead()) { return; }
        Rotate();
        PushUp();
    }

    private void Rotate()
    {
        Vector2 velocity = rb.velocity;
        float ang = Mathf.Atan2(velocity.y, 10) * Mathf.Rad2Deg;
        transform.root.rotation = Quaternion.Euler(new Vector3(0, 0, ang - 90));
    }

    private void PushUp()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * gravity * upForce * Time.deltaTime, ForceMode2D.Force);
            thrustEmission.enabled = true;
        }
        else
        {
            thrustEmission.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerDeath.IsDead()) { return; }

        if (other.gameObject.tag == "PointCounter")
        {
            pointCounter.AddPoint(pointIncrease);
        }
    }

    public void StartEmission(bool start)
    {
        movementEmission.enabled = start;
        thrustEmission.enabled = start;
    }
}
