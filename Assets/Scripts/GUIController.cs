using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {
    public GameObject[] modifierPrefabList;

    public ballModifier[] modifierList;
	public float iconGapDistance;
	public float selectionIconBorderPadding;
	public RectTransform infoRectTransform;
	public Transform infoTransform;
	public RectTransform selectionIconTransform;

	public static GUIController instance = null;
	public static GUIController GetInstance()
	{
		if(instance == null)
		{
			instance = new GUIController();
		}

		return instance;
	}

    static GUIController oldInstance;

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
		//If instance already exists and it's not this:
		else if (instance != null)
        {
            oldInstance = instance;
            oldInstance.gameObject.SetActive(false);
            instance = this;
        }

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			//Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		

		//Get a component reference to the attached BoardManager script
		//boardScript = GetComponent<BoardManager>();

		//Call the InitGame function to initialize the first level 
		InitGame();
	}

    void OnDestroy()
    {
        if(oldInstance != null)
        {
            instance = oldInstance;
            instance.gameObject.SetActive(true);
        }
    }

    void InitGame()
	{
		
	}

	// Use this for initialization
	void Start () {
		
		SetupIcons ();

	}

	public void SetupIcons(){
        //Clean previous images if present
        Component[] images = GetComponentsInChildren<Image>();
		foreach (Image image in images) {
			if (image.transform.name.Contains("icon")) {
				Destroy (image.transform.gameObject);
			}
		}
			
		RectTransform itemTransform;
		float startingXPostion = 0;
		float startingMaxYPostion = 0;
//		Debug.Log ("List Length:" + modifierList.Length);
		if (modifierList.Length > 0) {
			itemTransform = GetModifierCard (modifierList [0]).GetComponent<RectTransform> ();
			float totalWidth = modifierList.Length * itemTransform.rect.width + (modifierList.Length - 1 * iconGapDistance);
			startingXPostion = infoRectTransform.rect.center.x - itemTransform.rect.width - totalWidth / 2;
			startingMaxYPostion = infoRectTransform.rect.yMax;

			//locate the arrow
			int position = 0;
			selectionIconTransform.offsetMin = new Vector2 (startingXPostion + position * iconGapDistance + position * itemTransform.rect.width - selectionIconBorderPadding, startingMaxYPostion - itemTransform.rect.height - selectionIconBorderPadding);
			selectionIconTransform.offsetMax = new Vector2 (startingXPostion + position * iconGapDistance + position * itemTransform.rect.width + itemTransform.rect.width + selectionIconBorderPadding, startingMaxYPostion + selectionIconBorderPadding);
		}
		for (int i = 0; i < modifierList.Length; i++) {
			//Debug.Log ("X " + startingXPostion + " Y:" + startingMaxYPostion + " I:" + i);
			itemTransform = GetModifierCard(modifierList[i]).GetComponent<RectTransform> ();
			GameObject newItem = Instantiate (GetModifierCard(modifierList[i])) as GameObject;
			newItem.name = "icon " + modifierList[i];
			newItem.transform.parent = infoTransform;
			RectTransform rectTransform = newItem.GetComponent<RectTransform> ();
			//			Debug.Log ("left X " + startingXPostion + i * iconGapDistance + " right X:" + startingXPostion + i * iconGapDistance + itemTransform.rect.width + " I:" + i);
			rectTransform.offsetMin = new Vector2 (startingXPostion + i * iconGapDistance + i * itemTransform.rect.width, startingMaxYPostion - itemTransform.rect.height);
			rectTransform.offsetMax = new Vector2 (startingXPostion + i * iconGapDistance + i * itemTransform.rect.width + itemTransform.rect.width, startingMaxYPostion);
		}
	}

	public void SetModifierPosition(int position){
//		Debug.Log ("Action: List Length:" + modifierList.Length);
		if (position < modifierList.Length) {
			RectTransform itemTransform = GetModifierCard (modifierList [0]).GetComponent<RectTransform> ();
			float totalWidth = modifierList.Length * itemTransform.rect.width + (modifierList.Length - 1 * iconGapDistance);
			float startingXPostion = infoRectTransform.rect.center.x - itemTransform.rect.width - totalWidth / 2;
			float startingMaxYPostion = infoRectTransform.rect.yMax;
			selectionIconTransform.offsetMin = new Vector2 (startingXPostion + position * iconGapDistance + position * itemTransform.rect.width - selectionIconBorderPadding, startingMaxYPostion - itemTransform.rect.height - selectionIconBorderPadding);
			selectionIconTransform.offsetMax = new Vector2 (startingXPostion + position * iconGapDistance + position * itemTransform.rect.width + itemTransform.rect.width + selectionIconBorderPadding, startingMaxYPostion + selectionIconBorderPadding);
		}
	}

    public GameObject GetModifierCard(ballModifier ballMod)
    {
        //		Debug.Log ("GetMod Card: " + ballMod + " mod List:" + modifierPrefabList.Length);
        return modifierPrefabList[(int)ballMod];
    }
}
