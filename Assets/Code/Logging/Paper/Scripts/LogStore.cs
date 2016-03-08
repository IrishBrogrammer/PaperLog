﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Class to store logs
public static class LogStore 
{
	public static List<string> log = new List<string>();
	public static EditorLogStore store;

	public static void SetStore(EditorLogStore editorStore)
	{
		store = editorStore;
	}

	public static EditorLogStore GetLogStore()
	{
		return store;
	}

	public static void AddLog( LogChannel channel ,  string message , LogLevel level )
	{
		var log = new LogInfo(
			channel,
			message,
			level );

		if (store != null)
			store.AddLog( log );
	}

	public static int GetLogCount()
	{
		return log.Count;
	}

	public static void ClearStore()
	{
		store.ClearLogs();
	}

}


[System.Serializable]
public class EditorLogStore : ScriptableObject
{
	public List<LogInfo> Logs = new List<LogInfo>();

	static public EditorLogStore Create()
	{
		var editorDebug = ScriptableObject.FindObjectOfType<EditorLogStore>();

		if (editorDebug == null)
			editorDebug = ScriptableObject.CreateInstance<EditorLogStore>();
	
		return editorDebug;	
	}

	public void OnEnable()
	{
	
		hideFlags = HideFlags.HideAndDontSave;
	}

	public void AddLog(LogInfo message)
	{
		Logs.Add(message);
	}

	public void ClearLogs()
	{
		Logs.Clear();
	}


}
