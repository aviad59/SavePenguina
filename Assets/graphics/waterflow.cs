using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterflow : MonoBehaviour
{
    public TerrainLayer water;
    float speed = 0.02f;

    // Update is called once per frame
    void FixedUpdate()
    {

        if (water.tileOffset.x % 75 == 0)
            water.tileOffset = new Vector2(0, 0);

        water.tileOffset = new Vector2(water.tileOffset.x + speed, water.tileOffset.y);
    }
}
