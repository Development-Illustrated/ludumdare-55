using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BodyAssembly : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] headPrefabs;
    [SerializeField] 
    protected GameObject[] torsoPrefabs;
    [SerializeField]
    protected GameObject[] legPrefabs;

    private GameObject headPrefab;
    private GameObject torsoPrefab;
    private GameObject legPrefab;

    public void CreateBody(Transform parent, int randomSeed)
    {
        Random.InitState(randomSeed);
        RandomlyChooseGameObject();
        ConnectBodyParts(parent);
    }

    void RandomlyChooseGameObject()
    {
        headPrefab = headPrefabs[UnityEngine.Random.Range(0, headPrefabs.Length)];
        torsoPrefab = torsoPrefabs[UnityEngine.Random.Range(0, torsoPrefabs.Length)];
        legPrefab = legPrefabs[UnityEngine.Random.Range(0, legPrefabs.Length)];
    }

    void ConnectBodyParts(Transform parent)
    {
        GameObject head = Instantiate(headPrefab, transform.position, Quaternion.identity, parent);
        GameObject torso = Instantiate(torsoPrefab, transform.position, Quaternion.identity, parent);
        GameObject legs = Instantiate(legPrefab, transform.position, Quaternion.identity, parent);

        Transform headAnchorInTorso = torso.transform.Find("HeadAnchor");
        Transform legsAnchorInTorso = torso.transform.Find("LegsAnchor");
        Transform torsoAnchorInHead = head.transform.Find("TorsoAnchor");
        Transform torsoAnchorInLegs = legs.transform.Find("TorsoAnchor");

        if (headAnchorInTorso != null && torsoAnchorInHead != null && legsAnchorInTorso != null)
        {
            head.transform.position = headAnchorInTorso.position - (torsoAnchorInHead.position - head.transform.position);
            head.transform.rotation = headAnchorInTorso.rotation;

            legs.transform.position = legsAnchorInTorso.position - (torsoAnchorInLegs.position - legs.transform.position);
            legs.transform.rotation = legsAnchorInTorso.rotation;
        } 
        else
        {
            Debug.LogError("Anchors not found");
        }
    }
}
