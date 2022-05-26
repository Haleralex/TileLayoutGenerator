using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TilesArranger : MonoBehaviour
{
    public event Action<float> SurfaceUpdated;

    [SerializeField]
    private TilesCreator _tilesCreator;

    [SerializeField]
    private TilesCutter _tilesCutter;

    [SerializeField]
    private GameObject _borders;

    private float _surface = 0.0f;


    public float Gap { get; set; } = 0.0f;
    public float Degree { get; set; } = 0.0f;
    public float Offset { get; set; } = 0.0f;
    public float Surface { 
        get 
        {
            return _surface;
        } 
        set 
        {
            _surface = value;
            SurfaceUpdated?.Invoke(_surface);
        } 
    }

    private void Start()
    {
        ArrangeTiles();
    }

    public void ArrangeTiles()
    {
        _tilesCutter.DestroyAllTiles();

        var tileWidth = _tilesCreator.TileWidth;
        var tileHeight = _tilesCreator.TileHeight;

        float diagonale = Mathf.Sqrt(_borders.transform.localScale.x * _borders.transform.localScale.x + _borders.transform.localScale.z * _borders.transform.localScale.z);

        int amountTilesWidth = (int)(diagonale / tileWidth) + 2;
        int amountTilesHeight = (int)(diagonale / tileHeight) + 2;

        float offsetX = tileWidth * amountTilesWidth / 2.0f;
        float offsetZ = tileHeight * amountTilesHeight / 2.0f;


        var tiles = _tilesCreator.CreateTiles(amountTilesWidth * amountTilesHeight);

        GameObject[,] tilesDoubleMassive = new GameObject[amountTilesHeight, amountTilesWidth];

        for (int i = 0; i< amountTilesHeight; i++)
        {
            for(int j = 0; j < amountTilesWidth; j++)
            {
                tilesDoubleMassive[i,j] = tiles[i * amountTilesWidth + j];
            }
        }

        for (int i = 0; i < amountTilesHeight; i++)
        {
            for (int j = 0; j < amountTilesWidth; j++)
            {
                var position = Vector3.right * ((Offset * i) % (tileWidth*10))- new Vector3(offsetX * 10, 0,offsetZ * 10) + new Vector3(j * tileWidth * 10 + Gap * j, 0, i * tileHeight * 10 + Gap * i);
                tilesDoubleMassive[i, j].transform.localPosition = position;
            }
        }
        _tilesCreator.transform.rotation = Quaternion.identity;
        _tilesCreator.transform.Rotate(Vector3.up * Degree);

        _tilesCutter.Tiles = tiles.Select(a => a.GetComponent<Collider>()).ToList();
        _tilesCutter.Cut(); 

        Surface = _tilesCutter.CalculateSurface();
    }
}
