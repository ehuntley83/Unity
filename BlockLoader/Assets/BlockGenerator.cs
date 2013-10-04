using UnityEngine;
using System;
using System.IO;

public class BlockGenerator : MonoBehaviour
{
	public GameObject blockPrefab;
	public string filePath;
	public float boardUpperLeftX;
	public float boardUpperLeftZ;
	public float blockBufferSize;
	
	public void Start()
	{
		string fileContents = String.Empty;
		try
		{
			StreamReader fileReader = new StreamReader(filePath);
			fileContents = fileReader.ReadToEnd();
			fileReader.Close();
		}
		catch (Exception ex)
		{
			Debug.LogError("Cannot read file! " + ex.Message);
			return;
		}
		
		float drawX = boardUpperLeftX;
		float drawZ = boardUpperLeftZ;
		string[] fileLines = fileContents.Split('\n');
		foreach(string line in fileLines)
		{
			string[] blockNumbers = line.Split(' ');
			foreach(string block in blockNumbers)
			{
				// Need to trim to remove any hidden characters
				switch (block.Trim())
				{
				case "0":
					drawX += blockPrefab.renderer.bounds.size.x + blockBufferSize;
					break;
					
				case "1":
					Instantiate(blockPrefab, new Vector3(drawX, 1, drawZ), Quaternion.identity);
					drawX += blockPrefab.renderer.bounds.size.x + blockBufferSize;
					break;
				}
			}
			
			// Negative Z-axis is toward our camera
			drawZ -= blockPrefab.renderer.bounds.size.z + blockBufferSize;
			drawX = boardUpperLeftX;
		}
	}
}