using UnityEngine;

public class PickUpPro : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(200, 200, 200) * Time.deltaTime);
    }
}
