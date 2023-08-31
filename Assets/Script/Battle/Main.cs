using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
	public int num = 0;
	List<Dictionary<string, object>> data;
	void Awake()
	{
		data = CSVReader.Read ("CardsData");
		
		/*for(var i=0; i < data.Count; i++)
		{

			print ("ID " + data[i]["ID"] + " " +
				   "카드명 " + data[i]["카드명"] + " " +
			       "데미지 " + data[i]["데미지"] + " " +
			       "코스트" + data[i]["코스트"] + " " +
				   "부과효과 " + data[i]["부가효과"] + " " +
				   "설명 " + data[i]["설명"]);
		}*/
	
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Space))
        {
			if (num >= 0 && num < data.Count)
				print("ID " + data[num]["ID"] + " " +
				   "카드명 " + data[num]["카드명"] + " " +
				   "데미지 " + data[num]["데미지"] + " " +
				   "코스트" + data[num]["코스트"] + " " +
				   "부과효과 " + data[num]["부가효과"] + " " +
				   "설명 " + data[num]["설명"]);
			else
				print("범위를 벗어났습니다!");
		}
	}
}
