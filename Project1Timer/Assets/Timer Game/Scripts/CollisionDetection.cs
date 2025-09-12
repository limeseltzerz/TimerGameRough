using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Another object starts to collide with us
        Debug.Log(collision.gameObject.name + "Starts colliding with us");
    }
    private void OnCollisionStay(Collision collision)
    {
        // The other object is still colliding with us
        Debug.Log(collision.gameObject.name + "is colliding with us");
    }
    private void OnCollisionExit(Collision collision)
    {
        // the other object has stopped colliding with us
        Debug.Log(collision.gameObject.name + "has stopped colliding with us");
    }
}
