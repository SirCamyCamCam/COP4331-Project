using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnTrashHandlerButton : MonoBehaviour {

    public bool spawningAnts = false;
    public int antsToSpawn = 0;

    public void SpawnAnt()
    {
        
 
        var InputGameObject = GameObject.Find("Spawn TrashHandler - InputField");
        var stringval = InputGameObject.GetComponent<TMPro.TMP_InputField>();
        TextMeshProUGUI textMesh = stringval.GetComponent<TextMeshProUGUI>();
        var value = Convert.ToInt32(stringval.text);
        
        if (value == 0){
            antsToSpawn = 0;
            spawningAnts = false;

        }
        if (spawningAnts){
            return;
        }
        antsToSpawn = value;
        spawningAnts = true;
    }
    
    float timeSinceLastFrame = 0;
    public void Update()
    {
        timeSinceLastFrame += Time.deltaTime;
        if(spawningAnts && timeSinceLastFrame >= 1)
        {
            timeSinceLastFrame = 0;
            var InputGameObject = GameObject.Find("Spawn TrashHandler - InputField");
            var stringval = InputGameObject.GetComponent<TMPro.TMP_InputField>();
            
            //decrease counter by 1
            
            if (antsToSpawn > 0){
                antsToSpawn--;
                AntManager.main.AddAntsToSpawn(Ant.AntType.TRASH_HANDLER);
                stringval.text = antsToSpawn.ToString();
            }
            else
            {
                spawningAnts = false;
                stringval.text = null;
            }
        }
    }
}