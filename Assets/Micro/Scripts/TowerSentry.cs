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
            projectileDuration = 0.5f;
            BaseCreep baseCreep = target.gameObject.GetComponent<BaseCreep>();
            float targetSpeed = baseCreep.speed;
            Vector3 targetDirection = baseCreep.transform.forward * 1.5f;
            Vector3 predictedTargetPosition = target.transform.position + ((targetDirection * targetSpeed) * projectileDuration);

            projectileObject.transform.DOMove(predictedTargetPosition, projectileDuration)
                .OnUpdate(OnProjectileUpdate)
                .OnComplete(OnProjectileMoveComplete);
        }

        private void OnProjectileUpdate()
        {
            Vector3 relativeProjectilePosition = RelativePosition(projectileSpawner.transform, projectileObject.transform.position);
            Vector3 midPoint = (relativeProjectilePosition - projectileSpawner.transform.localPosition) * 0.75f;

            Vector3[] lnv = {
                projectileSpawner.transform.localPosition,
                midPoint,
                relativeProjectilePosition
            };

            LineRenderer lineRenderer = projectileSpawner.GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
            lineRenderer.SetPositions(lnv);
        }

        public static Vector3 RelativePosition(Transform origin, Vector3 position)
        {
            Vector3 distance = position - origin.position;
            Vector3 relativePosition = Vector3.zero;
            relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
            relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
            relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);

            return relativePosition;
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
