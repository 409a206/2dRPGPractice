using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private BattleManager battleManager;
    public Enemy EnemyProfile;
    Animator enemyAI;

    public BattleManager BattleManager {
        get {
            return battleManager;
        }
        set {
            battleManager = value;
        }
    }

    private bool selected;
    GameObject selectionCircle;
    private ParticleSystem bloodsplatterParticles;

    private void Update() {
        UpdateAI();
    }

    public void UpdateAI() {
        if(enemyAI != null && EnemyProfile != null) {
            enemyAI.SetInteger("EnemyHealth", EnemyProfile.Health);
            enemyAI.SetInteger("PlayerHealth", GameState.CurrentPlayer.Health);
            enemyAI.SetInteger("EnemiesInBattle", battleManager.EnemyCount);
        }
    }

    private void Awake() {
        bloodsplatterParticles = 
            GetComponentInChildren<ParticleSystem>();
        if(bloodsplatterParticles == null) {
            Debug.LogError("No PArticle System Found");
        }


        enemyAI = GetComponent<Animator>();
        if(enemyAI == null) {
            Debug.LogError("No AI System Found");
        }
    }

    IEnumerator SpinObject(GameObject target) {
        while(true) {
            target.transform.Rotate(0,0,180 * Time.deltaTime);
            yield return null;
        }
    }

    private void OnMouseDown() {
        if(battleManager.CanSelectEnemy) {
            var selection = !selected;
            battleManager.ClearSelectedEnemy();
            selected = selection;

            if(selected) {
                selectionCircle = (GameObject)GameObject.Instantiate(
                    battleManager.selectionCircle);
                selectionCircle.transform.parent = transform;

                selectionCircle.transform.localPosition = Vector3.zero;
                StartCoroutine("SpinObject", selectionCircle);
                battleManager.SelectEnemy(this, EnemyProfile.Name);
            }
        }
    }

    public void ClearSelection() {
        if(selected) {
            selected = false;
            if(selectionCircle != null) 
                DestroyObject(selectionCircle);
            StopCoroutine("SpinObject");
            
        }
    }

    void ShowBloodSplatter() {
        bloodsplatterParticles.Play();
        ClearSelection();
        if(battleManager!= null) {
            battleManager.ClearSelectedEnemy();
        } else {
            Debug.LogError("No BattleManater");
        }
    }

    
}
