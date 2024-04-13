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
    void Start()
    {
        RandomlyChooseGameObject();
        ConnectBodyParts();
    }

    void RandomlyChooseGameObject()
    {
        headPrefab = headPrefabs[UnityEngine.Random.Range(0, headPrefabs.Length)];
        torsoPrefab = torsoPrefabs[UnityEngine.Random.Range(0, torsoPrefabs.Length)];
        legPrefab = legPrefabs[UnityEngine.Random.Range(0, legPrefabs.Length)];
    }

    void ConnectBodyParts()
    {
        GameObject head = Instantiate(headPrefab, Vector3.zero, Quaternion.identity, this.transform);
        GameObject torso = Instantiate(torsoPrefab, Vector3.zero, Quaternion.identity, this.transform);
        GameObject legs = Instantiate(legPrefab, Vector3.zero, Quaternion.identity, this.transform);

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
