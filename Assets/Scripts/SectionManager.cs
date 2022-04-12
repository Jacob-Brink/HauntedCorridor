using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{

    public GameObject Section;
    public GameObject Corridor;

    public int SPAWN_DISTANCE = 20;
    public int SECTIONS_BEHIND_PLAYER = 10;
    public int numPanels = 4;

    private float currentPosition = 0f;
    private List<GameObject> sections = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        setupStartingSections();
    }

    // Update is called once per frame
    void Update()
    {

    }   

    void setupStartingSections()
    {
        for (int j = 0; j < SPAWN_DISTANCE; j++)
        {
            generateNextSection();
        }
    }

    public void generateNextSection()
    {

        currentPosition += 1f;

        GameObject section = Instantiate(Section, transform.position, Quaternion.identity);
        section.GetComponent<SpawnPanel>().setPosition(currentPosition);

        for (int i = 0; i < 4 * numPanels; i++)
        {
            section.GetComponent<SpawnPanel>().spawnPanel(numPanels, i);
        }

        // add code to rotate section to corridor orientation

        // add code to set corridor as parent of section
        section.transform.parent = Corridor.transform;

        sections.Add(section);

        // if the number of sections behind player exceeds limit, drop section
        if (sections.Count >= SPAWN_DISTANCE + SECTIONS_BEHIND_PLAYER)
        {
            Destroy(sections[0]);
            sections.RemoveAt(0);
        }


    }

}
