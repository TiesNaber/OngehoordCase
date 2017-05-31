using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager GM;

    [SerializeField]
    string[] songName;

    List<List<float[]>> songs;
    public List<float[]> Song
    {
        get { return songs[arrayIndex]; }
    }
    List<float[]> triggers;
    public float[] Triggers
    {
        get { return triggers[arrayIndex]; }
    }

    [SerializeField]
    LoadSong db;

    //Getter/Setter
    private string _school;
    public string School
    {
        set { _school = value; }
        get { return _school; }
    }

    private string _class;
    private List<string> _classes = new List<string>();
    public string CurrentClass
    {
        set
        {
            _class = value;
            _classes.Add(_class);
        }
        get { return _class; }
    }
    public List<string> Classes
    {
        get { return _classes; }
    }

    private string _name;
    public string Name
    {
        set { _name = value; }
        get { return _name; }
    }

    private int _dbIndex;
    public int DBIndex
    {
        set { _dbIndex += value; }
        get { return _dbIndex; }
    }

    private bool _female;
    public bool Female
    {
        set { _female = value; }
        get { return _female; }
    }

    private int arrayIndex;
    public int ArrayIndex
    {
        set { arrayIndex = value; }
        get { return arrayIndex; }
    }

    private int inputPhase = 0;
    public int InputPhase
    {
        get { return inputPhase; }
        set { inputPhase = value; }
    }

    public bool insert;



	// Use this for initialization
	void Start () {
        songs = new List<List<float[]>>();
        triggers = new List<float[]>();
        StartCoroutine(Setup());
	}
	
    void Awake()
    {
        MakeThisTheOnlyGameManager();
    }
	
    void MakeThisTheOnlyGameManager()
    {
        if(GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else
        {
            if (GM != this)
                Destroy(gameObject);
        }
    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(1);

        float fillAmount = 1 / songName.Length;

        for (int i = 0; i < songName.Length; i++)
        {
            songs.Add(db.ExtractData(songName[i]));
            triggers.Add(db.ExtractTriggerData("Triggers", i));
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SetupHighscoreData");

    }
}
