using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShower : MonoBehaviour
{
    public GameObject hrDD;
    public GameObject rrDD;
    public GameObject bpDD;
    public GameObject tempDD;
    public GameObject osDD;
    public GameObject hr1;
    public GameObject hr2;
    public GameObject hr3;
    public GameObject hr4;
    public GameObject hr5;
    public GameObject hr6;
    public GameObject hr7;
    public GameObject hr8;
    public GameObject rrh;
    public GameObject rruh;
    public GameObject temph;
    public GameObject templow;
    public GameObject temphigh;
    public GameObject osh;
    public GameObject osuh;
    public GameObject pathealthy;
    public GameObject grimreaper;

    public bool DDOptionsShown = false;
    public bool HROptionsShown = false;
    public bool RROptionsShown = false;
    public bool BPOptionsShown = false;
    public bool TEMPOptionsShown = false;
    public bool OSOptionsShown = false;
    // Start is called before the first frame update
    void Start()
    {
        DDOptionsOff();
        HROptionsOff();
        RROptionsOff();
        TEMPOptionsOff();
        OSOptionsOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DDOptionsOff()
    {
        hrDD.SetActive(false);
        rrDD.SetActive(false);
        bpDD.SetActive(false);
        tempDD.SetActive(false);
        osDD.SetActive(false);
        pathealthy.SetActive(false);
        grimreaper.SetActive(false);
    }
    void DDOptionsOn()
    {
        hrDD.SetActive(true);
        rrDD.SetActive(true);
        bpDD.SetActive(true);
        tempDD.SetActive(true);
        osDD.SetActive(true);
        pathealthy.SetActive(true);
        grimreaper.SetActive(true);
    }
    void HROptionsOff()
    {
        hr1.SetActive(false);
        hr2.SetActive(false);
        hr3.SetActive(false);
        hr4.SetActive(false);
        hr5.SetActive(false);
        hr6.SetActive(false);
        hr7.SetActive(false);
        hr8.SetActive(false);
    }
    void HROptionsOn()
    {
        hr1.SetActive(true);
        hr2.SetActive(true);
        hr3.SetActive(true);
        hr4.SetActive(true);
        hr5.SetActive(true);
        hr6.SetActive(true);
        hr7.SetActive(true);
        hr8.SetActive(true);
    }
    void RROptionsOff()
    {
        rrh.SetActive(false);
        rruh.SetActive(false);
    }
    void RROptionsOn()
    {
        rrh.SetActive(true);
        rruh.SetActive(true);
    }
    //INSERT BP STUFF HERE
    void TEMPOptionsOff()
    {
        temph.SetActive(false);
        temphigh.SetActive(false);
        templow.SetActive(false);
    }
    void TEMPOptionsOn()
    {
        temph.SetActive(true);
        temphigh.SetActive(true);
        templow.SetActive(true);
    }
    void OSOptionsOff()
    {
        osh.SetActive(false);
        osuh.SetActive(false);
    }
    void OSOptionsOn()
    {
        osh.SetActive(true);
        osuh.SetActive(true);
    }

    public void OptionsManager()
    {
        if (DDOptionsShown == false)
        {
            DDOptionsOn();
            DDOptionsShown = true;
        }
        else
        {
            DDOptionsOff();
            DDOptionsShown = false;
            HROptionsOff();
            HROptionsShown = false;
            RROptionsOff();
            RROptionsShown = false;
            TEMPOptionsOff();
            TEMPOptionsShown = false;
            OSOptionsOff();
            OSOptionsShown = false;
        }
    }
    public void HROptionsManager()
    {
        if (HROptionsShown == false)
        {
            HROptionsOn();
            HROptionsShown = true;
            RROptionsOff();
            RROptionsShown = false;
            TEMPOptionsOff();
            TEMPOptionsShown = false;
            OSOptionsOff();
            OSOptionsShown = false;
        }
        else
        {
            HROptionsOff();
            HROptionsShown = false;
        }
    }
    public void RROptionsManager()
    {
        if (RROptionsShown == false)
        {
            RROptionsOn();
            RROptionsShown = true;
            HROptionsOff();
            HROptionsShown = false;
            TEMPOptionsOff();
            TEMPOptionsShown = false;
            OSOptionsOff();
            OSOptionsShown = false;
        }
        else
        {
            RROptionsOff();
            RROptionsShown = false;
        }
    }
    //INSERT BP STUFF HERE
    public void TEMPOptionsManager()
    {
        if (TEMPOptionsShown == false)
        {
            TEMPOptionsOn();
            TEMPOptionsShown = true;
            HROptionsOff();
            HROptionsShown = false;
            RROptionsOff();
            RROptionsShown = false;
            OSOptionsOff();
            OSOptionsShown = false;
        }
        else
        {
            TEMPOptionsOff();
            TEMPOptionsShown = false;
        }
    }
    public void OSOptionsManager()
    {
        if (OSOptionsShown == false)
        {
            OSOptionsOn();
            OSOptionsShown = true;
            HROptionsOff();
            HROptionsShown = false;
            RROptionsOff();
            RROptionsShown = false;
            TEMPOptionsOff();
            TEMPOptionsShown = false;
        }
        else
        {
            OSOptionsOff();

            OSOptionsShown = false;
        }
    }
}
