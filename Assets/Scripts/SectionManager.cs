using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public class SectionManager : MonoBehaviour
{

    public GameObject Section;
    public GameObject Corridor;

    public int SPAWN_DISTANCE = 20;
    public int SECTIONS_BEHIND_PLAYER = 10;
    public int numPanels = 7;

    private float currentPosition = 0f;
    private List<GameObject> sections = new List<GameObject>();

    private List<int> mainPath = new List<int>();
    private Random random = new Random();

    public enum PATH_FEATURE
    {
        STRAIGHT,
        TURN_RIGHT,
        TURN_LEFT,
        FLIP,
        BREAK,
    }

    

    private PATH_FEATURE currentFeature = PATH_FEATURE.STRAIGHT;
    private int featureLeft = 30;

    // Start is called before the first frame update
    void Start()
    {
        mainPath.Add(numPanels / 2);
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

    void advancePath()
    {
        if (featureLeft <= 0)
        {
            System.Array values = System.Enum.GetValues(typeof(PATH_FEATURE));
            currentFeature = (PATH_FEATURE)values.GetValue(random.Next(values.Length));
            
            if (currentFeature == PATH_FEATURE.FLIP)
            {
                featureLeft = 1;
            } else if (currentFeature == PATH_FEATURE.BREAK)
            {
                featureLeft = 3;
            } else
            {
                featureLeft = 20;
            }
            
        }

        int nextPanel = 0;
        int previousPanel = mainPath[mainPath.Count - 1];

        
        switch(currentFeature)
        {
            
            case PATH_FEATURE.STRAIGHT:
            {
                nextPanel = previousPanel;
                break;
            }
                
            case PATH_FEATURE.TURN_RIGHT:
            {
                nextPanel = previousPanel + 1;
                break;
            }
                
            case PATH_FEATURE.TURN_LEFT:
            {
                nextPanel = previousPanel - 1;
                break;
            }
               
            case PATH_FEATURE.FLIP:
            {
                nextPanel = previousPanel + (2 * numPanels);
                break;
            }
                
        }

        featureLeft -= 1;
        print(nextPanel);
        mainPath.Add(nextPanel % (4 * numPanels));

    }

    public void generateNextSection()
    {

        currentPosition += 1f;

        GameObject section = Instantiate(Section, transform.position, Quaternion.identity);
        section.GetComponent<SpawnPanel>().setPosition(currentPosition);

        advancePath();

        section.GetComponent<SpawnPanel>().spawnPanel(numPanels, mainPath[0]);
        mainPath.RemoveAt(0);

        // add code to set corridor as parent of section
        section.transform.parent = Corridor.transform;

        // add code to rotate section to corridor orientation
        section.transform.Rotate(0f, 0f, Corridor.transform.rotation.eulerAngles.z);

        sections.Add(section);

        // if the number of sections behind player exceeds limit, drop section
        if (sections.Count >= SPAWN_DISTANCE + SECTIONS_BEHIND_PLAYER)
        {
            Destroy(sections[0]);
            sections.RemoveAt(0);
        }


    }

}
