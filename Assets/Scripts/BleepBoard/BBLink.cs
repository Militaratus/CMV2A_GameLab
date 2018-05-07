using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BBLink : MonoBehaviour
{
    public Transform lineTarget;
    private LineRenderer lineRenderer;

	// Use this for initialization
	void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        if (lineTarget != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, lineTarget.position);

            if (lineRenderer.material == null)
            {
                Shader shader = new Shader();
                Material myMaterial = new Material(shader);
                myMaterial.color = Color.green;
                lineRenderer.material = myMaterial;
            }

            lineRenderer.material.color = Color.green;
        }
	}
}
