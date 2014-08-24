using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Morse{

	private JSONObject morseData;
	private List<JSONObject> letterList;

	public int gap = 0;
	public int dot = 1;
	public int dash = 2;

	public int char_dot = 1;
	public int char_dash = 2;

	private int dataIndex_char = 0;
	private int dataIndex_alt = 1;
	private int dataIndex_morse = 2;

	public Morse(string morseDataString){
		morseData = new JSONObject(morseDataString);

		letterList = morseData.list;

	}

	public int[][] encode(string sentence){

		string[] words  = sentence.Split (' ');
		string[] morseWords = new string[words.Length];

		int[][] encodedSentence = new int[morseWords.Length][];

		for (int i=0; i<words.Length; i++) {

			string word = words[i];
			List<int> m_encodedWord = new List<int>();

			for(int i_letter=0;i_letter<word.Length;i_letter++){
				char letter = word[i_letter];
				int[] encoded = encodeChar(letter);
				for(int codes_i=0; codes_i<encoded.Length; codes_i++){
					m_encodedWord.Add(encoded[codes_i]);
				}
				m_encodedWord.Add(gap);
			}
			int[] encodedWord = new int[m_encodedWord.Count-1];

			for(int e=0; e<m_encodedWord.Count-1; e++){
				encodedWord[e] = m_encodedWord[e];
			}

			encodedSentence[i] = encodedWord;

		}

		return encodedSentence;
	}

	private int[] encodeChar(char letter){

		string letterStr = letter.ToString ();
		int[] morse = new int[0];

		for(int i=0; i<letterList.Count; i++){
			List<JSONObject> data = letterList[i].list;
			string chr = data[dataIndex_char].str;
			string alt = data[dataIndex_alt].str;
			string morseStr = data[dataIndex_morse].str;

			if(letterStr == chr || letterStr == alt){
				morse = new int[morseStr.Length];
				for(int chr_i=0;chr_i<morseStr.Length;chr_i++){

					string morseCharStr = morseStr[chr_i].ToString();
					int code = gap;
					if(morseCharStr == "-")code = dash;
					if(morseCharStr == ".")code = dot;

					morse[chr_i] = code;

				}
				return morse;
			}
		}

		return morse;
	}
}
