using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Robin Bornschein 2014


/// <summary>
/// Takes a Screenshot off of the MainCamera. Screenshots can be supersized and put in custom subDirectories.
/// Non-existing directories will automatically be created.
/// </summary>
public static class ScreenCap {

	public static void Take() {
		Take(string.Empty,1);
	}
	public static void Take(int superSize) {
		Take(string.Empty,superSize);
	}
	public static void Take(string subDirectory) {
		Take(subDirectory,1);
	}
	
	public static void Take(string subDirectory, int superSize) {
		string dir = "Screenshots/";
		string name;

		// Create name based on given sub directory
		if (subDirectory == string.Empty) name = "Screenshot_";
		else name = subDirectory + "_";

		// Create directory if necessairy
		if (!Directory.Exists("Screenshots")) {
			Directory.CreateDirectory("Screenshots");
		}

		// Create sub directory if necessairy
		if (subDirectory != string.Empty) {
			if (!Directory.Exists("Screenshots/" + subDirectory)) {
				Directory.CreateDirectory("Screenshots/" + subDirectory);
			}
			dir += subDirectory + "/";
		}

		// Get screenshot number
		int screenshotNumber = 0;
		string filePath = dir + name + screenshotNumber.ToString() + ".png";
		while (File.Exists(filePath)) {
			screenshotNumber++;
			filePath = dir + name + screenshotNumber.ToString() + ".png";	
		}

		// Make screenshot
		Debug.Log("Capturing Screenshot. Path: " + filePath);
		Application.CaptureScreenshot(filePath, superSize);
	}

}
