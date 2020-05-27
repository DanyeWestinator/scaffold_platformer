﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine.UI;


public class Node
{
    string speaker;
    string text;
    string sourceTitle;
    bool children = false;

    private Dictionary<string, string> nextNodes = new Dictionary<string, string>();
    private List<string> functions = new List<string>();

    public Node(string title, string speaker, string text)
    {
        this.sourceTitle = title;
        this.speaker = speaker;
        string pattern = @"\[\[[\w:.\|]*\]\]";
        MatchCollection next = Regex.Matches(text, pattern);
        foreach (Match x in next)
        {
            string nextNode = x.ToString().Substring(x.ToString().IndexOf('|') + 1).Replace("]]", "");
            if (nextNodes.ContainsKey(nextNode) == false)
            {
                nextNodes[nextNode] = "Placeholder";
                children = true;
            }
        }
        string pattern2 = @"%%%[\w ]*%%%";
        next = Regex.Matches(text, pattern2);
        foreach (Match x in next)
        {
            string temp = x.Value;
            temp = temp.Replace("%", "");
            Debug.Log(temp);
            temp = temp.Replace("%%%", "");
            functions.Add(temp);
        }
        this.text = Regex.Replace(text, pattern, "").Trim();
        this.text = Regex.Replace(this.text, pattern2, "");
    }

    public void updateDict(string next, string text)
    {
        if (nextNodes.ContainsKey(next) == true && next != this.sourceTitle)
        {
            nextNodes[next] = text;
        }
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
        if (functions.Count > 0)
        {
            toReturn += "\nFunctions:";
            foreach (string s in functions)
            {
                toReturn += "\n\t" + s;
            }
        }

        return toReturn;
    }

    //getters and setters
    public string getSpeaker() { return speaker; }
    public string getText() { return text; }
    public string getTitle() { return sourceTitle; }
    public bool hasChildren() { return children; }
    public List<string> getFunctions() { return functions; }
    public List<string> getResponseTitles() { return nextNodes.Keys.ToList(); }

    public List<string> getResponses()
    {
        List<string> responses = new List<string>();
        foreach (KeyValuePair<string, string> response in nextNodes)
        {
            responses.Add(response.Value);
        }

        return responses;
    }
}


public class dialogueJSON : MonoBehaviour
{
    public TextAsset JSONSource;
    public bool printList = false;
    
    //intenal things
    private List<Node> nodes = new List<Node>();
    private Node currentNode;

    //display things
    public List<Button> responseButtons = new List<Button> { null, null, null, null };
    public Text displayText;
    public Text speakerText;
    private Sprite speakerSprite;
    public Image speakerImage;

    // Start is called before the first frame update
    void Start()
    {
        parseJSON();
        currentNode = nodes[0];
        updateDisplay();
    }

    void updateDisplay()
    {
        displayText.text = currentNode.getText();
        speakerText.text = "Speaking:\n<b>" + currentNode.getSpeaker() + "</b>";
        speakerSprite = Resources.Load<Sprite>(currentNode.getSpeaker());
        speakerImage.sprite = speakerSprite;
        for (int i = 0; i < 4; i++)
        {
            var responses = currentNode.getResponses();
            responseButtons[i].gameObject.SetActive(responses.Count > i);
            if (i < responses.Count)
            {
                responseButtons[i].GetComponentInChildren<Text>().text = responses[i];


                //PLAYEST ME!!!!!!
                //if (nodes.Find(next => next.getTitle() == currentNode.getResponseTitles()[i]).getSpeaker() != "Player")
                if (responses.Count == 1)
                {
                    responseButtons[i].GetComponentInChildren<Text>().text = "Continue";
                }
            }
           
        }
    }

    public void getButtonPressed(int i)
    {
        string nextTitle = currentNode.getResponseTitles()[i];
        currentNode = nodes.Find(next => next.getTitle() == nextTitle);
        functions();
        updateDisplay();
    }

    void functions()
    {
        if (currentNode.getFunctions().Contains("lowerScore 1000"))
        {
            ScoreScript.scoreValue -= 1000;
        }
    }
    
    void parseJSON()
    {
        string JSONtext = JSONSource.text;
        JSONtext = JSONtext.Replace("[i]", "<i>");
        JSONtext = JSONtext.Replace("[/i]", "</i>");
        JSONNode data = JSON.Parse(JSONtext);
        foreach (JSONNode record in data)
        {
            Node currentNode = new Node(record["title"], record["tags"], record["body"]);
            nodes.Add(currentNode);
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            foreach (string currentTitle in nodes[i].getResponseTitles())
            {
                
                nodes[i].updateDict(currentTitle, nodes.Find(node => node.getTitle() == currentTitle).getText());
            }
        }
        if (printList) { foreach (Node x in nodes) { print(x.ToString());} }
    }
}
