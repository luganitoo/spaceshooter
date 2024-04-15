using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByEnemy : MonoBehaviour
{
    public GameObject enemyExplosion;
    public GameObject playerExplosion;
    private GameController gameController;
    public int scoreValue;


    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (enemyExplosion != null)
        {
            Instantiate(enemyExplosion, transform.position, transform.rotation);
        }

        if (other.CompareTag ("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            //gameController.Restart();
        }

        gameController.AddScore(scoreValue);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
