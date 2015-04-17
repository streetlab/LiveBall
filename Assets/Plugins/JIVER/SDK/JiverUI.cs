using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JiverModel;

public class JiverUI : JiverResponder {
	private List<System.Object> messages = new List<System.Object>();
	private List<Channel> channels = new List<Channel>();

	private static float EPSILON = 10e-4f;

	public Rect scale = new Rect(0, 0, 1.0f, 1.0f);
	public Color32 messageDefaultColor = ToColor (0xF2B8B8);
	public Color32 systemMessageDefaultColor = ToColor (0xEEE4A7);
	public Color32 messageSenderColor = ToColor (0x84B05F);
	public int messageFontSize = 15;
	public int scrollBarWidth = 25;

	Vector2 chatScrollViewVector = Vector2.zero;

	Vector2 channelScrollViewVector;

	string inputString = "";


	int TOP;
	int LEFT;
	int WIDTH;
	int HEIGHT;
	float LINE_HEIGHT;

	bool messageAdded;

	bool autoScroll;

	float INPUT_HEIGHT_WEIGHT;

	GUIStyle messageLabelStyle;
	GUIStyle systemMessageLabelStyle;
	GUIStyle inputTextFieldStyle;
	GUIStyle sendButtonStyle;

	string channelName = "Connecting...";
	string selectedChannelUrl = "";

	enum TAB_MODE {CHAT, CHANNEL};
	TAB_MODE tabMode;

	static string ToHex(Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}

	static Color32 ToColor(int HexVal)
	{
		byte R = (byte)((HexVal >> 16) & 0xFF);
		byte G = (byte)((HexVal >> 8) & 0xFF);
		byte B = (byte)((HexVal) & 0xFF);
		return new Color32(R, G, B, 255);
	}

	#region implemented abstract members of JiverResponder
	public override void OnConnect (JiverModel.Channel channel)
	{
		Debug.Log ("Connect to " + channel.GetName ());
		channelName = "#" +  channel.GetUrlWithoutAppPrefix();
		selectedChannelUrl = channel.GetUrl ();
	}
	public override void OnError (int errorCode)
	{
		Debug.Log ("JIVER Error: " + errorCode);
	}
	public override void OnMessageReceived (JiverModel.Message message)
	{
		messages.Add (message);
		messageAdded = true;
	}
	public override void OnSystemMessageReceived (JiverModel.SystemMessage message)
	{
		messages.Add (message);
		messageAdded = true;
	}

	public override void OnBroadcastMessageReceived (JiverModel.BroadcastMessage message)
	{
		messages.Add (message);
		messageAdded = true;
	}

	public override void OnQueryChannelList (List<Channel> channels)
	{
		this.channels = channels;
	}
	
	#endregion

	void Awake() {
	}

	string MessageRichText(Message message)
	{
		return "[<color=#" + ToHex(messageSenderColor) + ">" + message.GetSenderName() + "</color>]: " + message.GetMessage();
	}


	void InitStyle ()
	{
		if (messageLabelStyle == null) {
			messageLabelStyle = new GUIStyle (GUI.skin.label);
		}

		if (systemMessageLabelStyle == null) {
			systemMessageLabelStyle = new GUIStyle (GUI.skin.label);
		}

		if (inputTextFieldStyle == null) {
			inputTextFieldStyle = new GUIStyle(GUI.skin.textField);
			inputTextFieldStyle.alignment = TextAnchor.MiddleLeft;
		}

		if (sendButtonStyle == null) {
			sendButtonStyle = new GUIStyle(GUI.skin.button);
		}

		messageLabelStyle.fontSize = messageFontSize;
		messageLabelStyle.normal.textColor = messageDefaultColor;
		
		systemMessageLabelStyle.fontSize = messageFontSize;
		systemMessageLabelStyle.normal.textColor = systemMessageDefaultColor;

		inputTextFieldStyle.fontSize = messageFontSize;
		sendButtonStyle.fontSize = messageFontSize;


		GUI.skin.verticalScrollbar.fixedWidth = scrollBarWidth;
		GUI.skin.verticalScrollbarThumb.fixedWidth = scrollBarWidth;

		TOP = (int)(Screen.height * scale.y);
		LEFT = (int)(Screen.width * scale.x);
		WIDTH = (int)(Screen.width * scale.width);
		HEIGHT = (int)(Screen.height * scale.height); 

		LINE_HEIGHT = inputTextFieldStyle.CalcHeight (new GUIContent ("W"), WIDTH);
	}

	void DrawTabs(float width, float height)
	{
		if (GUI.Button (new Rect (0, 0, width * 0.5f, height), channelName)) {
			tabMode = TAB_MODE.CHAT;
		}
	
		if(GUI.Button(new Rect(width * 0.5f, 0, width * 0.5f, height), "Channels")) {
			if(tabMode == TAB_MODE.CHAT) {
				tabMode = TAB_MODE.CHANNEL;
				Jiver.QueryChannelList();
			} else {
				tabMode = TAB_MODE.CHAT;
			}
		}
	}


	void DrawChat(float width, float height)
	{

		float totalHeight = 0;
		float messageWidth = width - scrollBarWidth - 5;

		for (int i = 0; i < messages.Count; i++) {
			if(messages[i] is Message) {
				Message message = (Message)messages[i];
				float messageHeight = messageLabelStyle.CalcHeight (new GUIContent(MessageRichText(message)), messageWidth);
				totalHeight += messageHeight;
			} else if(messages[i] is SystemMessage) {
				SystemMessage message = (SystemMessage)messages[i];
				float messageHeight = systemMessageLabelStyle.CalcHeight (new GUIContent(message.GetMessage()), messageWidth);
				totalHeight += messageHeight;
			} else if(messages[i] is BroadcastMessage) {
				BroadcastMessage message = (BroadcastMessage)messages[i];
				float messageHeight = systemMessageLabelStyle.CalcHeight (new GUIContent(message.GetMessage()), messageWidth);
				totalHeight += messageHeight;
			}
		}
		
		float scrollHeight = Mathf.Max (totalHeight, height);




		chatScrollViewVector = GUI.BeginScrollView (new Rect(0, 0, width, height), chatScrollViewVector, new Rect (0, 0, messageWidth, scrollHeight), false, true);

		totalHeight = 0;
		for (int i = 0; i < messages.Count; i++) {
			if(messages[i] is Message) {
				Message message = (Message)messages[i];
				float messageHeight = messageLabelStyle.CalcHeight (new GUIContent(MessageRichText(message)), messageWidth);
				
				GUI.Label(new Rect (0, totalHeight, messageWidth, messageHeight), MessageRichText(message), messageLabelStyle);
				
				totalHeight += messageHeight;
			} else if(messages[i] is SystemMessage) {
				SystemMessage message = (SystemMessage)messages[i];
				float messageHeight = systemMessageLabelStyle.CalcHeight (new GUIContent(message.GetMessage()), messageWidth);
				
				GUI.Label(new Rect (0, totalHeight, messageWidth, messageHeight), message.GetMessage(), systemMessageLabelStyle);
				
				totalHeight += messageHeight;
			} else if(messages[i] is BroadcastMessage) {
				BroadcastMessage message = (BroadcastMessage)messages[i];
				float messageHeight = systemMessageLabelStyle.CalcHeight (new GUIContent(message.GetMessage()), messageWidth);
				
				GUI.Label(new Rect (0, totalHeight, messageWidth, messageHeight), message.GetMessage(), systemMessageLabelStyle);
				
				totalHeight += messageHeight;
			}
		}
		
		GUI.EndScrollView();

		if (Time.time <= 3 || height + chatScrollViewVector.y >= scrollHeight - LINE_HEIGHT) {
			chatScrollViewVector.y = scrollHeight - height; // Keep scroll to bottom
		}

		// Auto Scroll
		float diffToBottom = scrollHeight - (chatScrollViewVector.y + height);

		if (diffToBottom <= EPSILON) {
			autoScroll = true;
		} else {
			if(!messageAdded) {
				autoScroll = false;
			}

			messageAdded = false;
		}

		if(autoScroll) {
			chatScrollViewVector.y = scrollHeight - height;
		}
	}

	void DrawChannels(float width, float height)
	{
		int columns = 4;
		int rowCount = (int)Mathf.Ceil ((float)channels.Count / (float)columns);

		float rowWidth = width - scrollBarWidth - 1;
		float buttonHeight = LINE_HEIGHT * 2f;
		float buttonWidth = rowWidth / columns;

		float totalHeight = Mathf.Max (height, buttonHeight * rowCount);

		channelScrollViewVector = GUI.BeginScrollView (new Rect(0, 0, width, height), channelScrollViewVector, new Rect (0, 0, rowWidth, totalHeight), false, true);

		for (int i = 0; i < channels.Count; i++)
		{
			float buttonLeft = i % columns * buttonWidth;
			float buttonTop = (i / columns) * buttonHeight;
			Channel channel = channels[i];

			if(selectedChannelUrl == channel.GetUrl()) {
				Color c = GUI.backgroundColor;
				GUI.backgroundColor = Color.green;
				if(GUI.Button(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), "#" + channel.GetUrlWithoutAppPrefix())) {
					tabMode = TAB_MODE.CHAT;
				}
				GUI.backgroundColor = c;
			} else {
				if(GUI.Button(new Rect(buttonLeft, buttonTop, buttonWidth, buttonHeight), "#" + channel.GetUrlWithoutAppPrefix())) {
					Jiver.Join(channel.GetUrl());
					Jiver.Connect();
					messages.Clear();
					tabMode = TAB_MODE.CHAT;
					channelName = "Connecting...";
				}
			}
		}
		GUI.EndScrollView();

	}

	bool DrawInput(float width, float height)
	{
		float buttonWidth = width * 0.2f;

		inputString = GUI.TextField (new Rect(0, 0, width - buttonWidth, height), inputString, inputTextFieldStyle);

		if (GUI.Button(new Rect(width - buttonWidth, 0, buttonWidth, height), "Send", sendButtonStyle)) {
			return true;
		}

		return false;
	}

	bool CheckSubmit() {
		bool applying = false;
		if (Event.current.isKey) {
			Debug.Log ("Key code: " + Event.current.keyCode);
			switch (Event.current.keyCode) {
			case KeyCode.Return:
			case KeyCode.KeypadEnter:
				applying = true;
				break;
			}
		}
		
		
		if (Input.GetKeyDown (KeyCode.Return)) {
			applying = true;
		}

		return applying;
	}

	void OnGUI () {
		InitStyle();

		int tabHeight = (int)Mathf.Min (HEIGHT * 0.1f, 48);
		INPUT_HEIGHT_WEIGHT = LINE_HEIGHT / HEIGHT * 1.2f;

		bool submit = false;
		GUI.Box (new Rect (LEFT, TOP, WIDTH, HEIGHT), "");
		GUI.BeginGroup (new Rect (LEFT, TOP, WIDTH, HEIGHT));
			GUI.BeginGroup (new Rect (0, 0, WIDTH, tabHeight));
			DrawTabs (WIDTH, tabHeight);
			GUI.EndGroup ();	
			
			GUI.BeginGroup (new Rect (0, tabHeight, WIDTH, HEIGHT - tabHeight - HEIGHT * INPUT_HEIGHT_WEIGHT));
			if (tabMode == TAB_MODE.CHAT) {
				DrawChat (WIDTH, HEIGHT - tabHeight - HEIGHT * INPUT_HEIGHT_WEIGHT);
			} else if(tabMode == TAB_MODE.CHANNEL) {
				DrawChannels (WIDTH, HEIGHT - tabHeight - HEIGHT * INPUT_HEIGHT_WEIGHT);
			}
			GUI.EndGroup ();

			GUI.BeginGroup (new Rect (0, HEIGHT - HEIGHT * INPUT_HEIGHT_WEIGHT, WIDTH, HEIGHT * INPUT_HEIGHT_WEIGHT));
			if (DrawInput (WIDTH, HEIGHT * INPUT_HEIGHT_WEIGHT))
			{
				submit = true;
			}
			GUI.EndGroup ();

		GUI.EndGroup ();

		if (CheckSubmit ())
		{
			submit = true;
		}

		if (submit) {
			Submit();
		}

	}

	void Submit() {
		if (inputString.Length > 0) {
			Jiver.SendMessage(inputString);
			inputString = "";
		}
	}


}
