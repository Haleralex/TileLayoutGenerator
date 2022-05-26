using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileParametersView : MonoBehaviour
{
    [SerializeField]
    private GameObject _tilePrefab;

    [SerializeField]
    private TMPro.TextMeshProUGUI _width;

    [SerializeField]
    private TMPro.TextMeshProUGUI _height;

    private void Start()
    {
        _width.text = (_tilePrefab.transform.localScale.x * 10).ToString();
        _height.text = (_tilePrefab.transform.localScale.z * 10).ToString();
    }
}
