using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    
    public GameObject Panel;
    public int numPanels = 4;
    public int SPAWN_DISTANCE = 20;
    private int bottomLeftCorner, bottomRightCorner, upperRightCorner, upperLeftCorner;
    private float currentPosition = 0f;

    // Start is called before the first frame update
    void Start()
    {
        bottomLeftCorner = 0;
        bottomRightCorner = numPanels - 1;
        upperRightCorner = numPanels * 2 - 1;
        upperLeftCorner = numPanels * 3 - 1;

        setupStartingPanels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool between(int positionA, int positionB, int value)
    {
        return value >= positionA && value <= positionB;
    }

    void setupStartingPanels()
    {
        for (int j = 0; j < SPAWN_DISTANCE; j++)
        {
            generateNextSection();
        }

    }

    void placePanel(int sectionPosition, float z)
    {
        // convert sectionPosition (clockwise coordinate system of section panels) to x and y panel units
        int x, y;
        Quaternion rotation;
        if (between(bottomLeftCorner, bottomRightCorner, sectionPosition))
        {
            // floor
            x = sectionPosition;
            y = 0;
            rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (between(bottomLeftCorner + 1, upperRightCorner, sectionPosition))
        {
            // right wall
            x = numPanels - 1;
            y = sectionPosition - bottomRightCorner - 1;
            rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (between(upperRightCorner + 1, upperLeftCorner, sectionPosition))
        {
            // ceiling
            x = numPanels - (sectionPosition - upperRightCorner);
            y = numPanels - 1;
            rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else
        {
            // left wall
            x = 0;
            y = numPanels - (sectionPosition - upperLeftCorner);
            rotation = Quaternion.Euler(0f, 0f, -90f);
        }

        float panelWidth = 1f;

        // 0, 1, 2
        // 0, 1, 2, 3
        float realX = (x - (numPanels - 1) / 2f) * panelWidth;
        float realY = (y - (numPanels - 1) / 2f) * panelWidth;

        Vector3 offset = new Vector3(realX, realY, z);
        Instantiate(Panel, transform.position + offset, rotation);
    }

    public void generateNextSection()
    {
        // spawn next section
        for (int i = 0; i < numPanels * 4; i++)
        {
            if (Random.Range(0f, 1f) < 0.99)
            {
                placePanel(i, currentPosition);
            }
        }

        currentPosition += 1f;

    }

}
