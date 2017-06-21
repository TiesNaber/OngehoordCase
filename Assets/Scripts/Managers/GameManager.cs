using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager GM;

    [SerializeField]
    string[] songName;
    [SerializeField]
    int[] songLength;

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

    List<float[]> finalSong;
    public List<float[]> FinalSong {
        get { return finalSong; }
        set { finalSong = value; }
    }
    float[] finalTriggers;
    public float[] FinalTriggers {
        get { return finalTriggers; }
        set { finalTriggers = value; }
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

    [SerializeField]
    private AudioClip[] allSongs;
    public AudioClip AllSongs
    {
        get
        {
            return allSongs[arrayIndex];
        }
    }

    public bool insert;
    public bool finals;



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

    public List<float[]> GetSong()
    {
        if (!finals)
            return Song;
        else
            return FinalSong;
    }

    public float[] GetTriggers()
    {
        if (!finals)
            return Triggers;
        else
            return FinalTriggers;
    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(1);

        float fillAmount = 1 / songName.Length;

        for (int i = 0; i < songName.Length; i++)
        {
            songs.Add(db.ExtractData(songName[i], songLength[i]));
            triggers.Add(db.ExtractTriggerData("Triggers", i));
        }

        FinalSong = db.ExtractData("FinalSong", 7095);
        FinalTriggers = db.ExtractTriggerData("Triggers", 10);

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SetupHighscoreData");

    }
}
