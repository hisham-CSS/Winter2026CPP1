using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    //private - private to the class - you would use this for variables and methods that should not be accessible outside this class
    //public - global access - you would use this for variables and methods that need to be accessed from other classes.
    //protected - protected to this class and any derived classes - you would use this for variables or methods that tshould be accessible through the inheritance hierarchy

    [SerializeField] private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MAKE YOUR CODE DEFENSIVE AGAINST BAD INPUT
        if (!target)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (!player)
            {
                Debug.LogError("CameraFollow: No GameObject with tag Player exists!");
                return;
            }
            target = player.transform;
        }
    }


    //General rule of thumb: Inputs are polled in update
    //Physics are applied in FixedUpdate
    //Camera movement is applied in LateUpate

    private void LateUpdate()
    {
        //early return - this allows us to create exit conditions for functions to stop execution if issues arise
        if (!target) return;

        //store our current position
        Vector3 pos = transform.position;

        //update the x position to match the target's X position - clamping it to our minimum and maximum values
        pos.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);

        //apply the updated postion back to our transform
        transform.position = pos;
    }
}
