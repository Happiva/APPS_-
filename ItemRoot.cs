using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Item
    {
        public enum TYPE
        {
            NONE = -1,
            POTATO = 0,
            CRAB = 1
        };
    }

    public class ItemRoot : MonoBehaviour
    {

        public GameObject Potatoprefab = null;
        public GameObject Crabprefab = null;
        public static float RESPAWN_TIME_POTATO = 10.0f;
        public static float RESPAWN_TIME_CRAB = 15.0f;
        protected List<Vector3> respawn_points;
        private float respawn_timer_potato = 0.0f;
        private float respawn_timer_crab = 0.0f;

        public void respawnPotato()
        {
            GameObject go = GameObject.Instantiate(this.Potatoprefab) as GameObject;
            Vector3 pos = GameObject.Find("potato").transform.position;
            pos.y = 29.5f;
            pos.x = Random.Range(400f, 410f);
            pos.z = Random.Range(340f, 360f);
            go.transform.position = pos;
        }

        public void respawnCrab()
        {
            GameObject co = GameObject.Instantiate(this.Crabprefab) as GameObject;
            Vector3 pos = GameObject.Find("Crab").transform.position;
            pos.y = 28f;
            pos.x = Random.Range(300f, 330f);
            pos.z = Random.Range(95f, 125f);
            co.transform.position = pos;
        }

        void Start()
        {
            this.respawn_points = new List<Vector3>();
            GameObject[] respawns = GameObject.FindGameObjectsWithTag("Potato");
            foreach (GameObject go in respawns)
            {
                MeshRenderer renderer = go.GetComponentInChildren<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.enabled = false;
                }
                this.respawn_points.Add(go.transform.position);
            }
        }

        void Update()
        {
            respawn_timer_potato += Time.deltaTime;
            if (respawn_timer_potato > RESPAWN_TIME_POTATO)
            {
                respawn_timer_potato = 0.0f;
                this.respawnPotato();
            }
            respawn_timer_crab += Time.deltaTime;
            if (respawn_timer_crab > RESPAWN_TIME_CRAB)
            {
                respawn_timer_crab = 0.0f;
                this.respawnCrab();
            }
        }


    }