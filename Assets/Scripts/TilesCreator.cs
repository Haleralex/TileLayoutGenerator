using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject _tilePrefab;

    public float TileWidth => _tilePrefab.transform.localScale.x;
    public float TileHeight => _tilePrefab.transform.localScale.z;


    public List<GameObject> CreateTiles(int tilesAmount)
    {
        var tiles = new List<GameObject>();

        for(int i =0; i< tilesAmount; i++)
        {
            var tile = GameObject.Instantiate(_tilePrefab,transform);
            tiles.Add(tile);
        }

        return tiles;
    }
}
