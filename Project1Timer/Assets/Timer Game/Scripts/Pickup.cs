using UnityEngine;

public class Pickup : MonoBehaviour
{
    public ToyType ToyType;
    bool isHolding = false;
    bool hasScored = false; // tracks if item gave score already

    [SerializeField]
    float throwForce = 600f;
    [SerializeField]
    float maxDistance = 3f;
    float distance;


    TempParent tempParent;
    Rigidbody rb;

    Vector3 objectPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempParent = TempParent.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding)
            Hold();
    }

    private void OnMouseDown() // pickup method that can be changed with whatever input
    {
        distance = Vector3.Distance(this.transform.position, tempParent.transform.position);

        if (tempParent != null)
        {
            if (distance <= maxDistance)
            {
                isHolding = true;
                rb.useGravity = false;
                rb.detectCollisions = true;

                this.transform.SetParent(tempParent.transform);
            }
        }
        else
        {
            Debug.Log("Temp Parent Item not found in scene");
        }
    }
    private void OnMouseUp()
    {
        Drop();
    }
    private void OnMouseExit()
    {
        Drop();
    }

    private void Hold()
    {
        distance = Vector3.Distance(this.transform.position, tempParent.transform.position);

        if (distance >= maxDistance)
        {
            Drop();
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (Input.GetMouseButtonDown(1))
        {
            rb.AddForce(tempParent.transform.forward * throwForce);
            Drop();

        }
    }
    private void Drop()
    {
        if (isHolding)
        {
            isHolding = false;
            objectPos = this.transform.position; ;
            this.transform.position = objectPos;
            this.transform.SetParent(null);
            rb.useGravity = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " collided with " + collision.gameObject.name);
        if (hasScored) return;

        // Check if the object we collided with is a ToyBox
        ToyBox toyBox = collision.gameObject.GetComponent<ToyBox>();
        if (toyBox != null)
        {
            if (ToyType == toyBox.correctToyType)
            {
                GameManager.Instance?.IncreaseScore(1); // correct toy
                Debug.Log(name + " placed in correct box! +1 point");
            }
            else
            {
                GameManager.Instance?.IncreaseScore(-1); // wrong toy
                Debug.Log(name + " placed in wrong box! -1 point");
            }

            hasScored = true; // prevent scoring multiple times
        }
        else
        {
            Debug.Log("No Toybox Component found aww");
        }
    }
}