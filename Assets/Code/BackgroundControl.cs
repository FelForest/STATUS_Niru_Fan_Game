using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    [SerializeField] private Material material;

    [SerializeField] private float speedRate;
    
    float offsetX;

    void Awake()
    {
        material = Instantiate(material);
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        
        sr.material = material;
    }

    void Start()
    {
        SetMaterialData();
    }

    void SetMaterialData()
    {
        material.SetFloat("_Speed", GameManager.instance.CurSpeed);
        material.SetFloat("_Rate", speedRate);
    }

    void Update()
    {
        if (GameManager.instance.Running)
        {
            offsetX += Time.deltaTime;
            material.SetFloat("_OffsetX", offsetX);
        }
    }
    
}
