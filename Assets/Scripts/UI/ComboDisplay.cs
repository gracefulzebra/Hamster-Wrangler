using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float lifetime;
    public float minDist;
    public float maxDist;
    public float duration;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        //float direction = -Random.rotation.eulerAngles.z;
        iniPos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = -2f * transform.position;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = duration / 2;

        if (timer > duration) 
            Destroy(gameObject);
        else if (timer > fraction) 
            text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (duration - fraction));

        transform.position = Vector3.Lerp(iniPos, targetPos, timer / lifetime);
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer / lifetime);
    }

    public void SetComboText(int comboSize)
    {
        text.text = comboSize.ToString();
    }
}
