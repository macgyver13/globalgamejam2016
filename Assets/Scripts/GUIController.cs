using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {


	public ballModifier[] modifierList;
	public float iconGapDistance;
	public RectTransform containerRectTransform;
	public Transform infoTransform;

	public static GUIController instance = null;
	public static GUIController GetInstance()
	{
		if(instance == null)
		{
			instance = new GUIController();
		}

		return instance;
	}

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		//Get a component reference to the attached BoardManager script
		//boardScript = GetComponent<BoardManager>();

		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	void InitGame()
	{
		
	}

	// Use this for initialization
	void Start () {
		
		RectTransform itemTransform = GameManager.instance.GetModifierCard(modifierList[0]).GetComponent<RectTransform> ();
		float totalWidth = modifierList.Length * itemTransform.rect.width + (modifierList.Length -1 * iconGapDistance);
		float startingXPostion = containerRectTransform.rect.center.x - totalWidth / 2;
		float startingMaxYPostion = containerRectTransform.rect.yMax;
			
		for (int i = 0; i < modifierList.Length; i++) {
//			Debug.Log ("X " + startingXPostion + " Y:" + startingMaxYPostion + " I:" + i);
			itemTransform = GameManager.instance.GetModifierCard(modifierList[i]).GetComponent<RectTransform> ();
			GameObject newItem = Instantiate (GameManager.instance.GetModifierCard(modifierList[i])) as GameObject;
			newItem.name = "icon " + modifierList[i];
			newItem.transform.parent = infoTransform;
			RectTransform rectTransform = newItem.GetComponent<RectTransform> ();
//			Debug.Log ("left X " + startingXPostion + i * iconGapDistance + " right X:" + startingXPostion + i * iconGapDistance + itemTransform.rect.width + " I:" + i);
			rectTransform.offsetMin = new Vector2 (startingXPostion + i * iconGapDistance + i * itemTransform.rect.width, startingMaxYPostion - itemTransform.rect.height);
			rectTransform.offsetMax = new Vector2 (startingXPostion + i * iconGapDistance + i * itemTransform.rect.width + itemTransform.rect.width, startingMaxYPostion);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
