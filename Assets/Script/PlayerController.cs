using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PowerUpType currentPowerUp = PowerUpType.None;
    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;

    public float playerSpeed = 5.0f;
    private float awayEnemy = 15.0f;
    private Rigidbody playerRb;
    private GameObject focalPosition;
    public bool powerUp;
    public GameObject PowerUpSphere;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPosition = GameObject.Find("Focal Point");
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(playerSpeed * verticalInput * focalPosition.transform.forward);
        PowerUpSphere.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            powerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpTimer());
            PowerUpSphere.gameObject.SetActive(true);
        }
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }

    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp ==
PowerUpType.Pushback)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position -
            transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength,
            ForceMode.Impulse);
            Debug.Log("Player collided with: " + collision.gameObject.name + " with
            powerup set to " + currentPowerUp.ToString());
}

        if (collision.gameObject.CompareTag("BigEnemy"))
        {
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            playerRb.AddForce(Vector3.forward * 10, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(7);
        powerUp = false;
        PowerUpSphere.gameObject.SetActive(false);
    }
}
