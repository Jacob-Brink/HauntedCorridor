using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPanel : MonoBehaviour
{
    
    public GameObject Panel;
    public GameObject Section;
    private float z = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool between(int positionA, int positionB, int value)
    {
        return value >= positionA && value <= positionB;
    }

    public void setPosition(float position)
    {
        z = position;
    }

    public void spawnPanel(int numPanels, int panelPosition)
    {

        int bottomLeftCorner = 0;
        int bottomRightCorner = numPanels - 1;
        int upperRightCorner = numPanels * 2 - 1;
        int upperLeftCorner = numPanels * 3 - 1;

        // convert sectionPosition (clockwise coordinate system of section panels) to x and y panel units
        int x, y;
        Quaternion rotation;
        if (between(bottomLeftCorner, bottomRightCorner, panelPosition))
        {
            // floor
            x = panelPosition;
            y = 0;
            rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (between(bottomLeftCorner + 1, upperRightCorner, panelPosition))
        {
            // right wall
            x = numPanels - 1;
            y = panelPosition - bottomRightCorner - 1;
            rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (between(upperRightCorner + 1, upperLeftCorner, panelPosition))
        {
            // ceiling
            x = numPanels - (panelPosition - upperRightCorner);
            y = numPanels - 1;
            rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else
        {
            // left wall
            x = 0;
            y = numPanels - (panelPosition - upperLeftCorner);
            rotation = Quaternion.Euler(0f, 0f, -90f);
        }

        float panelWidth = 1f; // TODO

        // 0, 1, 2
        // 0, 1, 2, 3
        float realX = (x - (numPanels - 1) / 2f) * panelWidth;
        float realY = (y - (numPanels - 1) / 2f) * panelWidth;

        Vector3 offset = new Vector3(realX, realY, z);
        GameObject panel = Instantiate(Panel, transform.position + offset, rotation);
        panel.transform.parent = Section.transform;
    }

}
