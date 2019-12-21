using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowQualification : MonoBehaviour
{
    public List<GameObject> EntrepriseGdeb;
    public List<GameObject> EntrepriseDeb;
    public List<GameObject> EntrepriseConf;
    public List<GameObject> EntrepriseExpj;
    public List<GameObject> EntrepriseExp;

    public EnumQualif.StateQualif state;

   /* [SerializeField]
    public Dropdown dropdownPermis;*/

    public List<GameObject> Selection = new List<GameObject>();
    public List<GameObject> TotalEntreprise = new List<GameObject>();

    private int i = 0;

    private void Start()
    {
        state = EnumQualif.StateQualif.None;
        TotalEntreprise.AddRange(EntrepriseGdeb);
        TotalEntreprise.AddRange(EntrepriseDeb);
        TotalEntreprise.AddRange(EntrepriseConf);
        TotalEntreprise.AddRange(EntrepriseExpj);
        TotalEntreprise.AddRange(EntrepriseExp);
      
    }

    void ChangeLegende()
    {
        switch (state)
        {
            case EnumQualif.StateQualif.None:
                if (TotalEntreprise.Count != 0)
                {
                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "Pas de qualification choisie";

                    foreach (GameObject go in EntrepriseGdeb)
                    {
                        go.SetActive(true);
                        go.tag = "ENT";

                    }
                    foreach (GameObject go in EntrepriseDeb)
                    {
                        go.SetActive(true);
                        go.tag = "ENT";

                    }

                    foreach (GameObject go in EntrepriseConf)
                    {
                        go.SetActive(true);
                        go.tag = "ENT";

                    }

                    foreach (GameObject go in EntrepriseExpj)
                    {
                        go.SetActive(true);
                        go.tag = "ENT";

                    }

                    foreach (GameObject go in EntrepriseExp)
                    {
                        go.SetActive(true);
                        go.tag = "ENT";

                    }
                    Selection.Clear();


                }
                else
                {

                    Debug.Log("Liste vide !");
                    break;
                }
                break;
            case EnumQualif.StateQualif.Gdeb:

                #region Reinitialisation
                foreach (GameObject go in EntrepriseGdeb)
                {
                    go.SetActive(true);

                }
                foreach (GameObject go in EntrepriseDeb)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseConf)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseExpj)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseExp)
                {
                    go.SetActive(true);

                }
                #endregion

                if (EntrepriseGdeb.Count != 0)
                {


                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "Aucun diplôme / BEP / Equivalence / 0 année d'expérience dans le métier.";

                    foreach (GameObject go in EntrepriseDeb)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseConf)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseExpj)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseExp)
                    {
                        go.SetActive(false);

                    }
                    Selection.Clear();
                    Selection.AddRange(EntrepriseGdeb);

                }
                else
                {
                    Debug.Log("Liste vide !");
                    break;

                }
                break;
            case EnumQualif.StateQualif.Deb:

                #region Reinitialisation
                foreach (GameObject go in EntrepriseGdeb)
                {
                    go.SetActive(true);

                }
                foreach (GameObject go in EntrepriseDeb)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseConf)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseExpj)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseExp)
                {
                    go.SetActive(true);

                }
                #endregion


                if (EntrepriseDeb.Count != 0)
                {


                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "CAP / BAC / Equivalence / 1-2 années d'expérience dans le métier.";

                    foreach (GameObject go in EntrepriseGdeb)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseConf)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseExpj)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseExp)
                    {
                        go.SetActive(false);

                    }
                    Selection.Clear();
                    Selection.AddRange(EntrepriseDeb);


                }
                else
                {
                    Debug.Log("Liste vide !");
                    break;
                }
                break;
            case EnumQualif.StateQualif.Conf:

                #region Reinitialisation
                foreach (GameObject go in EntrepriseGdeb)
                {
                    go.SetActive(true);

                }
                foreach (GameObject go in EntrepriseDeb)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseConf)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseExpj)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseExp)
                {
                    go.SetActive(true);

                }
                #endregion


                if (EntrepriseConf.Count != 0)
                {

                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "Bac+2 / Bac+3 / Equivalence / 3-5 années d'expérience dans le métier.";

                    foreach (GameObject go in EntrepriseGdeb)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseDeb)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseExpj)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseExp)
                    {
                        go.SetActive(false);

                    }
                    Selection.Clear();
                    Selection.AddRange(EntrepriseConf);



                }
                else
                {
                    Debug.Log("Liste vide !");
                    break;
                }
                break;
            case EnumQualif.StateQualif.Expj:

                #region Reinitialisation
                foreach (GameObject go in EntrepriseGdeb)
                {
                    go.SetActive(true);

                }
                foreach (GameObject go in EntrepriseDeb)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseConf)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseExpj)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseExp)
                {
                    go.SetActive(true);

                }
                #endregion


                if (EntrepriseExpj.Count != 0)
                {

                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "Bac+4 / Bac+5 / Equivalence / 5-10 années d'expérience dans le métier.";

                    foreach (GameObject go in EntrepriseGdeb)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseDeb)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseConf)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseExp)
                    {
                        go.SetActive(false);

                    }
                    Selection.Clear();
                    Selection.AddRange(EntrepriseExpj);


                }
                else
                {
                    Debug.Log("Liste vide !");
                    break;
                }
                break;
            case EnumQualif.StateQualif.Exp:

                #region Reinitialisation
                foreach (GameObject go in EntrepriseGdeb)
                {
                    go.SetActive(true);

                }
                foreach (GameObject go in EntrepriseDeb)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseConf)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseExpj)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseExp)
                {
                    go.SetActive(true);

                }
                #endregion


                if (EntrepriseExp.Count != 0)
                {


                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "> Bac+5 / Equivalence / > 10 années d'expérience dans le métier.";

                    foreach (GameObject go in EntrepriseGdeb)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseDeb)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseConf)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseExpj)
                    {
                        go.SetActive(false);

                    }
                    Selection.Clear();
                    Selection.AddRange(EntrepriseExp);


                }
                else
                {
                    Debug.Log("Liste vide !");
                    break;
                }
                break;
            default:
                Debug.Log("Erreur");
                break;


        }
    }

    public void Update()
    {

        Invoke("ChangeLegende", 5.0F);

       

        #region coms

        /*

        if (state == EnumQualif.StateQualif.None)
        {
            if (TotalEntreprise.Count != 0)
            {
                GameObject goLeg = GameObject.Find("LegendeQualif");
                goLeg.GetComponent<Text>().text = "Pas de qualification choisie";

                foreach (GameObject go in EntrepriseBAC1)
                {
                    go.SetActive(true);

                }
                foreach (GameObject go in EntrepriseBAC2)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC3)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC4)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC5)
                {
                    go.SetActive(true);

                }
            }
            else
            {

                Debug.Log("Liste vide !");

            }

        }
        else if (state == EnumQualif.StateQualif.Bac1)
        {
            if (EntrepriseBAC1.Count != 0)
            {
                if (click == true)
                {
                    state = EnumQualif.StateQualif.None;
                    click = false;
                }
                else
                {

                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "BAC +1";

                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(false);

                    }
                    click = true;

                }
            }
            else
            {
                Debug.Log("Liste vide !");
            }
        }
        else if (state == EnumQualif.StateQualif.Bac2)
        {
            if (EntrepriseBAC2.Count != 0)
            {
                if (click2 == true)
                {
                    state = EnumQualif.StateQualif.None;
                    click2 = false;
                }
                else
                {

                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "BAC +2";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(false);

                    }
                    click2 = true;

                }
            }
            else
            {
                Debug.Log("Liste vide !");
            }

        }
        else if (state == EnumQualif.StateQualif.Bac3)
        {
            if (EntrepriseBAC3.Count != 0)
            {
                if (click3 == true)
                {
                    state = EnumQualif.StateQualif.None;
                    click3 = false;
                }
                else
                {

                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "BAC +2";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(false);

                    }
                    click3 = true;

                }
            }
            else
            {
                Debug.Log("Liste vide !");
            }
        }
        else if (state == EnumQualif.StateQualif.Bac4)
        {
            if (EntrepriseBAC4.Count != 0)
            {
                if (click4 == true)
                {
                    state = EnumQualif.StateQualif.None;
                    click4 = false;
                }
                else
                {

                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "BAC +4";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(false);

                    }
                    click4 = true;

                }
            }
            else
            {
                Debug.Log("Liste vide !");
            }

        }
        else if (state == EnumQualif.StateQualif.Bac5)
        {
            if (EntrepriseBAC5.Count != 0)
            {
                if (click5 == true)
                {
                    state = EnumQualif.StateQualif.None;
                    click5 = false;
                }
                else
                {

                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "BAC +5";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(false);

                    }
                    click5 = true;

                }
            }
            else
            {
                Debug.Log("Liste vide !");
            }
        }*/
        #endregion
    }



    public void None()
    {
        state = EnumQualif.StateQualif.None;
    }

    public void Gdeb()
    {
        state = EnumQualif.StateQualif.Gdeb;

        #region coms
        /* if (EntrepriseBAC1.Count != 0)
         {*/

        /*    if (click == true)
            {
                GameObject goLeg = GameObject.Find("LegendeQualif");
                goLeg.GetComponent<Text>().text = "Pas de qualification choisie";

                foreach (GameObject go in EntrepriseBAC2)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC3)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC4)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC5)
                {
                    go.SetActive(true);

                }
                click = false;
            }
            else
            {
                if (click2 == false && click3 == false && click4 == false && click5 == false)
                {

                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "BAC +1";

                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(false);

                    }
                    click = true;
                }
                else
                {
                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "Pas de qualification choisie";
                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(true);
                    }
                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(true);

                    }
                    click = false;
                    click2 = false;
                    click3 = false;
                    click4 = false;
                    click5 = false;

                }
            }

        }
        else
        {
            Debug.Log("Liste vide !");
        }
        */
        #endregion

    }

    // Update is called once per frame
    public void Deb()
    {
        state = EnumQualif.StateQualif.Deb;

        #region coms
        /*if (EntrepriseBAC2.Count != 0)
        {
            if (click2 == true)
            {
                GameObject goLeg = GameObject.Find("LegendeQualif");
                goLeg.GetComponent<Text>().text = "Pas de qualification choisie";

                foreach (GameObject go in EntrepriseBAC1)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC3)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC4)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC5)
                {
                    go.SetActive(true);

                }
                click2 = false;
            }
            else
            {
                if (click == false && click3 == false && click4 == false && click5 == false)
                {
                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "BAC +2";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(false);

                    }
                    click2 = true;
                }
                else
                {
                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "Pas de qualification choisie";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(true);
                    }
                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(true);

                    }
                    click = false;
                    click2 = false;
                    click3 = false;
                    click4 = false;
                    click5 = false;
                }
            }
        }
        else
        {
            Debug.Log("Liste vide !");
        }*/
        #endregion

    }

    public void Conf()
    {
        state = EnumQualif.StateQualif.Conf;

        #region coms
        /*if (EntrepriseBAC3.Count != 0)
        {
            if (click3 == true)
            {

                GameObject goLeg = GameObject.Find("LegendeQualif");
                goLeg.GetComponent<Text>().text = "Pas de qualification choisie";
                foreach (GameObject go in EntrepriseBAC1)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC2)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC4)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC5)
                {
                    go.SetActive(true);

                }
                click3 = false;
            }
            else
            {
                if (click == false && click3 == false && click4 == false && click5 == false)
                {
                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "BAC +3";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(false);

                    }
                    click3 = true;
                }
                else
                {
                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "Pas de qualification choisie";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(true);
                    }
                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(true);

                    }
                    click = false;
                    click2 = false;
                    click3 = false;
                    click4 = false;
                    click5 = false;
                }
            }
        }
        else
        {
            Debug.Log("Liste vide !");
        }*/
        #endregion
    }

    public void Expj()
    {
        state = EnumQualif.StateQualif.Expj;

        #region coms
        /*if (EntrepriseBAC4.Count != 0)
        {
            if (click4 == true)
            {
                GameObject goLeg = GameObject.Find("LegendeQualif");
                goLeg.GetComponent<Text>().text = "Pas de qualification choisie";
                foreach (GameObject go in EntrepriseBAC1)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC2)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC3)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC5)
                {
                    go.SetActive(true);

                }
                click4 = false;
            }
            else
            {
                if (click == false && click3 == false && click4 == false && click5 == false)
                {
                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "BAC +4";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(false);

                    }
                    click4 = true;
                }
                else
                {
                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "Pas de qualification choisie";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(true);
                    }
                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(true);

                    }
                    click = false;
                    click2 = false;
                    click3 = false;
                    click4 = false;
                    click5 = false;
                }
            }
        }
        else
        {
            Debug.Log("Liste vide !");
        }*/
        #endregion
    }

    public void Exp()
    {
        state = EnumQualif.StateQualif.Exp;

        #region coms
        /*if (EntrepriseBAC5.Count != 0)
        {
            if (click5 == true)
            {
                GameObject goLeg = GameObject.Find("LegendeQualif");
                goLeg.GetComponent<Text>().text = "Pas de qualification choisie";
                foreach (GameObject go in EntrepriseBAC1)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC2)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC3)
                {
                    go.SetActive(true);

                }

                foreach (GameObject go in EntrepriseBAC4)
                {
                    go.SetActive(true);

                }
                click5 = false;
            }
            else
            {
                if (click == false && click3 == false && click4 == false && click5 == false)
                {
                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "BAC +5";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(false);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(false);

                    }
                    click5 = true;
                }
                else
                {
                    GameObject goLeg = GameObject.Find("LegendeQualif");
                    goLeg.GetComponent<Text>().text = "Pas de qualification choisie";

                    foreach (GameObject go in EntrepriseBAC1)
                    {
                        go.SetActive(true);
                    }
                    foreach (GameObject go in EntrepriseBAC2)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC3)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC4)
                    {
                        go.SetActive(true);

                    }

                    foreach (GameObject go in EntrepriseBAC5)
                    {
                        go.SetActive(true);

                    }
                    click = false;
                    click2 = false;
                    click3 = false;
                    click4 = false;
                    click5 = false;
                }
            }
        }
        else
        {
            Debug.Log("Liste vide !");
        }*/
        #endregion
    }

    #region dropdowncom
    /* dropdownPermis.onValueChanged.RemoveListener(HandleValueChanged);
         dropdownPermis.onValueChanged.AddListener(delegate
         {
             HandleValueChanged(dropdownPermis.value);
 });
     public void HandleValueChanged(int newValue)
 {

     switch (newValue)
     {
         case 0:

             if (state == EnumQualif.StateQualif.None)
             {


                 foreach (GameObject go in TotalEntreprise)
                 {
                     GameObject child = go.transform.Find("Paneltag").gameObject;
                     GameObject child2 = child.GetComponentInChildren<Text>().gameObject;

                     rese.GetComponent<Text>().text = child.name;
                     res.GetComponent<Text>().text = child2.name;

                     if (child2.GetComponent<Text>().text.Contains("#Caces1") == true)
                     {
                         go.SetActive(true);
                     }
                     else
                     {
                         go.SetActive(false);
                     }

                 }
             }
             break;

         case 1:

             break;
         default:
             break;

     }
 }

 private void OnDestroy()
 {
     // To avoid errors also remove listeners as soon as they
     // are not needed anymore
     // Otherwise in the case this object is destroyed but the dropdown is not
     // it would still try to call your listener -> Exception
     dropdownPermis.onValueChanged.RemoveListener(HandleValueChanged);
 }*/
    #endregion

}
