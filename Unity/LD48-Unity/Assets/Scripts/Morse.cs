using System.Collections;
using System.Collections.Generic;

public class Morse{

	private JSONObject morseData;

	public Morse(string morseDataString){
		morseData = new JSONObject(morseDataString);
	}

	public string[] encode(string sentence){

		string[] words  = sentence.Split (' ');
		string[] morseWords = new string[words.Length];

		for (int i=0; i<words.Length; i++) {
			morseWords[i] = @"_._";	
		}

		return morseWords;
	}
}
