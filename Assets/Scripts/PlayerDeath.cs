using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] float timeTillRestart = 2f;

    private Rigidbody2D rb;
    private Vector2 startPosition;
    private PointCounter pointCounter;

    private bool Dead = false;

    #region Singleton
    public static PlayerDeath Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
        pointCounter = FindObjectOfType<PointCounter>();
    }
    #endregion

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Obstacle")
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (Dead) { return; }
        Dead = true;

        PlayerMovement.Instance.StartEmission(false);

        Explode();

        StartCoroutine(Restart());
    }

    private void Explode()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(Vector2.right * 100);
        rb.AddTorque(400f);
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(timeTillRestart);
        ReloadScene();
        pointCounter.RestartPoints();

        Dead = false;
    }

    private void SetStartPosition()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = startPosition;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    private void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public bool IsDead()
    {
        return Dead;
    }
}
