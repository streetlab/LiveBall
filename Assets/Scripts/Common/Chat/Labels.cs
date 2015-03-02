using UnityEngine;
using System.Collections;

public class Labels: MonoBehaviour {
	public GameObject item;
//	public ArrayList list;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void tmpFunction(string input_str){
		Debug.Log (input_str);

		GameObject obj = Instantiate(item, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		
		//생성된 GameObject의 부모가 누구인지 명확히 알려줍니다. (내가 니 애비다!!)
		obj.transform.parent = this.transform;
		
		//NGUI는 자동이 너무많이 짜증나니 수동으로 Scale을 조정해줍니다.
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
		
//		// Item 하위의 자식 요소들에 대한 객체를 얻어냅니다.
//		UISprite sprite = GetChildObj (obj, "Sprite").GetComponent<UISprite>(); 
		UILabel label = GetChildObj (obj, "Label").GetComponent<UILabel>(); 
//		
//		// 자식 객체들에게 이미지와 텍스트를 출력.
//		//			texture.mainTexture = images[i];
		label.text = input_str;
	
	
	//Prefab을 생성한 이후에 Position이 모두 같아서 겹쳐지므로 Reposition시키도록 합니다.
	GetComponent<UIGrid>().Reposition();
	}

	/** 객체의 이름을 통하여 자식 요소를 찾아서 리턴하는 함수 */
	GameObject GetChildObj( GameObject source, string strName  ) { 
		Transform[] AllData = source.GetComponentsInChildren< Transform >(); 
		GameObject target = null;
		
		foreach( Transform Obj in AllData ) { 
			if( Obj.name == strName ) { 
				target = Obj.gameObject;
				break;
			} 
		}
		
		return target;
	}

}
