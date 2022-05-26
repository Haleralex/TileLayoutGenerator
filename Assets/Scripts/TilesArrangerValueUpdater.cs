using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesArrangerValueUpdater : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_InputField _gap;

    [SerializeField]
    private TMPro.TMP_InputField _degree;

    [SerializeField]
    private TMPro.TMP_InputField _offset;

    [SerializeField]
    private TMPro.TextMeshProUGUI _surface;

    [SerializeField]
    private TilesArranger _tilesArranger;

    private void OnEnable()
    {
        _gap.onValueChanged.AddListener(OnGapValueChanged);

        _degree.onValueChanged.AddListener(OnDegreeValueChanged);

        _offset.onValueChanged.AddListener(OnOffsetValueChanged);

        _tilesArranger.SurfaceUpdated += OnSurfaceUpdated;
    }

    private void OnDisable()
    {
        _gap.onValueChanged.RemoveListener(OnGapValueChanged);

        _degree.onValueChanged.RemoveListener(OnDegreeValueChanged);

        _offset.onValueChanged.RemoveListener(OnOffsetValueChanged);

        _tilesArranger.SurfaceUpdated -= OnSurfaceUpdated;
    }

    private void OnGapValueChanged(string value)
    {
        if (!float.TryParse(value, out var gap))
            return;

        if (gap < 0)
            return;

        _tilesArranger.Gap = gap;
    }
    private void OnDegreeValueChanged(string value)
    {
        if (!float.TryParse(value, out var degree))
            return;

        _tilesArranger.Degree = degree;
    }

    private void OnOffsetValueChanged(string value)
    {
        if (!float.TryParse(value, out var offset))
            return;

        if (offset < 0)
            return;

        _tilesArranger.Offset = offset;
    }

    private void OnSurfaceUpdated(float surface)
    {
        _surface.text = surface.ToString();
    }
}
