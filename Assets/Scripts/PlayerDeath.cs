using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] float timeTillRestart = 2f;
    [SerializeField] Canvas gameOverCanvas;

    private Rigidbody2D rb;

    private PointCounter pointCounter;
    private PlayerMovement playerMovement;

    private bool Dead = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();

        gameOverCanvas.gameObject.SetActive(false);

        pointCounter = PointCounter.Instance;

        pointCounter.RestartPoints();
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

        playerMovement.StartEmission(false);

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
        Time.timeScale = 0;
        gameOverCanvas.gameObject.SetActive(true);

        Dead = false;
    }

    public bool IsDead()
    {
        return Dead;
    }
}
