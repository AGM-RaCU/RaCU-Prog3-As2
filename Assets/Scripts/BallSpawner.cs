using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public int ballSpawnCount = 100;
    public float ballSpawnInterval = 0.3f;
    public bool randomColours = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        for (int i = 0; i < ballSpawnCount; i++)
        {
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity, transform);

            Rigidbody2D body2D = ball.GetComponent<Rigidbody2D>();
            body2D.AddForce(randomColours.insideUnitCircle.normalized, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
