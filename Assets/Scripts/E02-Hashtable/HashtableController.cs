using System.Collections;
using TMPro;
using UnityEngine;

public class HashtableController : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text ageText;
    [SerializeField] private TMP_Text genderText;
    [SerializeField] private TMP_Text jobText;

    private Hashtable userInfo = new Hashtable();

    private void Start()
    {
        userInfo.Add("name", "ȫ�浿");
        userInfo.Add("age", "23");
        userInfo.Add("gender", "����");
        userInfo.Add("job", "���α׷���");

        nameText.text = userInfo["name"].ToString();
        ageText.text = userInfo["age"].ToString();
        genderText.text = userInfo["gender"].ToString();
        jobText.text = userInfo["job"].ToString();
    }
}
