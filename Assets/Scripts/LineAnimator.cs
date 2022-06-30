using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineAnimator : MonoBehaviour
{
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

    private float startTime;
    #endregion


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        totalPointCount = spawner.points.Count;
        lineRenderer.positionCount = 1; // Iš pradži? užregistruojamas 1 linijos taškas
    }
    /// <summary>
    /// Pirmas parinktas taškas yra ?terpiamas ? s?raš? ir nuo antro pasirinkto prasideda linijos piešimas
    /// </summary>
    /// <param name="finalPoint"> Pasirinktas taškas </param>
    public void MakeLine(Transform finalPoint)
    {
        if (lastPoints == null)
        {
            lastPoints = finalPoint;
            points.Add(lastPoints);
        }
        else
        {
            points.Add(finalPoint);
            lineRenderer.enabled = true;
            StartCoroutine(SetupLine());
        }
    }
    /// <summary>
    /// Linijos piešimas.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetupLine()
    {
        int pointListLength = points.Count;    // Tašk?, ant kuri? buvo paspausta, s?rašo ilgis

        if (!lineIsDrawing)                    // Jeigu linijos piešimo animacija neprasid?jo 
        {
            clickedPointIndex++;              // Užregistruojamas paspaudim? skai?ius, kai virv?s animacija nevyksta
            lineRenderer.positionCount++;     // Papildoma antru linijos tašku

            startTime = Time.time;            // Registruojamas animacijos pradžios laikas

            startPos = points[clickedPointIndex - 1].position;  // Taško pozicijos priskiriamos iš pasirinkt? tašk? s?rašo
            endPos = points[clickedPointIndex].position;

            lineRenderer.SetPosition(clickedPointIndex - 1, startPos);  // Priskiriama pirmo taško pozicija
        }
        while (startPos != endPos)      
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
