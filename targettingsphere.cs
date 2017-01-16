using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class targettingsphere : MonoBehaviour {

	public List<Transform> targets;
	public Transform selectedtarget;
	public GameObject Karateman;
	
	void Start () {
		
		targets = new List<Transform>();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		InvokeRepeating("SelectTarget", 0f, 1f);
	
	}
	
	void SortByNearest()
	{
		targets.Sort(delegate(Transform t1, Transform t2) 
		{
			return Vector3.Distance(t1.position, Karateman.transform.position).CompareTo
				(Vector3.Distance(t2.position, Karateman.transform.position));
		});
		
	}
	
	public GameObject SelectTarget()
	{
			SortByNearest();
			selectedtarget = targets[0];
			return selectedtarget.gameObject;
		
	}
	
	void OnTriggerEnter(Collider otherObject)
	{
		if (otherObject.tag == "Enemy")
		{
			targets.Add(otherObject.transform);
		}
		
	}
	
	void OnTriggerExit(Collider otherObject)
	{
		if (otherObject.tag == "Enemy")
		{
			targets.Remove(otherObject.transform);
		}
		
	}
}
