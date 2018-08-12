using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<GameObject> foodItems;
    public List<GameObject> tempClosest;
    public Vector3 heading;
    public float dot;
    public GameObject closestFood;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Eat();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 destination = transform.position + this.transform.forward * 2f;
        Gizmos.DrawLine(transform.position, destination);
    }

   

    private void Eat()
    {
        tempClosest.Clear();
        heading = Vector3.zero;
        dot = 0f;


        foreach (GameObject food in foodItems)
        {
            heading = (food.transform.position - this.transform.position).normalized;
            dot = Vector3.Dot(heading, this.transform.forward);
            Debug.Log(food.name + " dot = " + dot);
            if(dot > 0.5f)
            {
                tempClosest.Add(food);
                
            }
        }
        float dist = 100f;
        foreach (GameObject closest in tempClosest)
        {
            float newDist = Vector3.Distance(closest.transform.position, transform.position);
            if(newDist < dist)
            {
                dist = newDist;
                closestFood = closest;
            }
        }

        if(closestFood != null)
        {
            foodItems.Remove(closestFood);
            Destroy(closestFood);
            closestFood = null;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            foodItems.Add(other.gameObject);
            MaterialChange mC = other.gameObject.GetComponent<MaterialChange>();
            mC.ChangeMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            foodItems.Remove(other.gameObject);
            MaterialChange mC = other.gameObject.GetComponent<MaterialChange>();
            mC.ResetMaterial();
        }
    }
}
