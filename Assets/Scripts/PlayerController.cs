using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private float movementX;
    private float movementY;

    public int speed;

    private int count;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        SetCount();

        winText.gameObject.SetActive(false);
    }

    private void SetCount()
    {
        countText.text = $"score: {count}";
        if (count >= 86)
        {
            winText.gameObject.SetActive(true);
            countText.gameObject.SetActive(false);

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
    private void SetLooseCondition()
    {
        winText.text = "You loose!";
        winText.color = Color.red;
        winText.gameObject.SetActive(true);

        // Destroy the current object
        Destroy(gameObject);
    }

    private void OnMove(InputValue movementValue)
    {
        var movement = movementValue.Get<Vector2>();

        movementX = movement.x;
        movementY = movement.y;
    }

    private void FixedUpdate()
    {
        var vector3 = new Vector3(movementX, 0F, movementY);
        rb.AddForce(vector3 * speed);
    }

    private void Update()
    {
        if (gameObject != null && count < 86 && transform.position.y < -3)
        {
            SetLooseCondition();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SetLooseCondition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCount();
        }
        if (other.gameObject.CompareTag("PickUpPro"))
        {
            other.gameObject.SetActive(false);
            count+= 10;
            SetCount();
        }
    }
}
