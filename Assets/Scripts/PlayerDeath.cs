using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private float timeTillRestart = 2f;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private ParticleSystem explosionFX;

    private Rigidbody2D rb;

    private PointCounter pointCounter;
    private PlayerMovement playerMovement;

    private bool Dead = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();

        EnableGameOverCanvas(false);
        RestartPoints();
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
        pointCounter = PointCounter.Instance;
        if (pointCounter != null)
        {
            pointCounter.RestartPoints();
        }
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
        Instantiate(explosionFX, transform.position, Quaternion.identity);
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(timeTillRestart);
        Time.timeScale = 0;

        EnableGameOverCanvas(true);

        Dead = false;
    }

    public bool IsDead()
    {
        return Dead;
    }
}
