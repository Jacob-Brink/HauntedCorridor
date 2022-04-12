using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// see https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject Corridor; //make ref. in inspector window

    void Start()
    {
        
    }    

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            // generate next section
            print("Generating new section");
            Corridor.GetComponent<PanelManager>().generateNextSection();

            // update game collision box
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
        }
    }

    void Update()
    {
        
    }

}
