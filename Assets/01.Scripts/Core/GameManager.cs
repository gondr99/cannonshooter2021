using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Debri _debriPrefab;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple GameManager is running");
        Instance = this;
    }


    public void MakeDebris(float ratio, Sprite sprite, Vector3 pos, Vector3 forceDir, float power, int debriCount = 10)
    {
        Texture2D originalTex = sprite.texture;

        int debriSize = 8; //가로 세로 5픽셀의 조각들(텍스쳐 크기 기준임)
        
        List<Texture2D> pieceList = new List<Texture2D>();
        for(int i = 0; i < debriCount; i++)
        {
            Texture2D debriTexture = new Texture2D(debriSize, debriSize);
            int xStart = Random.Range(0, originalTex.width - debriSize);
            int yStart = Random.Range(0, originalTex.height - debriSize);
            SetPixel(xStart, yStart, debriSize, debriTexture, originalTex);
            debriTexture.Apply(); //정해진 픽셀에 따라 반영

            pieceList.Add(debriTexture);
        }

        for(int i = 0; i < debriCount; i++)
        {
            Sprite s = Sprite.Create(pieceList[i], new Rect(0, 0, debriSize, debriSize), Vector2.one * 0.5f, ratio);

            Debri d = Instantiate(_debriPrefab, pos, Quaternion.Euler(0, 0, Random.Range(0, 360f)));
            d.SetSprite(s);

            float spreadAngle = Random.Range(-30f, 30f);
            Vector3 spreadVector = Quaternion.Euler(0, 0, spreadAngle) * forceDir;

            d.AddForce(spreadVector * power);
        }

    }

    private void SetPixel(int x, int y, int size, Texture2D tex, Texture2D originalTex)
    {
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                Color c = originalTex.GetPixel(x + j, y + i); //원본텍스쳐에서 복사
                tex.SetPixel(j, i, c);
            }
        }
    }
}
