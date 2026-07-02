using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BirdLoadout[] birdLoadouts; // different birds for each level
    
    public Vector3 spawnPosition = new Vector3(-25.5f, -8.2f, 0); // where the bird spawns
    private float birdSpawnDelay = 2f;
    
    private int totalBirds = 3;
    private int totalEnemies;
    private int birdsUsed = 0;
    private int enemiesDestroyed = 0;
    private GameObject currentBird;
    private bool gameOver = false;
    private bool birdLaunched = false;
    private float launchTime = 0f;
    private UIManager uiManager;
    
    private GameObject[] currentLevelBirds;
    
    [System.Serializable]
    public class BirdLoadout
    {
        public int levelIndex; // build index
        public GameObject[] birds; // birds for the level
    }
    
    void Start()
    {
        // make sure time starts
        Time.timeScale = 1f;
        
        uiManager = FindObjectOfType<UIManager>();

        // get current level
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        LoadBirdsForLevel(currentLevelIndex);
        
        // total enemies
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            
        // spawn bird
        SpawnBird();

        UpdateAnimalCounter();
    }
    
    void LoadBirdsForLevel(int levelIndex)
    {
        // find the matching loadout for this level
        foreach (BirdLoadout loadout in birdLoadouts)
        {
            if (loadout.levelIndex == levelIndex)
            {
                currentLevelBirds = loadout.birds;
                totalBirds = currentLevelBirds.Length;
                return;
            }
        }
    }
    
    void Update()
    {
        if (gameOver) 
        {
            return;
        }
        
        // check win condition with enemy tag
        int remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (remainingEnemies == 0 && totalEnemies > 0)
        {
            Win();
            return;
        }
        
        // if bird is launched
        if (currentBird != null && !birdLaunched)
        {
            Rigidbody2D rb = currentBird.GetComponent<Rigidbody2D>();
            if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic)
            {
                birdLaunched = true;
                launchTime = Time.time;
            }
        }
        
        // if bird destroyed
        if (currentBird == null && birdsUsed > 0)
        {
            // bird launched and destroyed
            if (birdLaunched)
            {
                UpdateAnimalCounter();

                birdLaunched = false;
                
                if (birdsUsed < totalBirds)
                {
                    Invoke("SpawnBird", birdSpawnDelay);
                }
                else
                {
                    // check loss after 5 seconds to make sure everything is done
                    Invoke("CheckLoseCondition", 5f);
                }
            }
        }
        // check if launched bird stopped moving
        else if (birdLaunched && currentBird != null && IsBirdDone())
        {
            birdLaunched = false;
            UpdateAnimalCounter();

            if (birdsUsed < totalBirds)
            {
                Invoke("SpawnBird", birdSpawnDelay);
            }
            else
            {
                // check loss after 5 seconds to make sure everything is done
                Invoke("CheckLoseCondition", 5f);
            }
        }
    }
    
    void SpawnBird()
    {
        if (birdsUsed >= totalBirds || gameOver) return;
        
        // prevent spawning duplicates
        CancelInvoke("SpawnBird");
        
        // get the next bird
        if (currentLevelBirds != null && birdsUsed < currentLevelBirds.Length)
        {
            GameObject birdToSpawn = currentLevelBirds[birdsUsed];
            
            if (birdToSpawn != null)
            {
                // no rotation, citing: https://discussions.unity.com/t/how-do-i-set-no-rotation-in-instantiate/475143/2
                currentBird = Instantiate(birdToSpawn, spawnPosition, Quaternion.identity);
                birdsUsed++;
            }
            
            UpdateAnimalCounter();
        }
    }
    
    void UpdateAnimalCounter()
    {
        int animalsRemaining = totalBirds - birdsUsed;
        if (uiManager != null)
        {
            uiManager.UpdateAnimalsLeft(animalsRemaining);
        }
    }

    bool IsBirdDone()
    {
        if (currentBird == null)
        {
            return true;
        }
        
        Rigidbody2D rb = currentBird.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // if bird not launched
            if (rb.bodyType == RigidbodyType2D.Kinematic)
            {
                return false;
            }
            
            // give the bird 1 second before checking if it is done
            float timeSinceLaunch = Time.time - launchTime;
            
            if (timeSinceLaunch < 1f)
            {
                return false;
            }
            
            // bird is done if movement velocity if less than 0.5
            return rb.linearVelocity.magnitude < 0.5f;
        }
        
        return false;
    }
    
    public void EnemyDestroyed()
    {
        enemiesDestroyed++;
    }
    
    void CheckLoseCondition()
    {
        if (gameOver) 
        {
            return; // win
        }
        
        // check actual remaining enemies
        int remaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
        
        if (remaining > 0)
        {
            Lose();
        }
    }
    
    // using invoke, citing from 
    // https://docs.unity3d.com/6000.2/Documentation/ScriptReference/MonoBehaviour.Invoke.html
    void Win()
    {
        gameOver = true;
        Invoke(nameof(ShowWin), 3f);
    }

    // using invoke, citing from 
    // https://docs.unity3d.com/6000.2/Documentation/ScriptReference/MonoBehaviour.Invoke.html
    void Lose()
    {
        gameOver = true;
        Invoke(nameof(ShowLose), 3f);
    }

    void ShowWin()
    {
        if (uiManager != null)
        {
            uiManager.WinScreen();
        }
    }

    void ShowLose()
    {
        if (uiManager != null)
        {
            uiManager.LoseScreen();
        }
    }

    // loading next scene citing: https://discussions.unity.com/t/move-to-another-scene/615274/2
    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // reset time
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // reset time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void DisappearObject()
    {
        Destroy(gameObject);
    }
}