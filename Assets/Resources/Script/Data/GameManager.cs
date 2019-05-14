using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameManager : ScriptableObject {
    public static GameManager instance;
    public int diamond;

    [SerializeField]
    GameObject healthObj;
    [SerializeField]
    GameObject diamondObj;
    [SerializeField]
    int amount;
    Vector3 randPos;

    GameObject temp;


    private void OnEnable()
    {
        instance = this;
        diamond = 0;
    }

    public int UpdateDiamond(int amount)
    {
        diamond += amount;
        return diamond;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SpawnItem(Transform pos){
        SpawnHealth(pos);
        SpawnDiamond(pos);
    }

    void SpawnHealth(Transform pos){
        temp = Instantiate(healthObj, pos);
    }

    void SpawnDiamond(Transform pos){
        for(int i =0; i< amount; ++i){
            randPos = new Vector3(Random.Range(pos.position.x -1,pos.position.x + 1), 0, Random.Range(pos.position.z -1,pos.position.z + 1));
            temp = Instantiate(diamondObj, randPos, Quaternion.identity);
        }
    }
}
