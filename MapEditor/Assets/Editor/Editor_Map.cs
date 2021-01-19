using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor_Map : EditorWindow
{
    static Editor_Map mapwindow;
    GameObject obj;
    static GameObject house;
    [MenuItem("Map/Open")]
    public static void Init()
    {
        if (Application.isPlaying)
        {
            house = Resources.Load<GameObject>("building_house");
            Editor_Map mapwindow = GetWindow<Editor_Map>("Map_Editor");        
            mapwindow.Show();
        }
        else
        {
            Debug.Log("请先运行项目再打开");
            return;
        }
    }
    bool isshow = true;
    bool canbuild = false;
    private string rowlable = "请选择创建地图的行数:";
    private string collable = "请选择创建地图的列数:";
    int[] rows = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    int[] cols = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    int currow = 1;
    int curcol = 1;
    private void OnGUI()
    {
        isshow = GUILayout.Toggle(isshow, new GUIContent("是否显示创建信息", "显示"));
        if (isshow)
        {
            GUILayout.Label(rowlable);
            currow = EditorGUILayout.IntPopup(currow, new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, rows);
            GUILayout.Label(collable);
            curcol = EditorGUILayout.IntPopup(curcol, new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, cols);
            if (GUILayout.Button("确定创建"))
            {
                Debug.Log("创建成功");
                CreatMap();
            }
        }
        if (canbuild)
        {
            if (GUILayout.Button("生成"))
            {
                canbuild = false;
                var list = obj.GetComponent<Map>().tiles;
                for (int i = 0; i <list.Count; i++)
                {
                    Vector3 v3 = new Vector3(list[i].X, list[i].Y, 0);
                    GameObject h = Instantiate(house);
                    h.transform.localPosition = v3;
                   
                }
                
            }
        }
      
    }
    void CreatMap()
    {
        if (obj == null)
        {
          obj = new GameObject();
        }
        else
        {
            Destroy(obj);
            canbuild = false;
            return;
        }
        obj.AddComponent<Map>();
        obj.GetComponent<Map>().InitMap(currow, curcol);
        canbuild = true;
        isshow = false;
    }
}
