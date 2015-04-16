using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;

public class dbAccess : MonoBehaviour {
	private string connection;
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private StringBuilder builder;

	// Use this for initialization
	void Start () {
		
	}
	
	public void OpenDB(string p)
	{
		Debug.Log("Call to OpenDB:" + p);
		// check if file exists in Application.persistentDataPath
		//string filepath = Application.persistentDataPath + "/" + p;
		string filepath = Application.dataPath + "/StreamingAssets/" + p;
		if(!File.Exists(filepath))
		{
			Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
			                 Application.dataPath + "!/assets/" + p);
			// if it doesn't ->
			// open StreamingAssets directory and load the db -> 
			WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
			while(!loadDB.isDone) {}
			// then save to Application.persistentDataPath
			File.WriteAllBytes(filepath, loadDB.bytes);
		}
		
		//open db connection
		connection = "URI=file:" + filepath;
		Debug.Log("Stablishing connection to: " + connection);
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
	}
	
	public void CloseDB(){
		reader.Close(); // clean everything up
  	 	reader = null;
   		dbcmd.Dispose();
   		dbcmd = null;
   		dbcon.Close();
   		dbcon = null;
	}
	
	public IDataReader BasicQuery(string query){ // run a basic Sqlite query
		dbcmd = dbcon.CreateCommand(); // create empty command
		dbcmd.CommandText = query; // fill the command
		reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		return reader; // return the reader
	
	}
	
	
	public bool CreateTable(string name,string[] col, string[] colType){ // Create a table, name, column array, column type array
		string query;
		query  = "CREATE TABLE " + name + "(" + col[0] + " " + colType[0];
		for(var i=1; i< col.Length; i++){
			query += ", " + col[i] + " " + colType[i];
		}
		query += ")";
		try{
			dbcmd = dbcon.CreateCommand(); // create empty command
			dbcmd.CommandText = query; // fill the command
			reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		}
		catch(Exception e){
			
			Debug.Log(e);
			return false;
		}
		return true;
	}

	// 쓸일 없어 보임..
	public int InsertIntoSingle(string tableName, string colName , string value ) { // single insert

		string query;

		query = "INSERT INTO " + tableName + "(" + colName + ") " + "VALUES (" + value + ")";
		try
		{
			dbcmd = dbcon.CreateCommand(); // create empty command
			dbcmd.CommandText = query; // fill the command
			reader = dbcmd.ExecuteReader(); // execute command which returns a reader
		}
		catch(Exception e){
			
			Debug.Log(e);
			return 0;
		}
		return 1;
	}

	// 문자열. 지정컬럼에 데이터 넣기
	public int InsertIntoSpecific(string tableName, string[] col, string[] values) { // Specific insert with col and values
		string query;
		query = "INSERT INTO " + tableName + "(" + col[0];
		for(int i=1; i< col.Length; i++){
			query += ", " + col[i];
		}
		query += ") VALUES (" + values[0];
		for(int i=1; i< col.Length; i++){
			query += ", " + values[i];
		}
		query += ")";
		Debug.Log(query);
		try
		{
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		}
		catch(Exception e){
			
			Debug.Log(e);
			return 0;
		}
		return 1;
	}

	// 문자열. 모든컬럼에 데이터 넣기
	public int InsertInto(string tableName , string[] values ) { // basic Insert with just values
		string query;
		query = "INSERT INTO " + tableName + " VALUES (" + values[0];
		for(int i=1; i< values.Length; i++){
			query += ", " + values[i];
		}
		query += ")";
		try
		{
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		}
		catch(Exception e){
			
			Debug.Log(e);
			return 0;
		}
		return 1;
	}

	//  문자열.특정 데이터정보를 특정컬럼의 데이터로 찾기 
	public ArrayList SingleSelectWhere(string tableName , string itemToSelect,string wCol,string wPar, string wValue) { // Selects a single Item
		string query;
		query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + wCol + wPar + wValue;	
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		//string[,] readArray = new string[reader, reader.FieldCount];
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		Debug.Log(query);
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				print (row[j]);
				j++;
			}
			readArray.Add(row);
		}
		return readArray; // return matches
	}

	// 플롯형 특정컬럼 데이터 업데이트
	public int floatTypeUpdate (string tableName, string[] wCol, float[] wValue, string serCol, string serData) {

		string query;
		query = "UPDATE " + tableName + " SET " + wCol[0] + " = " + wValue[0];
		for(int i=1; i< wCol.Length; i++){
			query += ", " + wCol[i] + " = " + wValue[i];
		}
		query += " WHERE " + serCol + " = " + serData;
		Debug.Log(query);
		try {
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		} catch (Exception e) {
			Debug.Log(e);
			return 0;
		}
		
		//string[,] readArray = new string[reader, reader.FieldCount];
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		Debug.Log(query);
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				print (row[j]);
				j++;
			}
			readArray.Add(row);
		}

		return 1;
	}


	// 숫자형 특정컬럼 데이터 업데이트 
	public int intTypeUpdate (string tableName, string[] wCol, int[] wValue, string serCol, string serData) {

		string query;
		query = "UPDATE " + tableName + " SET " + wCol[0] + " = " + wValue[0];
		for(int i=1; i< wCol.Length; i++){
			query += ", " + wCol[i] + " = " + wValue[i];
		}
		query += " WHERE " + serCol + " = " + serData;
		Debug.Log(query);
		try {
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		} catch (Exception e) {
			Debug.Log(e);
			return 0;
		}
		
		//string[,] readArray = new string[reader, reader.FieldCount];
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		Debug.Log(query);
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				print (row[j]);
				j++;
			}
			readArray.Add(row);
		}

		return 1;
	}


	// 문자열 특정데이터를 특정컬럼의 데이터로 찾아서 업데이트
	public int UpdateData(string tableName, string[] wCol, string[] wValue, string serCol, string serData) {

		string query;
		query = "UPDATE " + tableName + " SET " + wCol[0] + " = " + wValue[0];
		for(int i=1; i< wCol.Length; i++){
			query += ", " + wCol[i] + " = " + wValue[i];
		}
		query += " WHERE " + serCol + " = " + serData;
		Debug.Log(query);
		try {
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		} catch (Exception e) {
			Debug.Log(e);
			return 0;
		}

		//string[,] readArray = new string[reader, reader.FieldCount];
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		Debug.Log(query);
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				print (row[j]);
				j++;
			}
			readArray.Add(row);
		}
		return 1; // return matches

	}

	// 로그인 회원아이디 비밀번호 체크
	public int loginCheck(string tableName, string id, string pw) {

		//string dbpasswd = "";
		int x = 0;
		string query;
		query = "SELECT pass FROM " + tableName + " WHERE id = " + id;	
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		Debug.Log(query);
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				print (row[j]);
				if (row[j].Equals(pw)) {
					x = 1;
				} else {
					x = 0;
				}
				j++;

			}
			readArray.Add(row);
		}
		return x;
	}

	// 슬롯 1 부대타입 
	public int slot1Unit (string tableName, string id) {
		int x = slotUnitInfo(tableName,id, "Slot1");
		return x;
	}
	// 슬롯 2부대타입 
	public int slot2Unit (string tableName, string id) {
		int x = slotUnitInfo(tableName, id, "Slot2");
		return x;
	}
	// 슬롯 3 부대타입 
	public int slot3Unit (string tableName, string id) {
		int x = slotUnitInfo(tableName, id, "Slot3");
		return x;
	}

	// int 데이터값 읽어오기
	public int intDBReader (string itemToSelect, string tableName, string whatSerch, string thisIsData) {

		int dataNumber = 0;

		string query;
		query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + whatSerch + " = " + thisIsData;	
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		int[] row = new int[reader.FieldCount];
		Debug.Log(query);
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetInt32(j);
				print (row[j]);
				dataNumber = row[j];
				j++;			
			}
			
		}

		return dataNumber;
	}


	// string 데이터값 읽어오기
	public string stringDBReader (string itemToSelect, string tableName, string whatSerch, string thisIsData) {

		string dataName = "";

		string query;
		query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE " + whatSerch + " = " + thisIsData;	
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		string[] row = new string[reader.FieldCount];
		Debug.Log(query);
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				print (row[j]);

				dataName = row[j];
				j++;			
			}

		}

		return dataName;
	}



	// 슬롯의 부대정보 가져오기

	public int slotUnitInfo (string tableName, string id, string itemToSelect) {

		int x = 0;
		string query;
		query = "SELECT " + itemToSelect + " FROM " + tableName + " WHERE id = " + id;	
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		string[] row = new string[reader.FieldCount];
		ArrayList readArray = new ArrayList();
		Debug.Log(query);
		while(reader.Read()){
			int j=0;
			while(j < reader.FieldCount)
			{
				row[j] = reader.GetString(j);
				print (row[j]);
				if (row[j].Equals("0")) {
					x = 0;
				} else if (row[j].Equals("1")) {
					x = 1;
				} else if (row[j].Equals("2")) {
					x = 2;
				} else if (row[j].Equals("3")) {
					x = 3;
				} else if (row[j].Equals("4")) {
					x = 4;
				} else if (row[j].Equals("5")) {
					x = 5;
				} else if (row[j].Equals("6")) {
					x = 6;
				}
				j++;			
			}
			readArray.Add(row);
		}
		return x;	
	}

	// Update is called once per frame
	void Update () {
	
	}
}
