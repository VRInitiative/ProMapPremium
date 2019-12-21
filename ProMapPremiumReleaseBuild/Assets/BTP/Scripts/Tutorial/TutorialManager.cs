using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    Material manetteMat;
    public Image img;
    public Sprite[] spr;
    public GameObject[] manette;
    public string[] tutoScript;
    public Material glow;
    public Text tutoInfo;
    public Text secondaryText;
    bool loopEnd;
    bool tutoStart;
    float timer, timerMax;

    void Start()
    {
        manetteMat = manette[0].GetComponent<MeshRenderer>().material;       
        loopEnd = false;
        tutoStart = false;
    }

    void Update()
    {
        if (!tutoStart && OVRInput.Get(OVRInput.Button.One))
        {
            tutoStart = true;
            img.enabled = false;
            StartCoroutine(ShowTutorial());
        }

        if (OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Three))
        {
            gameObject.SetActive(false);
        }

        if (loopEnd && tutoInfo.text == tutoScript[2])
        {
            tutoInfo.text = tutoScript[3];
            StartCoroutine(Fade(tutoInfo, 1, 2));
            manette[0].GetComponent<MeshRenderer>().material = glow;
            img.enabled = true;
            img.sprite = spr[0];
        }

        if (tutoInfo.text == tutoScript[3])
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 1 || Input.GetKeyDown(KeyCode.Space))
            {
                manette[0].GetComponent<MeshRenderer>().material = manetteMat;
                img.sprite = spr[1];
                tutoInfo.text = tutoScript[4];

                timer = Time.time;
                timerMax = timer + 5;
            }
        }

        if (tutoInfo.text == tutoScript[4])
        {
            timer = Time.time;

            if (timer >= timerMax)
            {
                tutoInfo.text = tutoScript[5];
                timer = Time.time;
                timerMax = timer + 5;
            }
        }

        if (tutoInfo.text == tutoScript[5])
        {
            timer = Time.time;

            if (timer >= timerMax)
            {
                tutoInfo.text = tutoScript[6];
            }
        }

        if (tutoInfo.text == tutoScript[6])
        {
            manette[1].GetComponent<MeshRenderer>().material = glow;
            img.sprite = spr[0];

            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1 || Input.GetKeyDown(KeyCode.Space))
            {
                manette[1].GetComponent<MeshRenderer>().material = manetteMat;
                img.sprite = spr[1];
                tutoInfo.text = tutoScript[7];

                timer = Time.time;
                timerMax = timer + 5;
            }
        }

        if (tutoInfo.text == tutoScript[7])
        {
            timer = Time.time;

            if (timer >= timerMax)
            {
                tutoInfo.text = tutoScript[8];
            }
        }

        if (tutoInfo.text == tutoScript[8])
        {
            img.sprite = spr[2];

            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) == 1 || OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 1)
            {
                tutoInfo.text = tutoScript[9];
                timer = Time.time;
                timerMax = timer + 3;
            }
        }

        if (tutoInfo.text == tutoScript[9])
        {
            timer = Time.time;
            secondaryText.text = "L'application va démarer" + "(" + Mathf.Round(timerMax - timer) + ")";

            if (timer >= timerMax)
            {
                gameObject.SetActive(false);
            }
        }
    }


    IEnumerator ShowTutorial()
    {
        WaitForSeconds waitSlide = new WaitForSeconds(4);
        WaitForSeconds waitFade = new WaitForSeconds(2);

        for (int i = 0; i < 3; i++)
        {
            if (tutoInfo.color.a == 1)
            {
                tutoInfo.text = tutoScript[i];

                yield return waitSlide;

                StartCoroutine(Fade(tutoInfo, 0, 2));

                yield return waitFade;
            }
            else
            {
                tutoInfo.text = tutoScript[i];
                StartCoroutine(Fade(tutoInfo, 1, 2));

                yield return waitSlide;

                StartCoroutine(Fade(tutoInfo, 0, 2));

                yield return waitFade;
            }
            
        }
        loopEnd = true;
    }

    IEnumerator Fade (Text text, float targetOpacity, float duration)
    {
        Color color = text.color;
        float startOpacity = color.a;
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / duration);
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);
            text.color = color;
            yield return null;
        }
    }
}

