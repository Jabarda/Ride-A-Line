using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour {

	public LineRenderer lineRenderer;
	public EdgeCollider2D edgeCol;
	public GameObject LinePrefab;
	List<Vector2> points;
	List<Vector2> pointsTail;

	public void UpdateLine (Vector2 Pos)
	{
		if (points == null)
		{
			points = new List<Vector2>();
			SetPoint(Pos);
			return;
		}

		if (Vector2.Distance(points.Last(), Pos) > .1f)
			SetPoint(Pos);
	}

	void SetPoint (Vector2 point)
	{
		points.Add(new Vector2(float.Parse(point.x.ToString("#.##")),float.Parse(point.y.ToString("#.##"))));

		lineRenderer.numPositions = points.Count;
		lineRenderer.SetPosition(points.Count - 1, points[points.Count-1]);

		if (points.Count > 1) {
			edgeCol.points = points.ToArray ();
			LineCreator.instance.StartNewLine ();
		}
	}
		
}
