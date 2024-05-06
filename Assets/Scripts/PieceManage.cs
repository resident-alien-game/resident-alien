using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManage : MonoBehaviour
{
    public List<GameObject> pieces;
    public void setMaxPiece(int value)
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].SetActive(false);
            pieces[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        for (int i = 0; i < value; i++)
        {
            pieces[i].SetActive(true);
            pieces[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void setCheckMark(int value)
    {
        if (value > 0)
        {
            pieces[value - 1].transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void setMissingPiece(int value)
    {
        if (value > 0)
        {
            Destroy(pieces[value]);
        }
    }
}
