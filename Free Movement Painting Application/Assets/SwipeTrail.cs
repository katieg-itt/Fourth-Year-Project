using UnityEngine;

public class SwipeTrail : MonoBehaviour {

    public GameObject trailPrefab;
    //private GameObject thisTrail;
    private Vector3 startPos;
    private Plane objPlane;
    private float distanceToHit;
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public GameObject quadHitPoint;

    // Use this for initialization
    void Start()
    {

        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        
       
        if ( Input.GetMouseButtonDown(0))
        {
            //thisTrail = (GameObject)Instantiate(trailPrefab, this.transform.position, Quaternion.identity);
             Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            float rayDistance;

            if (Physics.Raycast(mRay, out hit))
            {
                GameObject plane = hit.collider.gameObject;

                if (plane)
                {
                    distanceToHit = Vector3.Distance(Camera.main.transform.position, hit.point);
                    Debug.DrawLine(mRay.origin, 100 * mRay.direction, Color.blue);
                }
            }

            if (objPlane.Raycast(mRay, out rayDistance))
            {
                startPos = mRay.GetPoint(rayDistance);
            }

            quadHitPoint.transform.position = hit.point;
        }
        else if ( Input.GetMouseButtonUp(0))
        {
            //if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
            //{
            //    //Destroy(thisTrail);
            //}
        }

        
    }
}
