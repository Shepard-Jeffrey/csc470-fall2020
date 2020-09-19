using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public GameObject pikaPrefab;
	public GameObject boltPrefab;
	GameObject Ground;

	float createBoltTimer = 0.1f;
	float createBoltRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
      Ground = GameObject.Find("Ground"); 
    }

    // Update is called once per frame
    void Update()
    {
    	createBoltTimer -= Time.deltaTime;
    	if (createBoltTimer < 0) {
    		Vector3 pos = new Vector3(Ground.transform.position.x + Random.Range(-10, 10),
    							  Ground.transform.position.y + 30,
    							  Ground.transform.position.z + Random.Range(-10, 10));
    		GameObject lightningBolt = Instantiate(boltPrefab, pos, Quaternion.identity);

    		createBoltTimer = createBoltRate;
    	}
        
    }

    public void MakePikas()
    {
    	Vector3 pos = new Vector3(Ground.transform.position.x + Random.Range(-10, 10),
    							  Ground.transform.position.y + 10,
    							  Ground.transform.position.z + Random.Range(-10, 10));
    	Instantiate(pikaPrefab, pos, Quaternion.identity);
    }
}
