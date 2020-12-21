using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uitleg : MonoBehaviour
{
    public GameObject[] images = new GameObject[4];
    public GameObject button1;
    public GameObject button2;
    
    VRInput input;
    
    string[] uitlegtext = new string[4];
    public Text uitext;
    // Start is called before the first frame update
    private void Awake() {
        input = GameObject.Find("EventSystem").GetComponent<VRInput>();
        uitlegtext[0] = "DNA wordt omgezet naar RNA";
        uitlegtext[1] = "De codons op de RNA streng worden afgelezen en het anticodon komt naar de codon \n UCA => AGU";
        uitlegtext[2] = "Het ribosoom koppelt het aminozuur aan een grote keten (een aminozuurketen)";
        uitlegtext[3] = "Deze aminozuurketen wordt opgevouwen tot een bol. Dit is een eiwit.";
        uitext.text = uitlegtext[input.currentpanel];
        button2.SetActive(false);
    }
    

    // Update is called once per frame
    public void NextPanel(){
        input.currentpanel++;
        if(input.currentpanel == (uitlegtext.Length - 1)){
            button1.SetActive(false);
            
        }
        else{
           button1.SetActive(true); 
           button2.SetActive(true);
        }
        
        foreach (var image in images)
        {
            image.SetActive(false);
            
        }
        images[input.currentpanel].SetActive(true);
        uitext.text = uitlegtext[input.currentpanel];
        


    }

    public void PreviousPanel(){
        input.currentpanel--;
        if(input.currentpanel == 0){
            button2.SetActive(false);
            
        }else{
            button2.SetActive(true);
            button1.SetActive(true);
        }
        
        foreach (var image in images)
        {
            image.SetActive(false);
            
        }
        images[input.currentpanel].SetActive(true);
        uitext.text = uitlegtext[input.currentpanel];
        Debug.Log("currentpanel");
        


    }



}
