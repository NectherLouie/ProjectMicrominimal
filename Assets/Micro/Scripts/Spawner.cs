using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Micro
{
    public class Spawner : MonoBehaviour
    {
        public GameObject spawnObjectPrefab;
        public float cooldown = 2.0f;

        private bool isSpawning = false;
        private int spawnCount = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                BeginSpawn();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                EndSpawn();
            }
        }

        private void BeginSpawn()
        {
            isSpawning = true;

            GameObject g = Instantiate(spawnObjectPrefab);
            g.transform.position = transform.position;
            g.name = "Creep (" + spawnCount + ")";
            
            spawnCount++;

            StartCoroutine(CooldownSpawn(cooldown));
        }

        private void EndSpawn()
        {
            isSpawning = false;
        }

        private IEnumerator CooldownSpawn(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            if (isSpawning)
            {
                BeginSpawn();
            }
        }

        
    }
}
