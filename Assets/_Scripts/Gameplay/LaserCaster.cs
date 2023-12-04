using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class LaserCaster : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxDistance = 10f;

    private Laser laser;

    private void Awake()
    {
        laser = new Laser(lineRenderer);
    }

    private void Update()
    {
        laser.ClearLaser();
        laser.CastLaser(transform.position, transform.forward, maxDistance);
        laser.UpdateLaserRender();
    }
}

[System.Serializable, ExecuteInEditMode]
public class Laser
{
    public LineRenderer LaserRenderer { get; private set; }
    public List<Vector3> laserPoints { get; private set; }

    public Laser(LineRenderer lineRenderer)
    {
        LaserRenderer = lineRenderer;
        laserPoints = new List<Vector3>();
    }

    /// <summary>
    /// Casting a raycast from a point to create laser
    /// </summary>
    /// <param name="position"> Raycast origin </param>
    /// <param name="direction"> Raycast direction </param>
    /// <param name="maxDistance"> Raycast maximum distance </param>
    public void CastLaser(Vector3 position, Vector3 direction, float maxDistance)
    {
        //Add initial point to array
        AddLaserPoint(position);

        Ray ray = new Ray(position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            //Hit a reflective surface
            if (hit.collider.tag == "ReflectiveSurface")
                CastLaser(hit.point, hit.normal, maxDistance);
            //Hit an unreflective surface
            else
                AddLaserPoint(hit.point);

            CheckHit(hit.collider.gameObject);
        }
        //Hit nothing
        else
        {
            AddLaserPoint(ray.GetPoint(maxDistance));
        }
    }

    /// <summary>
    /// Checking if the laser hits a padlock
    /// </summary>
    private void CheckHit(GameObject hitObj)
    {
        if(hitObj.TryGetComponent(out LaserPadlock padlock))
        {
            padlock.UnlockPadlock();
        }
    }

    /// <summary>
    /// Add a new laser point to rebounds array
    /// </summary>
    /// <param name="position"> Rebound position </param>
    private void AddLaserPoint(Vector3 position)
    {
        laserPoints.Add(position);
    }

    /// <summary>
    /// Clear laser points array
    /// </summary>
    public void ClearLaser()
    {
        laserPoints.Clear();
    }
    
    /// <summary>
    /// Update laserRenderer points array
    /// </summary>
    public void UpdateLaserRender()
    {
        if (!LaserRenderer)
            return;

        int pointsAmount = laserPoints.Count;
        LaserRenderer.positionCount = pointsAmount;

        for (int i = 0; i < pointsAmount; i++)
        {
            Vector3 position = laserPoints[i];
            LaserRenderer.SetPosition(i, position);
        }
    }
}
