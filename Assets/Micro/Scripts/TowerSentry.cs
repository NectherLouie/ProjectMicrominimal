using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Micro
{
    public class TowerSentry : BaseTower
    {
        public GameObject prefabProjectile;
        public GameObject projectileSpawner;

        private GameObject projectileObject;
        private float projectileDuration = 0f;

        public override void Shoot()
        {
            base.Shoot();

            projectileObject = Instantiate(prefabProjectile);
            projectileObject.transform.position = projectileSpawner.transform.position;

            // Predict target position
            projectileDuration = 0.2f;
            BaseCreep baseCreep = target.gameObject.GetComponent<BaseCreep>();
            float targetSpeed = baseCreep.speed;
            Vector3 targetDirection = baseCreep.transform.forward;
            Vector3 predictedTargetPosition = target.transform.position + ((targetDirection * targetSpeed) * projectileDuration);

            projectileObject.transform.DOMove(predictedTargetPosition, projectileDuration)
                .OnUpdate(OnProjectileUpdate)
                .OnComplete(OnProjectileMoveComplete);
        }

        private void OnProjectileUpdate()
        {
            Vector3 midPoint = (projectileObject.transform.position - projectileSpawner.transform.position) * 0.75f;

            Vector3[] lnv = {
                projectileSpawner.transform.position,
                midPoint,
                projectileObject.transform.position
            };

            LineRenderer lineRenderer = projectileSpawner.GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
            lineRenderer.SetPositions(lnv);
        }

        private void OnProjectileMoveComplete()
        {
            projectileDuration = 0;

            LineRenderer lineRenderer = projectileSpawner.GetComponent<LineRenderer>();
            lineRenderer.enabled = false;

            Destroy(projectileObject);
        }
    }
}
