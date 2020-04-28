using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class dialogueNode : MonoBehaviour
{
    string speaker;
    string text;
    private int index;
    private bool children = false;

    private Dictionary<int, string> nextNodes = new Dictionary<int, string>();

    public dialogueNode(int i, string speaker, string text, string next)
    {
        this.index = i;
        this.speaker = speaker;
        this.text = text;
        string[] nums = next.Split(' ');
        foreach (string num in nums)
        {
            try
            {
                int current = Int32.Parse(num);
                if (current != -1)
                {
                    nextNodes.Add(current, "");
                    children = true;
                }
                
            }
            catch(FormatException)
            { print(num + " was not an integer"); }
        }
    }

    public void updateDict(int i, string text)
    {
        if (index == i || nextNodes.ContainsKey(i) == false)
        {
            return;
        }
        nextNodes[i] = text;
    }

    public override string ToString() 
    {
        string toReturn = "";
        toReturn += speaker + ":\n\t" + text;
        if (children)
        {
            int i = 1;
            foreach (string response in getResponses())
            {
                toReturn += "\n\t\t" + i + ") " + response;
                i++;
            }
        }

        return toReturn;
    }

    public string getSpeaker() { return speaker; }
    public int getIndex() { return index; }
    public string getText() { return text; }
    public bool hasChildren() { return children; }
    public int[] getResponseIndices() { return nextNodes.Keys.ToArray(); }
    public string[] getResponses()
    {
        string[] responses = new string[nextNodes.Count];
        int i = 0;
        foreach(KeyValuePair<int, string> entry in nextNodes)
        {
            responses[i] = entry.Value;
            i++;
        }
        return responses;
    }

    
}


[System.Serializable]
public class SpritePair : ScriptableObject
{
    public string speaker;
    public Sprite sprite;
    public SpritePair(string speaker, Sprite sprite)
    {
        this.speaker = speaker;
        this.sprite = sprite;
    }
}



public class dialogue : MonoBehaviour
{
    
    public TextAsset responses;
    private List<string> lines;

    private string currentText;
    private string currentSpeaker;
    private List<String> currentResponses;

    public Text displayText;
    public Text displaySpeaker;
    public Image speakerSprite;
    public List<Sprite> speakerSprites = new List<Sprite>();
    public List<Button> responseButtons;
    public List<SpritePair> spritePairs = new List<SpritePair>();



    private List<dialogueNode> nodes = new List<dialogueNode>();
    private List<string> speakers = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        lines = responses.text.Split('\n').ToList();
        foreach (string line in lines)
        {
            string[] currentLine = line.Split('|');
            if (currentLine.Length == 3)
            {
                dialogueNode test = new dialogueNode(nodes.Count, currentLine[0], currentLine[1], currentLine[2]);
                nodes.Add(test);
                if (speakers.Contains(currentLine[0]) == false)
                {
                    speakers.Add(currentLine[0]);
                }
            }
            else
            {
                throw new FormatException('"' + line + '"' + " is not formatted correctly");
            }
        }
        foreach (string speaker in speakers)
        {
            SpritePair current = new SpritePair(speaker, null);
            spritePairs.Add(current);
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            foreach (int j in nodes[i].getResponseIndices())
            {
                nodes[i].updateDict(j, nodes[j].getText());
            }
            //print(nodes[i].ToString());
        }
        currentSpeaker = nodes[0].getSpeaker();
        currentText = nodes[0].getText();
        currentResponses = nodes[0].getResponses().ToList();
        updateScreen();
    }

    private int speakerNum(string speaker)
    {
        switch (speaker)
        {
            case "Dane":
                return 0;
            case "Tester":
                return 1;
    
            default:
                return 0;
        }
    }

    void updateScreen()
    {
        displaySpeaker.text = currentSpeaker;
        displayText.text = currentText;
        speakerSprite.sprite = speakerSprites[speakerNum(currentSpeaker)];
    }

}
