using UnityEngine;

public class Pickup : MonoBehaviour
{
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

        if(distance >= maxDistance)
        {
            Drop();
        }

         rb.linearVelocity = Vector3.zero;
         rb.angularVelocity = Vector3.zero;

        if(Input.GetMouseButtonDown(1))
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
        if (!hasScored && collision.gameObject.CompareTag("Toybox"))
        {
            GameManager.Instance?.IncreaseScore(1);
            hasScored = true; // clamps/prevents score 
        }
    }
}
