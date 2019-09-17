using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Item
    {
        public enum TYPE
        {
            NONE = -1,
            POTATO = 0,
            CRAB = 1,
            CORN = 2,
            TREE = 3
        };
    }

    public class ItemRoot : MonoBehaviour
    {

        public GameObject Potatoprefab = null;
        public GameObject Crabprefab = null;
        public GameObject Cornprefab = null;
        public GameObject Treeprefab = null;
        public static float RESPAWN_TIME_POTATO = 10.0f;
        public static float RESPAWN_TIME_CRAB = 15.0f;
        public static float RESPAWN_TIME_CORN = 20.0f;
        public static float RESPAWN_TIME_TREE = 30.0f;
        protected List<Vector3> respawn_points;
        private float respawn_timer_potato = 0.0f;
        private float respawn_timer_crab = 0.0f;
        private float respawn_timer_corn = 0.0f;
        private float respawn_timer_tree = 0.0f;

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

        public void respawnCorn()
        {
            GameObject so = GameObject.Instantiate(this.Cornprefab) as GameObject;
            Vector3 pos = GameObject.Find("Corn").transform.position;
            pos.y = 29.5f;
            pos.x = Random.Range(390f, 400f);
            pos.z = Random.Range(350f, 370f);
            so.transform.position = pos;
        }

        public void respawnTree()
        {
            GameObject tree = GameObject.Instantiate(this.Treeprefab) as GameObject;
            Vector3 pos = GameObject.Find("Tree_").transform.position;
            pos.y = 28.0f;
            pos.x = Random.Range(140f, 282f);
            pos.z = Random.Range(305f, 420f);
            tree.transform.position = pos;
        }

    void Start()
        {
            /*
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
            */
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
            respawn_timer_corn += Time.deltaTime;
            if (respawn_timer_corn > RESPAWN_TIME_CORN)
            {
                respawn_timer_corn = 0.0f;
                this.respawnCorn();
            }

            respawn_timer_tree += Time.deltaTime;
            if (respawn_timer_tree > RESPAWN_TIME_TREE)
            {
                respawn_timer_tree = 0.0f;
                this.respawnTree();
            }
        }
    }