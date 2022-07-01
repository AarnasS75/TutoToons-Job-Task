using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineAnimator : MonoBehaviour
{
    CameraController camController;

    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private float animationDuration = 3f;

    private LineRenderer lineRenderer;


    #region Linijos piešimo kintamieji

    private Transform lastPoints;

    private int totalPointCount;

    private List<Transform> points = new List<Transform>();

    private int clickedPointIndex = 0;

    private Vector3 startPos, endPos;

    private bool lineIsDrawing = false;
    private bool drawingIsCompleted = false;

    private float startTime;
    #endregion


    private void Awake()
    {
        camController = Camera.main.GetComponent<CameraController>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        AnimationCurve curve = new AnimationCurve();
        Vector2 curveWidth = Vector2.one;

        curveWidth *= camController.PercentageIncreaseScale() / 1.4f;

        curve.AddKey(curveWidth.x, curveWidth.y);

        lineRenderer.widthCurve = curve;

        totalPointCount = spawner.points.Count;
        lineRenderer.positionCount = 1; // Sukuriamas pradžios taškas
    }
    /// <summary>
    /// Pirmas parinktas taškas neturi su kuo susijungti, todėl yra įterpiamas į sąrašą.
    /// Piešimas prasideda nuo antro pasirinkto taško.
    /// Kiekvienas pasirinktas taškas yra įterpiamas į sąrašą.
    /// </summary>
    /// <param name="selectedPoint"> Pasirinktas taškas </param>
    public void MakeLine(Transform selectedPoint)
    {
        if (lastPoints == null)
        {
            lastPoints = selectedPoint;
            points.Add(lastPoints);
        }
        else
        {
            points.Add(selectedPoint);
            lineRenderer.enabled = true;
            StartCoroutine(SetupLine());
        }
    }
    /// <summary>
    /// Linijos konstravimas
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetupLine()
    {
        int pointListLength = points.Count;    // Taškų, ant kurių buvo paspausta, sąrašo ilgis

        if (!lineIsDrawing)                    // Tikrinama ar linijos piešimo animacija prasidėjo 
        {
            clickedPointIndex++;              // Užregistruojamas paspaudimų skaičius, kai virvės animacija nevyksta
            lineRenderer.positionCount++;     // Papildoma antru linijos tašku

            startTime = Time.time;            // Registruojamas animacijos pradžios laikas

            startPos = points[clickedPointIndex - 1].position;  // Taško pozicijos priskiriamos iš pasirinktų taškų sąrašo
            endPos = points[clickedPointIndex].position;

            lineRenderer.SetPosition(clickedPointIndex - 1, startPos);  // Priskiriama pirmo taško pozicija
        }

        while (startPos != endPos && !drawingIsCompleted)      
        {
            lineIsDrawing = true;

            float t = (Time.time - startTime) / animationDuration;
            var newPos = Vector3.Lerp(startPos, endPos, t);             // Sukuriama antro taško animacija iki pabaigos pozicijos

            lineRenderer.SetPosition(clickedPointIndex, newPos);

            if (newPos == endPos)                                      
            {
                if (lineRenderer.positionCount + 1 <= pointListLength)      // Tikrinama, ar pridėjus papildomą linijos tašką, bendra taškų suma neviršys pasirinktų taškų sąrašo ilgio
                {
                    clickedPointIndex++;
                    lineRenderer.positionCount++;

                    startPos = newPos;
                    endPos = points[clickedPointIndex].position;
                }
                else
                {
                    if (pointListLength == totalPointCount && lineRenderer.positionCount == totalPointCount)  // Tikrinama, ar pasirinktų taškų sąrašo ilgis sutampa su visų sukurtų taškų kiekiu bei linijos taškų skaičiumi
                    {
                        clickedPointIndex++;
                        lineRenderer.positionCount++;

                        startPos = newPos;
                        endPos = points[0].position;        // Pabaigos taškui priskiriama pirmo pasirinkto elemnto pozicija
                    }
                    else
                    {
                        if (newPos == points[0].position)
                        {
                            drawingIsCompleted = true;
                            GameManager.Instance.EndLevel();
                        }

                        break;
                    }
                }
                startTime = Time.time;                              // Iš naujo nustatomas animacijos pradžios laikas
                
            }

            yield return null;
        }
        
        lineIsDrawing = false;                                       
    }
}
