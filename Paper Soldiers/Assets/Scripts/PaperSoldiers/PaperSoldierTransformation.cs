using UnityEngine;

public enum ESoldierType
{
  None,

  WhiteWarrior,
  WhiteMage,
  WhiteArcher,

  RedWarrior,
  RedMage,
  RedArcher,

  BlueWarrior,
  BlueMage,
  BlueArcher
}

[System.Serializable]
public struct TransformationData
{
  public ESoldierType Type;
  public GameObject TransformationResult;
}

public class PaperSoldierTransformation : MonoBehaviour
{
  public TransformationData[] TransformationData;

  public void PerformTransformation(ESoldierType transformationType)
  {
    GameObject transformationResult = null;
    foreach (var transfData in TransformationData)
    {
      if(transfData.Type == transformationType)
      {
        transformationResult = transfData.TransformationResult;
      }
    }

    if(transformationResult != null)
    {
      Instantiate(transformationResult, transform.position, Quaternion.identity);
      Destroy(this.gameObject);
    }
  }
}
