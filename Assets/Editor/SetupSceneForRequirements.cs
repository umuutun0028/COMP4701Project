using UnityEditor;
using UnityEngine;

public class SetupSceneForRequirements
{
    public static void Execute()
    {
        // 1. Create Bullet Prefab
        GameObject bulletObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bulletObj.name = "Bullet";
        bulletObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        bulletObj.AddComponent<Bullet>();
        Collider bulletCol = bulletObj.GetComponent<Collider>();
        bulletCol.isTrigger = true;
        Rigidbody bulletRb = bulletObj.AddComponent<Rigidbody>();
        bulletRb.useGravity = false;
        bulletRb.isKinematic = true;
        
        if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }
        GameObject bulletPrefab = PrefabUtility.SaveAsPrefabAsset(bulletObj, "Assets/Prefabs/Bullet.prefab");
        Object.DestroyImmediate(bulletObj);

        // 2. Setup Player
        GameObject player = GameObject.Find("Characters/Main");
        if (player != null)
        {
            player.tag = "Player";
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.bulletPrefab = bulletPrefab;
                
                Transform firePoint = player.transform.Find("FirePoint");
                if (firePoint == null)
                {
                    GameObject fpObj = new GameObject("FirePoint");
                    fpObj.transform.SetParent(player.transform);
                    fpObj.transform.localPosition = new Vector3(0, 0.5f, 1f);
                    firePoint = fpObj.transform;
                }
                pc.firePoint = firePoint;
            }
        }

        // 3. Setup Coins
        for (int i = 1; i <= 6; i++)
        {
            GameObject coin = GameObject.Find($"Coins/Coin{i}");
            if (coin != null)
            {
                Collider col = coin.GetComponent<Collider>();
                if (col != null) col.isTrigger = true;
                
                if (coin.GetComponent<Collectible>() == null)
                {
                    coin.AddComponent<Collectible>();
                }
            }
        }

        // 4. Setup Enemies
        GameObject guardEnemy = GameObject.Find("Characters/GuardEnemy");
        if (guardEnemy != null)
        {
            guardEnemy.tag = "Enemy";
            EnemyController ec = guardEnemy.GetComponent<EnemyController>();
            if (ec == null) ec = guardEnemy.AddComponent<EnemyController>();
            
            ec.enemyType = EnemyType.BorderPatrol;
            ec.borderPoints = new Vector3[] {
                new Vector3(-8, 0, -8),
                new Vector3(8, 0, -8),
                new Vector3(8, 0, 8),
                new Vector3(-8, 0, 8)
            };
            ec.player = player != null ? player.transform : null;
        }

        GameObject randomEnemy = GameObject.Find("Characters/RandomEnemy");
        if (randomEnemy != null)
        {
            randomEnemy.tag = "Enemy";
            EnemyController ec = randomEnemy.GetComponent<EnemyController>();
            if (ec == null) ec = randomEnemy.AddComponent<EnemyController>();
            
            ec.enemyType = EnemyType.RandomPatrol;
            ec.player = player != null ? player.transform : null;
        }

        Debug.Log("Scene setup completed.");
    }
}