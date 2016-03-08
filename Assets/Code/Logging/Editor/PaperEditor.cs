using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PaperEditor : EditorWindow
{
	private static List<string> message = new List<string>();
	private static PaperActionBar actionBar = new PaperActionBar();

	private static bool clearOnPlay = false;

	[UnityEngine.SerializeField]
	EditorLogStore editorstore;

	private Vector2 ScrollPos;

	[MenuItem("Paper/Show Console")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(PaperEditor));
	}

	public void Init()
	{
		var window = ScriptableObject.CreateInstance<PaperEditor>();
		window.Show();
		window.position = new Rect( 200, 200 , 400 , 300 );
	}

	void OnEnable()
	{	
		if (editorstore == null)
			editorstore = EditorLogStore.Create();

		if (LogStore.GetLogStore() == null)
			LogStore.SetStore(editorstore);
	}

	void OnInspectorUpdate()
	{
		Repaint();
	}

	void OnGUI()
	{
		actionBar.DrawActionBar();
		ScrollPos = GUILayout.BeginScrollView( ScrollPos );
	
		List<LogInfo> listOfLogs = editorstore.Logs;
		
		for( int i = 0; i < listOfLogs.Count; i++ )
		{
			GUILayout.BeginVertical();
			OutputMessage( i ,  listOfLogs[i] );
			GUILayout.EndVertical();
		}

		GUILayout.EndScrollView();
	}

	private List<Color> labelColours = new List<Color>()
	{
		Color.red,
		Color.black
	};

	private void OutputMessage( int index , LogInfo log  )
	{
		GUILayout.BeginHorizontal();

		int colorIndex = index % 2;

		GUI.contentColor = labelColours[colorIndex];
		GUILayout.Label( log.LogChannel.ToString() );

		GUILayout.Box(log.Message);
	
		GUILayout.EndHorizontal();
		
		
	}


}
