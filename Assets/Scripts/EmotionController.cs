using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
public class EmotionController : MonoBehaviour
{
    public static EmotionController instance;
    [SerializeField]private SkinnedMeshRenderer emotion;
    public Dictionary<string,int> expressionID;
    
    [Range(0,100)]public float neutarlWeight; 
   [Range(0,100)] public float AngerWeight;
   [Range(0,100)] public float happy;
    void Start()
    {
        expressionID = new Dictionary<string, int>();
        expressionID.Add("angry",1);
        expressionID.Add("surprised",5);
        expressionID.Add("leftBlink",15);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        emotion.SetBlendShapeWeight(0,neutarlWeight);
        emotion.SetBlendShapeWeight(1,AngerWeight);
        emotion.SetBlendShapeWeight(39,happy);
        emotion.SetBlendShapeWeight(40,happy);
        emotion.SetBlendShapeWeight(43,happy);
    }
    private void reset(){
        neutarlWeight = 0;
        AngerWeight = 0;
        happy = 0;
    }
    public void Neural(){
        reset();
        neutarlWeight = 100;
    }
    public void Angry(){
        reset();
        AngerWeight = 100;
    }
}
